using TerrafirmaRedux.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Global
{
    public class RecipeReplacer : ModSystem
    {
        public override void PostAddRecipes()
        {
            for(int i = 0; i < Recipe.numRecipes; i++) 
            {
                Recipe recipe = Main.recipe[i];
                if(recipe.HasResult(ItemID.HallowedGreaves) || recipe.HasResult(ItemID.HallowedPlateMail) || recipe.HasResult(ItemID.HallowedHelmet) || recipe.HasResult(ItemID.HallowedHeadgear) || recipe.HasResult(ItemID.HallowedHood) || recipe.HasResult(ItemID.HallowedMask) ||
                    recipe.HasResult(ItemID.AncientHallowedGreaves) || recipe.HasResult(ItemID.AncientHallowedPlateMail) || recipe.HasResult(ItemID.AncientHallowedHelmet) || recipe.HasResult(ItemID.AncientHallowedHeadgear) || recipe.HasResult(ItemID.AncientHallowedHood) || recipe.HasResult(ItemID.AncientHallowedMask))
                {
                    recipe.AddIngredient(ItemID.SoulofFright,5);
                    recipe.AddIngredient(ItemID.SoulofSight, 5);
                    recipe.AddIngredient(ItemID.SoulofMight, 5);

                }
                else if(recipe.HasResult(ItemID.AdamantiteForge) || recipe.HasResult(ItemID.TitaniumForge))
                {
                    recipe.AddIngredient(ModContent.ItemType<MajesticGel>(),10);
                }


            }
        }
    }
}
