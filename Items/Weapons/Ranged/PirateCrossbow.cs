
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Items.Weapons.Ranged
{
    internal class PirateCrossbow : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToBow(30, 12, true);
            Item.damage = 85;
            Item.knockBack = 2f;
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.sellPrice(0,1,0,0);

        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-4, -2);
        }


        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile projectile = Projectile.NewProjectileDirect(source, position, velocity * 0.7f, type, damage, knockback, player.whoAmI, -50);
            projectile.extraUpdates++;
            return false;
        }
    }
}
