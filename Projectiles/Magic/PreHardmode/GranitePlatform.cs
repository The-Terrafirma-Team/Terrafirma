using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using Terrafirma.Common;
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
        }
        public override void AI()
        {
            if (Projectile.ai[0] == 0)
            {
                SoundEngine.PlaySound(SoundID.Tink, Projectile.position);
            }
            if (Projectile.ai[0] > 5)
            {
                DoCollision = true;
            }
            Projectile.ai[0]++;

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
        }
    }
}
