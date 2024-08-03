using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terrafirma.Items.Placeable.Statues;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Terrafirma.Tiles
{
    public class SmoothStone : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;       
            DustType = DustID.Stone;
            AddMapEntry(new Color(144, 148, 144));
        }
    }
}
