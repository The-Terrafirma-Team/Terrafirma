using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terrafirma.Projectiles.Summon.Sentry.PreHardmode;

namespace Terrafirma.Items.Weapons.Summoner.Sentry.PreHardmode
{
    internal class BowTurretAssemblyKit : ModItem
    {
        public override void SetDefaults()
        {
            Item.sentry = true;
            Item.damage = 5;
            Item.DamageType = DamageClass.Summon;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.UseSound = SoundID.Item46;

            Item.width = 30;
            Item.height = 32;

            Item.autoReuse = true;
            Item.noMelee = true;
            Item.mana = 20;

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 0, 0, 40);
            Item.shoot = ModContent.ProjectileType<BowTurret>();
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddIngredient(ItemID.WoodenBow, 1).AddIngredient(ItemID.Wood, 10).AddRecipeGroup(RecipeGroupID.IronBar, 4).AddTile(TileID.Anvils).Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player == Main.LocalPlayer)
            {
                int WorldX;
                int WorldY;
                int PushUpY;
                Main.LocalPlayer.FindSentryRestingSpot(type, out WorldX, out WorldY, out PushUpY);

                Projectile.NewProjectile(source, new Vector2(WorldX, WorldY - PushUpY + 5), Vector2.Zero, type, damage, 0, player.whoAmI, 0, 0, 4);
                player.UpdateMaxTurrets();
            }
            return false;
        }
    }
}
