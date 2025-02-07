﻿using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using System;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;
using Terrafirma.Data;

namespace Terrafirma.Projectiles.Ranged.Arrows
{
    internal class LuckyArrow : ModProjectile
    {
        Color TrailColor = Color.White;
        int rotdirection = 1;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Projectile.type] = 4;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;
            ProjectileSets.CanBeReflected[Type] = true;
        }
        public override void SetDefaults()
        {
            Projectile.damage = 16;
            Projectile.width = 4;
            Projectile.height = 4;
            DrawOffsetX = -Projectile.width / 2 - 5;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.friendly = true;
            Projectile.arrow = true;
            Projectile.DamageType = DamageClass.Ranged;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {

        }

        public override void OnSpawn(IEntitySource source)
        {
            Projectile.ai[0] = Main.rand.Next(8);
            Projectile.netUpdate = true;
        }
        public override void AI()
        {
            if (Projectile.ai[1] == 0)
            {
                switch (Projectile.ai[0])
                {
                    case 0: TrailColor = new Color(60, 40, 0, 0); break;
                    case 1: TrailColor = new Color(40, 0, 0, 0); Projectile.penetrate = 4; break;
                    case 2: TrailColor = new Color(0, 60, 40, 0); Projectile.penetrate = 4; break;
                    case 3: TrailColor = new Color(60, 0, 60, 0); break;
                    case 4: TrailColor = new Color(40, 40, 40, 0); Projectile.velocity = Projectile.velocity * 0.1f; break;
                    case 5: TrailColor = new Color(0, 0, 0, 0); Projectile.velocity = Projectile.velocity * 2f; Projectile.penetrate = 10; break;
                    case 6: TrailColor = new Color(0, 60, 0, 0); Projectile.velocity = Projectile.velocity * 0.8f; break;
                    case 7: TrailColor = new Color(20, 40, 40, 0); Projectile.tileCollide = false; Projectile.penetrate = 4; break;
                }
            }
            

            Lighting.AddLight(Projectile.Center, new Vector3(TrailColor.R / 40, TrailColor.G / 40, TrailColor.B / 40));
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + (float)(90 * (Math.PI / 180));

            switch (Projectile.ai[0])
            {
                case 0:
                    Projectile.velocity.Y += 0.05f; ;
                    break;
                case 1:
                    Projectile.velocity = Projectile.velocity.RotatedBy(5 * (Math.PI / 180) * rotdirection);
                    if (Projectile.ai[1] % 20 == 0 && Projectile.ai[1] != 0)
                    {
                        rotdirection = Main.rand.NextBool() ? -1 : 1;
                    }
                    break;
                case 2:
                    if (Projectile.ai[1] % 20 == 0 && Projectile.ai[1] != 0)
                    {
                        Projectile.velocity = Projectile.velocity.RotatedBy(90 * (Math.PI / 180) * (Main.rand.NextBool() ? -1 : 1));
                    }
                    break;
                case 3:
                    Projectile.velocity = Projectile.velocity.RotatedBy(Math.Sin(Projectile.ai[1] * 0.3f - MathHelper.PiOver2) * 0.5f);
                    break;
                case 4:
                    Projectile.velocity *= 1.05f;
                    break;
                case 5:
                    Projectile.velocity *= 0.95f;
                    break;
                case 6:
                    if (TFUtils.FindClosestNPC(300f, Projectile.Center) != null)
                    {
                        Projectile.velocity += Projectile.Center.DirectionTo(TFUtils.FindClosestNPC(300f, Projectile.Center).Center);
                        Projectile.velocity = Vector2.Lerp(Projectile.velocity, Projectile.Center.DirectionTo(TFUtils.FindClosestNPC(300f, Projectile.Center).Center) * Projectile.velocity.Length(), 0.2f);
                        Projectile.velocity = Projectile.velocity.LengthClamp(20f);
                    }
                    break;
                case 7:
                    break;

            }

            Projectile.ai[1] += 1;

        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            return false;
        }

        public override void OnKill(int timeLeft)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            Lighting.AddLight(Projectile.Center, new Vector3(TrailColor.R / 50, TrailColor.G / 50, TrailColor.B / 50));

            for (int i = 0; i < 15; i++)
            {
                Dust.NewDust(Projectile.Center, 2, 2, DustID.Gold, Projectile.velocity.X * Main.rand.NextFloat(0.1f, 0.2f), Projectile.velocity.Y * Main.rand.NextFloat(0.1f, 0.2f), 0, default, Main.rand.NextFloat(0.8f, 1.0f));
            }
            for (int i = 0; i < 6; i++)
            {
                Dust.NewDust(Projectile.Center, 2, 2, DustID.GemDiamond, Projectile.velocity.X * Main.rand.NextFloat(0.1f, 0.2f), Projectile.velocity.Y * Main.rand.NextFloat(0.1f, 0.2f), 0, new Color(TrailColor.R * 10, TrailColor.G * 10, TrailColor.B * 10), Main.rand.NextFloat(0.8f, 1.0f));
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Asset<Texture2D> ArrowOutlineSprite = ModContent.Request<Texture2D>(Texture + "Afterimage");
            Asset<Texture2D> ArrowSprite = ModContent.Request<Texture2D>(Texture);
            Asset<Texture2D> ArrowGlowSprite = ModContent.Request<Texture2D>(Texture + "Glow");

            for (int i = 0; i < 20; i++)
            {

                Main.EntitySpriteDraw(ArrowOutlineSprite.Value, Projectile.oldPos[i] - Main.screenPosition + Projectile.Size / 2, null, TrailColor, Projectile.oldRot[i], ArrowSprite.Size() / 2, 1f - 0.05f * i, SpriteEffects.None, 0f);

            }

            Main.EntitySpriteDraw(ArrowGlowSprite.Value, Projectile.Center - Main.screenPosition, null, TrailColor, Projectile.rotation, ArrowGlowSprite.Size() / 2, 1f, SpriteEffects.None, 0f);
            Main.EntitySpriteDraw(ArrowSprite.Value, Projectile.Center - Main.screenPosition, null, lightColor, Projectile.rotation, ArrowSprite.Size() / 2, 1f, SpriteEffects.None, 0f);

            return false;
        }

    }
}
