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
    public class HallowPointBulletProjectile : ModProjectile
    {
        bool lodged = false;
        public override bool PreDraw(ref Color lightColor)
        {
            if (!lodged) BulletVisuals.drawBullet(Projectile, new Color(255, 255, 255, 0), new Color(150, 130, 10, 255), Projectile.scale);
            return false;
        }

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 8;
            ProjectileID.Sets.TrailingMode[Type] = 0;
        }
        public override void AI()
        {
            if (lodged)
            {
                Projectile.Center = Main.npc[(int)Projectile.ai[1]].Center;
                Projectile.ai[2]++;
                if (Projectile.ai[2] % 120 == 0)
                {
                    Main.npc[(int)Projectile.ai[1]].SimpleStrikeNPC(Projectile.damage, 0, false, 0, DamageClass.Ranged, true);
                    //for (int i = 0; i < 4; i++)
                    //{
                    //    Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.HallowedWeapons, Main.rand.NextVector2Circular(2f, 2f), 1, Scale: 1.2f);
                    //}
                    ParticleSystem.AddParticle(new BigSparkle() { Rotation = Main.rand.NextFloat(-0.3f, 0.3f), fadeInTime = 6, Scale = 1f },Main.rand.NextVector2FromRectangle(Main.npc[(int)Projectile.ai[1]].Hitbox),null, new Color(1f,1f,0f,0f));
                }

                if (!Main.npc[(int)Projectile.ai[1]].active) Projectile.Kill();
            }
            else
            {
                if (Main.rand.NextBool())
                {
                    Dust d2 = Dust.NewDustPerfect(Projectile.Center, DustID.HallowedWeapons, Main.rand.NextVector2Circular(2f, 2f), 1);
                    d2.noGravity = true;
                    d2.velocity *= 0.1f;
                }
            }
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
            Projectile.penetrate = 10;
            Projectile.light = 0.4f;

            Projectile.aiStyle = -1;
            AIType = ProjectileID.Bullet;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }

        private readonly Point[] stickingProjectiles = new Point[5];
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            lodged = true;
            Projectile.timeLeft = 360;
            Projectile.velocity = Vector2.Zero;
            Projectile.ai[0] = 1;
            Projectile.ai[1] = target.whoAmI;
            Projectile.netUpdate = true;
            Projectile.KillOldestJavelin(Projectile.whoAmI, Type, target.whoAmI, stickingProjectiles);
        }

        public override bool? CanHitNPC(NPC target)
        {
            return base.CanHitNPC(target);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            return false;
        }
    }
}
