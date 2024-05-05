using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terrafirma.Spells.Tempire;

namespace Terrafirma.Items.Weapons.Magic.Tempire
{
    public class GlimmerWand : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.staff[Item.type] = true;
        }
        public override void SetDefaults()
        {
            Item.DefaultToMagicWeapon(ModContent.ProjectileType<GlitterBolt>(), 10, 8, true);
            Item.mana = 3;
            Item.damage = 65;
            Item.value = Item.sellPrice(0, 0, 20, 0);
            Item.UseSound = SoundID.Item8;
            Item.rare = ItemRarityID.Lime;
            Item.scale = 0.9f;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position += velocity;
            base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0, 1, 0);
            return false;
        }
    }
}
