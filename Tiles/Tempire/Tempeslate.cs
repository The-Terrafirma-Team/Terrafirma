using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Global;
using Terrafirma.Items.Placeable.Tempire;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.UI;

namespace Terrafirma.Tiles.Tempire
{
    internal class Tempeslate : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            TileMerge.MergeWith(Type, ModContent.TileType<TempireDirt>());
            Main.tileMerge[Type][ModContent.TileType<TempireDirt>()] = true;
            Main.tileMerge[Type][ModContent.TileType<TempireGrass>()] = true;
            RegisterItemDrop(ModContent.ItemType<TempeslateBlock>());
            DustType = DustID.Stone;
            HitSound = SoundID.Tink;
            AddMapEntry(new Color(116, 112, 119));
            MinPick = 180;
            MineResist = 7f;
        }
        public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
        {
            TileMerge.MergeWithFrame(i, j, Type, ModContent.TileType<TempireDirt>(), false, false, false, false, resetFrame);
            return false;
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}
