using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terrafirma.Tiles;

namespace Terrafirma.Items.Materials
{
    public class AgnomalumBar : ModItem
    {
        public override void SetDefaults()
        {
            Item.Size = new Vector2(16);
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(0, 0, 15, 0);
            Item.DefaultToPlaceableTile(ModContent.TileType<PlacedBars>(), 0);
        }
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 25;
        }
    }
}
