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
    public class AntiheroSwing : MeleeSwing
    {
        private static Asset<Texture2D> slash;
        public override void SetStaticDefaults()
        {
            ProjectileSets.AutomaticallyGivenMeleeScaling[Type] = true;
            ProjectileSets.TrueMeleeProjectiles[Type] = true;
            slash = ModContent.Request<Texture2D>(Texture + "_Slash");
        }
        int swingTime = 36;
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
                ParticleSystem.AddParticle(new ImpactSparkle() { Scale = Main.rand.NextFloat(0.2f, 0.7f), LifeTime = Main.rand.Next(15, 30)}, target.Hitbox.ClosestPointInRect(Projectile.Center), new Vector2(0, Main.rand.NextFloat(5f) * Projectile.spriteDirection * player.direction).RotatedByRandom(0.3f) + new Vector2(player.direction * Main.rand.NextFloat(3), 0), Color.Black);
                ParticleSystem.AddParticle(new ImpactSparkle() { Scale = Main.rand.NextFloat(0.2f, 0.7f), LifeTime = Main.rand.Next(15, 30) }, target.Hitbox.ClosestPointInRect(Projectile.Center), new Vector2(0, Main.rand.NextFloat(1f, 7f) * Projectile.spriteDirection * player.direction).RotatedByRandom(0.3f) + new Vector2(player.direction * Main.rand.NextFloat(3), 0), new Color(1f,0f,Main.rand.NextFloat(),0f) * 0.5f);
            }
            for(int i = 0; i < 2; i++)
            {
                BigSparkle s = new BigSparkle();
                s.fadeInTime = Main.rand.NextFloat(8f,10f);
                s.smallestSize = 0.1f;
                s.Rotation = Main.rand.NextFloat(-MathHelper.PiOver2, MathHelper.PiOver2);
                s.Scale = Main.rand.NextFloat(0.5f, 1.5f);
                s.lengthMultiplier = 0.5f;
                s.secondaryColor = Color.Black;
                ParticleSystem.AddParticle(s, target.Hitbox.ClosestPointInRect(Projectile.Center) + Main.rand.NextVector2Circular(12,12), Vector2.Zero, new Color(1f, 0f, Main.rand.NextFloat(), 0f) * 0.5f);
            }
            BigSparkle bigsparkle = new BigSparkle();
            bigsparkle.fadeInTime = Main.rand.NextFloat(8f, 10f);
            bigsparkle.smallestSize = 0.1f;
            bigsparkle.Rotation = Main.rand.NextFloat(-MathHelper.PiOver2, MathHelper.PiOver2);
            bigsparkle.Scale = Main.rand.NextFloat(1.5f,2.5f);
            bigsparkle.lengthMultiplier = 0.7f;
            bigsparkle.secondaryColor = Color.Black;
            ParticleSystem.AddParticle(bigsparkle, target.Hitbox.ClosestPointInRect(Projectile.Center), Vector2.Zero, new Color(1f, 0f, Main.rand.NextFloat(), 0f) * 0.5f);
            player.GiveTension(5);
        }
        public override void AI()
        {
            Projectile.localAI[0]++;
            Projectile.ai[1] += player.GetAdjustedWeaponSpeedPercent(player.HeldItem);
            player.SetDummyItemTime(2);
            switch (Projectile.ai[0])
            {
                case 0:
                    Projectile.rotation = MathHelper.Lerp(-1 * player.direction, 3f * player.direction, Easing.OutPow(Projectile.ai[1] / swingTime, 5));
                    Projectile.spriteDirection = player.direction;
                    break;
                case 1:
                    //Projectile.ai[1] += player.GetAdjustedWeaponSpeedPercent(player.HeldItem) * 0.2f;
                    Projectile.rotation = MathHelper.Lerp(3.4f * player.direction, -2f * player.direction, Easing.OutPow(Projectile.ai[1] / swingTime, 3));
                    Projectile.spriteDirection = -player.direction;
                    break;
            }


            Projectile.Opacity = Easing.OutPow(Projectile.ai[1] / swingTime, 6);
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
        }
        public override bool PreDraw(ref Color lightColor)
        {
            float fade = Projectile.ai[1] / swingTime < 0.5f ? Projectile.ai[1] / 18f : 1f - Projectile.ai[1] / swingTime;
            //Main.NewText(fade, Color.Red);
            for (int i = 0; i < 4; i++)
            {
                Main.EntitySpriteDraw(slash.Value, Projectile.Center - Main.screenPosition, new Rectangle(0, slash.Height() / 4 * i, slash.Width(), slash.Height() / 8), Color.Black * fade * 0.5f, Projectile.rotation - MathHelper.PiOver4 - (i * Projectile.spriteDirection * 1f * (1f - Projectile.ai[1] / swingTime)), new Vector2(slash.Width() / 2, Projectile.spriteDirection == 1 ? slash.Height() / 8 : 0), Projectile.scale * 2f * Main.rand.NextFloat(0.8f, 1.2f), Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipVertically, 0);
            }
            for (int i = 0; i < 4; i++)
            {
                Main.EntitySpriteDraw(slash.Value, Projectile.Center - Main.screenPosition, new Rectangle(0, slash.Height() / 4 * i, slash.Width(), slash.Height() / 8), Color.Lerp(new Color(0.5f,0.2f,0.8f,0.5f) * 0.6f * fade,new Color(1f,0f,0f,0f), i /4f) * fade, Projectile.rotation - MathHelper.PiOver4 - (i * Projectile.spriteDirection * 1f * (1f - Projectile.ai[1] / swingTime)), new Vector2(slash.Width() / 2, Projectile.spriteDirection == 1 ? slash.Height() / 8 : 0), Projectile.scale * 2f * Main.rand.NextFloat(0.8f,1.1f), Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipVertically, 0);
            }
            commonDiagonalItemDraw(lightColor, TextureAssets.Projectile[Type], Projectile.scale);

            return false;
        }
    }
}
