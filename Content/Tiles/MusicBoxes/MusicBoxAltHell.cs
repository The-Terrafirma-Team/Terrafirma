using Terrafirma.Common.Templates;
using Terraria.ModLoader;

namespace Terrafirma.Content.Tiles.MusicBoxes
{
    public class MusicBoxAltHell : MusicBoxTile
    {
        public override int AssociatedItem => ModContent.ItemType<MusicBoxAltHellItem>();
    }
    public class MusicBoxAltHellItem : MusicBoxItem
    {
        public override int Tile => ModContent.TileType<MusicBoxAltHell>();
        public override string MusicName => "Assets/Music/Hell";
    }
}
