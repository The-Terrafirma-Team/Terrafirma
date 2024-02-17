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
        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = "Generating caves"; // Sets the text displayed for this pass

            FastNoiseLite gennoise = new FastNoiseLite();
            gennoise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2S);
            gennoise.SetFractalType(FastNoiseLite.FractalType.PingPong);
            gennoise.SetSeed(WorldGen._genRandSeed);

            for (int i = 0; i < TempireSubworld.WorldWidth - 1; i++)
            {
                for (int j = (int)(TempireSubworld.WorldHeight / 2f); j < TempireSubworld.WorldHeight - 1; j++)
                {
                    gennoise.SetFrequency(0.003f);
                    if (j > TempireSubworld.WorldHeight / 2f & Math.Abs(gennoise.GetNoise(i, j)) < gennoise.GetNoise(j, i) * MathHelper.Clamp((j - TempireSubworld.WorldHeight / 2f) / (float)TempireSubworld.WorldHeight, 0f, 0.6f))
                    {
                        Tile tile = Main.tile[i, j];
                        tile.HasTile = false;
                    }
                    gennoise.SetFrequency(0.005f);
                    if (Math.Abs(gennoise.GetNoise(i, j)) < 0.08f)
                    {
                        Tile tile = Main.tile[i, j];
                        tile.HasTile = false;
                    }
                }
            }
        }
    }
}
