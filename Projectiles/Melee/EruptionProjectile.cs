using Terraria.ID;
using Terraria;
using Terrafirma.Common.Templates;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System;
using Terraria.Audio;
using ReLogic.Content;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terrafirma.Common;
using Terraria.Graphics.CameraModifiers;
using Terrafirma.Particles;

namespace Terrafirma.Projectiles.Melee
{
    public class EruptionProjectile: HeldProjectile
    {
        private static Asset<Texture2D> glowTex;
        public override void Load()
        {
            glowTex = Mod.Assets.Request<Texture2D>("Projectiles/Melee/EruptionProjectile_Glow");
        }
        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(64);
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.ai[2] >= 200 && !Main.getGoodWorld)
                return;
            if (Projectile.ai[1] == 200)
            {
                player.AddImmuneTime(ImmunityCooldownID.General, 60);
                player.immune = true;
            }

            for(int i = 0; i < 40; i++)
            {
                ParticleSystem.AddParticle(new HiResFlame() { SizeMultiplier = 3.8f,gravity = 1f }, Projectile.Center + Main.rand.NextVector2Circular(10, 10), Main.rand.NextVector2Circular(8,8), new Color(1f, Main.rand.NextFloat(0.3f,0.6f), 0.2f, 0f));
                ParticleSystem.AddParticle(new HiResFlame() { SizeMultiplier = 1.8f }, Projectile.Center + Main.rand.NextVector2Circular(10, 10), Main.rand.NextVector2Circular(8, 0), new Color(0.6f, 0.3f, 0.2f, 0f));
                if (Main.rand.NextBool(5))
                    ParticleSystem.AddParticle(new ColorDot() { Size = 0.3f}, Projectile.Center + Main.rand.NextVector2Circular(10, 10), Main.rand.NextVector2Circular(8, 8), new Color(1f, Main.rand.NextFloat(0.3f, 0.6f), 0.2f, 0f));

            }

            int projectileVomit = (int)(Projectile.ai[1] / 66);
            if (Main.player[Projectile.owner] == Main.LocalPlayer)
            {
                PunchCameraModifier modifier = new PunchCameraModifier(Projectile.Center, Main.rand.NextVector2Unit(), 8 * Projectile.ai[1] / 100, 10, 20, -1);
                Main.instance.CameraModifiers.Add(modifier);
                for (int i = -projectileVomit; i <= projectileVomit; i++)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(Projectile.ai[1] / 20 * Main.rand.NextFloat(0.4f, 1f)).RotatedBy(Projectile.rotation - MathHelper.PiOver2 + Main.rand.NextFloat(0.2f)).RotatedBy(MathHelper.Pi / 8 * i), ModContent.ProjectileType<EruptionFloatProjectile>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0, 0, 0);
                }
            }
            player.velocity += new Vector2(-20 * (Projectile.ai[1] / 200)).RotatedBy(Projectile.rotation - MathHelper.PiOver2);

            Projectile.ai[0] -= 20f;
            Projectile.ai[2] = 200;
            SoundEngine.PlaySound(SoundID.Item14);
            base.OnHitNPC(target, hit, damageDone);
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.FinalDamage += (Projectile.ai[1] / 100) - 1f;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (!stoppedChanneling)
                return false;
            return base.CanHitNPC(target);
        }
        public override void AI()
        {
            if (Projectile.ai[1] == 200)
            {
                ParticleSystem.AddParticle(new HiResFlame() {SizeMultiplier = 1.8f }, Projectile.Center + Main.rand.NextVector2Circular(10, 10), Main.rand.NextVector2Circular(2, 0), new Color(0.3f, 0.15f, 0.1f, 0f));
            }

            commonHeldLogic(2);
            float rotation;
            if (player.channel && !stoppedChanneling)
            {
                if (Projectile.ai[1] < 200)
                Projectile.ai[1]++;
                Projectile.ai[0] = MathHelper.Lerp(40, 10, MathHelper.Clamp(Projectile.ai[1] / 100,0,1));
                rotation = (player.MountedCenter - player.velocity + new Vector2(0, player.gfxOffY)).DirectionTo(Main.MouseWorld).ToRotation();
                Projectile.rotation = rotation + MathHelper.PiOver4;
                faceDirection = Main.MouseWorld.X - player.Center.X < 0 ? -1 : 1;

                if ((Projectile.ai[1] % 20 == 0 || Projectile.ai[1] == 0) && Projectile.ai[1] != 200)
                    SoundEngine.PlaySound(SoundID.Item8, Projectile.position);
            }
            else if (Projectile.ai[1] > 30)
            {
                if (!stoppedChanneling)
                {
                    player.velocity += new Vector2(10 * (Projectile.ai[1] / 200)).RotatedBy(Projectile.rotation - MathHelper.PiOver2);
                    SoundEngine.PlaySound(SoundID.DD2_FlameburstTowerShot, Projectile.position);

                    if (Main.player[Projectile.owner] == Main.LocalPlayer)
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(Projectile.ai[1] / 10).RotatedBy(Projectile.rotation - MathHelper.PiOver2), ModContent.ProjectileType<EruptionFloatProjectile>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0, 0, 0);

                    Projectile.timeLeft = 30;
                }

                stoppedChanneling = true;
                if (Projectile.ai[2] < 200)
                {
                    Projectile.ai[2] += 0.08f;
                    Projectile.ai[0] = (MathF.Sin((Projectile.ai[2] - MathHelper.Pi)) * -74) + 10;
                }
                else
                {
                    Projectile.ai[0] *= 0.9f;
                    if (Projectile.ai[0] < 10)
                        Projectile.Kill();
                }
                player.SetDummyItemTime(Projectile.timeLeft);
            }

            PlayerAnimation.ArmPointToDirectionWithoutUpOrDown(Projectile.rotation - MathHelper.PiOver4, player);
            Projectile.Center = player.MountedCenter.ToPoint().ToVector2() + new Vector2(Projectile.ai[0], -Projectile.ai[0]).RotatedBy(Projectile.rotation) + new Vector2(0, player.gfxOffY);
            Projectile.Center += player.getFrontArmPosition();
            player.direction = faceDirection;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Asset<Texture2D> tex = TextureAssets.Projectile[Type];

            float rotation = player.direction == 1 ? Projectile.rotation : (Projectile.rotation - MathHelper.PiOver2);
            SpriteEffects effect = player.direction == 1 ? SpriteEffects.None : SpriteEffects.FlipVertically;

            for (int i = 0; i < 8; i++)
            {
                Main.EntitySpriteDraw(glowTex.Value, Projectile.Center + Main.rand.NextVector2Circular(1,1) + new Vector2(MathHelper.Clamp(Projectile.ai[1] / 200, 0, 2)).RotatedBy((i * MathHelper.PiOver4) + (0.02f * Main.timeForVisualEffects)) - Main.screenPosition, null, new Color(1f, 0.3f, 0f, 0) * 0.3f * MathHelper.Clamp(Projectile.ai[1] / 200, 0, 1), rotation, new Vector2(85, player.direction == 1 ? 20 : 90), Projectile.scale, effect);
            }

            Main.EntitySpriteDraw(tex.Value, Projectile.Center - Main.screenPosition, null, lightColor, rotation, new Vector2(85,player.direction == 1 ? 20 : 90),Projectile.scale, effect);
            
            Main.EntitySpriteDraw(glowTex.Value, Projectile.Center + Main.rand.NextVector2Circular(1, 1) - Main.screenPosition, null, new Color(1f, 0.3f, 0f, 0) * 0.7f * MathHelper.Clamp(Projectile.ai[1] / 200, 0, 1), rotation, new Vector2(85, player.direction == 1 ? 20 : 90), Projectile.scale, effect);
            
            return false;
        }
    }

    public class EruptionFloatProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.timeLeft = 200;

            DrawOffsetX = -15;
            DrawOriginOffsetY = -15;

            Projectile.penetrate = 15;
            Projectile.tileCollide = false;
            Projectile.friendly = true;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 30;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.Lerp(new Color(255, 60, 0, 0), new Color(255, 128, 0, 0),(float)Math.Sin((Main.timeForVisualEffects + Projectile.whoAmI * 100) * 0.01f) * 0.5f + 0.5f) * Projectile.Opacity;
        }

        public override void AI()
        {
            double rot = Projectile.spriteDirection == 1 ? MathHelper.PiOver4 : MathHelper.PiOver2 + MathHelper.PiOver4;
            
            Projectile.spriteDirection = Math.Sign(Projectile.velocity.X);
            Projectile.velocity *= 0.96f;

            Dust d = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(15, 15), DustID.Torch, Main.rand.NextVector2Circular(1, 1) + -Projectile.velocity.LengthClamp(4) * 2);
            d.noGravity = true;
            d.fadeIn = Main.rand.NextFloat(0, 1.5f);

            if (Projectile.timeLeft > 60) Projectile.rotation = Projectile.velocity.ToRotation() + (float)rot;
            else
            {
                Projectile.velocity.Y += 0.1f;
                Projectile.Opacity = Projectile.timeLeft / 60f;
                Projectile.rotation += 0.01f * Projectile.direction * (Math.Abs(Projectile.timeLeft - 60) / 10f);
            }
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 30; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(15, 15), DustID.Torch, Main.rand.NextVector2Circular(6, 6) + -Projectile.velocity * 2, 0);
                d.noGravity = true;
                d.fadeIn = Main.rand.NextFloat(0, 1.5f);
            }
            SoundEngine.PlaySound(SoundID.Item73, Projectile.position);
        }

    }
}
