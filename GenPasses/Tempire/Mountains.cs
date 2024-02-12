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
    public class Mountains : GenPass
    {
        //TODO: remove this once tML changes generation passes
        public Mountains() : base("Terrain", 1) { }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {

            Dictionary<int, Vector2[]> PointList = new Dictionary<int, Vector2[]>();
            int maxmountains = WorldGen.genRand.Next(6, 12);

            for (int i = 0; i < maxmountains; i++)
            {
                //int mountainstart = WorldGen.genRand.Next(40, TempireSubworld.WorldWidth - 600);
                int mountainstart = (TempireSubworld.WorldWidth / maxmountains) * i + WorldGen.genRand.Next(600);
                PointList.Add(i, new Vector2[] { });

                int maxpoints = WorldGen.genRand.Next(5, 10);
                int nextpoint = 0;

                if (mountainstart < TempireSubworld.WorldWidth / 2)
                {
                    for (int j = 0; j < maxpoints; j++)
                    {
                        int yextension = WorldGen.genRand.Next(20, 40);
                        int xextension = WorldGen.genRand.Next(10, 30);

                        if (j == 0 || j == maxpoints - 1)
                        {
                            PointList[i] = PointList[i].Append(new Vector2(mountainstart + (nextpoint + xextension), 0)).ToArray();
                        }
                        else
                        {
                            PointList[i] = PointList[i].Append(new Vector2(mountainstart + (nextpoint + xextension), yextension)).ToArray();
                        }

                        nextpoint += xextension;
                    }
                }
                else
                {
                    for (int j = maxpoints; j >= 0; j--)
                    {
                        int yextension = WorldGen.genRand.Next(20, 40);
                        int xextension = WorldGen.genRand.Next(10, 30);

                        if (j == maxpoints || j == 0)
                        {
                            PointList[i] = PointList[i].Append(new Vector2(mountainstart - (nextpoint - xextension), 0)).ToArray();
                        }
                        else
                        {
                            PointList[i] = PointList[i].Append(new Vector2(mountainstart - (nextpoint - xextension), yextension)).ToArray();
                        }

                        nextpoint -= xextension;
                    }
                }
            }



            progress.Message = "Testing Terrain";
            for (int i = 0; i < PointList.Count; i++)
            {
                int startx = (int)PointList[i][0].X;
                int xlength = (int)PointList[i][PointList[i].Length - 1].X - (int)PointList[i][0].X;
                int pointfollow = 1;
                int height = 0;

                int worldsurfaceoffset = 0;
                for (int k = 0; k < 50; k++)
                {
                    Tile tile = Main.tile[startx + xlength/2, TempireSubworld.WorldHeight / 2 - k];
                    if (!tile.HasTile) break;
                    worldsurfaceoffset = k;
                    
                }

                for (int j = 0; j < xlength; j++)
                {
                    if (j > PointList[i][pointfollow].X - (int)PointList[i][0].X && pointfollow < PointList[i].Length - 1)
                    {
                        pointfollow++;
                    }
                     
                    float dist = (j - (PointList[i][pointfollow - 1].X - startx)) / (PointList[i][pointfollow].X - PointList[i][pointfollow - 1].X);
                    height = (int)MathHelper.SmoothStep(PointList[i][pointfollow - 1].Y, PointList[i][pointfollow].Y, dist);

                    for (int k = 0; k < TempireSubworld.WorldHeight; k++)
                    {
                        if ( k > TempireSubworld.WorldHeight / 2 - height - worldsurfaceoffset)
                        {
                            Tile tile = Main.tile[(ushort)(PointList[i][0].X + j), k];
                            if (!tile.HasTile)
                            {
                                tile.HasTile = true;
                                tile.TileType = (ushort)ModContent.TileType<TempireDirt>();
                            }
                            
                        }
                    }

                }

            }
        }
    }
}
