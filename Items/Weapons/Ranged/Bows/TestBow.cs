using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Biomes.CaveHouse;
using Terraria.ModLoader;

namespace Terrafirma.Items.Weapons.Ranged.Bows
{
    public class TestBow : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToDrawnBow(ModContent.ProjectileType<Projectiles.Ranged.Bows.TestBow>(), 30, 80,5,10);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity, Item.shoot, damage, knockback,player.whoAmI,0f,source.AmmoItemIdUsed);
            return false;
        }
    }
}
