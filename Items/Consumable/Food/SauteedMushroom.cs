using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terrafirma.Buffs.Buffs;
using Terrafirma.Global.Templates;
using Terrafirma.Items.Materials;
using Terraria.DataStructures;
using Terrafirma.Systems.Cooking;

namespace Terrafirma.Items.Consumable.Food
{
    internal class SauteedMushroom : FoodTemplate
    {
        
        public override void SetDefaults()
        {
            Item.DefaultToFood(31, 16, BuffID.WellFed3, 3600 * 10);

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(silver: 2);
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 20;
            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(int.MaxValue, 3));
            ItemID.Sets.FoodParticleColors[Item.type] = new Color[3] {
                new Color(147, 77, 34),
                new Color(104, 42, 12),
                new Color(80, 153, 39)
            };
            ItemID.Sets.IsFood[Type] = true;
        }

        public override void AddRecipes()
        {
            CookingRecipe recipe = CookingRecipe.createCookingRecipe(Type);
            recipe.AddIngredient(ModContent.ItemType<Mistcap>());
            recipe.AddIngredient(ModContent.ItemType<Mistcap>());
            recipe.Register();
        }
    }
}
