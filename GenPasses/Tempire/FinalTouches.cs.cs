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
    public class FinalTouches : GenPass
    {
        public FinalTouches() : base("Terrain", 1) { }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {

            for (int i = TempireSubworld.WorldHeight - 1; i > 1; i--)
            {
                if (!Main.tile[TempireSubworld.WorldWidth / 2, i].HasTile)
                {
                    //Set Player spawn point here idk
                }
            }

            progress.Message = "Cherry on top of the cake"; // Sets the text displayed for this pass
        }
    }
}
