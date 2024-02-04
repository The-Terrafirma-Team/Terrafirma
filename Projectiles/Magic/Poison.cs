using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace Terrafirma.Projectiles.Magic
{
    public class Poison : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.Size = new Vector2(8);
            Projectile.aiStyle = -1;
            Projectile.hide = true;
            Projectile.timeLeft = 60;
        }
        public override void AI()
        {
            Projectile.ai[0]++;
            //Projectile.velocity.Y += 0.1f;
            Projectile.velocity = Projectile.velocity.RotatedBy(Math.Sin(Projectile.ai[0] * 0.3f - MathHelper.PiOver2) * 0.07f);
            if (Projectile.ai[0] > 2)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Poisoned, Projectile.velocity * 0.1f, 64);
                d.scale = 1;
                d.frame.Y = Projectile.timeLeft % 2 == 0 ? 10 : 0;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Poisoned, 60 * 4);
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.Poisoned, 60 * 4);
        }
    }
}
