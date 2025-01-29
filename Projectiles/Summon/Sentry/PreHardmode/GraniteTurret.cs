using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.ID;
using Terraria.GameContent;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Terrafirma.Common.Templates;

namespace Terrafirma.Projectiles.Summon.Sentry.PreHardmode
{
    public class GraniteTurret : SentryTemplate
    {
        NPC TargetNPC = null;
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.height = 30;
            Projectile.width = 34;
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
            NPC target = Projectile.FindSummonTarget(600f * TFUtils.GetSentryRangeMultiplier(Projectile), Projectile.Center, false);
            Player player = Main.player[Projectile.owner];
            Projectile.ai[0]++;
            if (target == null)
            {
                Projectile.ai[0] -= 2;
                Projectile.rotation = Utils.AngleLerp(Projectile.rotation, Projectile.Center.DirectionTo(player.Center).ToRotation(),0.06f);
            }
            else
            {
                Projectile.rotation = Utils.AngleLerp(Projectile.rotation, Projectile.Center.DirectionTo(target.Center).ToRotation(), 0.2f);
                if (Projectile.ai[0] >= 60 * Projectile.GetSentryAttackCooldownMultiplier())
                {
                    Projectile.rotation = Projectile.Center.DirectionTo(target.Center).ToRotation();
                    Projectile.ai[0] = 0;
                    if (Main.myPlayer == Projectile.owner)
                    {
                        Projectile.NewProjectileButWithChangesFromSentryBuffs(Projectile.GetSource_FromThis(), Projectile.Center - new Vector2(0, 2), Projectile.Center.DirectionTo(target.Center) * 12, ModContent.ProjectileType<GraniteTurretShot>(), Projectile.damage, Projectile.knockBack, Projectile.owner, RangeDoesNotAffectVelocity: true);
                    }
                    SoundEngine.PlaySound(SoundID.Item12, Projectile.position);
                    for (int i = 0; i < 10; i++)
                    {
                        Dust d = Dust.NewDustPerfect(Projectile.Center - new Vector2(0, 2), 206, Projectile.Center.DirectionTo(target.Center).RotatedByRandom(0.9f) * Main.rand.NextFloat(6f));
                        d.scale = 2f;
                        d.noGravity = true;
                    }
                }
            }
            Projectile.ai[0] = MathHelper.Clamp(Projectile.ai[0], 0, 60 * Projectile.GetSentryAttackCooldownMultiplier());
            Projectile.spriteDirection = Math.Abs(Projectile.rotation) < MathHelper.PiOver2? 1 : -1;
        }
        public override void OnHitByWrench(Player player, WrenchItem wrench)
        {
            Projectile.ai[0] = 60 * Projectile.GetSentryAttackCooldownMultiplier();
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Rectangle head = new Rectangle(0,0,34,22);
            Rectangle headGlow = new Rectangle(36, 0, 34, 22);
            Rectangle body = new Rectangle(0, 24, 34, 14);

            Asset<Texture2D> tex = TextureAssets.Projectile[Type];

            Main.EntitySpriteDraw(tex.Value, Projectile.Bottom - Main.screenPosition, body, lightColor, 0, new Vector2(17, 14),1,SpriteEffects.None);
            bool flip = Projectile.spriteDirection == -1;

            Main.EntitySpriteDraw(tex.Value, Projectile.Center + new Vector2(0, -2) - Main.screenPosition, head, lightColor, Projectile.rotation, new Vector2(15, flip ? 8 : 14), 1, flip ? SpriteEffects.FlipVertically : SpriteEffects.None);

            Main.EntitySpriteDraw(tex.Value, Projectile.Center + new Vector2(0, -2) - Main.screenPosition, headGlow, Color.Lerp(Color.Transparent, new Color(1f, 1f, 1f, 0f), MathHelper.Clamp(Projectile.ai[0] / 60f * Projectile.GetSentryAttackCooldownMultiplier(), 0, 1f)), Projectile.rotation, new Vector2(15, flip ? 8 : 14), 1, flip ? SpriteEffects.FlipVertically : SpriteEffects.None);
            return false;
        }
    }
}
