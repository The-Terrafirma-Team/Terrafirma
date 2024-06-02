using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Particles;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Projectiles.Hostile
{
    public class TerragrimBouncyFlame : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(8);
            Projectile.hostile = true;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 60 * 7;
        }
        public override bool CanHitPlayer(Player target)
        {
            if (Projectile.ai[0] == 0)
                return false;
            return base.CanHitPlayer(target);
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 0.1f, 0.4f + MathF.Sin((float)Main.timeForVisualEffects * 0.01f) * 0.1f, 0.3f + MathF.Sin((float)Main.timeForVisualEffects * 0.03f) * 0.1f);
            Projectile.rotation += Math.Sign(Projectile.velocity.X) * 0.2f;
            if (Projectile.ai[0] != 0)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(16,16), DustID.Terragrim);
                d.velocity *= 0.5f;
                d.scale = 1.3f;
                d.noGravity = true;
                d.noLightEmittence = true;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            ParticleSystem.AddParticle(new BigSparkle() { Scale = 1f, fadeInTime = 10, }, Projectile.Center, null, new Color(128, 255, 170, 0));
            Projectile.ai[0] = 0.5f;
            Projectile.velocity = Projectile.CommonBounceLogic(oldVelocity);
            return false;
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item4, Projectile.position);
            ParticleSystem.AddParticle(new BigSparkle() { Scale = 1.3f, fadeInTime = 10, }, Projectile.Center, null, new Color(128, 255, 170, 0));
            for (int i = 0; i < 24; i++)
            {
                Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Terragrim);
                d.velocity = new Vector2(3).RotatedBy(i * MathHelper.TwoPi / 24);
                d.scale = 2f;
                d.noGravity = true;
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Asset<Texture2D> tex = TextureAssets.Projectile[Type];
            Main.EntitySpriteDraw(tex.Value, Projectile.Center - Main.screenPosition, null, new Color(255, 255, 255, 0) * (Projectile.ai[0] + 0.5f) * 0.2f, Projectile.rotation, tex.Size() / 2, 1.3f, SpriteEffects.None);
            Main.EntitySpriteDraw(tex.Value, Projectile.Center - Main.screenPosition, null, new Color(255, 255, 255, 0) * (Projectile.ai[0] + 0.5f), Projectile.rotation, tex.Size() / 2, 1f, SpriteEffects.None);
            return false;
        }
    }
}
