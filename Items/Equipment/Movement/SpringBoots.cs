using Microsoft.Xna.Framework;
using System;
using TerrafirmaRedux.Global;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Items.Equipment.Movement
{
    [AutoloadEquip(EquipType.Shoes)]
    class SpringBoots : ModItem
    {
        public override void SetDefaults()
		{
			Item.DefaultToAccessory(26, 36);
			Item.rare = ItemRarityID.Blue;
			Item.value = Item.sellPrice(0, 0, 75, 0);
		}

        public override void UpdateEquip(Player player)
        {
            player.autoJump = true;
            player.noFallDmg = true;

            player.jumpSpeedBoost = (player.GetModPlayer<TerrafirmaGlobalPlayer>().JumpMultiplier) * (player.GetModPlayer<TerrafirmaGlobalPlayer>().JumpMultiplier) / 2;
            player.maxRunSpeed = 3 * (player.GetModPlayer<TerrafirmaGlobalPlayer>().JumpMultiplier);

            player.GetModPlayer<TerrafirmaGlobalPlayer>().SpringBoots = true;
        }

        public override void AddRecipes()
        {

            CreateRecipe()
            .AddIngredient(ItemID.HermesBoots, 1)
            .AddIngredient(ItemID.LuckyHorseshoe, 1)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();

        }

    }
}
