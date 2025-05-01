using Microsoft.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terrafirma.Common;
using Terrafirma.Data;
using Terrafirma.Particles;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Projectiles.Ranged.Bullets
{
    public class ChainshotProjectile : ModProjectile
    {
        Texture2D tex;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 9;
            ProjectileID.Sets.TrailingMode[Type] = 0;
            ProjectileSets.CanBeReflected[Type] = true;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = new Color(lightColor.R, lightColor.G, lightColor.B, 255);
            float rot = Projectile.velocity.Length() / 20f;
            for (int i = 0; i < 5; i++)
            {
                Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition, new Rectangle(2, 2, 20, 20), lightColor * (0.4f - i * 0.08f), Projectile.rotation - (i * rot * Projectile.direction), new Vector2(10 - (Projectile.width / 1.5f), 10), Projectile.scale, SpriteEffects.None);
            }
            for (int i = 0; i < 5; i++)
            {
                Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition, new Rectangle(2, 2, 20, 20), lightColor * (0.4f - i * 0.08f), Projectile.rotation - (i * rot * Projectile.direction), new Vector2(10 + (Projectile.width / 1.5f), 10), Projectile.scale, SpriteEffects.None);
            }

            for (int i = 0; i < (Projectile.width * 1.2f) / 16; i++)
            {
                Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition, new Rectangle(24, 2, 8, 6), lightColor, Projectile.rotation, new Vector2(0 - (i * 8), 3), Projectile.scale, SpriteEffects.None);
            }
            for (int i = 0; i < (Projectile.width * 1.2f) / 16; i++)
            {
                Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition, new Rectangle(24, 2, 8, 6), lightColor, Projectile.rotation, new Vector2(8 + (i * 8), 3), Projectile.scale, SpriteEffects.None);
            }

            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition, new Rectangle(2, 2, 20, 20), lightColor, Projectile.rotation, new Vector2(10 - (Projectile.width / 1.5f), 10), Projectile.scale, SpriteEffects.None);
            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition, new Rectangle(2, 2, 20, 20), lightColor, Projectile.rotation, new Vector2(10 + (Projectile.width / 1.5f), 10), Projectile.scale, SpriteEffects.None);

            return false;
        }
        public override void AI()
        {
            if (Projectile.width <= 80 && Projectile.timeLeft % 1 == 0)
            {
                Projectile.width += 1;
                Projectile.height += 1;
                Projectile.position -= new Vector2(0.5f);
            }
            Projectile.direction = Projectile.velocity.X > 0 ? 1 : -1;
            Projectile.rotation += (Projectile.velocity.Length() * 0.02f) * Projectile.direction;
            Projectile.velocity = (Projectile.velocity * 0.995f).LengthClamp(25f, 2f);           

        }
        public override void SetDefaults()
        {
            tex = ModContent.Request<Texture2D>("Terrafirma/Projectiles/Ranged/Bullets/ChainshotProjectile").Value;

            Projectile.width = 14;
            Projectile.height = 14;

            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 5;

            Projectile.timeLeft = 300;

            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 1;

            Projectile.aiStyle = -1;
            AIType = ProjectileID.Bullet;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
            return false;
        }
        public override void OnKill(int timeLeft)
        {      
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            for(int i = -(Projectile.width / 8); i < Projectile.width / 8; i++)
            {
                Dust.NewDustPerfect(Projectile.Center + new Vector2(i * 8f, 0).RotatedBy(Projectile.rotation), DustID.Stone);
            }
        }
    }
}
