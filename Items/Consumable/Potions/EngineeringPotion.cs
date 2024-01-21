using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using TerrafirmaRedux.Buffs.Buffs;

namespace TerrafirmaRedux.Items.Consumable.Potions
{
    internal class EngineeringPotion : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 30;

            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item3;

            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(silver: 2);

            Item.buffType = ModContent.BuffType<Engineering>();
            Item.buffTime = 3600 * 5;
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 20;

            ItemID.Sets.DrinkParticleColors[Type] = new Color[3] {
                new Color(255, 255, 20),
                new Color(240, 240, 40),
                new Color(230, 230, 20)
            };
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.BottledWater, 1)
            .AddIngredient(ItemID.VileMushroom, 1)
            .AddIngredient(ItemID.Wire, 3)
            .AddTile(TileID.Bottles)
            .Register();

            CreateRecipe()
            .AddIngredient(ItemID.BottledWater, 1)
            .AddIngredient(ItemID.ViciousMushroom, 1)
            .AddIngredient(ItemID.Wire, 3)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
}
