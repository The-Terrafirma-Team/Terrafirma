using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terrafirma.Projectiles.Summon.Sentry.Hardmode;

namespace Terrafirma.Items.Weapons.Summoner.Sentry.Hardmode
{
    internal class ClockworkTurretStaff : ModItem
    {
        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Summon;
            Item.sentry = true;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.UseSound = SoundID.DD2_DefenseTowerSpawn;
            Item.mana = 20;
            Item.width = 38;
            Item.height = 46;

            Item.autoReuse = true;
            Item.noMelee = true;



            Item.rare = ItemRarityID.Pink;
            Item.value = Item.buyPrice(0, 35, 0, 0);
            Item.shoot = ModContent.ProjectileType<ClockworkTurret>();

        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player == Main.LocalPlayer)
            {
                int WorldX;
                int WorldY;
                int PushUpY;
                Main.LocalPlayer.FindSentryRestingSpot(type, out WorldX, out WorldY, out PushUpY);

                Projectile.NewProjectile(source, new Vector2(WorldX, WorldY - PushUpY - 6), Vector2.Zero, type, damage, 0, player.whoAmI, 0, 0, 0);
                player.UpdateMaxTurrets();
            }
            return false;
        }
    }
}
