using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Items.Placeable.Tempire;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Tiles.Tempire
{
    internal class TempireDirt : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            TileID.Sets.CanBeDugByShovel[Type] = true;
            Main.tileMerge[Type][ModContent.TileType<TempireGrass>()] = true;
            Main.tileMerge[Type][ModContent.TileType<Tempeslate>()] = true;
            DustType = DustID.Dirt;
            RegisterItemDrop(ModContent.ItemType<TempirianSoil>());

            AddMapEntry(new Color(57, 48, 54));
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}
