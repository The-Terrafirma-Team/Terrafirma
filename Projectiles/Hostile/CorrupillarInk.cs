using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Projectiles.Hostile
{
    public class CorrupillarInk : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.QuickDefaults(true, 16);
            Projectile.hide = true;
            Projectile.extraUpdates = 1;
        }
        public override void AI()
        {
            if (Projectile.ai[0] == 0)
            {
                SoundEngine.PlaySound(SoundID.Item17, Projectile.position);
            }

            Projectile.ai[0]++;
            Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Wraith, Projectile.velocity * 0.2f);
            d.noGravity = true;
            d.scale *= 1.5f;
            if (Main.rand.NextBool(5))
            {
                Dust d2 = Dust.NewDustPerfect(Projectile.Center, DustID.Corruption);
                d2.velocity += Projectile.velocity;
                d2.alpha = 128;
                d2.velocity *= 0.3f;
            }

            if (Projectile.ai[0] > 60 * 3)
            {
                Projectile.velocity.Y += 0.1f;
            }
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.NPCDeath1, Projectile.position);
            for (int i = 0; i < 15; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Corruption, Projectile.velocity.RotateRandom(1.5f) * -Main.rand.NextFloat(0.5f));
                d.noGravity = !Main.rand.NextBool(3);
                d.alpha = 128;
            }
            for (int i = 0; i < 35; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Wraith, Projectile.velocity.RotateRandom(1.5f) * -Main.rand.NextFloat(1.3f));
                d.noGravity = !Main.rand.NextBool(3);
            }
            for(int i = 0; i < 3; i++)
            {
                Gore g = Gore.NewGorePerfect(Projectile.GetSource_FromThis(), Projectile.Center - new Vector2(16), Main.rand.NextVector2Circular(2, 2), 99);
                g.rotation = Main.rand.NextFloat(MathHelper.TwoPi);
                g.velocity -= Projectile.velocity * 0.2f;
            }
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.Blackout, 60 * 10);
            if (Main.rand.NextBool(5))
            {
                target.AddBuff(BuffID.Darkness, 60 * 20);
            }
            Projectile.Kill();
        }
    }
}
