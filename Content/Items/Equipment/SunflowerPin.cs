using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Content.Items.Equipment
{

    [AutoloadEquip(EquipType.Head)]
    public class SunflowerPin : ModItem
    {
        public override void SetStaticDefaults()
        {
            ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true;
        }
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 24;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(silver: 10);
            Item.defense = 1;
        }
        public override void UpdateEquip(Player player)
        {
            player.AddBuff(BuffID.Sunflower, 1);
        }

        public override void AddRecipes()
        {
            Recipe.Create(Type).AddIngredient(ItemID.Sunflower, 1).AddRecipeGroup(RecipeGroupID.IronBar, 1).AddTile(TileID.WorkBenches).Register();
            base.AddRecipes();
        }
    }
}
