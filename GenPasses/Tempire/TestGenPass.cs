using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.IO;
using Terraria.WorldBuilding;
using Terraria;
using Terrafirma.Subworlds.Tempire;
using System.Reflection;
using Terraria.Map;
using Microsoft.Xna.Framework.Graphics;
using Terrafirma.Tiles.Tempire;
using Terraria.ModLoader;

namespace Terrafirma.GenPasses.Tempire
{
    public class TestGenPass : GenPass
    {
        //TODO: remove this once tML changes generation passes
        public TestGenPass() : base("Terrain", 1) { }

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

            progress.Message = "Testing Terrain"; // Sets the text displayed for this pass
            for (int i = 0; i < TempireSubworld.WorldWidth - 1; i++)
            {
                
                for (int j = TempireSubworld.WorldHeight / 2; j < TempireSubworld.WorldHeight - 1; j++)
                {
                    progress.Set(0.5f); // Controls the progress bar, should only be set between 0f and 1f
                    if(j > TempireSubworld.WorldHeight / 2 + ((Math.Sin(i / 10f) * 5f) + 5f))
                    {
                        Tile tile = Main.tile[i, j];
                        tile.HasTile = true;
                        tile.TileType = (ushort)ModContent.TileType<TempireDirt>();
                    }
                }

                
            }
        }
    }
}
