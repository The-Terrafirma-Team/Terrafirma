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
    public class LapisBulletProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 9;
            ProjectileID.Sets.TrailingMode[Type] = 0;
            ProjectileSets.CanBeReflected[Type] = true;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            BulletVisuals.drawBullet(Projectile, new Color(255, 255, 0, 128), new Color(50, 50, 255, 128), Projectile.scale);
            return false;
        }
        public override void AI()
        {
            if (Main.player[Projectile.owner] == Main.LocalPlayer)
            {
                Projectile.ai[1] = Main.MouseWorld.X;
                Projectile.ai[2] = Main.MouseWorld.Y;
            }
            Vector2 mousePos = new Vector2(Projectile.ai[1], Projectile.ai[2]);

            if (Projectile.Center.Distance(mousePos) <= 400 && Projectile.ai[0] < 40 && Projectile.timeLeft <= 580)
            {
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, Projectile.velocity + Projectile.Center.DirectionTo(mousePos) * 12f, 0.06f);
                Projectile.ai[0]++;
            }
            if (Projectile.velocity.Length() < 12f) Projectile.velocity *= 1.05f;
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
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            return false;
        }
        public override void OnKill(int timeLeft)
        {
            Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        }
    }
}
