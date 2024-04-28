using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terrafirma.Projectiles.Ranged.Boomerangs;
using System.Collections.Generic;
using Terraria.ID;
using Terrafirma.Common;
using Terraria.GameContent;
using Terraria.DataStructures;
using Terrafirma.Buffs.Buffs;

namespace Terrafirma.Projectiles.Summon.Sentry.PreHardmode
{
    internal class GemSentry : ModProjectile
    {
        float turretradius = 200f;
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.height = 58;
            Projectile.width = 48;
            
            Projectile.tileCollide = true;
            Projectile.timeLeft = Projectile.SentryLifeTime;
            Projectile.penetrate = -1;
            Projectile.sentry = true;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            behindProjectiles.Add(index);
            base.DrawBehind(index, behindNPCsAndTiles, behindNPCs, behindProjectiles, overPlayers, overWiresUI);
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
            turretradius = 200f * Projectile.GetSentryRangeMultiplier();
            Projectile.ai[0]++;

            Color GemGlowColor = Color.White;
            switch (Projectile.ai[2])
            {
                case 0: GemGlowColor = new Color(192, 48, 245, 0); break;
                case 1: GemGlowColor = new Color(241, 159, 0, 0); break;
                case 2: GemGlowColor = new Color(10, 143, 93, 0); break;
                case 3: GemGlowColor = new Color(50, 58, 218, 0); break;
                case 4: GemGlowColor = new Color(238, 51, 53, 0); break;
                case 5: GemGlowColor = new Color(255, 255, 255, 0); break;
                case 6: GemGlowColor = new Color(207, 101, 0, 0); break;
            }
            Lighting.AddLight(Projectile.Center, new Vector3(GemGlowColor.R / 255f, GemGlowColor.G / 255f, GemGlowColor.B / 255f));

            for (int i = 0; i < Main.player.Length; i++)
            {
                if (Main.player[i].Center.Distance(Projectile.Center) <= turretradius)
                {
                    switch (Projectile.ai[2])
                    {
                        case 0: Main.player[i].AddBuff(ModContent.BuffType<AmethystElementalDamage>(), 1); break;
                        case 1: Main.player[i].AddBuff(ModContent.BuffType<TopazDefenseBuff>(), 1); break;
                        case 2: Main.player[i].AddBuff(ModContent.BuffType<EmeraldRegenBuff>(), 1); break;
                        case 3: Main.player[i].AddBuff(ModContent.BuffType<SapphireMovementBuff>(), 1); break;
                        case 4: Main.player[i].AddBuff(ModContent.BuffType<RubyDamageBuff>(), 1); break;
                        case 5: Main.player[i].AddBuff(ModContent.BuffType<DiamondPierceBuff>(), 1); break;
                        case 6: Main.player[i].AddBuff(ModContent.BuffType<AmberPenetrationBuff>(), 1); break;
                    }        
                }
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Asset<Texture2D> SentrySprite = TextureAssets.Projectile[Type];

            Rectangle BaseRect = new Rectangle(48 * (int)Projectile.ai[2], 40, 48, 24);
            Rectangle GemRect = new Rectangle(48 * (int)Projectile.ai[2], 0, 48, 40);
            Rectangle GemGlowRect = new Rectangle(48 * (int)Projectile.ai[2], 96, 48, 36);
            Rectangle GemBorderRect = new Rectangle(48 * (int)Projectile.ai[2], 64, 48, 32);
            Rectangle SparkleBorderRect = new Rectangle(366, 8, 16, 16);

            Color GemGlowColor = new Color(1f, 1f, 1f, 0f);
            switch (Projectile.ai[2])
            {
                case 0: GemGlowColor = new Color(192, 48, 245, 0); break;
                case 1: GemGlowColor = new Color(241, 159, 0, 0); break;
                case 2: GemGlowColor = new Color(10, 143, 93, 0); break;
                case 3: GemGlowColor = new Color(50, 58, 218, 0); break;
                case 4: GemGlowColor = new Color(238, 51, 53, 0); break;
                case 5: GemGlowColor = new Color(255, 255, 255, 0); break;
                case 6: GemGlowColor = new Color(207, 101, 0, 0); break;
            }        

            //Base
            Main.EntitySpriteDraw(SentrySprite.Value,
                Projectile.Center - Main.screenPosition + new Vector2(0,21),
                BaseRect,
                Color.Lerp(lightColor, Color.White, 0.6f),
                0,
                new Vector2(24,12),
                1f,
                SpriteEffects.None);
            //Gem
            Main.EntitySpriteDraw(SentrySprite.Value,
                Projectile.Center - Main.screenPosition - new Vector2(0, 12 + (float)(Math.Sin(Main.timeForVisualEffects * 0.05f) * 3)),
                GemGlowRect,
                GemGlowColor * 0.05f,
                0,
                new Vector2(24, 20),
                1.2f + (float)(Math.Sin((Main.timeForVisualEffects + 40) * 0.03f) + 1f) * 0.2f,
                SpriteEffects.None);
            Main.EntitySpriteDraw(SentrySprite.Value,
                Projectile.Center - Main.screenPosition - new Vector2(0, 12 + (float)(Math.Sin(Main.timeForVisualEffects * 0.05f) * 3)),
                GemGlowRect,
                GemGlowColor * 0.1f,
                0,
                new Vector2(24, 20),
                1.1f + (float)(Math.Sin(Main.timeForVisualEffects * 0.03f) + 1f) * 0.1f,
                SpriteEffects.None);
            Main.EntitySpriteDraw(SentrySprite.Value,
                Projectile.Center - Main.screenPosition - new Vector2(0, 12 + (float)(Math.Sin(Main.timeForVisualEffects * 0.05f) * 3)),
                GemRect,
                Color.Lerp(lightColor, Color.White, 0.6f),
                0,
                new Vector2(24, 20),
                1f,
                SpriteEffects.None);
            //Border
            float BorderSpriteInt = (int)(((turretradius) * 2f * MathHelper.Pi) / 50) / 2 * 2;
            for (int i = 0; i < BorderSpriteInt; i++)
            {
                Main.EntitySpriteDraw(SentrySprite.Value,
                    Projectile.Center - Main.screenPosition + new Vector2(turretradius , 0).RotatedBy((MathHelper.TwoPi / BorderSpriteInt) * i + (Main.timeForVisualEffects * 0.01f / TFUtils.GetSentryRangeMultiplier(Projectile))),
                    i % 2 == 0? GemBorderRect : SparkleBorderRect,
                    GemGlowColor * 0.2f,
                    ((MathHelper.TwoPi / BorderSpriteInt) * i + (float)(Main.timeForVisualEffects * 0.01f / TFUtils.GetSentryRangeMultiplier(Projectile))) + MathHelper.PiOver2,
                    i % 2 == 0 ? new Vector2(24, 20) : new Vector2(8, 8),
                    1f,
                    SpriteEffects.None);
            }

            return false;
        }
    }
}
