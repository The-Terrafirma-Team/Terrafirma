using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terrafirma.Particles;

namespace Terrafirma.Projectiles.Summon.Sentry.PreHardmode
{
    internal class GraniteTurretShot : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.timeLeft = 200;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.hide = true;
            Projectile.extraUpdates = Projectile.timeLeft;
        }
        public override void AI()
        {
            if(Projectile.timeLeft < 197)
                ParticleSystem.AddParticle(new ChlorophyteStyleLaserSegment() { Scale = 0.2f, Rotation = Projectile.velocity.ToRotation()},Projectile.Center - Projectile.velocity * 2, Vector2.Normalize(Projectile.velocity) * 2, Color.Lerp(new Color(0.3f, 0.9f, 1f, 0f), new Color(0.3f, 0.6f, 1f, 0f),(float)Math.Sin((Projectile.timeLeft + Main.timeForVisualEffects) * 0.4f)) * 0.6f);
            if (Main.rand.NextBool(5))
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center,206,Projectile.velocity.RotatedByRandom(0.3f) * 0.2f);
                d.scale = 2f;
                d.noGravity = true;
            }
        }
        public override void OnKill(int timeLeft)
        {
            for(int i = 0; i < 10; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, 206, Projectile.velocity.RotatedByRandom(0.9f) * Main.rand.NextFloat(-1f,0f));
                d.scale = 2f;
                d.noGravity = true;
            }
        }
    }
}
