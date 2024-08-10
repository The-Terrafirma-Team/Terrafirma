using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terrafirma.Buffs.Buffs;

namespace Terrafirma.Items.Consumable.Potions
{
    internal class CleansingPotion : ModItem
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
            Item.value = Item.sellPrice(copper: 20);
        }

        public override bool? UseItem(Player player)
        {
            for(int i = 0; i < player.buffTime.Length; i++)
            {
                if (Main.debuff[player.buffType[i]] && !BuffID.Sets.NurseCannotRemoveDebuff[player.buffType[i]])
                {
                    player.DelBuff(i);
                    i--;
                }
            }
            return true;
        }
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 20;
            ItemID.Sets.DrinkParticleColors[Type] = [new Color(255, 255, 255)];
        }

        public override void AddRecipes()
        {
            CreateRecipe(10)
            .AddIngredient(ItemID.BottledWater, 10)
            .AddIngredient(ItemID.Mushroom, 1)
            .AddIngredient(ItemID.GlowingMushroom, 1)
            .AddIngredient(ItemID.TealMushroom, 1)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
}
