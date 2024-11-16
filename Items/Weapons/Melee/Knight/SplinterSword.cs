using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Projectiles.Melee.Knight;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Weapons.Melee.Knight
{
    public class SplinterSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToSword(16, 25, 4);
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Melee.Knight.SplinterSword>();
        }
        public override bool MeleePrefix()
        {
            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source,position,velocity,type,damage,knockback,player.whoAmI,player.PlayerStats().TimesHeldWeaponHasBeenSwung % 4);
            return false;
        }
    }
}
