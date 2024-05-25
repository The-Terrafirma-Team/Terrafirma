using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Vanity.Reference
{
    [AutoloadEquip(EquipType.Head)]
    public class PatchouliHat : ModItem
    {
        public override void SetStaticDefaults()
        {
            ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true;
        }
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 30;
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.sellPrice(silver: 75);
            Item.vanity = true;
            Item.maxStack = 1;
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddTile(TileID.Loom).AddIngredient(ItemID.Silk, 6).AddRecipeGroup(RecipeGroupID.Wood, 15).AddRecipeGroup(RecipeGroupID.IronBar, 2).AddIngredient(ItemID.HellstoneBar, 2).Register();
        }
    }
}
