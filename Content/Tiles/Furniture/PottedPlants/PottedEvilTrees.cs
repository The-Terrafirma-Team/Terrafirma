using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Terrafirma.Content.Tiles.Furniture.PottedPlants
{
    public class PottedCorruptCedar: ModItem
    {
        public virtual int ForestVariant => ItemID.PottedForestCedar;
        public virtual int Powder => ItemID.VilePowder;
        public virtual int Style => 0;
        public override void SetDefaults()
        {
            Item.CloneDefaults(ForestVariant);
            Item.placeStyle = Style;
            Item.createTile = ModContent.TileType<PottedEvilTrees>();
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddIngredient(Powder, 15).AddIngredient(ForestVariant).Register();
        }
    }
    public class PottedCrimsonCedar: PottedCorruptCedar
    {
        public override int Powder => ItemID.ViciousPowder;
        public override int Style => 1;
    }
    public class PottedCorruptTree : PottedCorruptCedar
    {
        public override int ForestVariant => ItemID.PottedForestTree;
        public override int Style => 2;
    }
    public class PottedCrimsonTree : PottedCorruptCedar
    {
        public override int ForestVariant => ItemID.PottedForestTree;
        public override int Powder => ItemID.ViciousPowder;
        public override int Style => 3;
    }

    public class PottedCorruptBamboo : ModItem
    {
        public virtual int ForestVariant => ItemID.PottedForestBamboo;
        public virtual int Powder => ItemID.VilePowder;
        public virtual int Style => 0;
        public override void SetDefaults()
        {
            Item.CloneDefaults(ForestVariant);
            Item.placeStyle = Style;
            Item.createTile = ModContent.TileType<PottedEvilTreesTall>();
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddIngredient(Powder, 15).AddIngredient(ForestVariant).Register();
        }
    }
    public class PottedCrimsonBamboo : PottedCorruptBamboo
    {
        public override int Powder => ItemID.ViciousPowder;
        public override int Style => 1;
    }
    public class PottedCorruptPalm : PottedCorruptBamboo
    {
        public override int ForestVariant => ItemID.PottedForestPalm;
        public override int Style => 2;
    }
    public class PottedCrimsonPalm : PottedCorruptBamboo
    {
        public override int Powder => ItemID.ViciousPowder;
        public override int ForestVariant => ItemID.PottedForestPalm;
        public override int Style => 3;
    }
    public class PottedEvilTrees : ModTile
    {
        public override void SetStaticDefaults()
        {
            DustType = -1;
            Main.tileFrameImportant[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
            TileObjectData.newTile.Height = 5;
            TileObjectData.newTile.Origin = new Point16(1, 4);
            TileObjectData.newTile.CoordinateHeights = [16, 16, 16, 16, 16];
            TileObjectData.newTile.DrawYOffset = 2;
            TileObjectData.newTile.LavaDeath = true;
            TileObjectData.addTile(Type);
            AddMapEntry(new Color(120, 110, 100));
        }
    }
    public class PottedEvilTreesTall : ModTile
    {
        public override void SetStaticDefaults()
        {
            DustType = -1;
            Main.tileFrameImportant[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.newTile.Height = 6;
            TileObjectData.newTile.Origin = new Point16(1, 5);
            TileObjectData.newTile.CoordinateHeights = [16, 16, 16, 16, 16, 16];
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.DrawYOffset = 2;
            TileObjectData.newTile.LavaDeath = true;
            TileObjectData.addTile(Type);
            AddMapEntry(new Color(120, 110, 100));
        }
    }
}
