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
using Microsoft.Xna.Framework.Graphics;

namespace Terrafirma.Projectiles.Ranged
{
    internal class HotCoal : ModProjectile
    {
        Vector2 randpos = Vector2.Zero;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Projectile.type] = 4;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8;
        }
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.timeLeft = 100;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;

            DrawOffsetX = -2;
            DrawOriginOffsetY = -8;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
           target.AddBuff(BuffID.OnFire, 180, false);
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Projectile.spriteDirection = Projectile.direction;

            if (Projectile.ai[0] == 0)
            {
                Projectile.ai[1] = Main.rand.Next(0, 4) * 10;
            }

            if (Projectile.ai[0] % 3 == 0)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center + Projectile.velocity * Main.rand.Next(4), DustID.Torch, Projectile.velocity * 0.2f, 0, default, Main.rand.NextFloat(1.5f, 2f));
                d.noGravity = true;
            }

            Projectile.ai[0]++;

            if (Projectile.ai[0] > 20)
            {
                Projectile.velocity.Y += 0.5f;
            }

        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(Projectile.Center, 2, 2, DustID.Torch, Projectile.velocity.X * Main.rand.NextFloat(0.2f, 0.3f), Projectile.velocity.Y * Main.rand.NextFloat(0.2f, 0.3f), 0, default, Main.rand.NextFloat(1.5f, 2f));
            }
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item50, Projectile.position);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>("Terrafirma/Projectiles/Ranged/HotCoal").Value;
            Texture2D textureglow = ModContent.Request<Texture2D>("Terrafirma/Projectiles/Ranged/HotCoalGlow").Value;

            Main.EntitySpriteDraw(texture,
                Projectile.Center - Main.screenPosition,
                texture.Frame(),
                Color.White,
                Projectile.rotation,
                texture.Size() / 2,
                1f,
                SpriteEffects.None);


            for (int i = 0; i < 3; i++)
            {
                if (Projectile.ai[0] % 3 == 0)
                {
                    randpos = Main.rand.NextVector2Circular(3, 3);
                }

                Main.EntitySpriteDraw(textureglow,
                Projectile.oldPos[i * Main.rand.Next(2)] + Projectile.Size/2 - Main.screenPosition + randpos ,
                texture.Frame(),
                new Color(1f, 1f, 1f, 0f) * (0.8f + 0.3f * ((float)Math.Sin((float)Main.timeForVisualEffects / 10f + Projectile.ai[1]) + 1f)) * (1- (i * 0.3f)),
                Projectile.rotation,
                texture.Size() / 2,
                1.1f * (1 - (i * 0.2f)) + 0.4f * (float)Math.Sin((float)Main.timeForVisualEffects / 10f + Projectile.ai[1]),
                SpriteEffects.None);
            }

            return false;
        }
    }
}
