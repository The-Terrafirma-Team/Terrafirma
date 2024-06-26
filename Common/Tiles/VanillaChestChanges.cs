﻿using System.Runtime.CompilerServices;
using Terrafirma.Items.Consumable;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Common.Tiles
{
    public class ChestItemWorldGen : ModSystem
    {
        public override void PostWorldGen()
        {

            for (int chestIndex = 0; chestIndex < Main.maxChests; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex];
                if (chest != null)
                {
                    Tile chestTile = Main.tile[chest.x, chest.y];

                    //if (chestTile.TileType == TileID.Containers2 && chestTile.TileFrameX == 10 * 36)
                    //{
                    //    //Desert Chest
                    //    //1/8 Chance for Main Item to be Vulture Staff, 5 max per World

                    //    int desertchest_maxitems = 5;
                    //    int desertchest_itemsplaced = 0;

                    //    if (WorldGen.genRand.NextBool(8) && desertchest_itemsplaced < desertchest_maxitems)
                    //    {
                    //        chest.item[0].SetDefaults(ModContent.ItemType<VultureStaff>());
                    //        desertchest_itemsplaced++;
                    //    }

                    //}
                    if (chestTile.TileType == TileID.Containers && chestTile.TileFrameX == 36)
                    {
                        //Gold Chest
                        //1/3 Chance for 2 to 6 Leather to be added

                        //if (WorldGen.genRand.NextBool(3))
                        //{
                        //    for (int i = 0; i < chest.item.Length; i++)
                        //    {
                        //        if (chest.item[i].type == 0)
                        //        {
                        //            chest.item[i] = new Item(ItemID.Leather, WorldGen.genRand.Next(2, 6));
                        //            break;
                        //        }
                        //    }
                        //}

                        //60% Chance for 1 to 2 Repair Kits to be added

                        if (WorldGen.genRand.Next(5) < 2)
                        {
                            for (int i = 0; i < chest.item.Length; i++)
                            {
                                if (chest.item[i].type == 0)
                                {
                                    chest.item[i] = new Item(ModContent.ItemType<RepairKit>(), WorldGen.genRand.Next(1, 3));
                                    break;
                                }
                            }
                        }

                    }
                }
            }
        }
    }
}