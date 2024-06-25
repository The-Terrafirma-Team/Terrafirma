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
using Terrafirma.Buffs.Debuffs;
using Terrafirma.Systems.Primitives;
using Microsoft.Xna.Framework.Graphics;

namespace Terrafirma.Projectiles.Ranged
{
    internal class InkProjectile : ModProjectile
    {
        Trail trail;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            trail = new Trail(Projectile.oldPos, TrailWidth.WobblyWidth, 30f);

            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            DrawOffsetX = -2;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<Inked>(), 180, false);
        }

        public override void AI()
        {
            //Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            //Projectile.spriteDirection = Projectile.direction;

            //if (Projectile.ai[0] == 0)
            //{
            //    for (int i = 0; i < 8; i++)
            //    {
            //        Dust d = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.Poop, Projectile.velocity.X * Main.rand.NextFloat(0.2f, 0.3f), Projectile.velocity.Y * Main.rand.NextFloat(0.2f, 0.3f), 0, Color.Black, Main.rand.NextFloat(0.8f, 1f));
            //    }
            //}

            //Projectile.ai[0]++;


            //if (Projectile.ai[0] % 2 == 0)
            //{
            //    Dust newdust = Dust.NewDustPerfect(Projectile.Center + Vector2.Normalize(Projectile.velocity) * 5f, DustID.Poop, Vector2.Zero, 0, Color.Black, Main.rand.NextFloat(1f, 1.2f));
            //    newdust.noGravity = true;
            //}
            //if (Projectile.ai[0] % 4 == 0)
            //{
            //    Dust d = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.Poop, Projectile.velocity.X * Main.rand.NextFloat(0.2f, 0.3f), 0, 0, Color.Black, Main.rand.NextFloat(0.8f, 1f));
            //}

            //if (Projectile.ai[0] > 20)
            //{
            //    Projectile.velocity.Y += 0.3f;
            //}

            Projectile.position = Main.MouseWorld;

        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(Projectile.Center, 2, 2, DustID.Poop, Projectile.velocity.X * Main.rand.NextFloat(0.2f, 0.3f), Projectile.velocity.Y * Main.rand.NextFloat(0.2f, 0.3f), 0, Color.Black, Main.rand.NextFloat(0.8f, 1f));
            }
            SoundEngine.PlaySound(SoundID.SplashWeak, Projectile.position);
        }

        public override void PostDraw(Color lightColor)
        {
            trail.Draw(Projectile.position);
            base.PostDraw(lightColor);
        }
    }
}
