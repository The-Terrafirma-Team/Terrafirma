using Microsoft.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terrafirma.Common;
using Terrafirma.Data;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Projectiles.Ranged.Bullets
{
    internal class BuckshotProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 8;
            ProjectileID.Sets.TrailingMode[Type] = 0;
            ProjectileSets.CanBeReflected[Type] = true;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            BulletVisuals.drawBullet(Projectile, new Color(1f, 1f, 0.5f, 0f), new Color(1f, 0.1f, 0f, 0.5f), Projectile.scale * 0.7f);
            return false;
        }
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;

            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;

            Projectile.timeLeft = 400;

            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 1;

            Projectile.aiStyle = 1;
            AIType = ProjectileID.Bullet;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.ai[2] = 0;
        }
        public override void AI()
        {
            if (Projectile.ai[2] == 0 && Main.myPlayer == Projectile.owner)
            {
                for (int i = 0; i < 3; i++)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, Projectile.velocity.RotatedBy(Main.rand.NextFloat(-10, 10) * (Math.PI / 180)) * Main.rand.NextFloat(0.9f, 1.1f), Projectile.type, Projectile.damage / 2, Projectile.knockBack, Projectile.owner, 0, 0, 1);

                }
                Projectile.Kill();
            }
            else
            {
                Lighting.AddLight(Projectile.position, new Vector3(0.4f, 0.4f, 0));
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            return base.OnTileCollide(oldVelocity);
        }
    }
}
