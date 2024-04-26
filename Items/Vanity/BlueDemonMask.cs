using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Vanity
{
    [AutoloadEquip(EquipType.Head)]
    public class BlueDemonMask : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 30;
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.sellPrice(silver: 75);
            Item.vanity = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddTile(TileID.DemonAltar).AddIngredient(ItemID.MushroomGrassSeeds, 10).AddIngredient(ItemID.FamiliarWig).AddIngredient(ItemID.DemoniteBar,2).Register();
        }
    }
}
