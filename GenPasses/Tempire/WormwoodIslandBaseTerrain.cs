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
            gennoise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2);
            gennoise.SetSeed(WorldGen._genRandSeed);
            gennoise.SetFractalOctaves(16);
            gennoise.SetFractalWeightedStrength(1f);
            gennoise.SetRotationType3D(FastNoiseLite.RotationType3D.ImproveXYPlanes);
            gennoise.SetFractalLacunarity(4f);
            gennoise.SetFrequency(0.01f);

            FastNoiseLite gennoise2 = new FastNoiseLite();
            gennoise2.SetFractalType(FastNoiseLite.FractalType.Ridged);
            gennoise2.SetSeed(WorldGen._genRandSeed);
            gennoise2.SetFractalOctaves(4);
            gennoise2.SetRotationType3D(FastNoiseLite.RotationType3D.ImproveXYPlanes);
            gennoise2.SetFrequency(0.003f);
            

            progress.Message = "Testing Terrain"; // Sets the text displayed for this pass
            
            for (int i = 0; i < TempireSubworld.WorldWidth - 1; i++)
            {
                for (int j = 600; j < TempireSubworld.WorldHeight / 2 - 300; j++)
                {

                    float mainnoise = (gennoise2.GetNoise(i / 2, j * 3) * -1f ) - (gennoise2.GetNoise(i, j * 2) * 1.5f) - Math.Clamp((gennoise2.GetNoise(i / 4, j / 4)), 0f, 1f) * 3f;

                    if (j < TempireSubworld.WorldHeight / 3 && mainnoise / (((TempireSubworld.WorldHeight / 3) - j) * 0.007f) > 0.2f)
                    {
                        Tile tile = Main.tile[i, j];
                        tile.HasTile = true;
                        tile.TileType = (ushort)ModContent.TileType<Worne>();

                    }
                    else if (j >= TempireSubworld.WorldHeight / 3 && mainnoise / ((j - (TempireSubworld.WorldHeight / 3)) * 0.02f) > 0.2f)
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
