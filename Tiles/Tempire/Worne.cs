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
    internal class Worne : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;


            RegisterItemDrop(ModContent.ItemType<WorneBlock>());
            DustType = DustID.RedMoss;
            HitSound = SoundID.Tink;
            AddMapEntry(new Color(45, 18, 17));
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
