using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Equipment.Summoner
{
    public class EngineeringForDummies : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToAccessory();
            Item.rare = ItemRarityID.Blue;
        }
        public override void UpdateEquip(Player player)
        {
            player.maxTurrets++;
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddTile(TileID.WorkBenches).AddIngredient(ItemID.Book).AddRecipeGroup(RecipeGroupID.IronBar, 5).AddIngredient(ItemID.DartTrap).AddIngredient(ItemID.GrayPressurePlate).Register();
            CreateRecipe().AddTile(TileID.WorkBenches).AddIngredient(ItemID.Book).AddRecipeGroup(RecipeGroupID.IronBar, 5).AddIngredient(ItemID.DartTrap).AddIngredient(ItemID.BluePressurePlate).Register();
            CreateRecipe().AddTile(TileID.WorkBenches).AddIngredient(ItemID.Book).AddRecipeGroup(RecipeGroupID.IronBar, 5).AddIngredient(ItemID.DartTrap).AddIngredient(ItemID.BrownPressurePlate).Register();
        }
    }
}
