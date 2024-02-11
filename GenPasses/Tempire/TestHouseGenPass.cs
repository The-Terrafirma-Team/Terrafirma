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
using Microsoft.Xna.Framework;

namespace Terrafirma.GenPasses.Tempire
{
    public class TestHouseGenPass : GenPass
    {
        //TODO: remove this once tML changes generation passes
        public TestHouseGenPass() : base("Structure", 1) { }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {  
            progress.Message = "Making Houses"; // Sets the text displayed for this pass
            Vector2[] Randomspot = new Vector2[] { };
            Vector2[] RandomspotSize = new Vector2[] { };
            for (int i = 0; i < 9; i++)
            {
                RandomspotSize = RandomspotSize.Append( new Vector2(WorldGen.genRand.Next(15, 20), WorldGen.genRand.Next(10, 15))).ToArray();
            }

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < TempireSubworld.WorldHeight / 1.5f; j++)
                {
                    if (Main.tile[i * (TempireSubworld.WorldWidth / 9), j + (int)RandomspotSize[i].Y].HasTile)
                    {
                        Randomspot = Randomspot.Append(new Vector2(i * (TempireSubworld.WorldWidth / 9), j)).ToArray();
                        break;
                    }
                }
            }

            for (int i = 0; i < Randomspot.Length ; i++)
            {
                if (WorldGen.genRand.NextBool())
                {
                    for (int k = 0; k < RandomspotSize[i].X; k++)
                    {
                        for (int c = -(int)RandomspotSize[i].Y; c < 0; c++)
                        {
                            if (k == 0 || c == 0 || k == RandomspotSize[i].X - 1 || c == -(int)RandomspotSize[i].Y)
                            {

                                Tile tile = Main.tile[(ushort)Randomspot[i].X + k, (ushort)Randomspot[i].Y + c];
                                tile.HasTile = true;
                                tile.TileType = TileID.PlatinumBrick;

                            }
                            else
                            {
                                Tile tile = Main.tile[(ushort)Randomspot[i].X + k, (ushort)Randomspot[i].Y + c];
                                tile.HasTile = false;
                                tile.WallType = WallID.PlatinumBrick;
                            }
                        }
                    }
                }
                for (int k = 0; k < RandomspotSize[i].X; k++)
                {
                    for (int c = 0; c < RandomspotSize[i].Y; c++)
                    {
                        if (k == 0 || c == 0 || k == RandomspotSize[i].X - 1 || c == RandomspotSize[i].Y - 1)
                        {

                            Tile tile = Main.tile[(ushort)Randomspot[i].X + k, (ushort)Randomspot[i].Y + c];
                            tile.HasTile = true;
                            tile.TileType = TileID.PlatinumBrick;

                        }
                        else
                        {
                            Tile tile = Main.tile[(ushort)Randomspot[i].X + k, (ushort)Randomspot[i].Y + c];
                            tile.HasTile = false;
                            tile.WallType = WallID.PlatinumBrick;
                        }
                    }
                }
            }
        }
    }
}
