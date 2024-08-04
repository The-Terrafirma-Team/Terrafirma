using Microsoft.Xna.Framework;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.VanillaMagic.Spells.Hardmode
{
    internal class InfernoFork : Spell
    {
        public override int UseAnimation => 30;
        public override int UseTime => 30;
        public override int ManaCost => 18;
        public override int[] SpellItem => new int[] { ItemID.InfernoFork };

        public override void SetDefaults(Item entity)
        {
            entity.UseSound = null;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            type = ModContent.ProjectileType<InfernoForkProj>();
            velocity *= 1.2f;

            SoundEngine.PlaySound(SoundID.Item73, player.position);

            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0, 0, 0);
            return false;
        }
    }

    public class InfernoForkProj : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.Size = new Vector2(16);
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 600;
            DrawOriginOffsetY = -12;
            DrawOffsetX = -12;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(1f, 1f, 1f, 0f) * Projectile.Opacity;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver4;
            Dust d = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(4, 4), DustID.InfernoFork, Projectile.velocity * 0.1f);
            d.noGravity = true;
        }

        public override void OnKill(int timeLeft)
        {
            Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ProjectileID.InfernoFriendlyBlast, Projectile.damage = (int)(Projectile.damage * 0.4f), 0, Projectile.owner);
        }
    }

}
