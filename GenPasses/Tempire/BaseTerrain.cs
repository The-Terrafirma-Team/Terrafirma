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
    public class BaseTerrain : GenPass
    {
        //TODO: remove this once tML changes generation passes
        public BaseTerrain() : base("Terrain", 1) { }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            Vector3 GenSineProgress = Vector3.Zero;
            float GenHeight;
            progress.Message = "Testing Terrain"; // Sets the text displayed for this pass
            for (int i = 0; i < TempireSubworld.WorldWidth - 1; i++)
            {
                GenSineProgress += new Vector3(WorldGen.genRand.NextFloat(-5, 5), WorldGen.genRand.NextFloat(-1, 2), WorldGen.genRand.NextFloat(-20, 20)) * 0.3f;
                GenHeight = MathF.Sin(GenSineProgress.X * 0.1f) * 5f + MathF.Cos(GenSineProgress.Y * 0.05f) * 5f + MathF.Sin(GenSineProgress.Z * 0.01f) * 3f + MathF.Cos(GenSineProgress.Y * 0.01f) * 15;

                for (int j = TempireSubworld.WorldHeight / 4; j < TempireSubworld.WorldHeight - 1; j++)
                {
                    progress.Set(0.5f); // Controls the progress bar, should only be set between 0f and 1f
                    if(j > TempireSubworld.WorldHeight / 2.03f + GenHeight || j > TempireSubworld.WorldHeight / 2)
                    {
                        Tile tile = Main.tile[i, j];
                        tile.HasTile = true;
                        tile.TileType = (ushort)ModContent.TileType<TempireDirt>();
                    }
                }
            }
            // Stone
            GenSineProgress = Vector3.Zero;
            GenHeight = 0;
            for (int i = 0; i < TempireSubworld.WorldWidth - 1; i++)
            {
                GenSineProgress += new Vector3(WorldGen.genRand.NextFloat(-3, 3), WorldGen.genRand.NextFloat(-1, 2), WorldGen.genRand.NextFloat(-1, 5)) * 0.2f;
                GenHeight = MathF.Sin(GenSineProgress.X * 0.1f) * 5f + MathF.Cos(GenSineProgress.Y * 0.05f) * 3f + MathF.Sin(GenSineProgress.Z * 0.01f) * 2f + MathF.Cos(GenSineProgress.Y * 0.01f) * 15;

                for (int j = TempireSubworld.WorldHeight / 4; j < TempireSubworld.WorldHeight - 1; j++)
                {
                    progress.Set(0.5f); // Controls the progress bar, should only be set between 0f and 1f
                    if (j > TempireSubworld.WorldHeight / 2 + GenHeight)
                    {
                        Tile tile = Main.tile[i, j];
                        tile.HasTile = true;
                        tile.TileType = (ushort)ModContent.TileType<Tempeslate>();
                    }
                }
            }
        }
    }
}
