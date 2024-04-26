using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Vanity
{
    [AutoloadEquip(EquipType.Body)]
    public class PatchouliBody : ModItem
    {
        public override void Load()
        {
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }
            EquipLoader.AddEquipTexture(Mod, $"{Texture}_{EquipType.Legs}", EquipType.Legs, this);
        }
        public override void SetMatch(bool male, ref int equipSlot, ref bool robes)
        {
            robes = true;
            equipSlot = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Legs);
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
            CreateRecipe().AddTile(TileID.Loom).AddIngredient(ItemID.Silk, 8).AddRecipeGroup(RecipeGroupID.Wood,25).AddRecipeGroup(RecipeGroupID.IronBar,2).AddIngredient(ItemID.HellstoneBar, 2).Register();
        }
    }
}
