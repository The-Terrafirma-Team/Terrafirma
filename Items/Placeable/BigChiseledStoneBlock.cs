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
    public class BigChiseledStoneBlock : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.SlimeStatue);
            Item.createTile = ModContent.TileType<Tiles.BigChiseledStoneBlock>();
            Item.placeStyle = 0;
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddTile(TileID.HeavyWorkBench).AddIngredient(ItemID.GrayBrick, 4).Register();
            Recipe.Create(ItemID.GrayBrick, 4).AddTile(TileID.HeavyWorkBench).AddIngredient(Type).Register();
        }
    }
}
