using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using System;
using Terrafirma.Particles;
using Terrafirma.Items.Weapons.Melee.Knight;
using Terraria.GameContent;
using ReLogic.Content;
using System.Collections.Generic;
using Terrafirma.Data;

namespace Terrafirma.Projectiles.Melee.Knight
{
    public class AntiheroProjectile : ModProjectile
    {
        NPC targetNPC = null;
        Vector2 targetOffset = Vector2.Zero;
        private static Asset<Texture2D> glow;
        public override void SetStaticDefaults()
        {
            ProjectileSets.AutomaticallyGivenMeleeScaling[Type] = true;
            glow = ModContent.Request<Texture2D>(Texture + "_White");
            ProjectileID.Sets.TrailingMode[Projectile.type] = 4;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
        }
        public override void SetDefaults()
        {          
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 10000;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.extraUpdates = 2;
            Projectile.hide = true;
        }
        public override void AI()
        {
            ParticleSystem.AddParticle(new HiResFlame() { SizeMultiplier = 2f}, Projectile.Center + new Vector2(0,Main.rand.NextFloat(-30,30) * Projectile.scale).RotatedBy(Projectile.rotation + MathHelper.PiOver2), Projectile.velocity.RotatedByRandom(0.3f) * 0.3f + Main.rand.NextVector2Circular(1,1), new Color(1f, 0f, Main.rand.NextFloat(), 0f) * 0.2f);
            if (Projectile.ai[1] == 1)
            {
                Projectile.tileCollide = false;
                Projectile.spriteDirection = -1;              
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, Projectile.Center.DirectionTo(Main.player[Projectile.owner].Center) * 16f, 0.04f);                
                targetNPC = null;
                if (Main.player[Projectile.owner].Center.Distance(Projectile.Center) <= 50) Projectile.Kill();
                Projectile.rotation += Projectile.direction * 0.2f;
            }
            else
            {
                Projectile.rotation = Projectile.velocity.ToRotation();
            }

            if (Main.player[Projectile.owner].Center.Distance(Projectile.Center) > 600f) Projectile.ai[1] = 1;
            if (Main.player[Projectile.owner].Center.Distance(Projectile.Center) > 900f) Projectile.Kill();

            if (targetNPC != null)
            {
                Projectile.Center = targetNPC.Center + targetOffset.RotatedBy(targetNPC.rotation);
                Projectile.rotation = Projectile.Center.AngleTo(targetNPC.Center);

                if (targetNPC.immune[Projectile.owner] == 0)
                {
                    Main.player[Projectile.owner].ApplyDamageToNPC(targetNPC, Projectile.damage / 3, 0f, 1, damageType: DamageClass.Melee);
                    targetNPC.immune[Projectile.owner] = 20;
                    if (!Main.player[Projectile.owner].CheckTension(5, true))
                    {
                        Projectile.ai[1] = 1;
                        return;
                    }

                    int healAmount = (int)(4 * (((600 - (int)Main.player[Projectile.owner].Center.Distance(Projectile.Center)) / 300f) + 1));

                    if (healAmount > 0 && targetNPC.type != NPCID.TargetDummy) Main.player[Projectile.owner].HealWithAdjustments(healAmount);

                    for (int i = 0; i < Projectile.Center.Distance(Main.player[Projectile.owner].Center); i += 5)
                    {
                        float dist = i / Projectile.Center.Distance(Main.player[Projectile.owner].Center);
                        Vector2 pos = new Vector2(
                            MathHelper.Lerp(Projectile.Center.X, Main.player[Projectile.owner].Center.X, dist),
                            MathHelper.Lerp(Projectile.Center.Y, Main.player[Projectile.owner].Center.Y, dist * dist)
                        );
                        Dust d = Dust.NewDustPerfect(pos, DustID.LifeDrain, Vector2.Zero);
                        d.noGravity = true;
                    }

                    for (int i = 0; i < 10; i++)
                    {
                        Dust.NewDustPerfect(Projectile.Center + Projectile.Center.DirectionTo(targetNPC.Center) * (40 * Projectile.scale), DustID.Blood, Main.rand.NextVector2Circular(4f, 4f), Scale: 1.5f);

                        //ParticleSystem.AddParticle(new HiResFlame() { SizeMultiplier = 1.5f }, Main.rand.NextVector2FromRectangle(target.Hitbox), Main.rand.NextVector2Circular(3, 3), new Color(1f, 0f, Main.rand.NextFloat(), 0f) * 0.6f);
                        ParticleSystem.AddParticle(new ImpactSparkle() { Scale = Main.rand.NextFloat(0.4f, 0.7f), LifeTime = Main.rand.Next(15, 30) }, targetNPC.Hitbox.ClosestPointInRect(Projectile.Center), Main.rand.NextVector2Circular(3,3), Color.Black);
                        ParticleSystem.AddParticle(new ImpactSparkle() { Scale = Main.rand.NextFloat(0.4f, 0.7f), LifeTime = Main.rand.Next(15, 30) }, targetNPC.Hitbox.ClosestPointInRect(Projectile.Center), Main.rand.NextVector2Circular(5,5), new Color(1f, 0f, Main.rand.NextFloat(), 0f) * 0.5f);
                    }

                }
                if (!targetNPC.active) Projectile.ai[1] = 1;
            }

        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.ai[1] == 0) Projectile.velocity = Vector2.Zero;
            if (targetNPC == null || !targetNPC.active)
            {
                targetNPC = target;
                targetOffset = (Projectile.Center - target.Center).RotatedBy(-targetNPC.rotation);
            }
            base.OnHitNPC(target, hit, damageDone);
        }

        public override bool? CanHitNPC(NPC target)
        {
            if (targetNPC == null)
            {
                return base.CanHitNPC(target);
            }
            else if (target == targetNPC)
            {
                return false;
            }
            return false;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.ai[1] = 1;
            return false;
        }
        public override void OnKill(int timeLeft)
        {
            if (Main.player[Projectile.owner].HeldItem.ModItem is Antihero item )
            {
                item.auraPresence = 1f;
                item.auraFadeTimer = 30;
            }
        }

        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            behindNPCsAndTiles.Add(index);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D SwordTexture = TextureAssets.Projectile[Type].Value;
            //for (int i = 0; i < Projectile.oldPos.Length; i+=2)
            //{
            //    Main.EntitySpriteDraw(SwordTexture,
            //        (Projectile.oldPos[i] + Projectile.Size/2f) - Main.screenPosition,
            //        SwordTexture.Frame(),
            //        new Color(1f,0f,0f,0f) * (1f - (i / (float)Projectile.oldPos.Length)) * 0.5f,
            //        Projectile.rotation + MathHelper.PiOver4 * Projectile.spriteDirection,
            //        SwordTexture.Size() / 2,
            //        Projectile.scale + (float)((Math.Sin(Main.timeForVisualEffects / 10f) + 1f) / 10f),
            //        Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
            //}

            for (int i = 0; i < 4; i++)
            {
                
                Main.EntitySpriteDraw(glow.Value,
                    Projectile.Center - Main.screenPosition + Main.rand.NextVector2Circular(4,4),
                    glow.Frame(),
                    Color.Black * 0.5f,
                    Projectile.rotation + MathHelper.PiOver4 * Projectile.spriteDirection,
                    SwordTexture.Size() / 2,
                    Projectile.scale * Main.rand.NextFloat(0.8f, 1.2f),
                    Projectile.spriteDirection == 1? SpriteEffects.None : SpriteEffects.FlipHorizontally);
            }
            for (int i = 0; i < 4; i++)
            {

                Main.EntitySpriteDraw(glow.Value,
                    Projectile.Center - Main.screenPosition + Main.rand.NextVector2Circular(4, 4),
                    glow.Frame(),
                    Color.Lerp(new Color(0.5f, 0.2f, 0.8f, 0.5f) * 0.25f, new Color(1f, 0f, 0f, 0f), i / 4f) * 0.5f,
                    Projectile.rotation + MathHelper.PiOver4 * Projectile.spriteDirection,
                    SwordTexture.Size() / 2,
                    Projectile.scale * Main.rand.NextFloat(0.8f, 1.2f),
                    Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
            }

            Main.EntitySpriteDraw(SwordTexture,
                Projectile.Center - Main.screenPosition,
                SwordTexture.Frame(),
                lightColor,
                Projectile.rotation + MathHelper.PiOver4 * Projectile.spriteDirection,
                SwordTexture.Size() / 2,
                Projectile.scale,
                Projectile.spriteDirection == 1? SpriteEffects.None : SpriteEffects.FlipHorizontally);

            //Main.EntitySpriteDraw(SwordTexture,
            //    Projectile.Center - Main.screenPosition,
            //    SwordTexture.Frame(),
            //    new Color(1f, 1f, 1f, 0f) * 0.5f,
            //    Projectile.rotation + MathHelper.PiOver4 * Projectile.spriteDirection,
            //    SwordTexture.Size() / 2,
            //    Projectile.scale + (float)((Math.Sin(Main.timeForVisualEffects / 10f) + 1f) / 10f),
            //    Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally);

            return false;
        }

    }
}
