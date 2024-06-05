using Microsoft.Xna.Framework;
using System;
using Terrafirma.Common;
using Terraria;
using Terraria.ID;

namespace Terrafirma.Projectiles.Misc
{
    public class CloudInABottleProj : SolidProjectile
    {
        public override SolidProjectileCollisionType CollisionType => SolidProjectileCollisionType.SemiSolid;

        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.timeLeft = 100000;
            Projectile.width = 50;
            Projectile.height = 18;
            Projectile.penetrate = -1;
            Projectile.damage = 0;
            Projectile.tileCollide = false;
            Projectile.velocity.Y = 1;
            DoCollision = false;
        }

        public override void AI()
        {
            if (Projectile.ai[0] > 5)
            {
                DoCollision = true;
                Projectile.velocity.X *= 0.95f;
                Projectile.velocity.Y = MathHelper.Lerp(Projectile.velocity.Y, (float)Math.Sin(Projectile.ai[0] / 10) * 0.5f, 0.1f);
            }
            if (Projectile.ai[0] % 40 == 0) Dust.NewDust(Projectile.Left, Projectile.width, Projectile.height / 2, DustID.Cloud, 0, 1f);
            Projectile.ai[0]++;
            
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 10; i++) Dust.NewDust(Projectile.Center, Projectile.width / 2, Projectile.height / 2, DustID.Cloud);
            base.OnKill(timeLeft);
        }
    }
}
