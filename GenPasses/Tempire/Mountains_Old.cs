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
    public class Mountains_Old : GenPass
    {
        //TODO: remove this once tML changes generation passes
        public Mountains_Old() : base("Terrain", 1) { }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            float GenHeight;
            int[] MountainSpawn = new int[] {};
            int[] MountainRand = new int[] { };

            float genint = 0;


            for (int i = 0; i < 10; i++)
            {
                MountainSpawn = MountainSpawn.Append(WorldGen.genRand.Next(40, TempireSubworld.WorldWidth - 40)).ToArray();
                MountainRand = MountainRand.Append(WorldGen.genRand.Next(20, 50)).ToArray();
            }

            progress.Message = "Testing Terrain"; 
            for (int i = 0; i < TempireSubworld.WorldWidth - 1; i++)
            {
                genint = -20;
                for (int k = 0; k < MountainSpawn.Length; k++)
                {

                    if ((i - MountainSpawn[k]) >= -((float)MountainRand[k] * 0.5f) * Math.PI && (i - MountainSpawn[k]) <= ((float)MountainRand[k] * 1.5f) * Math.PI)
                    {
                        genint += Math.Clamp(MathF.Sin((i - MountainSpawn[k]) / (float)MountainRand[k]) * 30f + 30f, -30f, 1000f);
                    }

                }



                for (int j = TempireSubworld.WorldHeight / 4; j < TempireSubworld.WorldHeight - 1; j++)
                {
                    progress.Set(0.5f); // Controls the progress bar, should only be set between 0f and 1f
                    if(j > TempireSubworld.WorldHeight / 2.03f - genint - 40)
                    {
                        Tile tile = Main.tile[i, j];
                        tile.HasTile = true;
                        tile.TileType = (ushort)ModContent.TileType<TempireDirt>();
                    }
                }
            }
        }
    }
}
