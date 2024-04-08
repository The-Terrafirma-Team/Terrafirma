using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Equipment.Summoner
{
    public class Overclock : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToAccessory();
            Item.value = Item.sellPrice(0, 0, 25, 0);
            Item.rare = ItemRarityID.Orange;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<PlayerStats>().SentrySpeedMultiplier -= 0.15f;
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddRecipeGroup(RecipeGroupID.IronBar,15).AddIngredient(ItemID.Wire,12).AddIngredient(ItemID.SwiftnessPotion).AddTile(TileID.TinkerersWorkbench).Register();
        }
    }
}
