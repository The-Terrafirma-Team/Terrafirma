using Microsoft.Xna.Framework;
using Terrafirma.Common.Players;
using Terrafirma.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Terrafirma.Items.Weapons.Magic.PreHardmode
{
    public class GraniteStaff : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToMagicWeapon(10,30,2,true);
            Item.mana = 8;
            Item.damage = 10;
            Item.knockBack = 2;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(silver: 75);
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddTile(TileID.Anvils).AddIngredient(ItemID.Granite, 25).AddIngredient(ItemID.Sapphire, 3).AddIngredient(ModContent.ItemType<EnchantedStone>()).Register();
        }
    }
}
