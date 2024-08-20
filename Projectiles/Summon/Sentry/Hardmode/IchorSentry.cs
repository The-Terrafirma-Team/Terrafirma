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

namespace Terrafirma.Projectiles.Summon.Sentry.Hardmode
{
    internal class IchorSentry : ModProjectile
    {
        float sentryrot = MathHelper.PiOver2;
        float backtimer = 0;
        public override string Texture => "Terrafirma/Projectiles/Summon/Sentry/Hardmode/IchorSentryBase";
        public override void SetDefaults()
        {
            Projectile.damage = 30;
            Projectile.friendly = true;

            Projectile.height = 38;
            Projectile.width = 36;
            Projectile.DamageType = DamageClass.Summon;

            Projectile.tileCollide = true;
            Projectile.timeLeft = Projectile.SentryLifeTime;
            Projectile.penetrate = -1;

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
            if (Projectile.ai[0] >= 30 * Projectile.GetSentryAttackCooldownMultiplier() && TFUtils.FindClosestNPC(600f * Projectile.GetSentryRangeMultiplier(), Projectile.Center) != null)
            {
                Projectile.ai[0] = 0;
                Projectile proj = Projectile.NewProjectileButWithChangesFromSentryBuffs(Projectile.GetSource_FromThis(), Projectile.Center + new Vector2(0, -8) + new Vector2(-32, 0).RotatedBy(sentryrot), -new Vector2(4.2f, 0f).RotatedBy(sentryrot), ProjectileID.IchorSplash, Projectile.damage, Projectile.knockBack, Projectile.owner, 0, 0, 0);
                proj.DamageType = DamageClass.Summon;
                backtimer = 10;
                SoundEngine.PlaySound(SoundID.Item17, Projectile.position);
            }
            else if (TFUtils.FindClosestNPC(600f * Projectile.GetSentryRangeMultiplier(), Projectile.Center) != null)
            {
                float toenemyrot = (Projectile.Center - TFUtils.FindClosestNPC(600f * Projectile.GetSentryRangeMultiplier(), Projectile.Center).Center).ToRotation();
                if (sentryrot - toenemyrot < toenemyrot + (float)Math.PI * 2f - sentryrot)
                {
                    sentryrot = MathHelper.Lerp(sentryrot, (Projectile.Center - TFUtils.FindClosestNPC(600f * Projectile.GetSentryRangeMultiplier(), Projectile.Center).Center).ToRotation(), 0.1f);
                }
                else
                {
                    sentryrot = MathHelper.Lerp(sentryrot, (Projectile.Center - TFUtils.FindClosestNPC(600f * Projectile.GetSentryRangeMultiplier(), Projectile.Center).Center).ToRotation() + (float)Math.PI * 2f, 0.1f);
                }
                sentryrot = sentryrot % ((float)Math.PI * 2f);

            }

            if (backtimer > 0.1f)
            {
                backtimer *= 0.9f;
            }

        }
        public override bool PreDraw(ref Color lightColor)
        {
            Asset<Texture2D> SentryBase = ModContent.Request<Texture2D>("Terrafirma/Projectiles/Summon/Sentry/Hardmode/IchorSentryBase");
            Asset<Texture2D> SentryShooter = ModContent.Request<Texture2D>("Terrafirma/Projectiles/Summon/Sentry/Hardmode/IchorSentryTentacle");

            Main.EntitySpriteDraw(SentryShooter.Value, Projectile.Center - Main.screenPosition + new Vector2(0, -8) + new Vector2(backtimer, 0).RotatedBy(sentryrot), null, lightColor, sentryrot - MathHelper.PiOver2, new Vector2(SentryShooter.Width() / 2, SentryShooter.Height()), 1, SpriteEffects.None, 0);
            Main.EntitySpriteDraw(SentryBase.Value, Projectile.Center - Main.screenPosition + new Vector2(0, 1), null, lightColor, 0, SentryBase.Size() / 2, 1, SpriteEffects.None, 0);

            return false;
        }

    }
}
