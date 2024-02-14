using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace Terrafirma
{
    public class GenUtils
    {
        public static void MakeCircle(int x, int y, int radius, int type)
        {
            int xmin = x - radius;
            int xmax = x + radius;
            int ymin = y - radius;
            int ymax = y + radius;
            for (int i = xmin; i < xmax; i++)
            {
                for (int j = ymin; j < ymax; j++)
                {
                    if (Vector2.Distance(new Vector2(i, j), new Vector2(x, y)) < radius)
                    {
                        Tile t = Main.tile[i, j];
                        t.HasTile = true;
                        t.TileType = (ushort)type;
                    }
                }
            }
        }
        public static void CarveCircle(int x, int y, int radius)
        {
            int xmin = x - radius;
            int xmax = x + radius;
            int ymin = y - radius;
            int ymax = y + radius;
            for (int i = xmin; i < xmax; i++)
            {
                for (int j = ymin; j < ymax; j++)
                {
                    if (Vector2.Distance(new Vector2(i, j), new Vector2(x, y)) < radius)
                    {
                        Tile t = Main.tile[i, j];
                        t.HasTile = false;
                    }
                }
            }
        }
    }
}
