using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Terrafirma.Common.Tiles
{
    internal class TilePlacedByPlayerSystem : ModSystem
    {
        public static List<Point> TilesPlacedByPlayers = new List<Point>();
        public override void SaveWorldData(TagCompound tag)
        {
            tag["Terrafirma:TilesPlacedByPlayers"] = TilesPlacedByPlayers;
        }
        public override void LoadWorldData(TagCompound tag)
        {
            if (tag.ContainsKey("Terrafirma:TilesPlacedByPlayers"))
            {
                TilesPlacedByPlayers = tag.Get<List<Point>>("Terrafirma:TilesPlacedByPlayers");
            }
        }
    }
}
