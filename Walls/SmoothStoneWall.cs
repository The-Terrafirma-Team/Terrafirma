using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Walls
{
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