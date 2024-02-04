using System.Runtime.CompilerServices;
using Terrafirma.Items.Weapons.Summoner.Swarm;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Global
{
    public class ChestItemWorldGen : ModSystem
    {
        public override void PostWorldGen()
        {

            //Desert Chest
            //1/8 Chance for Main Item to be

            int desertchest_maxitems = 5;
            int desertchest_itemsplaced = 0;

            for (int chestIndex = 0; chestIndex < Main.maxChests; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex]; 
                if (chest != null)
                {
                    Tile chestTile = Main.tile[chest.x, chest.y];

                    if (chestTile.TileType == TileID.Containers2 && chestTile.TileFrameX == 10 * 36)
                    {

                        if (WorldGen.genRand.NextBool(8) && desertchest_itemsplaced < desertchest_maxitems)
                        {
                            chest.item[0].SetDefaults(ModContent.ItemType<VultureStaff>());
                            desertchest_itemsplaced++;
                        }

                    }
                }
            }

        }
    }
}