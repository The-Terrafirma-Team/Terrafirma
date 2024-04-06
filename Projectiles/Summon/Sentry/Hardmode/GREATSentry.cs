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
    internal class GREATSentry : ModProjectile
    {
        NPC closestnpc = null;
        public override void SetDefaults()
        {
            Projectile.friendly = true;

            Projectile.height = 80;
            Projectile.width = 74;
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
            return Projectile.ai[0] > 180 && Projectile.ai[1] % (int)(10 * Main.projectile[Projectile.whoAmI].GetSentryAttackCooldownMultiplier()) == 0;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float collsionPoint = 0;
            return Collision.CheckAABBvLineCollision(targetHitbox.Center(), targetHitbox.Size(),
                    Projectile.TopLeft + new Vector2(37, 45) - new Vector2(0, 12).RotatedBy(Projectile.rotation),
                    Projectile.TopLeft + new Vector2(37, 45) + new Vector2(1800, 0).RotatedBy(Projectile.rotation) - new Vector2(0, 12).RotatedBy(Projectile.rotation),
                    80,
                    ref collsionPoint);
        }
        public override void AI()
        {
            Projectile.damage = (int)(Projectile.originalDamage * (Math.Clamp((Projectile.ai[0] - 120f) / 240f, 0f, 2f) + 1f));

            if (Projectile.ai[1] % 27 == 0) SoundEngine.PlaySound(SoundID.Item15 with { Volume = Math.Clamp(Projectile.ai[0] / 480f, 0.05f, 1f), IsLooped = false }, Projectile.Center);

            //for (int i = 0; i < 40; i++)
            //{
            //    Dust dust = Dust.NewDustPerfect((Projectile.TopLeft + new Vector2(37, 45)) + new Vector2(18 * i, 0).RotatedBy(Projectile.rotation) - new Vector2(0, 12 * Projectile.spriteDirection).RotatedBy(Projectile.rotation), DustID.CrimsonTorch, Vector2.Zero, 1, Color.White, 1.5f);
            //    dust.noGravity = true;
            //}

            if (Main.player[Projectile.owner].HasMinionAttackTargetNPC && Projectile.Center.Distance(Main.npc[Main.player[Projectile.owner].MinionAttackTargetNPC].Center) < 600f * Main.projectile[Projectile.whoAmI].GetSentryRangeMultiplier())
            {
                closestnpc = Main.npc[Main.player[Projectile.owner].MinionAttackTargetNPC];

                Projectile.rotation = Projectile.rotation.AngleLerp((Projectile.TopLeft + new Vector2(37, 37)).AngleTo(closestnpc.Center), 0.07f);

                if (Math.Abs(Projectile.rotation) > MathHelper.PiOver2) Projectile.spriteDirection = -1;
                else Projectile.spriteDirection = 1;

                Projectile.ai[0] = Math.Clamp(Projectile.ai[0] + 1, 0, 600);
            }
            else
            {
                if (closestnpc != null)
                {
                    Projectile.rotation = Projectile.rotation.AngleLerp((Projectile.TopLeft + new Vector2(37, 37)).AngleTo(closestnpc.Center), 0.07f);

                    if (Math.Abs(Projectile.rotation) > MathHelper.PiOver2) Projectile.spriteDirection = -1;
                    else Projectile.spriteDirection = 1;

                    Projectile.ai[0] = Math.Clamp(Projectile.ai[0] + 1, 0, 600);
                }

                if (closestnpc == null || !closestnpc.active || Projectile.Center.Distance(closestnpc.Center) > 600f * Main.projectile[Projectile.whoAmI].GetSentryRangeMultiplier())
                {
                    NPC closestsnpcsearch = TFUtils.FindClosestNPC(600f * Main.projectile[Projectile.whoAmI].GetSentryRangeMultiplier(), Projectile.Center);
                    closestnpc = null;

                    if (Math.Abs(Projectile.rotation) > MathHelper.PiOver2)
                    {
                        Projectile.rotation = Projectile.rotation.AngleLerp((float)Math.PI, 0.01f);
                        Projectile.spriteDirection = -1;
                    }
                    else
                    {
                        Projectile.rotation = Projectile.rotation.AngleLerp(0f, 0.01f);
                        Projectile.spriteDirection = 1;
                    }

                    if (closestsnpcsearch != null)
                    {
                        closestnpc = closestsnpcsearch;
                    }
                    Projectile.ai[0] = Projectile.ai[0] > 0 ? Projectile.ai[0] - 3 : 0;

                }
            }
            Projectile.ai[1]++;

            if (Projectile.ai[0] > 120 && Projectile.ai[1] % 4 == 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    Dust dust = Dust.NewDustPerfect(Projectile.TopLeft + new Vector2(37, 45) + new Vector2(1820, -10 + i * 4).RotatedBy(Projectile.rotation) - new Vector2(0, 12 * Projectile.spriteDirection).RotatedBy(Projectile.rotation), DustID.DungeonSpirit, Vector2.Zero, 1, Color.White, 1.5f);
                    dust.noGravity = true;
                }
            }

        }

        public override bool PreDraw(ref Color lightColor)
        {
            float ChargeUpFloat;
            Texture2D SentryBase = ModContent.Request<Texture2D>("Terrafirma/Projectiles/Summon/Sentry/Hardmode/GREATSentry").Value;
            Texture2D SentryGlow = ModContent.Request<Texture2D>("Terrafirma/Projectiles/Summon/Sentry/Hardmode/GREATSentryGlow").Value;
            Texture2D SentryLaser = ModContent.Request<Texture2D>("Terrafirma/Projectiles/Summon/Sentry/Hardmode/GREATLaser").Value;
            Texture2D SentryLaserBase = ModContent.Request<Texture2D>("Terrafirma/Projectiles/Summon/Sentry/Hardmode/GREATLaserHead").Value;



            Main.EntitySpriteDraw(SentryBase, Projectile.Bottom - Main.screenPosition, new Rectangle(0, 40, 80, 34), lightColor, 0, new Vector2(32, 35), 1f, SpriteEffects.None, 0f);
            Main.EntitySpriteDraw(SentryBase, Projectile.Center - Main.screenPosition + new Vector2(0, 5), new Rectangle(0, 0, 80, 38), lightColor, Projectile.rotation, new Vector2(32, Projectile.spriteDirection == 1 ? 28 : 10), 1f, Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipVertically, 0f);
            Main.EntitySpriteDraw(SentryGlow, Projectile.Center - Main.screenPosition + new Vector2(0, 5), new Rectangle(0, 0, 80, 38), new Color(1f, 1f, 1f, 0) * 0.2f, Projectile.rotation, new Vector2(32, Projectile.spriteDirection == 1 ? 28 : 10), 1f + Math.Abs((float)Math.Sin(Main.timeForVisualEffects / 40f)) / 20f, Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipVertically, 0f);
            ChargeUpFloat = Math.Clamp(Projectile.ai[0] / 120f, 0f, 1f);
            Main.EntitySpriteDraw(SentryGlow, Projectile.Center - Main.screenPosition + new Vector2(0, 5), new Rectangle(0, 0, 80, 38), new Color(1f, 1f, 1f, 0) * ChargeUpFloat, Projectile.rotation, new Vector2(32, Projectile.spriteDirection == 1 ? 28 : 10), 1f, Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipVertically, 0f);

            ChargeUpFloat = MathHelper.Lerp(0f, 1f, Math.Clamp((Projectile.ai[0] - 120f) / 480f, 0f, 1f));
            Main.EntitySpriteDraw(SentryLaser,
                Projectile.Center - Main.screenPosition + new Vector2(0, 11 * -Projectile.spriteDirection).RotatedBy(Projectile.rotation) + new Vector2(0, 5),
                SentryLaser.Bounds,
                new Color(1f, 1f, 1f, 0) * ChargeUpFloat,
                Projectile.rotation,
                new Vector2(-0.23f, SentryLaser.Height / 2),
                new Vector2(100f, ChargeUpFloat),
                SpriteEffects.None,
                0f);
            Main.EntitySpriteDraw(SentryLaser,
                Projectile.Center - Main.screenPosition + new Vector2(0, 11 * -Projectile.spriteDirection).RotatedBy(Projectile.rotation) + new Vector2(0, 5),
                SentryLaser.Bounds,
                new Color(1f, 1f, 1f, 0) * (ChargeUpFloat / 3f),
                Projectile.rotation,
                new Vector2(-0.23f, SentryLaser.Height / 2),
                new Vector2(100f, ChargeUpFloat + Math.Abs((float)Math.Sin(Main.timeForVisualEffects / 10f)) / 3f),
                SpriteEffects.None,
                0f);
            Main.EntitySpriteDraw(SentryLaser,
                Projectile.Center - Main.screenPosition + new Vector2(0, 11 * -Projectile.spriteDirection).RotatedBy(Projectile.rotation) + new Vector2(0, 5),
                SentryLaser.Bounds,
                new Color(1f, 1f, 1f, 0) * (ChargeUpFloat / 10f),
                Projectile.rotation,
                new Vector2(-0.23f, SentryLaser.Height / 2),
                new Vector2(100f, ChargeUpFloat + Math.Abs((float)Math.Sin(Main.timeForVisualEffects / 20f)) / 2f),
                SpriteEffects.None,
                0f);


            Main.EntitySpriteDraw(SentryLaserBase,
                Projectile.Center - Main.screenPosition + new Vector2(0, 5),
                new Rectangle(0, (int)(Main.timeForVisualEffects / 4f) % 4 * SentryLaserBase.Height / 4, SentryLaserBase.Width, SentryLaserBase.Height / 4),
                new Color(1f, 1f, 1f, 0) * ChargeUpFloat,
                Projectile.rotation,
                new Vector2(-20 / ChargeUpFloat, Projectile.spriteDirection == 1 ? 52 : 26),
                ChargeUpFloat,
                SpriteEffects.None,
                0f);


            return false;
        }

    }
}
