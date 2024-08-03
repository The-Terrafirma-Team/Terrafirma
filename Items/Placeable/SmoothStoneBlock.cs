using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Placeable
{
    public class SmoothStoneBlock : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.StoneBlock);
            Item.createTile = ModContent.TileType<Tiles.SmoothStone>();
            Item.placeStyle = 0;
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddTile(TileID.HeavyWorkBench).AddIngredient(ItemID.StoneBlock, 2).Register();
        }
    }
}
