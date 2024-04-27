using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Tiles;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Placeable.Statues
{
    public class StrangeBulbStatue : ModItem
    {
        public virtual int style => 0;
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.SlimeStatue);
            Item.createTile = ModContent.TileType<Tiles.Statues>();
            Item.placeStyle = style;
        }
    }
    public class ExcaliburStatue : StrangeBulbStatue
    {
        public override int style => 1;
    }
    public class CrossStatue : StrangeBulbStatue
    {
        public override int style => 2;
    }
    public class OverseeingStatue : StrangeBulbStatue
    {
        public override int style => 3;
    }
    public class FourPointedStarStatue : StrangeBulbStatue
    {
        public override int style => 4;
    }
    public class WrenchStatue : StrangeBulbStatue
    {
        public override int style => 5;
    }
    public class JewelStatue : StrangeBulbStatue
    {
        public override int style => 6;
    }
}
