using Microsoft.Xna.Framework;
using Terrafirma.Common.Players;
using Terrafirma.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Terrafirma.Items.Tools
{
    public class AncientBronzePickaxe : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToSword(7, 15, 4);
            Item.pick = 55;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(silver: 75);
            Item.tileBoost = 2;
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddTile(TileID.Anvils).AddRecipeGroup(RecipeGroupID.Wood, 25).AddIngredient(ModContent.ItemType<AncientBronze>(),8).Register();
        }
    }
}
