using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using static Terraria.GameContent.Animations.IL_Actions.NPCs;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Terrafirma.Projectiles.Summon.Sentry
{
    internal class CursedFlameSentry : ModProjectile
    {
        float sentryrot = MathHelper.PiOver2;
        public override string Texture => "Terrafirma/Projectiles/Summon/Sentry/CursedFlameSentryBase";
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.height = 30;
            Projectile.width = 36;
            Projectile.DamageType = DamageClass.Summon;

            Projectile.tileCollide = true;
            Projectile.timeLeft = Projectile.SentryLifeTime;
            Projectile.penetrate = -1;

            Projectile.ArmorPenetration = 5;

            Projectile.sentry = true;


        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            fallThrough = false;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override void AI()
        {
            Projectile.velocity.Y += 0.5f;
            Projectile.ai[0]++;
            Projectile.ai[1]--;
            if (Projectile.ai[0] >= 4 * Projectile.GetSentryAttackCooldownMultiplier() && TFUtils.FindClosestNPC(350f * Projectile.GetSentryRangeMultiplier(), Projectile.Center) != null)
            {
                Projectile cursedflame = Projectile.NewProjectileButWithChangesFromSentryBuffs(Projectile.GetSource_FromThis(), Projectile.Center + new Vector2(0, -8) + new Vector2(-32, 0).RotatedBy(sentryrot), -new Vector2(12f, 0f).RotatedBy(sentryrot), ModContent.ProjectileType<CursedFlames>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0, Main.rand.NextFloat(0.5f,1f), 0);
                cursedflame.ArmorPenetration = 15;
                Projectile.ai[0] = 0;
                if (Projectile.ai[1] <= 0)
                {
                    Projectile.ai[1] = 32;
                    SoundEngine.PlaySound(SoundID.Item34, Projectile.position);
                }
            }
            else if (TFUtils.FindClosestNPC(600f, Projectile.Center) != null)
            {
                float toenemyrot = (Projectile.Center - TFUtils.FindClosestNPC(600f, Projectile.Center).Center).ToRotation();
                if ( sentryrot - toenemyrot < toenemyrot + (float)Math.PI * 2f - sentryrot)
                {
                    sentryrot = MathHelper.Lerp(sentryrot, (Projectile.Center - TFUtils.FindClosestNPC(600f * Projectile.GetSentryRangeMultiplier(), Projectile.Center).Center).ToRotation(), 0.1f);
                }
                else
                {
                    sentryrot = MathHelper.Lerp(sentryrot, (Projectile.Center - TFUtils.FindClosestNPC(600f * Projectile.GetSentryRangeMultiplier(), Projectile.Center).Center).ToRotation() + (float)Math.PI * 2f, 0.1f);
                }
                    sentryrot = sentryrot % ((float)Math.PI * 2f);

            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Asset<Texture2D> SentryBase = ModContent.Request<Texture2D>("Terrafirma/Projectiles/Summon/Sentry/CursedFlameSentryBase");
            Asset<Texture2D> SentryShooter = ModContent.Request<Texture2D>("Terrafirma/Projectiles/Summon/Sentry/CursedFlameShooter");
            Asset<Texture2D> SentryShooterGlow = ModContent.Request<Texture2D>("Terrafirma/Projectiles/Summon/Sentry/CursedFlameShooterGlow");

            Main.EntitySpriteDraw(SentryShooter.Value, Projectile.Center - Main.screenPosition + new Vector2(0, 0), null, lightColor, sentryrot - MathHelper.PiOver2, new Vector2(SentryShooter.Width()/2, SentryShooter.Height()/2 + 10), 1, SpriteEffects.None, 0);
            Main.EntitySpriteDraw(SentryShooterGlow.Value, Projectile.Center - Main.screenPosition + new Vector2(0, 0), null, Color.White, sentryrot - MathHelper.PiOver2, new Vector2(SentryShooter.Width() / 2, SentryShooter.Height() / 2 + 10), 1, SpriteEffects.None, 0);

            Main.EntitySpriteDraw(SentryBase.Value, Projectile.Center - Main.screenPosition + new Vector2(0, 1), null, lightColor, 0, SentryBase.Size() / 2, 1, SpriteEffects.None, 0);

            return false;
        }

    }
}
