using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
using TerrafirmaRedux.Particles;

namespace TerrafirmaRedux.Reworks.VanillaMagic.Projectiles
{
    #region Homing Amethyst
    public class HomingAmethyst : ModProjectile
    {
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.AmethystBolt}";
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.AmethystBolt);
            AIType = ProjectileID.AmethystBolt;
            Projectile.Size = new Vector2(16);
        }
        public override void AI()
        {
            NPC target = Utils.FindClosestNPC(200, Projectile.Center);
            if (target != null && target.active)
            {
                Projectile.velocity = Vector2.Lerp(Projectile.velocity += Projectile.Center.DirectionTo(target.Center) * 0.1f, Projectile.Center.DirectionTo(target.Center) * Projectile.velocity.Length(), 0.1f);
            }
            Projectile.velocity = Projectile.velocity.LengthClamp(5);
        }
    }
    #endregion

    #region Splitting Topaz
    public class SplittingTopaz : ModProjectile
    {
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.TopazBolt}";
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.TopazBolt);
            AIType = ProjectileID.TopazBolt;
            Projectile.ai[2] = 0;
            Projectile.Size = new Vector2(16);
        }

        public override void AI()
        {
            Projectile.ai[0]++;

            if (Projectile.ai[0] > 20 && Projectile.ai[2] == 0)
            {
                for (int i = -1; i < 2; i++)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, Projectile.velocity.RotatedBy(20 * (Math.PI / 180) * i), Projectile.type, (int)(Projectile.damage * 0.4f), Projectile.knockBack, Projectile.owner, 0, 0, 2);

                }
                Projectile.Kill();
            }
        }
    }
    #endregion

    #region Piercing Emerald
    public class PiercingEmerald : ModProjectile
    {
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.EmeraldBolt}";
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.EmeraldBolt);
            AIType = ProjectileID.EmeraldBolt;
            Projectile.penetrate = 6;
            Projectile.tileCollide = false;
            Projectile.Size = new Vector2(16);
        }
    }
    #endregion

    #region Exploding Ruby
    public class ExplodingRuby : ModProjectile
    {
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.RubyBolt}";
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.RubyBolt);
            AIType = ProjectileID.RubyBolt;
            Projectile.penetrate = 1;
            Projectile.Size = new Vector2(16);
        }

        public override void AI()
        {
            Projectile.velocity *= 0.985f;

            if (Projectile.velocity.Length() < 1)
            {
                Projectile.Kill();
            }

        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 20; i++)
            {
                Dust newdust = Dust.NewDustPerfect(Projectile.position, DustID.GemRuby, new Vector2(Main.rand.NextFloat(-5.8f, 5.8f), Main.rand.NextFloat(-5.8f, 5.8f)), 0, Color.White, Main.rand.NextFloat(1.7f, 2f));
                newdust.noGravity = true;
            }
            Projectile.Explode(100);
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
        }
    }
    #endregion

    #region Diamond Turret
    public class DiamondTurret : ModProjectile
    {
        public override string Texture => $"TerrafirmaRedux/Reworks/VanillaMagic/Projectiles/DiamondTurret";
        public override void SetDefaults()
        {
            Projectile.penetrate = -1;
            Projectile.timeLeft = 600;
            Projectile.Size = new Vector2(16);
        }

        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }

        public override void AI()
        {
            Projectile.ai[0]++;
            Projectile.velocity *= 0.98f;
            if (Projectile.ai[0] % 90 == 0 && Utils.FindClosestNPC(600f, Projectile.position) != null)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Projectile.Center.DirectionTo(Utils.FindClosestNPC(600f, Projectile.position).Center) * 10f, ProjectileID.DiamondBolt, Projectile.damage, Projectile.knockBack, Projectile.owner, 0, 0, 0);
            }
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Tink, Projectile.Center);
            for (int i = 0; i < 15; i++)
            {
                Dust newdust = Dust.NewDustPerfect(Projectile.Center, DustID.GemDiamond, new Vector2(Main.rand.NextFloat(-4f, 4f), Main.rand.NextFloat(-4f, 4f)), 0, Color.White, Main.rand.NextFloat(1f, 1.3f));
                newdust.noGravity = true;
            }
        }
    }
    #endregion

    #region Amber Wall Crystal
    public class AmberWallCrystal : ModProjectile
    {
        public override string Texture => $"TerrafirmaRedux/Reworks/VanillaMagic/Projectiles/AmberCrystal";
        public override void SetDefaults()
        {
            Projectile.tileCollide = false;
            Projectile.Size = new Vector2(16);
            Projectile.timeLeft = 90;
            Projectile.penetrate = 1;
            Projectile.friendly = true;
        }

        public override void AI()
        {
            Projectile.ai[0]++;
            Projectile.velocity *= 0.98f;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            if (Projectile.ai[0] % 2 == 0)
            {
                Dust newdust = Dust.NewDustPerfect(Projectile.Center, DustID.AmberBolt, Vector2.Zero, 0, default, Main.rand.NextFloat(0.9f, 1.4f));
                newdust.noGravity = true;
            }


        }

        public override void OnKill(int timeLeft)
        {
            Vector2 length = Main.player[Projectile.owner].MountedCenter - Projectile.Center;
            for (int i = -3; i < 4; i++)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Main.player[Projectile.owner].MountedCenter - length.RotatedBy(0.05f * i), Vector2.Zero, ModContent.ProjectileType<AmberWall>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0, 0, 1);
            }
            SoundEngine.PlaySound(SoundID.Tink, Projectile.Center);
        }
    }
    #endregion

    #region Amber Wall
    public class AmberWall : ModProjectile
    {
        float randfall = 0f;
        public override string Texture => $"TerrafirmaRedux/Reworks/VanillaMagic/Projectiles/AmberWall";
        public override void SetDefaults()
        {
            Projectile.penetrate = 12;
            Projectile.tileCollide = false;
            DrawOffsetX = -5;
            DrawOriginOffsetY = -5;
            Projectile.Size = new Vector2(16);
            Projectile.timeLeft = 400;
            Projectile.friendly = true;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 40;
        }

        public override void OnSpawn(IEntitySource source)
        {
            randfall = Main.rand.NextFloat(0.05f, 0.15f);
            for (int j = 0; j < 5; j++)
            {
                Dust newdust = Dust.NewDustPerfect(Projectile.Center, DustID.GemAmber, new Vector2(Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f)), 0, Color.White, Main.rand.NextFloat(1f, 1.3f));
                newdust.noGravity = true;
            }

            Projectile.scale = Main.rand.NextFloat(0.7f, 1.3f);
        }

        public override void AI()
        {
            if (Projectile.timeLeft < 30)
            {
                Projectile.velocity.Y += randfall;
            }
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Tink, Projectile.Center);
            for (int j = 0; j < 5; j++)
            {
                Dust newdust = Dust.NewDustPerfect(Projectile.Center, DustID.GemAmber, new Vector2(Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f)), 0, Color.White, Main.rand.NextFloat(1f, 1.3f));
                newdust.noGravity = true;
            }
        }
    }
    #endregion

    #region ColoredPrism
    public class ColoredPrism : ModProjectile
    {
        Color ShotColor = new Color(Main.DiscoColor.R, Main.DiscoColor.G, Main.DiscoColor.B, 0);
        public override string Texture => $"TerrafirmaRedux/Reworks/VanillaMagic/Projectiles/RainbowShot";

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Projectile.type] = 4;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;
        }
        public override void SetDefaults()
        {
            Projectile.penetrate = 3;
            Projectile.tileCollide = true;
            Projectile.friendly = true;

            Projectile.timeLeft = 300;
            Projectile.Opacity = 0f;

            Projectile.Size = new Vector2(10);
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D RainbowShot = ModContent.Request<Texture2D>("TerrafirmaRedux/Reworks/VanillaMagic/Projectiles/RainbowShot").Value;

            for (int i = 0; i < 5; i++)
            {
                Main.EntitySpriteDraw(RainbowShot, Projectile.oldPos[i] + Projectile.Size / 2 - Main.screenPosition, new Rectangle(0, 0, RainbowShot.Width, RainbowShot.Height), ShotColor * (Projectile.Opacity - (i * 0.2f) - 0.3f), Projectile.oldRot[i], RainbowShot.Size() / 2, 1, SpriteEffects.None, 0);
            }
            Main.EntitySpriteDraw(RainbowShot, Projectile.Center - Main.screenPosition, new Rectangle(0, 0, RainbowShot.Width, RainbowShot.Height), ShotColor * Projectile.Opacity, Projectile.rotation, RainbowShot.Size() / 2, 1, SpriteEffects.None, 0);
            Main.EntitySpriteDraw(RainbowShot, Projectile.Center - Main.screenPosition, new Rectangle(0, 0, RainbowShot.Width, RainbowShot.Height), new Color(1f, 1f, 1f, 0f) * 0.4f * Projectile.Opacity, Projectile.rotation, RainbowShot.Size() / 2, 1, SpriteEffects.None, 0);
            return false;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Projectile.Opacity += 0.075f;

            if (Projectile.timeLeft == 300)
            {
                ParticleSystem.AddParticle(new BigSparkle(), Projectile.Center + Vector2.Normalize(Projectile.velocity) * 46f, Vector2.Zero, ShotColor, 0, 10, 1, 1f, Main.rand.NextFloat(-0.1f, 0.1f));
            }

            if (Main.rand.NextBool(10))
            {
                ParticleSystem.AddParticle(new BigSparkle(), Projectile.Center, Vector2.Zero, ShotColor * 0.3f, 0, 8, Main.rand.NextFloat(0.3f, 0.8f), 1f, Main.rand.NextFloat(-MathHelper.PiOver2, MathHelper.PiOver2));
            }
        }

        public override void OnKill(int timeLeft)
        {
            ParticleSystem.AddParticle(new BigSparkle(), Projectile.Center, Vector2.Zero, ShotColor, 0, 10, 1, 1f, Main.rand.NextFloat(-MathHelper.PiOver2, MathHelper.PiOver2));
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            ParticleSystem.AddParticle(new BigSparkle(), Projectile.Center, Vector2.Zero, ShotColor, 0, 10, 1, 1f, Main.rand.NextFloat(-MathHelper.PiOver2, MathHelper.PiOver2));
        }
    }
    #endregion

    #region Ichor Bubble
    public class IchorBubble : ModProjectile
    {
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(1f, 1f + (float)Math.Sin(Projectile.timeLeft * 0.14f) * 0.3f, 1f + (float)Math.Sin(Projectile.timeLeft * 0.12f) * 0.3f, 0.4f) * (0.8f + (float)Math.Sin(Projectile.timeLeft * 0.1f) * 0.2f);
        }
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.ToxicBubble);
            Projectile.timeLeft = 150;
            DrawOffsetX = 5;
            DrawOriginOffsetY = 5;
        }
        public override void AI()
        {
            if (Projectile.ai[0] == 0)
                Projectile.ai[0] = 1;

            Projectile.ai[0] *= 1.02f;
            Projectile.scale = 1 + (float)Math.Sin(Projectile.ai[0] * 0.1f) * 0.1f;

            Dust d = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(10, 10), DustID.Ichor, Projectile.velocity, 0);
            d.noGravity = true;
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item14);
            for (int i = 0; i < 24; i++)
            {
                float rot = MathHelper.TwoPi / 24 * Main.rand.NextFloat(0.9f, 1.1f) * i;
                Dust d = Dust.NewDustPerfect(Projectile.Center + new Vector2(0, 10).RotatedBy(rot), DustID.Ichor, new Vector2(0, Main.rand.NextFloat(2, 4)).RotatedBy(rot));
                d.noGravity = !Main.rand.NextBool(6);
            }
            for (int i = 0; i < 12; i++)
            {
                Dust d2 = Dust.NewDustPerfect(Projectile.Center, DustID.Torch, Main.rand.NextVector2Circular(6, 6));
                d2.noGravity = true;
                d2.fadeIn = 1.2f;
            }
            Projectile.Explode(100);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Ichor, 60 * 17);
            if (Main.rand.NextBool())
            {
                target.AddBuff(BuffID.OnFire3, 60 * 6);
            }
        }
    }
    #endregion

    #region Water Geyser
    public class WaterGeyser : ModProjectile
    {
        public override string Texture => $"TerrafirmaRedux/Reworks/VanillaMagic/Projectiles/WaterGeyser";

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 8;
        }

        Vector2 OriginalPos;
        bool findtile = false;
        public override void SetDefaults()
        {
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.Size = new Vector2(28, 20);
            Projectile.timeLeft = 400;
            Projectile.Opacity = 0;
            Projectile.friendly = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override bool? CanHitNPC(NPC target)
        {
            if (Projectile.ai[2] % 2 == 0)
            {
                return base.CanHitNPC(target);
            }
            return false;

        }
        public override void AI()
        {

            Projectile.position.X += (float)Math.Sin(Projectile.ai[0] / 10) / 4;
            if (Projectile.timeLeft > 390)
            {
                Projectile.Opacity += 1 / 10f;
            }
            else if (Projectile.timeLeft < 60)
            {
                Projectile.Opacity -= 1 / 60f;
            }


            if (Projectile.ai[2] == 0 && !findtile)
            {
                float SetPos = 0;
                for (int i = 300; i > 0; i -= 4)
                {
                    if (Collision.SolidCollision(Projectile.Center + new Vector2(0, i * 2), Projectile.width, Projectile.height, true))
                    {
                        SetPos = new Vector2(0, Projectile.Bottom.Y + i * 2).ToTileCoordinates().Y * 16;
                    }
                }
                Projectile.position.Y = SetPos;
                findtile = true;
            }

            if (Projectile.ai[0] == 0)
            {
                OriginalPos = Projectile.Center;
            }

            if (Projectile.ai[0] % 3 == 0)
            {
                if (Projectile.frame == 3 + Projectile.ai[1] * 4)
                {
                    Projectile.frame = 0 + (int)(Projectile.ai[1] * 4);

                }
                else { Projectile.frame++; }
            }

            if (Projectile.ai[0] % 5 == 0)
            {

                Dust newdust = Dust.NewDustDirect(Projectile.Center, Projectile.width / 2, Projectile.height / 2, DustID.DungeonWater, 0, -2f, Projectile.alpha, Color.White, Projectile.Opacity);
                newdust.velocity.X *= 0.3f;
                newdust.noGravity = Main.rand.NextBool();
            }


            if (Projectile.ai[0] == 4 && Projectile.ai[2] <= 10)
            {
                if (Collision.SolidCollision(OriginalPos + new Vector2(0, -Projectile.height + 4), Projectile.width, Projectile.height))
                {
                    Projectile newproj = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), OriginalPos + new Vector2(0, -Projectile.height + 6), Vector2.Zero, ModContent.ProjectileType<WaterGeyser>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0, 1, 11);
                    newproj.frame = Projectile.frame - 1;
                }
                else
                {
                    if (Projectile.ai[2] < 10)
                    {
                        Projectile newproj = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), OriginalPos + new Vector2(0, -Projectile.height + 4), Vector2.Zero, ModContent.ProjectileType<WaterGeyser>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0, 0, Projectile.ai[2] + 1);
                        newproj.frame = Projectile.frame - 1;

                    }
                    else if (Projectile.ai[2] == 10)
                    {
                        Projectile newproj = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), OriginalPos + new Vector2(0, -Projectile.height + 8), Vector2.Zero, ModContent.ProjectileType<WaterGeyser>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0, 1, Projectile.ai[2] + 1);
                        newproj.frame = Projectile.frame - 1;

                    }
                }



            }

            for (int i = 0; i < Main.player.Length; i++)
            {
                if (Main.player[i].Hitbox.Intersects(Projectile.Hitbox) && Main.player[i].velocity.Y > -45f)
                {
                    Main.player[i].AddBuff(BuffID.Wet, 120);
                    if (Math.Abs(Main.player[i].velocity.X) > 2f)
                    {
                        Main.player[i].velocity.Y -= Math.Abs(Main.player[i].velocity.X) / 20;
                    }
                    else
                    {
                        Main.player[i].velocity.Y -= 0.2f;
                    }
                }
            }
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].Hitbox.Intersects(Projectile.Hitbox) && Main.npc[i].velocity.Y > -45f && Main.npc[i].friendly)
                {
                    Main.npc[i].AddBuff(BuffID.Wet, 120);
                    if (Math.Abs(Main.npc[i].velocity.X) > 2f)
                    {
                        Main.npc[i].velocity.Y -= Math.Abs(Main.npc[i].velocity.X) / 20;
                    }
                    else
                    {
                        Main.npc[i].velocity.Y -= 0.2f;
                    }
                }
            }

            Projectile.ai[0]++;

        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            fallThrough = false;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
        }
    }
    #endregion

    #region Aurawave
    public class AuraWave : ModProjectile
    {
        public override string Texture => $"TerrafirmaRedux/Reworks/VanillaMagic/Projectiles/AuraWave";

        Vector2 playerpos = Vector2.Zero;
        public override void SetDefaults()
        {
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.Size = new Vector2(10, 10);
            Projectile.timeLeft = 400;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override void AI()
        {
            Projectile.ai[0]++;


            if (Projectile.ai[2] == 0)
            {
                for (int i = 0; i < 16; i++)
                {
                    Projectile newproj = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Main.player[Projectile.owner].MountedCenter, new Vector2(3f, 0f).RotatedBy(Math.PI / 8f * i), Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0, 0, 1);
                    newproj.Opacity = 0.5f;
                    newproj.timeLeft = 60;
                }
                Projectile.Kill();

            }
            else
            {
                Projectile.Opacity = Projectile.timeLeft / 60f;
                Projectile.velocity = Projectile.velocity * 0.95f + ((Projectile.Center - playerpos) - (Projectile.Center - playerpos).RotatedBy(0.01f));
                Projectile.velocity *= 0.95f;
                Projectile.rotation = Projectile.velocity.ToRotation() - MathHelper.PiOver2;

                //Projectile.velocity = Projectile.velocity.RotatedBy(0.08f);


                if (Projectile.ai[0] % 2 == 0)
                {
                    Dust newdust = Dust.NewDustPerfect(Projectile.Center + new Vector2(5, 0).RotatedBy(Projectile.velocity.ToRotation()), DustID.DungeonWater, Vector2.Zero, Projectile.alpha, Color.White, 1);
                    newdust.noGravity = !Main.rand.NextBool(8);
                }
            }
        }

        public override void OnSpawn(IEntitySource source)
        {
            playerpos = Main.player[Projectile.owner].MountedCenter;
        }
    }
    #endregion

    #region Bone Fragment
    public class BoneFragment : ModProjectile
    {
        public override string Texture => $"TerrafirmaRedux/Reworks/VanillaMagic/Projectiles/BoneFragments";

        Vector2 playerpos = Vector2.Zero;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 4;
        }
        public override void SetDefaults()
        {
            Projectile.penetrate = 1;
            Projectile.Size = new Vector2(14, 14);
            Projectile.timeLeft = 200;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            Projectile.ai[0]++;
            if (Projectile.ai[0] > 20)
            {
                Projectile.velocity.Y += 0.3f;
            }
        }

        public override void OnSpawn(IEntitySource source)
        {
            Projectile.frame = Main.rand.Next(Main.projFrames[Type]);
            for (int i = 0; i < 2; i++)
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.Bone, Projectile.velocity / 2 + new Vector2(Main.rand.NextFloat(-3f, 1f), Main.rand.NextFloat(-1f, 1f)).RotatedBy(Projectile.velocity.ToRotation()), 0, Color.White, 1);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.TileCollision(Projectile.position, Projectile.velocity / 2, Projectile.width, Projectile.height);
            return base.OnTileCollide(oldVelocity);
        }
        public override void OnKill(int timeLeft)
        {
            SoundStyle bonesound = SoundID.NPCHit2;
            bonesound.Volume = 0.2f;
            bonesound.PitchRange = (-0.2f, 0.2f);
            SoundEngine.PlaySound(bonesound, Projectile.position);
            for (int i = 0; i < 4; i++)
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.Bone, Projectile.velocity / 2 + new Vector2(Main.rand.NextFloat(-3f, 1f), Main.rand.NextFloat(-1f, 1f)).RotatedBy(Projectile.velocity.ToRotation()), 0, Color.White, 1);
            }
        }
    }
    #endregion

    #region Healing Bubble
    public class HealingBubble : ModProjectile
    {
        public override string Texture => $"TerrafirmaRedux/Reworks/VanillaMagic/Projectiles/HealingBubble";
        public override void SetDefaults()
        {
            Projectile.penetrate = 1;
            Projectile.Size = new Vector2(22, 22);
            Projectile.timeLeft = 200;
            Projectile.friendly = false;
        }

        public override void OnSpawn(IEntitySource source)
        {
            Projectile.velocity.Y *= 0.2f;
        }
        public override void AI()
        {
            Projectile.ai[0]++;
            Projectile.velocity.Y -= 0.05f;
            Projectile.velocity.X *= 0.98f;

            if (Projectile.ai[0] % 2 == 0)
            {
                Dust newdust = Dust.NewDustPerfect(Projectile.position + new Vector2(Main.rand.Next(23), Main.rand.Next(23)), DustID.DungeonWater, Vector2.Zero, 0, Color.White, 1f);
            }

            for (int i = 0; i < Main.player.Length; i++)
            {
                if (Projectile.Hitbox.Intersects(Main.player[i].Hitbox) && 
                    Main.player[i].team == Main.player[Projectile.owner].team &&
                    Main.player[i] != Main.player[Projectile.owner])
                {
                    Main.player[i].Heal(8);
                    Projectile.Kill();
                }
                    
            }
            

        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust newdust = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(10f, 10f), DustID.DungeonWater, Main.rand.NextVector2Circular(5f, 5f), 0, Color.White, 1.25f);
            }
        }

    }
    #endregion

    #region SkeletonHand
    public class SkeletonHand : ModProjectile
    {
        public override string Texture => $"TerrafirmaRedux/Reworks/VanillaMagic/Projectiles/SkeletonHand";

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 2;
        }

        Vector2 OriginalPos = Vector2.Zero;
        public override void SetDefaults()
        {
            Projectile.penetrate = -1;
            Projectile.Size = new Vector2(0, 0);
            Projectile.timeLeft = 400;
            Projectile.friendly = false;
            Projectile.DamageType = DamageClass.Magic;
            DrawOffsetX = -18;
            DrawOriginOffsetY = -28;
        }
        public override void AI()
        {


            if (Projectile.ai[0] == 0)
            {
                Projectile.frame = 0;
                Projectile.velocity = new Vector2(0, -5f);
                Projectile.position = Main.MouseWorld;
                OriginalPos = Projectile.Center + new Vector2(14, 0);
            }
            else if (Projectile.ai[0] < 60)
            {
                Projectile.velocity.Y = Math.Clamp(Projectile.velocity.Y + 0.1f, -5f, 0f);
            }
            else
            {
                Projectile.frame = 1;
                NPC[] AreaNPCs = new NPC[] { };
                if (Projectile.ai[0] % 40 == 0 && AreaNPCs != null)
                {
                    AreaNPCs = Utils.GetAllNPCsInArea(400f, Projectile.Center + new Vector2(18, 28));
                    for (int i = 0; i < AreaNPCs.Length; i++)
                    {
                        NPC.HitInfo hitinfo = new NPC.HitInfo();

                        hitinfo.Damage = (int)(Projectile.damage / 3f);
                        hitinfo.Knockback = 0f;
                        hitinfo.DamageType = DamageClass.Magic;

                        AreaNPCs[i].StrikeNPC(hitinfo);
                        NetMessage.SendStrikeNPC(AreaNPCs[i], hitinfo, 1);

                        for (int j = 0; j < Projectile.Center.Distance(AreaNPCs[i].Center) / 2; j++)
                        {
                            Dust newflame = Dust.NewDustDirect(
                                Projectile.Center + (Projectile.Center.DirectionTo(AreaNPCs[i].Center) * (j * 2f)) +
                                new Vector2(0, (float)Math.Sin(j / 4f) * 10f).RotatedBy(Projectile.Center.DirectionTo(AreaNPCs[i].Center).ToRotation()),
                                1,
                                1,
                                DustID.Torch,
                                0,
                                0,
                                0,
                                new Color(255, 0, 255, 0),
                                2f);
                            newflame.noGravity = true;
                        }
                    }
                }
            }

            Dust newddust = Dust.NewDustDirect(
                                OriginalPos + new Vector2(-16, 28) + new Vector2(0, (Projectile.Center.Y + 28 - OriginalPos.Y) % 32),
                                1,
                                1,
                                DustID.Torch,
                                Main.rand.NextFloat(-4f, 4f),
                                Main.rand.NextFloat(-1f, 1f),
                                0,
                                new Color(255, 0, 255, 0),
                                2.5f);
            newddust.noGravity = true;

            Projectile.ai[0]++;
        }

        public override void PostDraw(Color lightColor)
        {
            Texture2D SkeletonHandBone = ModContent.Request<Texture2D>("TerrafirmaRedux/Reworks/VanillaMagic/Projectiles/SkeletonHandBone").Value;
            for (int i = 0; i < Math.Abs((Projectile.Center.Y - OriginalPos.Y) / 32); i++)
            {
                Main.EntitySpriteDraw(
                SkeletonHandBone,
                (Projectile.Center + new Vector2(-6, 28 + (i * 32))) - Main.screenPosition,
                SkeletonHandBone.Frame(),
                Color.White,
                0,
                Vector2.Zero,
                1f,
                SpriteEffects.None
                );
            }

            base.PostDraw(lightColor);
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust newddust = Dust.NewDustDirect(
                                Projectile.Center + new Vector2(18, 28),
                                1,
                                1,
                                DustID.Torch,
                                Main.rand.NextFloat(-3f, 3f),
                                Main.rand.NextFloat(-3f, 3f),
                                0,
                                new Color(255, 0, 255, 0),
                                2f);
                newddust.noGravity = true;
            }
        }

    }
    #endregion

    #region ManaBloomProj
    public class ManaBloomProj : ModProjectile
    {
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.TopazBolt}";
        public override void SetDefaults()
        {
            Projectile.Opacity = 0f;
            Projectile.timeLeft = 60;
            Projectile.Size = new Vector2(4);
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
        }

        public override void AI()
        {
            Projectile.ai[0]++;

            if (Main.player[Projectile.owner] == Main.LocalPlayer && Projectile.ai[1] == 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    Projectile newproj = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center, Projectile.velocity.RotatedByRandom(2f), Type, 0, 0, Projectile.owner, 0, 1,  0);
                    newproj.timeLeft = Main.rand.Next(40, 60);
                }
                Projectile.Kill();
            }
            if (Projectile.ai[1] == 1)
            {
                
                if (Projectile.timeLeft < 30)
                {
                    //Projectile.velocity = Projectile.Center.DirectionTo(Main.player[Projectile.owner].MountedCenter) * 2f;
                    Projectile.velocity += Vector2.Lerp(Projectile.velocity, Main.player[Projectile.owner].MountedCenter - Projectile.Center, 0.5f);
                    Projectile.velocity = Projectile.velocity.LengthClamp(16f);
                    if (Projectile.Hitbox.Intersects(Main.player[Projectile.owner].Hitbox)) Projectile.Kill();
                }
                else
                {
                    Projectile.velocity *= 0.925f;
                }
            }
            Dust newDust = Dust.NewDustPerfect(Projectile.Center, DustID.ManaRegeneration, Vector2.Zero, 0, new Color(255, 255, 255, 0), 1f);
            newDust.noGravity = true;
            
        }
    } 
    #endregion
}
