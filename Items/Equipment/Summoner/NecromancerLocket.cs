using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Global.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Equipment.Summoner
{
    public class NecromancerLocket : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToAccessory();
            Item.value = Item.sellPrice(0, 0, 25, 0);
            Item.rare = ItemRarityID.Green;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<PlayerStats>().SwarmSpeedMultiplier += 0.15f;
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddRecipeGroup(RecipeGroupID.IronBar,15).AddIngredient(ItemID.Emerald,3).AddIngredient(ItemID.Amethyst, 3).AddTile(TileID.Anvils).Register();
        }
    }
}
