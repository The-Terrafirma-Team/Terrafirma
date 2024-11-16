using Microsoft.Xna.Framework;
using System;
using Terrafirma.Common.Players;
using Terrafirma.Common.Templates;
using Terrafirma.Data;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.CameraModifiers;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Projectiles.Melee.Knight
{
    public class SplinterSword : HeldProjectile
    {
        public override string Texture => "Terrafirma/Items/Weapons/Melee/Knight/SplinterSword";
        public override void SetStaticDefaults()
        {
            ProjectileSets.AutomaticallyGivenMeleeScaling[Type] = true;
            ProjectileSets.TrueMeleeProjectiles[Type] = true;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }
        public override void SetDefaults()
        {
            ProjectileID.Sets.TrailingMode[Type] = 2;
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
            if (Projectile.ai[0] == 0)
            {
                if (Terrafirma.ScreenshakeEnabled)
                {
                    PunchCameraModifier modifier = new PunchCameraModifier(Projectile.Center, Main.rand.NextVector2Unit(), 4, 8, 15, -1);
                    Main.instance.CameraModifiers.Add(modifier);
                }

                for (int i = 0; i < 3; i++)
                {
                    Projectile P = Projectile.NewProjectileDirect(player.GetSource_ItemUse(player.HeldItem), player.Center, player.Center.DirectionTo(target.Center + new Vector2(0, -40)).RotatedByRandom(0.1f) * Main.rand.NextFloat(3, 5), ModContent.ProjectileType<Splinter>(), (int)(Projectile.damage * 0.45f), 0, player.whoAmI, target.whoAmI);
                    if (Main.rand.NextBool())
                        P.timeLeft = (int)(60 * 16.5);
                }

                for (int i = 0; i < 15; i++)
                {
                    Dust d = Dust.NewDustPerfect(target.Hitbox.ClosestPointInRect(player.MountedCenter), DustID.RichMahogany, Main.rand.NextVector2Circular(5, 5));
                    d.alpha = 128;
                    d.scale = Main.rand.NextFloat(1f, 2f);
                    d.noGravity = true;
                }

                for (int i = 0; i < 15; i++)
                {
                    Dust d = Dust.NewDustPerfect(target.Hitbox.ClosestPointInRect(player.MountedCenter), DustID.RichMahogany, player.Center.DirectionTo(target.Center).RotatedByRandom(0.5f) * Main.rand.NextFloat(3, 7));
                    d.scale = Main.rand.NextFloat(0.5f, 1.5f);
                    d.noGravity = Main.rand.NextBool();
                }
                SoundEngine.PlaySound(new SoundStyle("Terrafirma/Sounds/SplinterHitBig"), player.position);
            }
            else
            {
                SoundEngine.PlaySound(new SoundStyle("Terrafirma/Sounds/SplinterHit_" + Main.rand.Next(4)) { PitchVariance = 0.2f }, player.position);
                if (Terrafirma.ScreenshakeEnabled)
                {
                    PunchCameraModifier modifier = new PunchCameraModifier(Projectile.Center, Main.rand.NextVector2Unit(), 2, 3, 10, -1);
                    Main.instance.CameraModifiers.Add(modifier);
                }
                for (int i = 0; i < 7; i++)
                {
                    Dust d = Dust.NewDustPerfect(target.Hitbox.ClosestPointInRect(player.MountedCenter), DustID.RichMahogany, player.Center.DirectionTo(target.Center).RotatedByRandom(0.5f) * Main.rand.NextFloat(2, 5));
                    d.scale = Main.rand.NextFloat(0.5f, 1.5f);
                    d.noGravity = Main.rand.NextBool();
                }
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
            switch (Projectile.ai[0])
            {
                case 1:
                    Projectile.rotation = MathHelper.Lerp(-1 * player.direction, 1.8f * player.direction, Easing.OutPow(Projectile.ai[1] / 30f, 5));
                    Projectile.spriteDirection = player.direction;
                    break;
                case 2:
                    Projectile.rotation = MathHelper.Lerp(2f * player.direction, -0.7f * player.direction, Easing.OutPow(Projectile.ai[1] / 30f, 2));
                    Projectile.spriteDirection = -player.direction;
                    break;
                case 3:
                    Projectile.rotation = MathHelper.Lerp(-1.4f * player.direction, 1.5f * player.direction, Easing.OutPow(Projectile.ai[1] / 30f, 6));
                    Projectile.spriteDirection = player.direction;
                    break;
                case 0:
                    Projectile.rotation = MathHelper.Lerp(3.4f * player.direction, -1.4f * player.direction, Easing.OutPow(Projectile.ai[1] / 30f, 3));
                    Projectile.spriteDirection = -player.direction;
                    break;
            }


            Projectile.Opacity = Easing.OutPow(Projectile.ai[1] / 30f, 6);
            if (Projectile.ai[1] > 30)
            {
                Projectile.Kill();
            }

            player.heldProj = Projectile.whoAmI;

            PlayerAnimation.ArmPointToDirection(Projectile.rotation * player.direction + 0.8f, player);

            Projectile.Center = player.RotatedRelativePoint(player.MountedCenter + player.getFrontArmPosition());
            Projectile.Center = new Vector2((int)Projectile.Center.X, (int)Projectile.Center.Y);
            if (player.direction == -1)
                Projectile.rotation -= MathHelper.PiOver2;
            Dust d = Dust.NewDustPerfect(Projectile.Center - new Vector2(66, 60).RotatedBy(Projectile.rotation + MathHelper.PiOver2) * Main.rand.NextFloat(), DustID.RichMahogany);
            d.alpha = 128;
            d.scale = Main.rand.NextFloat(2f) * (1f - Projectile.ai[1] / 30f);
            d.noGravity = true;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            for (int i = 0; i < Math.Min(ProjectileID.Sets.TrailCacheLength[Type], Projectile.localAI[0]); i++)
            {
                commonDiagonalItemDrawManualRotation(((lightColor * (1f - (i * 0.1f))) with { A = lightColor.A } * (0.5f - (i * 0.1f))) * (1f - (Projectile.ai[1] / 30f)) * Projectile.Opacity, TextureAssets.Projectile[Type], Projectile.scale * (1f - (i * 0.03f)), Projectile.oldRot[i] - (Projectile.spriteDirection == 1 ? 0 : MathHelper.PiOver2));
            }
            commonDiagonalItemDraw(lightColor * Projectile.Opacity, TextureAssets.Projectile[Type], Projectile.scale);
            return false;
        }
    }
}
