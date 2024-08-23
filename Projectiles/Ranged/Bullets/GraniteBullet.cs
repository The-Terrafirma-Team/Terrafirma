using Microsoft.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terrafirma.Common;
using Terrafirma.Particles;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Projectiles.Ranged.Bullets
{
    public class GranitBulletPlayer : ModPlayer
    {
        public float speedBonus;
        public override void ResetEffects()
        {
            if (Player.PlayerStats().hasSwappedItems)
                speedBonus = 0;

            if(speedBonus > 1)
                speedBonus = 1;

            if(speedBonus > 0)
            speedBonus -= 0.002f;
        }
        public override float UseSpeedMultiplier(Item item)
        {
            if (speedBonus > 0f && item.DamageType == DamageClass.Ranged)
                return speedBonus * 0.5f + 1f;
            return base.UseSpeedMultiplier(item);
        }
    }
    public class GraniteBullet : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 9;
            ProjectileID.Sets.TrailingMode[Type] = 0;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            BulletVisuals.drawBullet(Projectile, new Color(183, 247, 255, 0), Color.Lerp(new Color(64, 64, 246, 128),new Color(0f,Main.masterColor,1f,0f), Main.player[Projectile.owner].GetModPlayer<GranitBulletPlayer>().speedBonus), Projectile.scale);
            return false;
        }
        public override void AI()
        {
            if (Projectile.ai[0] == 0)
            {
                Projectile.velocity = Projectile.velocity.RotatedByRandom(Main.player[Projectile.owner].GetModPlayer<GranitBulletPlayer>().speedBonus * 0.25f);
            }
            Projectile.ai[0]++;
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

            Projectile.aiStyle = -1;
            AIType = ProjectileID.Bullet;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Main.player[Projectile.owner].GetModPlayer<GranitBulletPlayer>().speedBonus += 0.05f;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            return false;
        }
        public override void OnKill(int timeLeft)
        {
            ParticleSystem.AddParticle(new BigSparkle() { Rotation = Main.rand.NextFloat(-0.3f, 0.3f), fadeInTime = 6, Scale = 1f, secondaryColor = new Color(0.5f, 1f, 1f, 0) * 0.25f}, Projectile.Center - Projectile.velocity, null, new Color(0f, 0.2f, 1f, 0) * 0.25f);
            ParticleSystem.AddParticle(new BigSparkle() {Rotation = Main.rand.NextFloat(-0.3f,0.3f), fadeInTime = 6, Scale = 1.5f, secondaryColor = new Color(0.5f, 1f, 1f, 0) * 0.5f }, Projectile.Center, null, new Color(0f, 0.2f, 1f, 0) * 0.5f);
            ParticleSystem.AddParticle(new BigSparkle() { Rotation = Main.rand.NextFloat(-0.3f, 0.3f), fadeInTime = 6, Scale = 2f, secondaryColor = new Color(0.5f, 1f, 1f, 0) }, Projectile.Center + Projectile.velocity, null, new Color(0f, 0.2f, 1f, 0));
            Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        }
    }
}
