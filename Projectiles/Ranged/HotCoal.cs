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
using Terrafirma.Systems.Primitives;
using ReLogic.Content;
using Terrafirma.Data;

namespace Terrafirma.Projectiles.Ranged
{
    internal class HotCoal : ModProjectile
    { 

        Vector2 randpos = Vector2.Zero;
        Trail trail;
        private static Asset<Texture2D> trailTex;
        public override void Load()
        {
            trailTex = ModContent.Request<Texture2D>("Terrafirma/Assets/Particles/FireTrail");
        }
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Projectile.type] = 4;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;
            ProjectileSets.CanBeReflected[Type] = true;
        }
        public override void SetDefaults()
        {
            trail = new Trail(Projectile.oldPos, TrailWidth.FlatWidth, 40);
            trail.trailtexture = ModContent.Request<Texture2D>("Terrafirma/Assets/Particles/FireTrail").Value;
            trail.color = f => new Color(0.4f, 0.4f, 0.4f, 0f) * 0.2f;

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

            if (Main.rand.NextBool(4))
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(6,6), DustID.Torch, Projectile.velocity * 0.2f, 0, default, Main.rand.NextFloat(1.5f, 2f));
                d.noGravity = !Main.rand.NextBool(2);
                if (!d.noGravity)
                    d.scale *= 1.2f;
                d.customData = 1;
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
                Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, Projectile.velocity.X * -Main.rand.NextFloat(0.2f, 0.3f), Projectile.velocity.Y * -Main.rand.NextFloat(0.2f, 0.3f), 0, default, Main.rand.NextFloat(1.5f, 2f));
                //d.noGravity = !Main.rand.NextBool(6);
                d.customData = 1;
            }
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item50, Projectile.position);

        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>("Terrafirma/Projectiles/Ranged/HotCoal").Value;
            Texture2D textureglow = ModContent.Request<Texture2D>("Terrafirma/Projectiles/Ranged/HotCoalGlow").Value;

            trail.Draw(Projectile.Right);

            Main.EntitySpriteDraw(texture,
                Projectile.Center - Main.screenPosition,
                texture.Frame(),
                Color.White,
                Projectile.rotation,
                texture.Size() / 2,
                Projectile.scale,
                SpriteEffects.None);


            for (int i = 0; i < 3; i++)
            {
                if (Projectile.ai[0] % 3 == 0)
                {
                    randpos = Main.rand.NextVector2Circular(1, 1);
                }

                Main.EntitySpriteDraw(textureglow,
                Projectile.oldPos[i] + Projectile.Size/2 - Main.screenPosition + randpos ,
                texture.Frame(),
                new Color(1f, 0.5f, 0f, 0f) * (0.8f + 0.3f * ((float)Math.Sin((float)Main.timeForVisualEffects / 10f + Projectile.ai[1]) + 1f)) * (1- (i * 0.3f)),
                Projectile.rotation,
                texture.Size() / 2,
                (Projectile.scale * 1.1f) * (1 - (i * 0.2f)) + 0.4f * (float)Math.Sin((float)Main.timeForVisualEffects / 10f + Projectile.ai[1]),
                SpriteEffects.None);
            }

            return false;
        }
    }
}
