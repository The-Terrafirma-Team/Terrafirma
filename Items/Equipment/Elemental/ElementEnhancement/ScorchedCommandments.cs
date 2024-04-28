using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Equipment.Elemental.ElementEnhancement
{
    public class ScorchedCommandments : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToAccessory();
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 0, 2, 0);
        }
        public override void UpdateEquip(Player player)
        {
            player.PlayerStats().FireEnhancement = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddTile(TileID.Hellforge).AddIngredient(ItemID.HellstoneBar,15).AddIngredient(ItemID.AshBlock, 15).AddIngredient(ItemID.Fireblossom, 5).Register();
        }
    }
}
