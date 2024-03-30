using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Systems.MageClass;
using Terrafirma.Systems.NPCQuests;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Terrafirma.Systems.Cooking
{
    public sealed class CookingRecipe
    {
        public List<Item> items = new List<Item>();
        public List<RecipeGroup> groups = new List<RecipeGroup>();

        public int Result;
        public int Stack;
        public int Tile = TileID.CookingPots;

        public static CookingRecipe createCookingRecipe(int Result, int Stack = 1)
        {
            CookingRecipe rec = new CookingRecipe();
            rec.Result = Result;
            rec.Stack = Stack;
            return rec;
        }

        /// <summary>
        /// returns true if the list contains all this recipe's ingredients
        /// </summary>
        public bool CanCraft(List<int> itemlist)
        {
            List<int> CheckList = new List<int> { };
            List<RecipeGroup> GroupsCheckList = new List<RecipeGroup> { };


            for (int i = 0; i < items.Count; i++) CheckList.Add(items[i].type);
            for (int i = 0; i < groups.Count; i++) GroupsCheckList.Add(groups[i]);

            int IngredientInt = CheckList.Count + GroupsCheckList.Count;

            for (int i = 0; i < itemlist.Count; i++)
            {
                int FalseCheck = 0;
                if (CheckList.Contains(itemlist[i]))
                {
                    CheckList.Remove(itemlist[i]);
                    IngredientInt--;
                }
                else FalseCheck++;

                int FalseGroupCheck = GroupsCheckList.Count;
                for (int j = GroupsCheckList.Count - 1; j >= 0; j--)
                {
                    if (GroupsCheckList[j].ValidItems.Contains(itemlist[i]))
                    {
                        IngredientInt--;
                        GroupsCheckList.Remove(GroupsCheckList[j]);
                        break;
                    }
                }

                if (GroupsCheckList.Count == FalseGroupCheck) FalseCheck++;

                if (FalseCheck == 2 && itemlist[i] != 0) return false;
            }

            if (IngredientInt != 0) return false;
            return true;
        }

        //public bool CanCraft(List<Item> itemlist)
        //{
        //    List<int> ItemsID = new List<int>();

        //    for (int i = 0; i < items.Count; i++)
        //    {
        //        ItemsID.Add(items[i].type);
        //    }

        //    if (ItemsID.Count < 3) ItemsID.Add(0);

        //    for (int i = 0; i < itemlist.Count; i++)
        //    {
        //        if (ItemsID.Contains(itemlist[i].type)) { ItemsID.Remove(itemlist[i].type); }              
        //        else if (!ItemsID.Contains(itemlist[i].type)) return false;
        //    }

        //    if (ItemsID.Count > 0) return false;
        //    return true;
        //}

        public bool CanCraft(List<Item> itemlist)
        {
            List<int> CheckList = new List<int>{};
            List<RecipeGroup> GroupsCheckList = new List<RecipeGroup> { };


            for (int i = 0; i < items.Count; i++) CheckList.Add(items[i].type);
            for (int i = 0; i < groups.Count; i++) GroupsCheckList.Add(groups[i]);

            int IngredientInt = CheckList.Count + GroupsCheckList.Count;

            for (int i = 0; i < itemlist.Count; i++)
            {
                int FalseCheck = 0;
                if (CheckList.Contains(itemlist[i].type))
                {
                    CheckList.Remove(itemlist[i].type);
                    IngredientInt--;
                }
                else FalseCheck++;

                int FalseGroupCheck = GroupsCheckList.Count;
                for (int j = GroupsCheckList.Count - 1; j >= 0; j--)
                {
                    if (GroupsCheckList[j].ValidItems.Contains(itemlist[i].type))
                    {
                        IngredientInt--;
                        GroupsCheckList.Remove(GroupsCheckList[j]);
                        break;
                    }
                }

                if (GroupsCheckList.Count == FalseGroupCheck) FalseCheck++;

                if (FalseCheck == 2 && itemlist[i].type != 0) return false;
            }

            if (IngredientInt != 0) return false;
            return true;
        }

        /// <summary>
        /// Adds an ingredient to the recipe, will not do anything if this recipe already has 3 ingredients
        /// </summary>
        public void AddIngredient(Item item)
        {
            if (items.Count < 3) this.items.Add( item );
        }

        public void AddIngredient(int itemid)
        {
            if (items.Count < 3) this.items.Add( new Item(itemid, 1) );
        }

        public void AddRecipeGroup(int groupid)
        {
            if (groups.Count < 3)
            {
                this.groups.Add(RecipeGroup.recipeGroups[groupid]);
            }
        }

        /// <summary>
        /// Adds a tile to the recipe
        /// </summary>
        public void AddTile(int tileid)
        {
            this.Tile = tileid;
        }

        public void SetResult(int result)
        {
            this.Result = result;
        }

        /// <summary>
        /// Registers the recipe in the CookingRecipeIndex. 
        /// </summary>
        public void Register()
        {
            //while (this.items.Count < 3)
            //{
            //        items.Add(new Item(0));
            //}
            CookingRecipeIndex.cookingrecipes.Add(this);
        }

        /// <summary>
        /// Use this method to register multiple recipes with the same result and tile. Good for making multiple recipe variants for a single item
        /// </summary>
        /// <param name="items"> A list that contains a list of recipes</param>
        //public void RegisterMultiple(List<List<Item>> items)
        //{
        //    for (int i = 0; i < items.Count; ++i)
        //    {
        //        CookingRecipe recipe = new CookingRecipe(Result, Stack);
        //        for (int j = 0;  j < items[i].Count; ++j)
        //        {
        //            recipe.AddIngredient(items[i][j]);
        //        }
        //        recipe.Register();
        //    }
        //}

        //public void RegisterMultiple(List<List<int>> items)
        //{
        //    for (int i = 0; i < items.Count; ++i)
        //    {
        //        CookingRecipe recipe = new CookingRecipe(Result, Stack);
        //        for (int j = 0; j < items[i].Count; ++j)
        //        {
        //            recipe.AddIngredient(items[i][j]);
        //        }
        //        recipe.Register();
        //    }
        //}

        public void RegisterVariant(int itemid)
        {
            CookingRecipe recipe = CookingRecipe.createCookingRecipe(Result, Stack);
            recipe.AddIngredient(itemid);
            recipe.Register();
        }

        public void RegisterVariant(Item item)
        {
            CookingRecipe recipe = CookingRecipe.createCookingRecipe(Result, Stack);
            recipe.AddIngredient(item);
            recipe.Register();
        }

        public void RegisterVariant(List<int> itemids)
        {
            CookingRecipe recipe = CookingRecipe.createCookingRecipe(Result, Stack);
            for (int i  = 0; i < itemids.Count; i++)
            {
                recipe.AddIngredient(itemids[i] );
            }
            recipe.Register();
        }

        public void RegisterVariant(List<Item> items)
        {
            CookingRecipe recipe = CookingRecipe.createCookingRecipe(Result, Stack);
            recipe.items = items;
            recipe.Register();
        }
    }

    public class CookingRecipeIndex
    {
        public static List<CookingRecipe> cookingrecipes = new List<CookingRecipe> { };

    }
}
