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

namespace Terrafirma.GenPasses.Tempire
{
    public class TestSmootheningGenPass : GenPass
    {
        //TODO: remove this once tML changes generation passes
        public TestSmootheningGenPass() : base("Smoothening", 3) { }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {  
            progress.Message = "Smoothening Grass"; // Sets the text displayed for this pass
            for (int i = 0; i < TempireSubworld.WorldWidth - 1; i++)
            {
                for (int j = 0; j < TempireSubworld.WorldHeight - 1; j++)
                {

                    if (Main.tile[i, j].HasTile)
                    {
                        if (i != 0 && i != TempireSubworld.WorldWidth && j != 0 && j != TempireSubworld.WorldHeight)
                        {
                            if (!Main.tile[i - 1, j - 1].HasTile && !Main.tile[i - 1, j].HasTile && !Main.tile[i, j - 1].HasTile)
                            {
                                if (WorldGen.genRand.NextBool()) WorldGen.SlopeTile(i, j, (int)SlopeType.SlopeDownRight);
                                else WorldGen.PoundTile(i, j);

                            }
                            else if (!Main.tile[i + 1, j - 1].HasTile && !Main.tile[i + 1, j].HasTile && !Main.tile[i, j - 1].HasTile)
                            {
                                if (WorldGen.genRand.NextBool()) WorldGen.SlopeTile(i, j, (int)SlopeType.SlopeDownLeft);
                                else WorldGen.PoundTile(i, j);
                            }
                            else if (!Main.tile[i - 1, j + 1].HasTile && !Main.tile[i - 1, j].HasTile && !Main.tile[i, j + 1].HasTile)
                            {
                                WorldGen.SlopeTile(i, j, (int)SlopeType.SlopeUpRight);
                            }
                            else if (!Main.tile[i + 1, j + 1].HasTile && !Main.tile[i + 1, j].HasTile && !Main.tile[i, j + 1].HasTile)
                            {
                                WorldGen.SlopeTile(i, j, (int)SlopeType.SlopeUpLeft);
                            }
                        }
                    }

                } 
            }
        }
    }
}
