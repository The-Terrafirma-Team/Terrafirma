using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terrafirma.Common;
using Terrafirma.Common.Templates;
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
    public class SteelGreatsword : HeldProjectile
    {
        private static Asset<Texture2D> Flame;
        private static Asset<Texture2D> FlameAfter;

        private float Charge { get => MathHelper.Clamp(player.PlayerStats().Tension / (float)player.ApplyTensionBonusScaling(60,false,true),0,1f); }
        public override void SetStaticDefaults()
        {
            ProjectileSets.AutomaticallyGivenMeleeScaling[Type] = true;
            ProjectileSets.TrueMeleeProjectiles[Type] = true;
            ProjectileID.Sets.TrailingMode[Type] = 2;
            Flame = ModContent.Request<Texture2D>(Texture + "_Flame");
            FlameAfter = ModContent.Request<Texture2D>(Texture + "_FlameAfter");
        }
        public override void SetDefaults()
        {
            Projectile.QuickDefaults();
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            return targetHitbox.IntersectsConeSlowMoreAccurate(player.MountedCenter, 90 * Projectile.scale, Projectile.rotation - MathHelper.PiOver4, 0.2f) && Projectile.ai[1] < 20;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            player.GiveTension(5);
            //if (Terrafirma.ScreenshakeEnabled)
            //{
            //    PunchCameraModifier modifier = new PunchCameraModifier(Projectile.Center, Main.rand.NextVector2Unit(), 1, 3, 10, -1);
            //    Main.instance.CameraModifiers.Add(modifier);
            //}
            for(int i = 0; i < 10 + (Charge * 0.5f); i++)
            {
                ParticleSystem.AddParticle(new ImpactSparkle() {Scale = Main.rand.NextFloat(0.2f,0.7f) + (Charge * 0.2f), LifeTime = Main.rand.Next(15,30), secondaryColor = new Color(1f,1f,0.3f,0f) * Charge }, target.Hitbox.ClosestPointInRect(Projectile.Center),new Vector2(0,Main.rand.NextFloat(2f,5f) * Projectile.spriteDirection * player.direction).RotatedByRandom(0.3f) + new Vector2(player.direction * Main.rand.NextFloat(3),0),Color.Lerp(new Color(1f, 1f, 1f, 0f) * Main.rand.NextFloat(0.5f, 1f),new Color(1f,Main.rand.NextFloat(0.7f),0f,0f),Charge));
            }
            if (Main.rand.NextFloat() < Charge * 0.5f)
            target.AddBuff(BuffID.OnFire3, 60 * 3);
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.HitDirectionOverride = player.direction;
        }
        public override void AI()
        {
            Projectile.localAI[0]++;
            Projectile.ai[1] += player.GetAdjustedWeaponSpeedPercent(player.HeldItem) * (1f + (Charge * 0.25f));
            player.SetDummyItemTime(2);
            int swingTime = 35;
            if (Projectile.ai[0] == 2)
                swingTime = 45;
            switch (Projectile.ai[0])
            {
                case 0:
                    Projectile.rotation = MathHelper.Lerp(-1 * player.direction, 3f * player.direction, Easing.OutPow(Projectile.ai[1] / swingTime, 5));
                    Projectile.spriteDirection = player.direction;
                    break;
                case 1:
                    Projectile.ai[1] += player.GetAdjustedWeaponSpeedPercent(player.HeldItem) * 0.2f;
                    Projectile.rotation = MathHelper.Lerp(3.4f * player.direction, -2f * player.direction, Easing.OutPow(Projectile.ai[1] / swingTime, 3));
                    Projectile.spriteDirection = -player.direction;
                    break;
                case 2:
                    Projectile.rotation = MathHelper.Lerp(-3 * player.direction, 3f * player.direction, Easing.OutPow(Projectile.ai[1] / swingTime, 5));
                    Projectile.spriteDirection = player.direction;
                    break;
            }


            Projectile.Opacity = Easing.OutPow(Projectile.ai[1] / swingTime, 6);
            if (Projectile.ai[1] > swingTime)
            {
                Projectile.Kill();
            }

            player.heldProj = Projectile.whoAmI;

            PlayerAnimation.ArmPointToDirection(Projectile.rotation * player.direction + 0.8f, player);

            Projectile.Center = player.RotatedRelativePoint(player.MountedCenter + player.getFrontArmPosition());
            Projectile.Center = new Vector2((int)Projectile.Center.X, (int)Projectile.Center.Y);
            if (player.direction == -1)
                Projectile.rotation -= MathHelper.PiOver2;

            for (int i = 0; i < (Easing.OutPow(Projectile.ai[1] / swingTime, 3) * 15); i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center + new Vector2(0, Main.rand.NextFloat(100)).RotatedBy(Projectile.rotation + MathHelper.PiOver4 + MathHelper.Pi), DustID.Torch);
                d.noGravity = !Main.rand.NextBool(17);
                d.customData = 0;
                d.velocity = new Vector2(Projectile.spriteDirection * -Main.rand.NextFloat(6), 0).RotatedBy(Projectile.rotation + MathHelper.PiOver4 + MathHelper.Pi) * (1f - Easing.OutPow(Projectile.ai[1] / swingTime, 3));
                d.scale *= Charge * (1f - Easing.OutPow(Projectile.ai[1] / swingTime, 3)) * 2;
                if (Main.rand.NextBool(20))
                {
                    Dust d2 = Dust.NewDustPerfect(Projectile.Center + new Vector2(0, Main.rand.NextFloat(100)).RotatedBy(Projectile.rotation + MathHelper.PiOver4 + MathHelper.Pi), DustID.Wraith);
                    d2.noGravity = true;
                    d2.fadeIn = 1f;
                    d2.velocity = new Vector2(Projectile.spriteDirection * -Main.rand.NextFloat(12), 0).RotatedBy(Projectile.rotation + MathHelper.PiOver4 + MathHelper.Pi) * (1f - Easing.OutPow(Projectile.ai[1] / swingTime, 3));
                    d2.scale *= Charge * (1f - Easing.OutPow(Projectile.ai[1] / swingTime, 3));
                    d2.alpha = 200;
                }
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            for (int i = 0; i < Math.Min(ProjectileID.Sets.TrailCacheLength[Type], Projectile.localAI[0]); i++)
            {
                commonDiagonalItemDrawManualRotation(((Color.White * (1f - (i * 0.1f))) with { A = 0 } * (0.5f - (i * 0.1f))) * (1f - (Projectile.ai[1] / 30f)) * Projectile.Opacity * (1f - Charge), TextureAssets.Projectile[Type], Projectile.scale * (1f - (i * 0.03f)), Projectile.oldRot[i] - (Projectile.spriteDirection == 1 ? 0 : MathHelper.PiOver2));
                commonDiagonalItemDrawManualRotation(((Color.White * (1f - (i * 0.1f))) with { A = 0 } * (Charge - (i * 0.1f))) * (1f - (Projectile.ai[1] / 30f)) * Projectile.Opacity * Charge, FlameAfter, Projectile.scale * (1f - (i * 0.03f)), Projectile.oldRot[i] - (Projectile.spriteDirection == 1 ? 0 : MathHelper.PiOver2));

            }
            commonDiagonalItemDraw(lightColor * Projectile.Opacity, TextureAssets.Projectile[Type], Projectile.scale);
            commonDiagonalItemDraw(Color.Lerp(lightColor,Color.White,0.5f) * Projectile.Opacity * Charge, FlameAfter, Projectile.scale);
            commonDiagonalItemDraw(new Color(1f,1f,1f,0f) * Projectile.Opacity * Charge * 0.5f, FlameAfter, Projectile.scale * (1f + (Main.masterColor * 0.1f * Charge)));
            return false;
        }
    }
}
