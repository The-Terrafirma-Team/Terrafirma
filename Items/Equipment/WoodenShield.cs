using TerrafirmaRedux.Global;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Items.Equipment
{
    [AutoloadEquip(EquipType.Shield)]
    public class WoodenShield : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToAccessory();
            Item.defense = 4;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<PlayerStats>().KnockbackResist -= 0.35f;
        }
        public override void AddRecipes()
        {
            Recipe.Create(Type).AddRecipeGroup(RecipeGroupID.Wood,25).AddRecipeGroup(RecipeGroupID.IronBar, 5).AddTile(TileID.WorkBenches).Register();
        }
    }
}
