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
    //May not be needeed at all lmao
    public class BaseWalls : GenPass
    {
        //TODO: remove this once tML changes generation passes
        public BaseWalls() : base("Terrain", 1) { }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            

            progress.Message = "Placing Walls"; // Sets the text displayed for this pass


            for(int x = 1; x < TempireSubworld.WorldWidth - 1; x++)
            {
                for (int y = 1000; y < TempireSubworld.WorldHeight / 2 + 18; y++)
                {
                    if (Main.tile[x,y].TileType == ModContent.TileType<TempireDirt>() || Main.tile[x, y].TileType == ModContent.TileType<Tempeslate>())
                    {
                        bool place = true;
                        for(int i = -1; i <= 1; i++)
                        {
                            for (int j = -1; j <= 1; j++)
                            {
                                if (!Main.tile[x + i, y + j].HasTile)
                                {
                                    place = false;
                                    break;
                                }
                            }
                        }
                        if((y < TempireSubworld.WorldHeight / 2 + 16 || Main.rand.NextBool(3)) && place)
                        Main.tile[x, y].WallType = WallID.Dirt;
                    }
                }
            }

            //for (int i = 0; i < TempireSubworld.WorldWidth - 1; i++)
            //{
            //    int[] columns = new int[] { };

            //    for (int j = 1000; j > TempireSubworld.WorldHeight - 1; j++)
            //    {

            //        if (i != 0 && i != TempireSubworld.WorldWidth - 1)
            //        {
            //            bool placewall = true;
            //            for (int k = -1; k <= 1; k++)
            //            {
            //                for (int c = -1; c <= 1; c++)
            //                {
            //                    if (!Main.tile[i + k, j + c].HasTile)
            //                    {
            //                        placewall = false;
            //                        break;
            //                    }
            //                }
            //            }
            //            if (placewall)
            //            {
            //                Tile tile = Main.tile[i, j];
            //                tile.WallType = WallID.Dirt;
            //            }

            //        }

            //        //bool[] column = new bool[] {};

            //        //if (Main.tile[i, j].HasTile)
            //        //{
            //        //    for (int c = j; c > j - 21; c--)
            //        //    {
            //        //        if (!Main.tile[i, j - c].HasTile)
            //        //        {
            //        //            column = column.Append(true).ToArray();
            //        //        }
            //        //        else if (Main.tile[i, j - c].HasTile)
            //        //        {
            //        //            column = column.Append(false).ToArray();
            //        //        }

            //        //    }
            //        //}

            //        //bool canplacecolumn = false;

            //        //for (int c = 1; c < column.Length; c++)
            //        //{
            //        //    if (!column[c])
            //        //    {
            //        //        canplacecolumn = true; 
            //        //        break;
            //        //    }
            //        //}

            //        //if (canplacecolumn)
            //        //{
            //        //    for (int c = 0; c < column.Length; c++)
            //        //    {
            //        //        if (column[c])
            //        //        {
            //        //            Tile tile = Main.tile[i, j - c];
            //        //            tile.WallType = WallID.Dirt;
            //        //        }
            //        //    }
            //        //}

            //    }

            //}
        }
    }
}
