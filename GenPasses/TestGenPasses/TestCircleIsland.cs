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
using System.Reflection;
using Terraria.Map;
using Microsoft.Xna.Framework.Graphics;
using System.Numerics;

namespace Terrafirma.GenPasses.TestGenPasses
{
    public class TestCircleIsland : GenPass
    {
        //TODO: remove this once tML changes generation passes
        public TestCircleIsland() : base("Structure", 1) { }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = "Creating Circles"; // Sets the text displayed for this pass
            Vector2[] Randomspot = new Vector2[] { };
            int[] RandomspotSize = new int[] { };
            for (int i = 0; i < 26; i++)
            {
                Randomspot = Randomspot.Append(new Vector2(WorldGen.genRand.Next(60, TempireSubworld.WorldWidth - 60) * 16, WorldGen.genRand.Next(100, TempireSubworld.WorldHeight / 3) * 16)).ToArray();
                RandomspotSize = RandomspotSize.Append(WorldGen.genRand.Next(20, 60) * 16).ToArray();
            }

            for (int i = 0; i < TempireSubworld.WorldWidth - 1; i++)
            {
                for (int j = 0; j < TempireSubworld.WorldHeight / 3; j++)
                {
                    for (int k = 0; k < Randomspot.Length; k++)
                    {
                        if (Vector2.Distance(Randomspot[k], new Vector2(i * 16, j * 16)) < RandomspotSize[k])
                        {
                            Tile tile = Main.tile[i, j];
                            tile.HasTile = true;
                            Main.tile[i, j].TileType = TileID.Dirt;
                        }
                    }
                }
            }
        }
    }
}
