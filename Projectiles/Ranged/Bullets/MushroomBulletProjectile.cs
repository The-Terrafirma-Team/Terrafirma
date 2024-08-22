using Microsoft.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using Terrafirma.Common;
using Terrafirma.Particles;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Projectiles.Ranged.Bullets
{
    public class MushroomBulletProjectile : ModProjectile
    {
        Vector2 offset = Vector2.Zero;
        NPC targetnpc = null;
        float stickrot = 0f;
        float startnpcrot = 0f;
        public override string Texture => "Terrafirma/Projectiles/Ranged/Bullets/MushroomBulletMushrooms";
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 4;
            ProjectileID.Sets.TrailingMode[Type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;

            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;

            Projectile.timeLeft = 600;

            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 1;
            Projectile.penetrate = -1;

            Projectile.aiStyle = -1;
            AIType = ProjectileID.Bullet;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }
        public override void AI()
        {
            if (Projectile.ai[1] == 1)
            {
                Projectile.Center = targetnpc.Center + offset.RotatedBy(targetnpc.rotation - startnpcrot);
                Projectile.rotation = stickrot + (float)(Math.Sin(Main.timeForVisualEffects / 100f) / 2f) + (targetnpc.rotation - startnpcrot);
                Projectile.scale = MathHelper.Lerp(Projectile.scale, 1f, 0.02f);

                Lighting.AddLight(Projectile.Center, (new Vector3(60, 70, 200) / 128f) * Projectile.scale);
                Projectile.spriteDirection = offset.X > 0 ? 1 : -1;
                if (Projectile.ai[0] % 120 == 0) Dust.NewDust(Projectile.Center - Projectile.velocity, 0, 0, DustID.GlowingMushroom);
            }
            else
            {
                Lighting.AddLight(Projectile.Center, new Vector3(60, 70, 200) / 256f);
            }

            if (Projectile.timeLeft < 100)
            {
                Projectile.Opacity -= 0.01f;
                Projectile.scale -= 0.01f;
            }

            if (targetnpc != null && !targetnpc.active) Projectile.Kill();

            Projectile.ai[0]++;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.ai[1] == 0)
            {
                Projectile.ai[1] = 1;
                offset = Projectile.Center - Projectile.velocity - target.Center;
                targetnpc = target;
                stickrot = target.Center.DirectionTo(Projectile.Center).ToRotation() + MathHelper.PiOver2;
                startnpcrot = target.rotation;
                Projectile.tileCollide = false;
                Projectile.frame = Main.rand.Next(0, 3);
                Projectile.scale = 0f;
            }

        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            return false;
        }

        public override bool? CanHitNPC(NPC target)
        {
            if (Projectile.ai[1] == 1) return false;
            return base.CanHitNPC(target);
        }
        public override void OnKill(int timeLeft)
        {
            Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            if (Projectile.ai[1] == 0) BulletVisuals.drawBullet(Projectile, new Color(180, 170, 130, 0), new Color(60, 70, 200, 128));
            else
            {
                Asset<Texture2D> tex = TextureAssets.Projectile[Type];

                Main.EntitySpriteDraw(tex.Value,
                Projectile.Center - Main.screenPosition,
                new Rectangle(0, tex.Width() * Projectile.frame, tex.Width(), tex.Width()),
                lightColor * Projectile.Opacity,
                Projectile.rotation,
                new Vector2(tex.Width() /2),
                Projectile.scale,
                Projectile.spriteDirection == 1? SpriteEffects.None : SpriteEffects.FlipHorizontally
                );
            }

            return false;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (targetHitbox.Width > 8 && targetHitbox.Height > 8)
            {
                targetHitbox.Inflate(-targetHitbox.Width / 12, -targetHitbox.Height / 12);
            }
            return projHitbox.Intersects(targetHitbox);
        }
    }
}
