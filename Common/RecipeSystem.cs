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
            base.AddRecipes();
        }
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];
                if (recipe.Mod != null)
                    return;

                #region bricks
                if (recipe.HasResult(ItemID.GrayBrick))
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
