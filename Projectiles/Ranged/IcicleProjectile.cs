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

namespace TerrafirmaRedux.Projectiles.Ranged
{
    internal class IcicleProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.damage = 8;
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 600;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            DrawOffsetX = -5;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.rand.NextBool(10))
            {
                target.AddBuff(BuffID.Frostburn, 180, false);
            }
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Projectile.spriteDirection = Projectile.direction;

            if (Projectile.ai[0] == 0)
            {
                for (int i = 0; i < 8; i++)
                {
                    Dust d = Dust.NewDustDirect(Projectile.Center, 0, 0, Main.rand.NextBool(3) ? DustID.Ice : DustID.Snow, Projectile.velocity.X * Main.rand.NextFloat(0.2f, 0.3f), Projectile.velocity.Y * Main.rand.NextFloat(0.2f, 0.3f), 0, default, Main.rand.NextFloat(0.8f, 1f));
                    d.noGravity = !Main.rand.NextBool(5) || (d.type == DustID.Snow);
                }
            }

            Projectile.ai[0]++;

            if (Projectile.ai[0] > 20)
            {
                Projectile.velocity.Y += 0.3f;
            }

        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(Projectile.Center, 2, 2, DustID.Ice, Projectile.velocity.X * Main.rand.NextFloat(0.2f, 0.3f), Projectile.velocity.Y * Main.rand.NextFloat(0.2f, 0.3f), 0, default, Main.rand.NextFloat(0.8f, 1f));
            }
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item50, Projectile.position);
        }
    }
}
