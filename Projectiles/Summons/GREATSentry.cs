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

namespace Terrafirma.Projectiles.Summons
{
    internal class GREATSentry : ModProjectile
    {
        NPC closestnpc = null;
        public override void SetDefaults()
        {
            Projectile.damage = 30;
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
            return false;
        }
        public override void AI()
        {

            if (closestnpc != null)
            {
                Projectile.ai[1] = Utils.AngleLerp(Projectile.ai[1], (Projectile.TopLeft + new Vector2(32, 38)).AngleTo(closestnpc.Center), 0.07f);
                if (Math.Abs(Projectile.ai[1]) > MathHelper.PiOver2) Projectile.spriteDirection = -1;
                else Projectile.spriteDirection = 1;
                Projectile.ai[0] = Math.Clamp(Projectile.ai[0] + 1, 0, 600);
            }

            if (closestnpc == null || !closestnpc.active || Projectile.Center.Distance(closestnpc.Center) > 400f)
            {
                NPC closestsnpcsearch = TFUtils.FindClosestNPC(400f, Projectile.Center);
                closestnpc = null;
                if (closestsnpcsearch != null)
                {
                    closestnpc = closestsnpcsearch;
                }
                Projectile.ai[0] = Projectile.ai[0] > 0 ? Projectile.ai[0] - 1 : 0;
                
            }
            
                
        }

        public override bool PreDraw(ref Color lightColor)
        {
            float ChargeUpFloat;
            Texture2D SentryBase = ModContent.Request<Texture2D>("Terrafirma/Projectiles/Summons/GREATSentry").Value;
            Texture2D SentryGlow = ModContent.Request<Texture2D>("Terrafirma/Projectiles/Summons/GREATSentryGlow").Value;
            Texture2D SentryLaser = ModContent.Request<Texture2D>("Terrafirma/Projectiles/Summons/GREATLaser").Value;
            Texture2D SentryLaserBase = ModContent.Request<Texture2D>("Terrafirma/Projectiles/Summons/GREATLaserHead").Value;

            

            Main.EntitySpriteDraw(SentryBase, Projectile.Bottom - Main.screenPosition , new Rectangle(0,40,80,34), lightColor, 0, new Vector2(32, 35), 1f, SpriteEffects.None, 0f);
            Main.EntitySpriteDraw(SentryBase, Projectile.Center - Main.screenPosition + new Vector2(0, 5), new Rectangle(0, 0, 80, 38), lightColor, Projectile.ai[1], new Vector2(32, Projectile.spriteDirection == 1 ? 28 : 10), 1f, Projectile.spriteDirection == 1? SpriteEffects.None : SpriteEffects.FlipVertically, 0f);
            Main.EntitySpriteDraw(SentryGlow, Projectile.Center - Main.screenPosition + new Vector2(0, 5), new Rectangle(0, 0, 80, 38), new Color(1f, 1f, 1f, 0) * 0.2f, Projectile.ai[1], new Vector2(32, Projectile.spriteDirection == 1 ? 28 : 8), 1f + Math.Abs((float)Math.Sin(Main.timeForVisualEffects / 40f)) / 20f, Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipVertically, 0f);
            ChargeUpFloat = Math.Clamp(Projectile.ai[0] / 120f, 0f, 1f);
            Main.EntitySpriteDraw(SentryGlow, Projectile.Center - Main.screenPosition + new Vector2(0, 5), new Rectangle(0, 0, 80, 38), new Color(1f,1f,1f,0) * ChargeUpFloat, Projectile.ai[1], new Vector2(32, Projectile.spriteDirection == 1 ? 28 : 8), 1f, Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipVertically, 0f);

            ChargeUpFloat = MathHelper.Lerp(0f, 1f, Math.Clamp((Projectile.ai[0] - 120f) / 480f, 0f, 1f));
            Main.EntitySpriteDraw(SentryLaser, 
                Projectile.Center - Main.screenPosition + new Vector2(0, 11 * -Projectile.spriteDirection).RotatedBy(Projectile.ai[1]) + new Vector2(0,5), 
                SentryLaser.Bounds, 
                new Color(1f, 1f, 1f, 0) * ChargeUpFloat,
                Projectile.ai[1], 
                new Vector2(-0.23f, SentryLaser.Height / 2), 
                new Vector2(100f, ChargeUpFloat), 
                SpriteEffects.None, 
                0f);
            Main.EntitySpriteDraw(SentryLaser,
                Projectile.Center - Main.screenPosition + new Vector2(0, 11 * -Projectile.spriteDirection).RotatedBy(Projectile.ai[1]) + new Vector2(0, 5),
                SentryLaser.Bounds,
                new Color(1f, 1f, 1f, 0) * (ChargeUpFloat / 3f),
                Projectile.ai[1],
                new Vector2(-0.23f, SentryLaser.Height / 2),
                new Vector2(100f, ChargeUpFloat + Math.Abs((float)Math.Sin(Main.timeForVisualEffects / 10f)) / 3f),
                SpriteEffects.None,
                0f);

            Main.EntitySpriteDraw(SentryLaserBase,
                Projectile.Center - Main.screenPosition + new Vector2(0, 5),
                new Rectangle(0, ((int)(Main.timeForVisualEffects / 4f ) % 4) * SentryLaserBase.Height / 4, SentryLaserBase.Width, SentryLaserBase.Height / 4),
                new Color(1f, 1f, 1f, 0) * ChargeUpFloat,
                Projectile.ai[1],
                new Vector2(-20 / ChargeUpFloat, Projectile.spriteDirection == 1 ? 52 : 26),
                ChargeUpFloat,
                SpriteEffects.None,
                0f);


            return false;
        }

    }
}
