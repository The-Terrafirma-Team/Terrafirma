using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terrafirma.Particles;

namespace Terrafirma.Projectiles.Melee
{
    public class HeroSwordShot: ModProjectile
    {
        public override string Texture => "Terrafirma/Items/Weapons/Melee/Knight/HeroSword";

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Projectile.type] = 4;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
        }
        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            DrawOriginOffsetX = -9;
            DrawOriginOffsetY = -9;
            Projectile.friendly = true;
            Projectile.extraUpdates = 1;
            Projectile.DamageType = DamageClass.Melee;
        }
        public override void AI()
        {
            Projectile.ai[0]++;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver4;
            BigSparkle bigsparkle = new BigSparkle();
            bigsparkle.fadeInTime = 10;
            bigsparkle.Rotation = Main.rand.NextFloat(-MathHelper.PiOver2, MathHelper.PiOver2);
            bigsparkle.Scale = 0.6f * Projectile.scale;
            ParticleSystem.AddParticle(bigsparkle, Projectile.Center + Main.rand.NextVector2Circular(10, 10), Vector2.Zero, new Color(Main.DiscoColor.R, Main.DiscoColor.G, Main.DiscoColor.B, 0) * (Projectile.scale / 3f));
            //LegacyParticleSystem.AddParticle(new BigSparkle(), Projectile.Center + Main.rand.NextVector2Circular(10, 10), Vector2.Zero, new Color(Main.DiscoColor.R, Main.DiscoColor.G, Main.DiscoColor.B, 0) * (Projectile.scale / 3f), 0, 10, 1, 0.6f * Projectile.scale, Main.rand.NextFloat(-MathHelper.PiOver2, MathHelper.PiOver2));
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            BigSparkle bigsparkle = new BigSparkle();
            bigsparkle.fadeInTime = 10;
            bigsparkle.Rotation = Main.rand.NextFloat(-MathHelper.PiOver2, MathHelper.PiOver2);
            bigsparkle.Scale = 1f;
            ParticleSystem.AddParticle(bigsparkle, Projectile.Center, Vector2.Zero, new Color(Main.DiscoColor.R, Main.DiscoColor.G, Main.DiscoColor.B, 0) * (Projectile.scale / 3f));
            //LegacyParticleSystem.AddParticle(new BigSparkle(), Projectile.Center, Vector2.Zero, new Color(Main.DiscoColor.R, Main.DiscoColor.G, Main.DiscoColor.B, 0), 0, 10, 1, 1f, Main.rand.NextFloat(-MathHelper.PiOver2, MathHelper.PiOver2));
            base.OnHitNPC(target, hit, damageDone);
        }

        public override void OnKill(int timeLeft)
        {
            BigSparkle bigsparkle = new BigSparkle();
            bigsparkle.fadeInTime = 10;
            bigsparkle.Rotation = Main.rand.NextFloat(-MathHelper.PiOver2, MathHelper.PiOver2);
            bigsparkle.Scale = 2f;
            ParticleSystem.AddParticle(bigsparkle, Projectile.Center, Vector2.Zero, new Color(Main.DiscoColor.R, Main.DiscoColor.G, Main.DiscoColor.B, 0) * (Projectile.scale / 3f));
            //LegacyParticleSystem.AddParticle(new BigSparkle(), Projectile.Center, Vector2.Zero, new Color(Main.DiscoColor.R, Main.DiscoColor.G, Main.DiscoColor.B, 0), 0, 10, 1, 2f, Main.rand.NextFloat(-MathHelper.PiOver2, MathHelper.PiOver2));
        }
    }
}
