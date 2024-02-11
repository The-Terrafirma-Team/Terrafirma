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
using Terraria.ModLoader;
using Terrafirma.Tiles.Tempire;

namespace Terrafirma.GenPasses.Tempire
{
    public class TempireGrassPass : GenPass
    {
        //TODO: remove this once tML changes generation passes
        public TempireGrassPass() : base("Grass", 2) { }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {  
            progress.Message = "Placing Grass"; // Sets the text displayed for this pass
            for (int i = 0; i < TempireSubworld.WorldWidth - 1; i++)
            {
                for (int j = 0; j < TempireSubworld.WorldHeight - 1; j++)
                {

                    if (Main.tile[i, j].HasTile && Main.tile[i,j].TileType == (ushort)ModContent.TileType<TempireDirt>())
                    {
                        for (int k = -1; k <= 1; k++)
                        {
                            for (int c = -1; c <= 1; c++)
                            {
                                if (i != 0 && i != TempireSubworld.WorldWidth)
                                {
                                    if (!Main.tile[i + k, j + c].HasTile) Main.tile[i, j].TileType = (ushort)ModContent.TileType<TempireGrass>();
                                }   
                            }
                        }

                        if (WorldGen.genRand.NextBool(10) && i > 10 && i < TempireSubworld.WorldWidth - 10 && j > 10 && j < TempireSubworld.WorldWidth - 10)
                        {
                            if(!Main.tile[i + Main.rand.Next(-6,6), j + Main.rand.Next(-6, 6)].HasTile)
                            {
                                Main.tile[i, j].TileType = (ushort)ModContent.TileType<TempireGrass>();
                            }
                        }
                    }
                }
            }
        }
    }
}
