using Microsoft.Xna.Framework;
using Terrafirma.Common.Players;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Equipment.Ranged
{
    public class LeatherGlove : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToAccessory();
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(0, 0, 2, 0);
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddTile(TileID.WorkBenches).AddIngredient(ItemID.Leather,10).AddIngredient(ItemID.Cobweb, 25).Register();
        }
        public override void UpdateEquip(Player player)
        {
            player.PlayerStats().ThrowerVelocity += 0.15f;
            player.PlayerStats().BowChargeTimeMultipler -= 0.1f;
        }
    }
}
