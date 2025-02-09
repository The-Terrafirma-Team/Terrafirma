﻿using Microsoft.Xna.Framework;
using Terrafirma.Common.Players;
using Terrafirma.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Terrafirma.Items.Tools
{
    public class GranitePickaxe : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToSword(10, 25, 4);
            Item.pick = 55;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(silver: 75);
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddTile(TileID.Anvils).AddIngredient(ItemID.Granite, 25).AddIngredient(ItemID.Sapphire, 3).AddIngredient(ModContent.ItemType<EnchantedStone>()).Register();
        }
    }
}
