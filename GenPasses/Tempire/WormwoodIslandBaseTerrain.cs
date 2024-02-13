using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.IO;
using Terraria.WorldBuilding;
using Terraria;
using Terrafirma.Subworlds.Tempire;
using Terrafirma.Tiles.Tempire;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Terrafirma.GenPasses.Tempire
{
    public class WormwoodIslandBaseTerrain : GenPass
    {
        //TODO: remove this once tML changes generation passes
        public WormwoodIslandBaseTerrain() : base("Terrain", 1) { }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            
            FastNoiseLite gennoise = new FastNoiseLite();
            gennoise.SetNoiseType(FastNoiseLite.NoiseType.Perlin);
            gennoise.SetFrequency(0.03f);

            progress.Message = "Testing Terrain"; // Sets the text displayed for this pass
            
            for (int i = 0; i < TempireSubworld.WorldWidth - 1; i++)
            {
                for (int j = TempireSubworld.WorldHeight / 4; j < TempireSubworld.WorldHeight - 1; j++)
                {
                    if (Math.Abs(gennoise.GetNoise(i,j)) > 0.1f)
                    {
                        Tile tile = Main.tile[i, j];
                        tile.HasTile = true;
                        tile.TileType = (ushort)ModContent.TileType<Worne>();
                    }
                }
            }
        }
    }
}
