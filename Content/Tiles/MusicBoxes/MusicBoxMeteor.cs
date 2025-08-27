using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Common.Templates;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace Terrafirma.Content.Tiles.MusicBoxes
{
    public class MusicBoxMeteor : MusicBoxTile
    {
        public override int AssociatedItem => ModContent.ItemType<MusicBoxMeteorItem>();
    }
    public class MusicBoxMeteorItem : MusicBoxItem
    {
        public override int Tile => ModContent.TileType<MusicBoxMeteor>();
        public override string MusicName => "Assets/Music/Meteor";
    }
}
