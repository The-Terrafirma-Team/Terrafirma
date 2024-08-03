using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Walls;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Placeable
{
    public class SmoothStoneWallItem : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.StoneWall);
            Item.createWall = ModContent.WallType<SmoothStoneWall>();
            Item.placeStyle = 0;
        }
        public override void AddRecipes()
        {
            CreateRecipe(4).AddTile(TileID.WorkBenches).AddIngredient(ModContent.ItemType<SmoothStoneBlock>(), 1).Register();
            Recipe.Create(ModContent.ItemType<SmoothStoneBlock>()).AddTile(TileID.WorkBenches).AddIngredient(Type, 4).Register();
        }
    }
}
