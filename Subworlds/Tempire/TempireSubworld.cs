using SubworldLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.WorldBuilding;
using Terraria;
using Terrafirma.GenPasses.Tempire;
using System.Reflection;
using Terrafirma.Reworks;

namespace Terrafirma.Subworlds.Tempire
{
    public class TempireSubworld : Subworld
    {
        public const int WorldWidth = 3000;
        public const int WorldHeight = 4200;
        public override int Width => WorldWidth;
        public override int Height => WorldHeight;

        public override bool ShouldSave => false;
        public override bool NoPlayerSaving => false;

        public override List<GenPass> Tasks => new List<GenPass>()
        {
            //new TestGenPass(),
            ////new TestCircleIsland(),
            //new TestHouseGenPass(),
            //new TestGrassGenPass(),
            //new TestSmootheningGenPass()
            new MakeWorldNotBreakWhenItIsTallPass(),
            new BaseTerrain(),
            new Mountains(),
            new TempireGrassPass(),
            new SmootheningGenPass(),
            new TempireBackGrassPass()

        };
        public override void OnLoad()
        {

            Main.NewText("YIPPIE!!!");
        }

    }
}
