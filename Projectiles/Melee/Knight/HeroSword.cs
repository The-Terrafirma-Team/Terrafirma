using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Runtime.InteropServices;
using Terrafirma.Common;
using Terrafirma.Common.Templates;
using Terrafirma.Common.Templates.Melee;
using Terrafirma.Data;
using Terrafirma.Particles;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.CameraModifiers;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Projectiles.Melee.Knight
{
    public class HeroSword : MeleeSwing
    {
        private static Asset<Texture2D> slash;
        public override void SetStaticDefaults()
        {
            ProjectileSets.AutomaticallyGivenMeleeScaling[Type] = true;
            ProjectileSets.TrueMeleeProjectiles[Type] = true;
            slash = ModContent.Request<Texture2D>(Texture + "_Slash");
        }
        int swingTime = 66;
        public override void SetDefaults()
        {
            Projectile.QuickDefaults();
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.extraUpdates = 1;
        }
        public override int Length => 130;
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            for(int i = 0; i < 7; i++)
            {
                //ParticleSystem.AddParticle(new HiResFlame() { SizeMultiplier = 1.5f }, Main.rand.NextVector2FromRectangle(target.Hitbox), Main.rand.NextVector2Circular(3, 3), new Color(1f, 0f, Main.rand.NextFloat(), 0f) * 0.6f);
                ParticleSystem.AddParticle(new ImpactSparkle() { Scale = Main.rand.NextFloat(1.3f), LifeTime = Main.rand.Next(30), secondaryColor = new Color(1f,1f,1f,0f)}, target.Hitbox.ClosestPointInRect(Projectile.Center), new Vector2(0, Main.rand.NextFloat(12f) * Projectile.spriteDirection * player.direction).RotatedByRandom(0.3f) + new Vector2(player.direction * Main.rand.NextFloat(7), 0), Main.DiscoColor with { A = 0});
            }
            BigSparkle bigsparkle = new BigSparkle();
            bigsparkle.fadeInTime = 10;
            bigsparkle.smallestSize = 0.1f;
            bigsparkle.Rotation = Main.rand.NextFloat(-MathHelper.PiOver2, MathHelper.PiOver2);
            bigsparkle.Scale = 1.5f;
            bigsparkle.lengthMultiplier = 1.5f;
            ParticleSystem.AddParticle(bigsparkle, target.Hitbox.ClosestPointInRect(Projectile.Center), Vector2.Zero, new Color(Main.DiscoColor.R, Main.DiscoColor.G, Main.DiscoColor.B, 0));
            player.GiveTension(10);
        }
        public override void AI()
        {
            //Projectile.localAI[0]+= 0.5f;
            Projectile.ai[1] += player.GetAdjustedWeaponSpeedPercent(player.HeldItem);
            player.SetDummyItemTime(2);

            switch (Projectile.ai[0])
            {
                case 0:
                    Projectile.rotation = MathHelper.Lerp(-1 * player.direction, 2f * player.direction, Easing.OutPow(Projectile.ai[1] / swingTime, 5));
                    Projectile.spriteDirection = player.direction;
                    break;
                case 1:
                    //Projectile.ai[1] += player.GetAdjustedWeaponSpeedPercent(player.HeldItem) * 0.2f;
                    Projectile.rotation = MathHelper.Lerp(2f * player.direction, -1f * player.direction, Easing.OutPow(Projectile.ai[1] / swingTime, 3));
                    Projectile.spriteDirection = -player.direction;
                    break;
                case 2:
                    Projectile.rotation = MathHelper.Lerp(-4f * player.direction, 3f * player.direction, Easing.OutPow(Projectile.ai[1] / swingTime, 2));
                    Projectile.spriteDirection = player.direction;
                    Projectile.scale += player.GetAdjustedWeaponSpeedPercent(player.HeldItem) * 0.01f;
                    Projectile.ai[2] -= player.GetAdjustedWeaponSpeedPercent(player.HeldItem) * 0.01f;
                    break;
            }


            Projectile.Opacity = Easing.OutPow(Projectile.ai[1] / swingTime, 4);
            if (Projectile.ai[1] >= swingTime)
            {
                Projectile.Kill();
            }

            player.heldProj = Projectile.whoAmI;

            PlayerAnimation.ArmPointToDirection(Projectile.rotation * player.direction + 0.8f, player);

            Projectile.Center = player.RotatedRelativePoint(player.MountedCenter + player.getFrontArmPosition());
            Projectile.Center = new Vector2((int)Projectile.Center.X, (int)Projectile.Center.Y);
            if (player.direction == -1)
                Projectile.rotation -= MathHelper.PiOver2;

            for (int i = 0; i < 4 + Projectile.ai[0] * 2; i++)
            {
                if (Main.rand.NextBool(Math.Max((int)(Projectile.ai[1]),1)))
                {
                    BigSparkle bigsparkle = new BigSparkle();
                    bigsparkle.fadeInTime = 12;
                    bigsparkle.smallestSize = 0.1f;
                    bigsparkle.Rotation = Main.rand.NextFloat(-MathHelper.PiOver2, MathHelper.PiOver2);
                    bigsparkle.Scale = Main.rand.NextFloat();
                    bigsparkle.lengthMultiplier = Main.rand.NextFloat();
                    bigsparkle.secondaryColor *= 0.3f;
                    ParticleSystem.AddParticle(bigsparkle, Projectile.Center + new Vector2(Main.rand.NextFloat(100 * Projectile.scale)).RotatedBy(Projectile.rotation - MathHelper.PiOver2) + Main.rand.NextVector2Circular(4,4), new Vector2(-Main.rand.NextFloat(), Main.rand.NextFloat()).RotatedBy(Projectile.rotation - MathHelper.PiOver2) * Projectile.spriteDirection * (Projectile.ai[0] + 1f), new Color(Main.DiscoColor.R, Main.DiscoColor.G, Main.DiscoColor.B, 0) * 0.3f);

                }
            }
            Lighting.AddLight(Projectile.Center, new Vector3(Main.DiscoColor.R, Main.DiscoColor.G, Main.DiscoColor.B) / 128 * (1f - Projectile.Opacity));
        }
        public override bool PreDraw(ref Color lightColor)
        {
            float fade = Projectile.ai[1] / swingTime < 0.5f ? Projectile.ai[1] / 18f : 1f - Projectile.ai[1] / swingTime;
            //Main.NewText(fade, Color.Red);
            for (float i = 0; i < Projectile.Opacity * 4; i += 0.5f)
            {
                Main.EntitySpriteDraw(
                    slash.Value, 
                    Projectile.Center - Main.screenPosition, 
                    new Rectangle(0, slash.Height() / 4 * (int)i, slash.Width(), slash.Height() / 4),
                    //Color.Lerp(Main.DiscoColor with { A = 0} * Projectile.Opacity * 0.4f, new Color(1f,1f,1f,1f),i / 4f * fade) * fade * 0.5f, 
                    Color.Lerp(new Color(1f, 1f, 1f, 0f) * Projectile.Opacity * 0.4f, Main.DiscoColor with { A = 0 }, i / 2f * fade) * fade * 0.5f,
                    Projectile.rotation - MathHelper.PiOver4 + (-0.3f * Projectile.spriteDirection) - (i * Projectile.spriteDirection * (Projectile.ai[0] == 2? 4f : 1.4f) * MathF.Pow((1f - Projectile.ai[1] / swingTime),2)), 
                    new Vector2(slash.Width() / 2, slash.Height() / 8), 
                    Projectile.scale * 1.5f, 
                    Projectile.spriteDirection == 0 ? SpriteEffects.None : SpriteEffects.FlipVertically, 0);
            }
            commonDiagonalItemDraw(lightColor, TextureAssets.Projectile[Type], Projectile.scale + Projectile.ai[2]);
            commonDiagonalItemDraw(Main.DiscoColor with { A = 0}, TextureAssets.Projectile[Type], Projectile.scale + Projectile.ai[2]);
            commonDiagonalItemDraw(new Color(1f,1f,1f,0f), TextureAssets.Projectile[Type], Projectile.scale + Projectile.ai[2]);
            return false;
        }
    }
}
