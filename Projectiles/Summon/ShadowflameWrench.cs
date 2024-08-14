using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Projectiles.Summon
{
    public class ShadowflameWrench : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.QuickDefaults(false, 40);
            Projectile.timeLeft = 60 * 5;
            Projectile.alpha = 255;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity = Projectile.CommonBounceLogic(oldVelocity);
            return false;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 8;
            height = 8;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(1f, 1f, 1f, 0.5f) * Projectile.Opacity;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.ShadowFlame, 60 * 3);
        }
        public override void AI()
        {
            Projectile.spriteDirection = Projectile.direction;
            //if(Projectile.timeLeft < 60 * 4)
            //    Projectile.velocity.Y += 0.1f;
            Projectile.velocity *= 0.985f;
            Projectile.rotation += Projectile.velocity.Length() * Projectile.direction * 0.1f;
            if(Projectile.alpha > 0)
            {
                Projectile.alpha -= 25;
            }

            if(Main.rand.NextBool(3))
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(20,20), DustID.Shadowflame);
                d.velocity += Projectile.velocity * 1.5f;
                d.alpha = 200;
                //d.color = new Color(1f, 1f, 1f, 0f);
            }
        }
    }
}
