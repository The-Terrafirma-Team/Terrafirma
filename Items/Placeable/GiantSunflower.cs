using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;

namespace Terrafirma.Items.Placeable
{
    public class GiantSunflower : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Purity.GiantSunflower>());
            Item.value = Item.sellPrice(0, 0, 25);
        }
    }
}
