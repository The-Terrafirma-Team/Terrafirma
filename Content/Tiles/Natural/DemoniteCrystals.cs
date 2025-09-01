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
    public class DemoniteCrystalSmall : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.DemoniteOre);
            Item.createTile = ModContent.TileType<DemoniteCrystal1x1>();
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddTile(TileID.HeavyWorkBench).AddIngredient(ItemID.DemoniteOre).Register();
        }
    }
    public class DemoniteCrystalMedium : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.DemoniteOre);
            Item.createTile = ModContent.TileType<DemoniteCrystal2x2>();
            Item.value *= 4;
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddTile(TileID.HeavyWorkBench).AddIngredient(ItemID.DemoniteOre, 4).Register();
        }
    }
    public class DemoniteCrystalLarge : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.DemoniteOre);
            Item.createTile = ModContent.TileType<DemoniteCrystal3x2>();
            Item.value *= 6;
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddTile(TileID.HeavyWorkBench).AddIngredient(ItemID.DemoniteOre, 6).Register();
        }
    }
    public class DemoniteCrystal1x1 : DemoniteCrystal2x2
    {
        public override void SetStaticDefaults()
        {
            Main.tileLighted[Type] = true;
            Main.tileFrameImportant[Type] = true;
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.RandomStyleRange = 3;
            DustType = DustID.Demonite;
            MinPick = 55;
            TileObjectData.addTile(Type);
            AddMapEntry(new Color(98, 95, 167), Language.GetText("MapObject.Demonite"));
        }
        public override IEnumerable<Item> GetItemDrops(int i, int j)
        {
            yield return new Item(ItemID.DemoniteOre);
        }
    }
    public class DemoniteCrystal2x2 : ModTile
    {
        public override void EmitParticles(int i, int j, Tile tile, short tileFrameX, short tileFrameY, Color tileLight, bool visible)
        {
            if(Main.rand.NextBool(8))
            {
                Dust d = Dust.NewDustDirect(new Vector2(i * 16, j * 16 + 14),16,1,DustID.CorruptTorch);
                d.noLight = true;
                d.noGravity = true;
                d.velocity = new Vector2(Main.rand.NextFloat(-1, 1), Main.rand.NextFloat(-4, -2));
            }
        }
        public override void SetStaticDefaults()
        {
            Main.tileLighted[Type] = true;
            Main.tileFrameImportant[Type] = true;
            TileObjectData.newTile.Width = 2;
            TileObjectData.newTile.Height = 2;
            TileObjectData.newTile.Origin = new Point16(1, 1);
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.newTile.CoordinateHeights = [16, 16];
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.RandomStyleRange = 3;
            DustType = DustID.Demonite;
            MinPick = 55;
            HitSound = SoundID.Tink;
            TileObjectData.addTile(Type);
            AddMapEntry(new Color(98, 95, 167), Language.GetText("MapObject.Demonite"));
        }
        public override IEnumerable<Item> GetItemDrops(int i, int j)
        {
            yield return new Item(ItemID.DemoniteOre, 4);
        }
        public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
        {
            offsetY = 2;
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            float extra = (float)Math.Sin((Main.timeForVisualEffects + (i + j) * 20) * 0.03f) * 0.2f;
            r = 0.12f + extra * 0.4f;
            g = 0.07f + extra * 0.1f;
            b = 0.32f + extra * 0.3f;
        }
    }
    public class DemoniteCrystal3x2 : DemoniteCrystal2x2
    {
        public override void SetStaticDefaults()
        {
            Main.tileLighted[Type] = true;
            Main.tileFrameImportant[Type] = true;
            TileObjectData.newTile.Width = 3;
            TileObjectData.newTile.Height = 2;
            TileObjectData.newTile.Origin = new Point16(1, 1);
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.newTile.CoordinateHeights = [16, 16];
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.RandomStyleRange = 0;
            DustType = DustID.Demonite;
            MinPick = 55;
            TileObjectData.addTile(Type);
            AddMapEntry(new Color(98, 95, 167), Language.GetText("MapObject.Demonite"));
        }
        public override IEnumerable<Item> GetItemDrops(int i, int j)
        {
            yield return new Item(ItemID.DemoniteOre, 6);
        }
    }
}
