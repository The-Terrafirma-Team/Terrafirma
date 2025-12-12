using Terraria;

namespace Terrafirma.Utilities
{
    public static class CollisionUtils
    {
        public static bool SolidTiles(int startX, int endX, int startY, int endY, bool allowTopSurfacesAndWater)
        {
            if (startX < 0)
                return true;

            if (endX >= Main.maxTilesX)
                return true;

            if (startY < 0)
                return true;

            if (endY >= Main.maxTilesY)
                return true;
            for (int i = startX; i < endX + 1; i++)
            {
                for (int j = startY; j < endY + 1; j++)
                {
                    Tile tile = Main.tile[i, j];
                    if (tile == null)
                        return false;
                    if (allowTopSurfacesAndWater && tile.LiquidAmount > 0)
                        return true;
                    if (tile.HasTile && !Main.tile[i, j].IsActuated)
                    {
                        ushort type = tile.TileType;
                        bool flag = Main.tileSolid[type] && !Main.tileSolidTop[type];
                        if (allowTopSurfacesAndWater)
                        {
                            flag |= Main.tileSolidTop[type] && tile.TileFrameY == 0;
                        }

                        if (flag)
                            return true;
                    }
                }
            }

            return false;
        }

    }
}
