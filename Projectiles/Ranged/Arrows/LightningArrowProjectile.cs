using Microsoft.Xna.Framework;
using System;
using Terrafirma.Buffs.Debuffs;
using Terrafirma.Data;
using Terrafirma.Systems.Primitives;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Projectiles.Ranged.Arrows
{
    internal class LightningArrowProjectile : ModProjectile
    {
        TFAdvancedTrail trail;
        Vector2[] points;
        float trailOpacity;
        Vector2 startPoint;
        public override void SetStaticDefaults()
        {
            ProjectileSets.CanBeReflected[Type] = true;
            ProjectileID.Sets.TrailCacheLength[Type] = 20;
            ProjectileID.Sets.TrailingMode[Type] = 0;
        }
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            DrawOffsetX = -Projectile.width / 2 - 5;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.friendly = true;
            Projectile.arrow = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;

            trail = new TFAdvancedTrail();
            trail.pixellate = true;
            trail.texture = TextureAssets.MagicPixel.Value;
            trail.width = (f) => (1f - f) * 10f;
            trail.color = (f) => Color.Lerp(Color.White, Color.Yellow, f * f) * trailOpacity;
            trail.textureOffset = (f) => Vector2.Zero;
            trail.trailOffset = (f, r) =>
            {
                float zigZag = (float)Math.Sin(f * 30f - Projectile.ai[1] / 10f);
                zigZag = Projectile.whoAmI % 2 == 0 ? zigZag * -1 : zigZag * 1;
                return new Vector2(0f, zigZag * 10f).RotatedBy(r);
            };
        }

        public override bool PreDraw(ref Color lightColor)
        {
            trail.QueueDraw(Projectile.oldPos);
            return base.PreDraw(ref lightColor);
        }

        public override void AI()
        {

            if (Projectile.ai[0] < 30)
            {
                Projectile.extraUpdates = 5;
                Projectile.penetrate = -1;
                startPoint = Main.player[Projectile.owner].MountedCenter;             
                Lighting.AddLight(Projectile.Center, new Vector3(1f, 1f, 0.5f));

                trailOpacity = 1f;
                points = new Vector2[] { startPoint, Projectile.Center };
            }
            else if (Projectile.ai[0] == 30)
            {
                Projectile.velocity = new Vector2(13f, 0f).RotatedBy(Projectile.rotation - MathHelper.PiOver2);
                Projectile.penetrate = 1;
            }
            else
            {
                Projectile.extraUpdates = 0;
                Projectile.velocity.Y = Math.Clamp(Projectile.velocity.Y + 0.4f, 0f, 12f);
                trailOpacity -= 0.035f;

                if (Projectile.ai[0] % 4 == 0)
                {
                    Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.TreasureSparkle, -Projectile.velocity.RotatedByRandom(0.5f) * 0.2f, 0);
                }
            }
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;


            Projectile.ai[0]++;
            Projectile.ai[1] += Projectile.velocity.Length();



        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {

            Projectile.Kill();

            for (int i = 0; i < 5; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.position + Projectile.velocity, DustID.TreasureSparkle, new Vector2(-Projectile.velocity.X * Main.rand.NextFloat(-0.4f, 0.4f), -Projectile.velocity.Y * Main.rand.NextFloat(-0.4f, 0.4f)), 0, default, Main.rand.NextFloat(1.5f, 1.8f));
            }

            return false;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        }

        public override void OnKill(int timeLeft)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        }
    }
}
