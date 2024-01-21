
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Items.Weapons.Ranged.Guns.Shroomite
{
    internal class ShroomiteCrossbow : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToBow(15, 12, true);
            Item.damage = 105;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(gold: 10);
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-7, -4);
        }
        public override void HoldItem(Player player)
        {
            player.scope = true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI, -75);
            projectile.extraUpdates++;
            return false;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.ShroomiteBar, 20)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
