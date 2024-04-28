using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Equipment.Elemental.ElementEnhancement
{
    public class MoonlightCharm : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToAccessory();
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 0, 2, 0);
        }
        public override void UpdateEquip(Player player)
        {
            player.PlayerStats().DarkEnhancement = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddTile(TileID.DemonAltar).AddIngredient(ItemID.BlackLens).AddIngredient(ItemID.Chain, 5).AddIngredient(ItemID.UnholyWater,20).Register();
            CreateRecipe().AddTile(TileID.DemonAltar).AddIngredient(ItemID.BlackLens).AddIngredient(ItemID.Chain, 5).AddIngredient(ItemID.BloodWater, 20).Register();
        }
    }
}
