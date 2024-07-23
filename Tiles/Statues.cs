using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terrafirma.Items.Placeable.Statues;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Terrafirma.Tiles
{
    public class Statues : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileObsidianKill[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
            TileObjectData.newTile.DrawYOffset = 2;
            TileObjectData.newTile.StyleWrapLimit = 7;
            TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;
            TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
            TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;
            TileObjectData.addAlternate(TileObjectData.newTile.StyleWrapLimit);
            TileObjectData.addTile(Type);
            DustType = DustID.Stone;
            TileID.Sets.DisableSmartCursor[Type] = true;
            AddMapEntry(new Color(144, 148, 144), Language.GetText("MapObject.Statue"));
        }
        public override IEnumerable<Item> GetItemDrops(int i, int j)
        {
            int item = 0;
            switch(Main.tile[i, j].TileFrameX / 36)
            {
                case 0:
                    item = ModContent.ItemType<StrangeBulbStatue>();
                    break;
                case 1:
                    item = ModContent.ItemType<ExcaliburStatue>();
                    break;
                case 2:
                    item = ModContent.ItemType<CrossStatue>();
                    break;
                case 3:
                    item = ModContent.ItemType<OverseeingStatue>();
                    break;
                case 4:
                    item = ModContent.ItemType<FourPointedStarStatue>();
                    break;
                case 5:
                    item = ModContent.ItemType<WrenchStatue>();
                    break;
                case 6:
                    item = ModContent.ItemType<JewelStatue>();
                    break;
            }
            yield return new Item(item);
        }
    }
}
