using Terrafirma.Systems.MageClass;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Terrafirma.Spells.Accessory
{
    public class ManaTalon : Spell
    {
        public override int UseAnimation => 25;
        public override int UseTime => 25;
        public override int ManaCost => 2;
        public override int[] SpellItem => new int[] {ModContent.ItemType<Items.Equipment.Magic.ManaTalon>()};

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, Vector2.Normalize(velocity) * 8, ModContent.ProjectileType<Projectiles.Magic.ManaTalon>(), damage, knockback, player.whoAmI, 0, 0, 0);
            return false;
        }
    }
}
