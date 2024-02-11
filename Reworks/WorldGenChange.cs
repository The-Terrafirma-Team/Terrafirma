using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SubworldLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Subworlds.Tempire;
using Terraria;
using Terraria.Map;
using Terraria.ModLoader;

namespace Terrafirma.Reworks
{
    internal class WorldGenChange : ModSystem
    {
        public static int MainWorldMapTargetX;
        public static int MainWorldMapTargetY;
        public static Tilemap MainWorldTilemap;
        public override void OnWorldLoad()
        {
            if (!SubworldSystem.AnyActive())
            {
                MainWorldTilemap = Main.tile;
                MainWorldMapTargetX = Main.mapTargetX;
                MainWorldMapTargetY = Main.mapTargetY;
            }
            base.OnWorldLoad();
        }

        public override void OnWorldUnload()
        {
            Main.mapTargetX = MainWorldTilemap.Width / 600;
            Main.mapTargetY = MainWorldTilemap.Height / 600;
            Main.instance.mapTarget = new RenderTarget2D[MainWorldMapTargetX, MainWorldMapTargetY];

            Main.tile = MainWorldTilemap;
            Main.Map = new WorldMap(MainWorldTilemap.Width, MainWorldTilemap.Height);
            Main.bottomWorld = MainWorldTilemap.Height * 16f;

            Main.initMap = new bool[Main.mapTargetX, Main.mapTargetY];
            Main.mapWasContentLost = new bool[Main.mapTargetX, Main.mapTargetY];

            base.OnWorldUnload();
        }

    }
}
