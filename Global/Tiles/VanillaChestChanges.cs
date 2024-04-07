using System.Runtime.CompilerServices;
using Terrafirma.Items.Weapons.Summoner.Swarm;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Global.Tiles
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

                    if (chestTile.TileType == TileID.Containers2 && chestTile.TileFrameX == 10 * 36)
                    {
                        //Desert Chest
                        //1/8 Chance for Main Item to be Vulture Staff

                        int desertchest_maxitems = 5;
                        int desertchest_itemsplaced = 0;

                        if (WorldGen.genRand.NextBool(8) && desertchest_itemsplaced < desertchest_maxitems)
                        {
                            chest.item[0].SetDefaults(ModContent.ItemType<VultureStaff>());
                            desertchest_itemsplaced++;
                        }

                    }
                    else if (chestTile.TileType == TileID.Containers2 && chestTile.TileFrameX == 2 * 36)
                    {
                        //Gold Chest
                        //1/3 Chance for Leather to be added

                        if (WorldGen.genRand.NextBool(3))
                        {
                            for (int i = 0; i < chest.item.Length; i++)
                            {
                                if (chest.item[i].type == 0)
                                {
                                    chest.item[i] = new Item(ItemID.Leather, WorldGen.genRand.Next(2, 6));
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