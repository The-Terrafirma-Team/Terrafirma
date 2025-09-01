using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Terrafirma.Content.Tiles.Natural
{
    public class CrimtaneGrowthLarge : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.CrimtaneOre);
            Item.createTile = ModContent.TileType<CrimtaneGrowth2x3>();
            Item.value *= 6;
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddTile(TileID.HeavyWorkBench).AddIngredient(ItemID.CrimtaneOre, 6).Register();
        }
    }
    public class CrimtaneGrowth2x3 : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileLighted[Type] = true;
            Main.tileFrameImportant[Type] = true;
            TileObjectData.newTile.Width = 2;
            TileObjectData.newTile.Height = 3;
            TileObjectData.newTile.Origin = new Point16(1, 2);
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.newTile.CoordinateHeights = [16, 16, 16];
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.RandomStyleRange = 4;
            DustType = DustID.CrimtaneWeapons;
            MinPick = 55;
            HitSound = SoundID.Tink;
            TileObjectData.addTile(Type);
            AddMapEntry(new Color(125, 55, 65), Language.GetText("MapObject.Crimtane"));
        }
        public override IEnumerable<Item> GetItemDrops(int i, int j)
        {
            yield return new Item(ItemID.CrimtaneOre, 6);
        }
        public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
        {
            offsetY = 2;
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.5f;
        }
    }
}
