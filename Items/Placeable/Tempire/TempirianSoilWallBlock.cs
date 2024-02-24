using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Tiles.Tempire;
using Terrafirma.Walls.Tempire;
using Terraria.ModLoader;

namespace Terrafirma.Items.Placeable.Tempire
{
    public class TempirianSoilWallBlock : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableWall(ModContent.WallType<TempirianSoilWall>());
        }
    }
}
