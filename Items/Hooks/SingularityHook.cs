// DO NOT USE THIS SHIT IT"S BAD AND VERY BAD AND EVIL AND IT WILL EAT ALL YOUR BREADCRUMBS AND BEDBUGS.
//Pls Fix Jeo :3 :3


using Microsoft.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Items.Hooks
{
	internal class SingularityHook : ModItem
	{
		public override void SetDefaults() {
			Item.CloneDefaults(ItemID.AmethystHook);
			Item.shootSpeed = 18f;
			Item.shoot = ModContent.ProjectileType<SingularityHookProjectile>();
		}

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

    }

	internal class SingularityHookProjectile : ModProjectile
	{
 
		Vector2 MousePos = Vector2.Zero;
		private static Asset<Texture2D> chainTexture;

		public override void Load() {
			chainTexture = ModContent.Request<Texture2D>("TerrafirmaRedux/Items/Hooks/SingularityHookChain");
		}

        public override void Unload() { // This is called once on mod reload when this piece of content is being unloaded.
			// It's currently pretty important to unload your static fields like this, to avoid having parts of your mod remain in memory when it's been unloaded.
			chainTexture = null;
		}
		public override void SetStaticDefaults() {
			// If you wish for your hook projectile to have ONE copy of it PER player, uncomment this section.
			ProjectileID.Sets.SingleGrappleHook[Type] = true;
		}

        public override void AI()
        {
			if (Projectile.ai[1] == 0) 
			{
			Projectile.ai[1] = 1;
			MousePos = Main.MouseWorld;
			}

            base.AI();
        }
        public override void UseGrapple(Player player, ref int type)
        {
            base.UseGrapple(player, ref type);
        }

        public override void SetDefaults() {
			Projectile.CloneDefaults(ProjectileID.GemHookAmethyst); // Copies the attributes of the Amethyst hook's projectile.
		}

        // Use this hook for hooks that can have multiple hooks mid-flight: Dual Hook, Web Slinger, Fish Hook, Static Hook, Lunar Hook.
        public override float GrappleRange() {
			return 400f;
		}

        public override bool? GrappleCanLatchOnTo(Player player, int x, int y) {
			// By default, the hook returns null to apply the vanilla conditions for the given tile position (this tile position could be air or an actuated tile!)
			// If you want to return true here, make sure to check for Main.tile[x, y].HasUnactuatedTile (and Main.tileSolid[Main.tile[x, y].TileType] and/or Main.tile[x, y].HasTile if needed)

			// We make this hook latch onto trees just like Squirrel Hook

			// Tree trunks cannot be actuated so we don't need to check for that here
			Tile tile = Main.tile[x, y];
			if (Projectile.active && Projectile.Distance(Main.player[Projectile.owner].Center) >= GrappleRange() || Projectile.active &&  Projectile.position.Distance(MousePos) < 5f ) {
				return true;
			}

			// In any other case, behave like a normal hook
			return null;
		}
		

        public override void GrappleRetreatSpeed(Player player, ref float speed)
        {
            speed = 20f;
        }

        public override void GrapplePullSpeed(Player player, ref float speed) {
			speed = 18;
		}

        public override bool PreDrawExtras() {
			Vector2 playerCenter = Main.player[Projectile.owner].MountedCenter;
			Vector2 center = Projectile.Center;
			Vector2 directionToPlayer = playerCenter - Projectile.Center;
			float chainRotation = directionToPlayer.ToRotation() - MathHelper.PiOver2;
			float distanceToPlayer = directionToPlayer.Length();

			while (distanceToPlayer > 20f && !float.IsNaN(distanceToPlayer)) {
				directionToPlayer /= distanceToPlayer; // get unit vector
				directionToPlayer *= chainTexture.Height(); // multiply by chain link length

				center += directionToPlayer; // update draw position
				directionToPlayer = playerCenter - center; // update distance
				distanceToPlayer = directionToPlayer.Length();

				Color drawColor = Lighting.GetColor((int)center.X / 16, (int)(center.Y / 16));

				// Draw chain
				Main.EntitySpriteDraw(chainTexture.Value, center - Main.screenPosition,
					chainTexture.Value.Bounds, drawColor, chainRotation,
					chainTexture.Size() * 0.5f, 1f, SpriteEffects.None, 0);
			}
			// Stop vanilla from drawing the default chain.
			return false;
		}
    }
}