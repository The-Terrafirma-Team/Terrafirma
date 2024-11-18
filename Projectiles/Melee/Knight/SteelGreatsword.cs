using Microsoft.Xna.Framework;
using System;
using Terrafirma.Common.Players;
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
        public override string Texture => "Terrafirma/Items/Weapons/Melee/Knight/SteelGreatsword";
        public override void SetStaticDefaults()
        {
            ProjectileSets.AutomaticallyGivenMeleeScaling[Type] = true;
            ProjectileSets.TrueMeleeProjectiles[Type] = true;
            ProjectileID.Sets.TrailingMode[Type] = 2;

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
            for(int i = 0; i < 10; i++)
            {
                ParticleSystem.AddParticle(new ImpactSparkle() {Scale = Main.rand.NextFloat(0.2f,0.7f), LifeTime = Main.rand.Next(15,30) }, target.Hitbox.ClosestPointInRect(Projectile.Center),new Vector2(0,Main.rand.NextFloat(2f,5f) * Projectile.spriteDirection * player.direction).RotatedByRandom(0.3f) + new Vector2(player.direction * Main.rand.NextFloat(3),0),new Color(1f,1f,1f,0f) * Main.rand.NextFloat(0.5f,1f));
            }
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.HitDirectionOverride = player.direction;
        }
        public override void AI()
        {
            Projectile.localAI[0]++;
            Projectile.ai[1] += player.GetAdjustedWeaponSpeedPercent(player.HeldItem);
            player.SetDummyItemTime(2);
            int swingTime = 35 * (Projectile.extraUpdates + 1);
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
        }
        public override bool PreDraw(ref Color lightColor)
        {
            for (int i = 0; i < Math.Min(ProjectileID.Sets.TrailCacheLength[Type], Projectile.localAI[0]); i++)
            {
                commonDiagonalItemDrawManualRotation(((Color.White * (1f - (i * 0.1f))) with { A = 0 } * (0.5f - (i * 0.1f))) * (1f - (Projectile.ai[1] / 30f)) * Projectile.Opacity, TextureAssets.Projectile[Type], Projectile.scale * (1f - (i * 0.03f)), Projectile.oldRot[i] - (Projectile.spriteDirection == 1 ? 0 : MathHelper.PiOver2));
            }
            commonDiagonalItemDraw(lightColor * Projectile.Opacity, TextureAssets.Projectile[Type], Projectile.scale);
            return false;
        }
    }
}
