using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Common
{
    public class RecipeSystem : ModSystem
    {
        public override void AddRecipes()
        {
            #region Finish vanilla sets recipes
            //Blue Dungeon
            Recipe.Create(ItemID.BlueDungeonBathtub).AddTile(TileID.Sawmill).AddIngredient(ItemID.BlueBrick, 14).Register();
            Recipe.Create(ItemID.BlueDungeonBed).AddTile(TileID.Sawmill).AddIngredient(ItemID.BlueBrick, 15).AddIngredient(ItemID.Silk,5).Register();
            Recipe.Create(ItemID.BlueDungeonBookcase).AddTile(TileID.Sawmill).AddIngredient(ItemID.BlueBrick, 20).AddIngredient(ItemID.Book, 10).Register();
            Recipe.Create(ItemID.BlueDungeonCandelabra).AddTile(TileID.WorkBenches).AddIngredient(ItemID.BlueBrick, 5).AddIngredient(ItemID.Torch,3).Register();
            Recipe.Create(ItemID.BlueDungeonCandle).AddTile(TileID.WorkBenches).AddIngredient(ItemID.BlueBrick, 4).AddIngredient(ItemID.Torch, 1).Register();
            Recipe.Create(ItemID.BlueDungeonChandelier).AddTile(TileID.Anvils).AddIngredient(ItemID.BlueBrick, 4).AddIngredient(ItemID.Torch, 4).AddIngredient(ItemID.Chain).Register();
            Recipe.Create(ItemID.BlueDungeonChair).AddTile(TileID.WorkBenches).AddIngredient(ItemID.BlueBrick, 4).Register();
            Recipe.Create(ItemID.DungeonClockBlue).AddTile(TileID.Sawmill).AddIngredient(ItemID.BlueBrick, 10).AddRecipeGroup(RecipeGroupID.IronBar).AddIngredient(ItemID.Glass, 6).Register();
            Recipe.Create(ItemID.BlueDungeonDoor).AddTile(TileID.WorkBenches).AddIngredient(ItemID.BlueBrick, 6).Register();
            Recipe.Create(ItemID.BlueDungeonDresser).AddTile(TileID.Sawmill).AddIngredient(ItemID.BlueBrick, 16).Register();
            Recipe.Create(ItemID.BlueDungeonLamp).AddTile(TileID.WorkBenches).AddIngredient(ItemID.BlueBrick, 3).AddIngredient(ItemID.Torch).Register();
            Recipe.Create(ItemID.BlueDungeonPiano).AddTile(TileID.Sawmill).AddIngredient(ItemID.BlueBrick, 15).AddIngredient(ItemID.Bone,4).AddIngredient(ItemID.Book).Register();
            Recipe.Create(ItemID.BlueDungeonSofa).AddTile(TileID.Sawmill).AddIngredient(ItemID.BlueBrick, 5).AddIngredient(ItemID.Silk, 2).Register();
            Recipe.Create(ItemID.BlueDungeonTable).AddTile(TileID.WorkBenches).AddIngredient(ItemID.BlueBrick, 8).Register();
            Recipe.Create(ItemID.BlueDungeonWorkBench).AddIngredient(ItemID.BlueBrick, 10).Register();
            //Green Dungeon
            Recipe.Create(ItemID.GreenDungeonBathtub).AddTile(TileID.Sawmill).AddIngredient(ItemID.GreenBrick, 14).Register();
            Recipe.Create(ItemID.GreenDungeonBed).AddTile(TileID.Sawmill).AddIngredient(ItemID.GreenBrick, 15).AddIngredient(ItemID.Silk, 5).Register();
            Recipe.Create(ItemID.GreenDungeonBookcase).AddTile(TileID.Sawmill).AddIngredient(ItemID.GreenBrick, 20).AddIngredient(ItemID.Book, 10).Register();
            Recipe.Create(ItemID.GreenDungeonCandelabra).AddTile(TileID.WorkBenches).AddIngredient(ItemID.GreenBrick, 5).AddIngredient(ItemID.Torch, 3).Register();
            Recipe.Create(ItemID.GreenDungeonCandle).AddTile(TileID.WorkBenches).AddIngredient(ItemID.GreenBrick, 4).AddIngredient(ItemID.Torch, 1).Register();
            Recipe.Create(ItemID.GreenDungeonChandelier).AddTile(TileID.Anvils).AddIngredient(ItemID.GreenBrick, 4).AddIngredient(ItemID.Torch, 4).AddIngredient(ItemID.Chain).Register();
            Recipe.Create(ItemID.GreenDungeonChair).AddTile(TileID.WorkBenches).AddIngredient(ItemID.GreenBrick, 4).Register();
            Recipe.Create(ItemID.DungeonClockGreen).AddTile(TileID.Sawmill).AddIngredient(ItemID.GreenBrick, 10).AddRecipeGroup(RecipeGroupID.IronBar).AddIngredient(ItemID.Glass, 6).Register();
            Recipe.Create(ItemID.GreenDungeonDoor).AddTile(TileID.WorkBenches).AddIngredient(ItemID.GreenBrick, 6).Register();
            Recipe.Create(ItemID.GreenDungeonDresser).AddTile(TileID.Sawmill).AddIngredient(ItemID.GreenBrick, 16).Register();
            Recipe.Create(ItemID.GreenDungeonLamp).AddTile(TileID.WorkBenches).AddIngredient(ItemID.GreenBrick, 3).AddIngredient(ItemID.Torch).Register();
            Recipe.Create(ItemID.GreenDungeonPiano).AddTile(TileID.Sawmill).AddIngredient(ItemID.GreenBrick, 15).AddIngredient(ItemID.Bone, 4).AddIngredient(ItemID.Book).Register();
            Recipe.Create(ItemID.GreenDungeonSofa).AddTile(TileID.Sawmill).AddIngredient(ItemID.GreenBrick, 5).AddIngredient(ItemID.Silk, 2).Register();
            Recipe.Create(ItemID.GreenDungeonTable).AddTile(TileID.WorkBenches).AddIngredient(ItemID.GreenBrick, 8).Register();
            Recipe.Create(ItemID.GreenDungeonWorkBench).AddIngredient(ItemID.GreenBrick, 10).Register();
            //Pink Dungeon
            Recipe.Create(ItemID.PinkDungeonBathtub).AddTile(TileID.Sawmill).AddIngredient(ItemID.PinkBrick, 14).Register();
            Recipe.Create(ItemID.PinkDungeonBed).AddTile(TileID.Sawmill).AddIngredient(ItemID.PinkBrick, 15).AddIngredient(ItemID.Silk, 5).Register();
            Recipe.Create(ItemID.PinkDungeonBookcase).AddTile(TileID.Sawmill).AddIngredient(ItemID.PinkBrick, 20).AddIngredient(ItemID.Book, 10).Register();
            Recipe.Create(ItemID.PinkDungeonCandelabra).AddTile(TileID.WorkBenches).AddIngredient(ItemID.PinkBrick, 5).AddIngredient(ItemID.Torch, 3).Register();
            Recipe.Create(ItemID.PinkDungeonCandle).AddTile(TileID.WorkBenches).AddIngredient(ItemID.PinkBrick, 4).AddIngredient(ItemID.Torch, 1).Register();
            Recipe.Create(ItemID.PinkDungeonChandelier).AddTile(TileID.Anvils).AddIngredient(ItemID.PinkBrick, 4).AddIngredient(ItemID.Torch, 4).AddIngredient(ItemID.Chain).Register();
            Recipe.Create(ItemID.PinkDungeonChair).AddTile(TileID.WorkBenches).AddIngredient(ItemID.PinkBrick, 4).Register();
            Recipe.Create(ItemID.DungeonClockPink).AddTile(TileID.Sawmill).AddIngredient(ItemID.PinkBrick, 10).AddRecipeGroup(RecipeGroupID.IronBar).AddIngredient(ItemID.Glass, 6).Register();
            Recipe.Create(ItemID.PinkDungeonDoor).AddTile(TileID.WorkBenches).AddIngredient(ItemID.PinkBrick, 6).Register();
            Recipe.Create(ItemID.PinkDungeonDresser).AddTile(TileID.Sawmill).AddIngredient(ItemID.PinkBrick, 16).Register();
            Recipe.Create(ItemID.PinkDungeonLamp).AddTile(TileID.WorkBenches).AddIngredient(ItemID.PinkBrick, 3).AddIngredient(ItemID.Torch).Register();
            Recipe.Create(ItemID.PinkDungeonPiano).AddTile(TileID.Sawmill).AddIngredient(ItemID.PinkBrick, 15).AddIngredient(ItemID.Bone, 4).AddIngredient(ItemID.Book).Register();
            Recipe.Create(ItemID.PinkDungeonSofa).AddTile(TileID.Sawmill).AddIngredient(ItemID.PinkBrick, 5).AddIngredient(ItemID.Silk, 2).Register();
            Recipe.Create(ItemID.PinkDungeonTable).AddTile(TileID.WorkBenches).AddIngredient(ItemID.PinkBrick, 8).Register();
            Recipe.Create(ItemID.PinkDungeonWorkBench).AddIngredient(ItemID.PinkBrick, 10).Register();
            //Obsidian
            Recipe.Create(ItemID.ObsidianBathtub).AddTile(TileID.Sawmill).AddIngredient(ItemID.Obsidian, 12).AddIngredient(ItemID.Hellstone, 2).Register();
            Recipe.Create(ItemID.ObsidianBed).AddTile(TileID.Sawmill).AddIngredient(ItemID.Obsidian, 13).AddIngredient(ItemID.Hellstone, 2).AddIngredient(ItemID.Silk, 5).Register();
            Recipe.Create(ItemID.ObsidianBookcase).AddTile(TileID.Sawmill).AddIngredient(ItemID.Obsidian, 18).AddIngredient(ItemID.Hellstone, 2).AddIngredient(ItemID.Book, 10).Register();
            Recipe.Create(ItemID.ObsidianCandelabra).AddTile(TileID.WorkBenches).AddIngredient(ItemID.Obsidian, 3).AddIngredient(ItemID.Hellstone, 2).AddIngredient(ItemID.DemonTorch, 3).Register();
            Recipe.Create(ItemID.ObsidianCandle).AddTile(TileID.WorkBenches).AddIngredient(ItemID.Obsidian, 2).AddIngredient(ItemID.Hellstone, 2).AddIngredient(ItemID.DemonTorch, 1).Register();
            Recipe.Create(ItemID.ObsidianChandelier).AddTile(TileID.Anvils).AddIngredient(ItemID.Obsidian, 2).AddIngredient(ItemID.Hellstone, 2).AddIngredient(ItemID.DemonTorch, 4).AddIngredient(ItemID.Chain).Register();
            Recipe.Create(ItemID.ObsidianChair).AddTile(TileID.WorkBenches).AddIngredient(ItemID.Obsidian, 2).AddIngredient(ItemID.Hellstone, 2).Register();
            Recipe.Create(ItemID.ObsidianClock).AddTile(TileID.Sawmill).AddIngredient(ItemID.Obsidian, 8).AddIngredient(ItemID.Hellstone, 2).AddRecipeGroup(RecipeGroupID.IronBar).AddIngredient(ItemID.Glass, 6).Register();
            Recipe.Create(ItemID.ObsidianDoor).AddTile(TileID.WorkBenches).AddIngredient(ItemID.Obsidian, 4).AddIngredient(ItemID.Hellstone, 2).Register();
            Recipe.Create(ItemID.ObsidianDresser).AddTile(TileID.Sawmill).AddIngredient(ItemID.Obsidian, 14).AddIngredient(ItemID.Hellstone, 2).Register();
            Recipe.Create(ItemID.ObsidianLamp).AddTile(TileID.WorkBenches).AddIngredient(ItemID.Obsidian, 2).AddIngredient(ItemID.Hellstone, 1).AddIngredient(ItemID.DemonTorch).Register();
            Recipe.Create(ItemID.ObsidianLantern).AddTile(TileID.WorkBenches).AddIngredient(ItemID.Obsidian, 4).AddIngredient(ItemID.Hellstone, 2).AddIngredient(ItemID.DemonTorch).Register();
            Recipe.Create(ItemID.ObsidianPiano).AddTile(TileID.Sawmill).AddIngredient(ItemID.Obsidian, 13).AddIngredient(ItemID.Hellstone, 2).AddIngredient(ItemID.Bone, 4).AddIngredient(ItemID.Book).Register();
            Recipe.Create(ItemID.ObsidianSofa).AddTile(TileID.Sawmill).AddIngredient(ItemID.Obsidian, 3).AddIngredient(ItemID.Hellstone, 2).AddIngredient(ItemID.Silk, 2).Register();
            Recipe.Create(ItemID.ObsidianTable).AddTile(TileID.WorkBenches).AddIngredient(ItemID.Obsidian, 6).AddIngredient(ItemID.Hellstone, 2).Register();
            Recipe.Create(ItemID.ObsidianWorkBench).AddIngredient(ItemID.Obsidian, 8).AddIngredient(ItemID.Hellstone, 2).Register();
            #endregion Finish vanilla sets recipes
        }
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];
                if (recipe.Mod != null)
                    return;

                bool hasWall = false;
                for(int j = 0; j < recipe.requiredItem.Count; j++)
                {
                    if (recipe.requiredItem[j].createWall != -1)
                    {
                        hasWall = true;
                        break;
                    }
                }
                if (hasWall)
                    continue;

                #region bricks
                if (recipe.HasResult(ItemID.GrayBrick) && !recipe.HasIngredient(ItemID.StonePlatform))
                {
                    recipe.RemoveIngredient(ItemID.StoneBlock);
                    recipe.AddIngredient(ItemID.StoneBlock);
                }
                else if (recipe.HasResult(ItemID.RedBrick))
                {
                    recipe.RemoveIngredient(ItemID.ClayBlock);
                    recipe.AddIngredient(ItemID.ClayBlock);
                }
                else if (recipe.HasResult(ItemID.SnowBrick))
                {
                    recipe.RemoveIngredient(ItemID.SnowBlock);
                    recipe.AddIngredient(ItemID.SnowBlock);
                }
                else if (recipe.HasResult(ItemID.IceBrick))
                {
                    recipe.RemoveIngredient(ItemID.IceBlock);
                    recipe.AddIngredient(ItemID.IceBlock);
                }
                else if (recipe.HasResult(ItemID.MudstoneBlock))
                {
                    recipe.ReplaceResult(ItemID.MudstoneBlock,2);
                }
                else if (recipe.HasResult(ItemID.IridescentBrick))
                {
                    recipe.ReplaceResult(ItemID.IridescentBrick, 2);
                }
                else if (recipe.HasResult(ItemID.EbonstoneBrick))
                {
                    recipe.RemoveIngredient(ItemID.EbonstoneBlock);
                    recipe.AddIngredient(ItemID.EbonstoneBlock);
                }
                else if (recipe.HasResult(ItemID.CrimstoneBrick))
                {
                    recipe.RemoveIngredient(ItemID.CrimstoneBlock);
                    recipe.AddIngredient(ItemID.CrimstoneBlock);
                }
                else if (recipe.HasResult(ItemID.PearlstoneBrick))
                {
                    recipe.RemoveIngredient(ItemID.PearlstoneBlock);
                    recipe.AddIngredient(ItemID.PearlstoneBlock);
                }
                else if (recipe.HasResult(ItemID.ShimmerBrick))
                {
                    recipe.ReplaceResult(ItemID.ShimmerBrick, 2);
                }
                #endregion bricks
                #region crafted blocks
                else if (recipe.HasResult(ItemID.FleshBlock))
                {
                    recipe.RemoveIngredient(ItemID.CrimstoneBlock);
                    recipe.AddIngredient(ItemID.CrimstoneBlock);
                }
                else if (recipe.HasResult(ItemID.FrozenSlimeBlock))
                {
                    recipe.ReplaceResult(ItemID.FrozenSlimeBlock, 2);
                }
                else if (recipe.HasResult(ItemID.Glass))
                {
                    recipe.RemoveRecipeGroup(RecipeGroupID.Sand);
                    recipe.AddRecipeGroup(RecipeGroupID.Sand);
                }
                else if (recipe.HasResult(ItemID.HardenedSand))
                {
                    recipe.ReplaceResult(ItemID.HardenedSand, 2);
                }
                else if (recipe.HasResult(ItemID.LesionBlock))
                {
                    recipe.RemoveIngredient(ItemID.EbonstoneBlock);
                    recipe.AddIngredient(ItemID.EbonstoneBlock);
                }
                else if (recipe.HasResult(ItemID.Sandstone))
                {
                    recipe.ReplaceResult(ItemID.Sandstone, 2);
                }
                #endregion crafted blocks
            }
        }
    }
}
