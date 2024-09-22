using Microsoft.Xna.Framework;
using Terrafirma.Common.Players;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Equipment.Ranged
{
    public class SporeVial : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToAccessory();
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.buyPrice(0, 0, 35, 0);
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddTile(TileID.Bottles).AddIngredient(ItemID.Bottle).AddIngredient(ItemID.JungleSpores, 10).Register();
        }
        public override void UpdateEquip(Player player)
        {
            player.PlayerStats().ThrowerDebuffPower += 0.15f;
        }
    }
}
