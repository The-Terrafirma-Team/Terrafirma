using Terrafirma.Content.Items.Consumable;
using Terrafirma.Content.Items.Equipment.Accessories.Defense.Shield;
using Terrafirma.Content.Items.Equipment.Accessories.Movement.WindCharm;
using Terrafirma.Content.Items.Weapons.Melee;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Content.Worldgen
{
    public class ChestItemWorldGen : ModSystem
    {
        private void InsertItem(Chest chest, int insertPoint, Item item)
        {
            for(int i = chest.item.Length - 1; i > insertPoint; i--)
            {
                chest.item[i] = chest.item[i - 1].Clone();
            }
            chest.item[insertPoint] = item;
        }
        private void AddItemAtTheEnd(Chest chest, Item item)
        {
            for (int i = 0; i < chest.item.Length; i++)
            {
                if (chest.item[i].type == 0)
                {
                    chest.item[i] = item;
                    break;
                }
            }
        }
        public override void PostWorldGen()
        {
            int[] CabinChestItems = { ModContent.ItemType<Shield>(), ModContent.ItemType<Lance>(), ItemID.MiningShirt, ItemID.MiningPants };
            for (int chestIndex = 0; chestIndex < Main.maxChests; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex];
                if (chest != null)
                {
                    Tile chestTile = Main.tile[chest.x, chest.y];
                    if (chestTile.TileType == TileID.Containers && chestTile.TileFrameX == 36) // Unlocked Gold Chest
                    {
                        if (!WorldGen.genRand.NextBool(3))
                        {
                            InsertItem(chest, 1, new Item(ModContent.ItemType<RepairKit>(), WorldGen.genRand.Next(1, 3)));
                        }
                        InsertItem(chest, 1, new Item(CabinChestItems[Main.rand.Next(CabinChestItems.Length)],1,-1));
                    }
                    else if (chestTile.TileType == TileID.Containers && chestTile.TileFrameX == 468) // Skyware Chest
                    {
                        InsertItem(chest, 1, new Item(ModContent.ItemType<WindCharm>(), 1, -1));
                    }
                }
            }
        }
    }
}