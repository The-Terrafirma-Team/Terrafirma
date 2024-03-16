using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terrafirma.Projectiles.Summon.Sentry;
using Terraria.DataStructures;

namespace Terrafirma.Items.Weapons.Summoner.Sentry.PreHardmode
{
    internal class RoyalJellyStaff : ModItem
    {
        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Summon;
            Item.sentry = true;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 40;
            Item.useTime = 40;
            Item.UseSound = SoundID.DD2_DefenseTowerSpawn;

            Item.width = 46;
            Item.height = 46;

            Item.autoReuse = true;
            Item.noMelee = true;
            Item.mana = 40;


            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(0, 70, 0, 0);
            Item.shoot = ModContent.ProjectileType<RoyalJellyDispenser>();

        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player == Main.LocalPlayer)
            {
                int WorldX;
                int WorldY;
                int PushUpY;
                Main.LocalPlayer.FindSentryRestingSpot(type, out WorldX, out WorldY, out PushUpY);

                Projectile.NewProjectile(default, new Vector2(WorldX, WorldY - PushUpY ), Vector2.Zero, type, damage, 0, player.whoAmI, 0, 0, 0);
                player.UpdateMaxTurrets();
            }
            return false;
        }
    }
}
