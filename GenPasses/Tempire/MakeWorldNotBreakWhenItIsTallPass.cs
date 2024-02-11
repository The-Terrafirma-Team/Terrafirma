using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Subworlds.Tempire;
using Terrafirma.Tiles.Tempire;
using Terraria.IO;
using Terraria.Map;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using Terraria;

namespace Terrafirma.GenPasses.Tempire
{
    public class MakeWorldNotBreakWhenItIsTallPass : GenPass
    {
        public MakeWorldNotBreakWhenItIsTallPass() : base("Terrain", 1) { }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {

            Main.mapTargetX = TempireSubworld.WorldWidth / 600;
            Main.mapTargetY = TempireSubworld.WorldHeight / 600;
            Main.instance.mapTarget = new RenderTarget2D[Main.mapTargetX, Main.mapTargetY];

            Main.tile = (Tilemap)Activator.CreateInstance(typeof(Tilemap), BindingFlags.NonPublic | BindingFlags.Instance, null, new object[] { (ushort)TempireSubworld.WorldWidth, (ushort)TempireSubworld.WorldHeight }, null);
            Main.Map = new WorldMap(TempireSubworld.WorldWidth, TempireSubworld.WorldHeight);
            Main.bottomWorld = TempireSubworld.WorldHeight * 16f;

            Main.initMap = new bool[Main.mapTargetX, Main.mapTargetY];
            Main.mapWasContentLost = new bool[Main.mapTargetX, Main.mapTargetY];

            Main.rockLayer = TempireSubworld.WorldHeight / 1.5;
            Main.worldSurface = TempireSubworld.WorldHeight / 2 + 15;

            progress.Message = "I'd be concerned if you could even see this."; // Sets the text displayed for this pass
        }
    }
}
