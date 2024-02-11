using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Tiles.Tempire;
using Terraria.ModLoader;

namespace Terrafirma.Items.Placeable.Tempire
{
    public class TempirianSoil : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<TempireDirt>());
        }
    }
}
