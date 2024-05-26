using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Projectiles.Melee;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Weapons.Melee.Swords
{
    public class SplinterSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToSword(16, 25, 4);
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.rand.NextBool(5))
            {
                for (int i = 0; i < 3; i++)
                {
                    Projectile.NewProjectile(player.GetSource_ItemUse(Item), player.Center, player.Center.DirectionTo(target.Center + new Vector2(0, -40)).RotatedByRandom(0.1f) * Main.rand.NextFloat(3, 5), ModContent.ProjectileType<Splinter>(), (int)(Item.damage * 0.45f), 0, player.whoAmI, target.whoAmI);
                }

                for (int i = 0; i < 15; i++)
                {
                    Dust d = Dust.NewDustPerfect(target.Hitbox.ClosestPointInRect(player.MountedCenter), DustID.RichMahogany, Main.rand.NextVector2Circular(5, 5));
                    d.alpha = 128;
                    d.scale = Main.rand.NextFloat(1f, 3f);
                    d.noGravity = true;
                }
            }
        }
    }
}
