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
    public class Caves : GenPass
    {
        //TODO: remove this once tML changes generation passes
        public Caves() : base("Terrain", 1) { }

        public void StarCaves()
        {
            FastNoiseLite gennoise = new FastNoiseLite();
            gennoise.SetNoiseType(FastNoiseLite.NoiseType.Perlin);
            gennoise.SetFrequency(0.02f);

            for (int i = 0; i < TempireSubworld.WorldWidth - 1; i++)
            {
                for (int j = (int)(TempireSubworld.WorldHeight / 1.95f); j < TempireSubworld.WorldHeight - 1; j++)
                {
                    if (Math.Abs(gennoise.GetNoise(i, j)) < gennoise.GetNoise(j, i) * MathHelper.Clamp((j - TempireSubworld.WorldHeight / 2f) / (float)TempireSubworld.WorldHeight,0f,0.3f))
                    {
                        Tile tile = Main.tile[i, j];
                        tile.HasTile = false;
                    }
                }
            }
        }
        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = "Generating caves"; // Sets the text displayed for this pass

            StarCaves();
        }
    }
}
