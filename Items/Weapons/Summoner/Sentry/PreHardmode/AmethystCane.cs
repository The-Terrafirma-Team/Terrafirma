using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terrafirma.Systems.Elements;
using Terrafirma.Projectiles.Summon.Sentry.PreHardmode;

namespace Terrafirma.Items.Weapons.Summoner.Sentry.PreHardmode
{
    internal class AmethystCane : ModItem
    {
        public override void SetDefaults()
        {
            Item.sentry = true;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.UseSound = SoundID.Item46;

            Item.width = 30;
            Item.height = 32;

            Item.autoReuse = true;
            Item.noMelee = true;
            Item.mana = 20;

            Item.rare = ItemRarityID.White;
            Item.value = Item.sellPrice(0, 0, 4, 0);
            Item.shoot = ModContent.ProjectileType<GemSentry>();

        }
        public override void AddRecipes()
        {
            CreateRecipe().AddIngredient(ItemID.CopperBar, 10).AddIngredient(ItemID.Amethyst, 8).AddTile(TileID.Anvils).Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player == Main.LocalPlayer)
            {
                int WorldX;
                int WorldY;
                int PushUpY;
                Main.LocalPlayer.FindSentryRestingSpot(type, out WorldX, out WorldY, out PushUpY);

                Projectile.NewProjectile(source, new Vector2(WorldX, WorldY - PushUpY + 6), Vector2.Zero, type, damage, 0, player.whoAmI, 0, 0, 0);
                player.UpdateMaxTurrets();
            }
            return false;
        }
    }
}
