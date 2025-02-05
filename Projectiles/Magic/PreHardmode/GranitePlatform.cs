using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using Terrafirma.Common;
using Terrafirma.Particles;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

namespace Terrafirma.Projectiles.Magic.PreHardmode
{
    public class GranitePlatform : SolidProjectile
    {
        public override SolidProjectileCollisionType CollisionType => SolidProjectileCollisionType.SemiSolid;

        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.timeLeft = 3600 * 5;
            Projectile.width = 46;
            Projectile.height = 22;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            DoCollision = false;
            Projectile.alpha = 250;
        }
        public override void AI()
        {
            if (Projectile.ai[0] == 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    float opacity = Main.rand.NextFloat(0.5f);
                    ParticleSystem.AddParticle(new Smoke() { Scale = 1f, secondaryColor = new Color(25, 23, 54) * opacity }, Main.rand.NextVector2FromRectangle(Projectile.Hitbox), Main.rand.NextVector2Circular(2, 2), new Color(116, 132, 169) * opacity, ParticleLayer.Normal);
                }
                SoundEngine.PlaySound(SoundID.Tink, Projectile.position);
            }
            if (Projectile.ai[0] > 5)
            {
                DoCollision = true;
            }
            Projectile.ai[0]++;

            if(Projectile.alpha > 0)
            {
                Projectile.alpha -= 10;
            }

            Projectile.velocity.Y *= 0.9f;
            if(!Collision.SolidCollision(Projectile.position,Projectile.width,16 * 20))
            {
                Projectile.velocity.Y += 0.1f;
                if (!Collision.SolidCollision(Projectile.position, Projectile.width, 16 * 30))
                {
                    Projectile.velocity.Y += 0.4f;
                }
            }
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.NPCDeath43, Projectile.position);
            for(int i = 0; i < 60; i++)
            {
                Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Granite);
                d.noGravity = Main.rand.NextBool();
            }
            for(int i = 0; i < 4; i++)
            {
                float opacity = Main.rand.NextFloat(0.5f);
                ParticleSystem.AddParticle(new Smoke() { Scale = 1f, secondaryColor = new Color(25, 23, 54) * opacity }, Main.rand.NextVector2FromRectangle(Projectile.Hitbox), Main.rand.NextVector2Circular(2,2), new Color(116, 132, 169) * opacity, ParticleLayer.Normal);
            }
        }
    }
}
