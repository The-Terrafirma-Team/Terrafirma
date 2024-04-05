using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terrafirma.Projectiles.Summon.Sentry;
using Terraria.DataStructures;
using Terrafirma.Systems.Elements;

namespace Terrafirma.Items.Weapons.Summoner.Sentry.PreHardmode
{
    internal class MechanicsPocketDefenseSystem : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 10;
            Item.knockBack = 0.5f;
            Item.DamageType = DamageClass.Summon;
            Item.sentry = true;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.UseSound = SoundID.DD2_DefenseTowerSpawn;

            Item.width = 16;
            Item.height = 16;

            Item.autoReuse = true;
            Item.noMelee = true;
            Item.mana = 20;


            Item.rare = ItemRarityID.Green;
            Item.value = Item.buyPrice(0, 12, 0, 0);
            Item.shoot = ModContent.ProjectileType<MechanicsPocketSentry>();

            Item.GetElementItem().elementData.Electric = true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player == Main.LocalPlayer)
            {
                int WorldX;
                int WorldY;
                int PushUpY;
                Main.LocalPlayer.FindSentryRestingSpot(type, out WorldX, out WorldY, out PushUpY);

                Projectile.NewProjectile(default, new Vector2(WorldX, WorldY - PushUpY + 4), Vector2.Zero, type, damage, 0, player.whoAmI, 0, 0, 0);
                player.UpdateMaxTurrets();
            }
            return false;
        }
    }
}
