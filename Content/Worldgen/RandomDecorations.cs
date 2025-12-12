using System.Collections.Generic;
using Terrafirma.Content.Tiles.Natural;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Terrafirma.Content.Worldgen
{
    public class RandomDecorationsPass : GenPass
    {
        public RandomDecorationsPass(string name, double loadWeight) : base(name, loadWeight)
        {

        }
        private void TryToPlaceEvilOreChunks(int x, int y)
        {
            if (Main.tile[x,y + 1].TileType == TileID.Ebonstone)
            {
                if (Main.rand.NextBool(23))
                {
                    switch (Main.rand.Next(3))
                    {
                        case 0:
                            WorldGen.PlaceTile(x, y, ModContent.TileType<DemoniteCrystal2x2>(), true, false, -1, Main.rand.Next(3));
                            break;
                        case 1:
                            WorldGen.PlaceTile(x, y, ModContent.TileType<DemoniteCrystal1x1>(), true, false, -1, Main.rand.Next(3));
                            break;
                        case 2:
                            WorldGen.PlaceTile(x, y, ModContent.TileType<DemoniteCrystal3x2>(), true, false, -1, 0);
                            break;
                    }
                }
            }
            else if(Main.tile[x, y + 1].TileType == TileID.Crimstone)
            {
                if (Main.rand.NextBool(15))
                {
                    WorldGen.PlaceTile(x, y, ModContent.TileType<CrimtaneGrowth2x3>(), true, false, -1, Main.rand.Next(4));
                }
            }
            else
            {
                if (y < GenVars.rockLayerHigh || y > Main.maxTilesY - 200|| !Main.rand.NextBool(220))
                    return;
                if (WorldGen.crimson)
                {
                    WorldGen.PlaceTile(x, y, ModContent.TileType<CrimtaneGrowth2x3>(), true, false, -1, Main.rand.Next(4));
                }
                else
                {
                    switch (Main.rand.Next(3))
                    {
                        case 0:
                            WorldGen.PlaceTile(x, y, ModContent.TileType<DemoniteCrystal2x2>(), true, false, -1, Main.rand.Next(3));
                            break;
                        case 1:
                            WorldGen.PlaceTile(x, y, ModContent.TileType<DemoniteCrystal1x1>(), true, false, -1, Main.rand.Next(3));
                            break;
                        case 2:
                            WorldGen.PlaceTile(x, y, ModContent.TileType<DemoniteCrystal3x2>(), true, false, -1, 0);
                            break;
                    }
                }
            }
        }
        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = RandomDecorationsSystem.RandomDecorationsMessage.Value;
            for(int x = 20; x < Main.maxTilesX - 20; x++)
            {
                for (int y = 100; y < Main.maxTilesY - 20; y++)
                {
                    if (Main.tile[x, y + 1].TileType is TileID.BlueDungeonBrick or TileID.GreenDungeonBrick or TileID.PinkDungeonBrick or TileID.LihzahrdBrick)
                        continue;
                    TryToPlaceEvilOreChunks(x, y);
                }
            }
        }
    }

    public class RandomDecorationsSystem : ModSystem
    {
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {
            int index = tasks.FindIndex(GenPass => GenPass.Name.Equals("Stalac"));
            tasks.Insert(index + 1, new RandomDecorationsPass("RandomDecorations",100f));
        }
        public static LocalizedText RandomDecorationsMessage { get; private set; }
        public override void SetStaticDefaults()
        {
            RandomDecorationsMessage = Language.GetOrRegister(Mod.GetLocalizationKey($"WorldGen.{nameof(RandomDecorationsMessage)}"));
        }
    }
}
