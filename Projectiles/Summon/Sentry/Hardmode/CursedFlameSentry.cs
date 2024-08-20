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
    internal class CursedFlameSentry : ModProjectile
    {
        NPC targetnpc = null;
        int sentryradius = 350;
        int verticalshooteroffset = 14;
        float shootoffset = 0f;
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.height = 22;
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

            if (targetnpc == null)
            {
                targetnpc = TFUtils.FindSummonTarget(Projectile, sentryradius * TFUtils.GetSentryRangeMultiplier(Projectile), Projectile.Center, false);
                shootoffset = MathHelper.Lerp(shootoffset, 0f, 0.1f);
            }

            if (targetnpc != null && (targetnpc.Center.Distance(Projectile.Center) > sentryradius || !targetnpc.active))
            {
                targetnpc = null;
            }

            if (targetnpc != null) 
            {
                Projectile.rotation = Utils.AngleLerp(Projectile.rotation, (Projectile.Center - new Vector2(0, verticalshooteroffset)).DirectionTo(targetnpc.Center).ToRotation() + MathHelper.PiOver2, 0.15f);
                if (Projectile.ai[0] > 8 * TFUtils.GetSentryAttackCooldownMultiplier(Projectile))
                {
                    Projectile.NewProjectileButWithChangesFromSentryBuffs(
                        Projectile.GetSource_FromThis(),
                        (Projectile.Center - new Vector2(0, verticalshooteroffset)) + new Vector2(0,-12).RotatedBy(Projectile.rotation),
                        Projectile.Center.DirectionTo(targetnpc.Center) * 16f,
                        ModContent.ProjectileType<CursedFlames>(),
                        Projectile.damage,
                        Projectile.knockBack,
                        Projectile.owner);
                    Projectile.ai[0] = 0;
                    for (int i = 0; i < 3; i++)
                    {
                        Vector2 shootvel = new Vector2(4f,0f).RotatedBy(Projectile.rotation);
                        Dust.NewDust(Projectile.Center - new Vector2(0, verticalshooteroffset) + new Vector2(0, -26).RotatedBy(Projectile.rotation), 4, 4, DustID.CursedTorch, shootvel.X, shootvel.Y, 0, Scale: 1.5f);
                    }
                }
                shootoffset = MathHelper.Lerp(shootoffset, 6, 0.1f);
                if (Projectile.ai[0] == 0) SoundEngine.PlaySound(SoundID.Item20, Projectile.Center);
            }

            Projectile.ai[0]++;
            
            
        }
        private static Asset<Texture2D> BaseTex;
        public override void SetStaticDefaults()
        {
            BaseTex = ModContent.Request<Texture2D>("Terrafirma/Projectiles/Summon/Sentry/Hardmode/CursedFlameSentry");
        }
        public override bool PreDraw(ref Color lightColor)
        {

            Main.EntitySpriteDraw(BaseTex.Value,
                Projectile.Center - Main.screenPosition,
                new Rectangle(0,42,44,24),
                lightColor,
                0f,
                new Vector2(22,12),
                Projectile.scale,
                SpriteEffects.None);
            Main.EntitySpriteDraw(BaseTex.Value,
                Projectile.Center - Main.screenPosition - new Vector2(0, verticalshooteroffset) + new Vector2(0, shootoffset).RotatedBy(Projectile.rotation),
                new Rectangle(8, 0, 28, 38),
                lightColor,
                Projectile.rotation,
                new Vector2(14, 28),
                Projectile.scale,
                SpriteEffects.None);
            Main.EntitySpriteDraw(BaseTex.Value,
                Projectile.Center - Main.screenPosition - new Vector2(0, verticalshooteroffset) + new Vector2(0, shootoffset).RotatedBy(Projectile.rotation),
                new Rectangle(38, 0, 28, 38),
                Color.White,
                Projectile.rotation,
                new Vector2(14, 28),
                Projectile.scale,
                SpriteEffects.None);
            Main.EntitySpriteDraw(BaseTex.Value,
                Projectile.Center - Main.screenPosition,
                new Rectangle(46, 42, 44, 24),
                lightColor,
                0f,
                new Vector2(22, 12),
                Projectile.scale,
                SpriteEffects.None);

            return false;
        }

    }
}
