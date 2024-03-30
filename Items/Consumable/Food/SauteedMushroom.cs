using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terrafirma.Buffs.Buffs;
using Terrafirma.Global.Templates;
using Terrafirma.Items.Materials;

namespace Terrafirma.Items.Consumable.Food
{
    internal class SauteedMushroom : FoodTemplate
    {
        public override void SetDefaults()
        {
            Item.width = 31;
            Item.height = 16;

            Item.useStyle = ItemUseStyleID.EatFood;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item2;

            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(silver: 2);

            Item.buffType = BuffID.WellFed;
            Item.buffTime = 3600 * 10;
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 20;
        }

        public override void AddRecipes()
        {
            Recipe.Create(Type, 1)
                .AddIngredient(ModContent.ItemType<Mistcap>())
                .Register();
            base.AddRecipes();
        }
    }
}
