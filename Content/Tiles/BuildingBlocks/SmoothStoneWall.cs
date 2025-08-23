using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Content.Tiles.BuildingBlocks
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
            CreateRecipe(4).AddTile(TileID.WorkBenches).AddIngredient(ModContent.ItemType<SmoothStone>(), 1).Register();
            Recipe.Create(ModContent.ItemType<SmoothStone>()).AddTile(TileID.WorkBenches).AddIngredient(Type, 4).Register();
        }
    }
    public class SmoothStoneWall : ModWall
    {
        public override void SetStaticDefaults()
        {
            Main.wallHouse[Type] = true;

            DustType = DustID.Stone;

            AddMapEntry(new Color(56, 56, 56));
        }
    }
}