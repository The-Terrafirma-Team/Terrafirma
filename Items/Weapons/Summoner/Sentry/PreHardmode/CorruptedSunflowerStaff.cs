using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terrafirma.Projectiles.Summon.Sentry.PreHardmode;

namespace Terrafirma.Items.Weapons.Summoner.Sentry.PreHardmode
{
    internal class CorruptedSunflowerStaff : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 12;
            Item.knockBack = 1f;
            Item.DamageType = DamageClass.Summon;
            Item.sentry = true;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.UseSound = SoundID.Item46;

            Item.width = 16;
            Item.height = 16;

            Item.autoReuse = true;
            Item.noMelee = true;
            Item.mana = 20;

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 0, 15, 0);
            Item.shoot = ModContent.ProjectileType<CorruptedSunflower>();
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddIngredient(ItemID.Sunflower,5).AddIngredient(ItemID.VilePowder,25).AddTile(TileID.DemonAltar).Register();
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
