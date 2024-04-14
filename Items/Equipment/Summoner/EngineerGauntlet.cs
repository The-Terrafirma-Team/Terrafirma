using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Equipment.Summoner
{
    [AutoloadEquip(EquipType.HandsOff, EquipType.HandsOn)]
    public class EngineerGauntlet : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToAccessory(32, 28);
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = ItemRarityID.Orange;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<PlayerStats>().WrenchBuffTimeMultiplier += 1f;
        }

        public override void AddRecipes()
        {
            Recipe silverrecipe = Recipe.Create(Type)
                .AddIngredient(ItemID.Wire, 10)
                .AddIngredient(ItemID.SilverBar, 10)
                .AddTile(TileID.Anvils)
                .Register();
            Recipe tungstenrecipe = Recipe.Create(Type)
                .AddIngredient(ItemID.Wire, 10)
                .AddIngredient(ItemID.TungstenBar, 10)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
