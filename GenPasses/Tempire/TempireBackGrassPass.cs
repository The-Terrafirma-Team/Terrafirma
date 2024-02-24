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
    public class TempireBackGrassPass : GenPass
    {
        //TODO: remove this once tML changes generation passes
        public TempireBackGrassPass() : base("Grass", 2) { }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {  
            progress.Message = "Placing Grass"; // Sets the text displayed for this pass
            for (int i = 0; i < TempireSubworld.WorldWidth - 1; i++)
            {
                for (int j = 0; j < TempireSubworld.WorldHeight / 2 + 20; j++)
                {
                    if (Main.tile[i, j + 1].HasTile && Main.tile[i,j + 1].TileType == (ushort)ModContent.TileType<TempireGrass>() && !Main.tile[i, j].HasTile && !WorldGen.genRand.NextBool(4))
                    {
                        if (WorldGen.genRand.NextBool(4))
                        {
                            if (WorldGen.genRand.NextBool(3)) WorldGen.Place1xX(i, j, (ushort)ModContent.TileType<BigTempireGrass>(), WorldGen.genRand.Next(10,16));
                            else WorldGen.Place1xX(i, j, (ushort)ModContent.TileType<TempireSurfaceGrass>(), WorldGen.genRand.Next(6, 12));
                        }
                        else
                        {
                            if (WorldGen.genRand.NextBool(3))  WorldGen.Place1xX(i, j, (ushort)ModContent.TileType<BigTempireGrass>(), WorldGen.genRand.Next(11));
                            else WorldGen.Place1xX(i, j, (ushort)ModContent.TileType<TempireSurfaceGrass>(), WorldGen.genRand.Next(6));
                        }
                        
                    }
                }
            }
        }
    }
}
