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
    internal class TempireGrass : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            TileID.Sets.NeedsGrassFraming[Type] = true;
            TileID.Sets.NeedsGrassFramingDirt[Type] = ModContent.TileType<TempireDirt>();
            Main.tileMerge[Type][ModContent.TileType<Tempeslate>()] = true;
            TileID.Sets.CanBeDugByShovel[Type] = true;
            TileID.Sets.Grass[Type] = true;

            DustType = DustID.Dirt;
            RegisterItemDrop(ModContent.ItemType<TempirianSoil>());
            AddMapEntry(new Color(76, 69, 59));
        }
        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (!effectOnly)
                Main.tile[i, j].TileType = (ushort)ModContent.TileType<TempireDirt>();
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}
