using Microsoft.Xna.Framework;
using Terrafirma.Common.Players;
using Terrafirma.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Terrafirma.Items.Tools
{
    public class SteelPickaxe : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToSword(25, 15, 4);
            Item.pick = 100;
            Item.shootSpeed = 8;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(0, 0, 55, 0);
            Item.attackSpeedOnlyAffectsWeaponAnimation = true;
            Item.tileBoost = 2;
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddTile(TileID.Anvils).AddIngredient(ModContent.ItemType<SteelBar>(), 10).AddRecipeGroup(RecipeGroupID.Wood, 4).Register();
        }
    }
}
