using Microsoft.Xna.Framework;
using System;
using Terrafirma.Data;
using Terrafirma.Particles;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.VanillaMagic.Spells.PreHardmode.GemStaves.TopazStaff
{
    internal class SplittingTopazBolt : Spell
    {
        public override int UseAnimation => 43;
        public override int UseTime => 43;
        public override int ManaCost => 6;
        public override int[] SpellItem => new int[] { ItemID.TopazStaff };

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            type = ModContent.ProjectileType<SplittingTopaz>();
            damage = (int)(damage * 0.8f);

            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0, 0, 0);
            return false;
        }
    }
    public class SplittingTopaz : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileSets.CanBeReflected[Type] = true;
        }
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.TopazBolt}";
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.TopazBolt);
            Projectile.aiStyle = -1;
            Projectile.Size = new Vector2(16);
        }

        public override void AI()
        {
            Projectile.ai[0]++;

            ParticleSystem.AddParticle(new ColorDot() { Size = 0.5f, TimeInWorld = 40, Waviness = Main.rand.NextFloat(0.02f), gravity = 0f, secondaryColor = new Color(255, 255, 255, 0) * 0.2f }, Projectile.Center, Projectile.velocity * 0.5f, new Color(255, 180, 0, 0) * Math.Min(Projectile.ai[0] / 10, 1));
            if (Main.rand.NextBool())
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.GemTopaz, Projectile.velocity.RotatedByRandom(0.3f) * 1f);
                d.noGravity = true;
                d.scale = 1f;
            }
            ParticleSystem.AddParticle(new ChlorophyteStyleLaserSegment() { Scale = 1.2f, Rotation = Projectile.velocity.ToRotation(), TimeInWorld = 6 }, Projectile.Center + Projectile.velocity, Vector2.Normalize(Projectile.velocity) * 2, new Color(255, 180, 64, 64) * Math.Min(Projectile.ai[0] / 30, 0.5f));

            if (Projectile.ai[0] > 30 && Projectile.owner == Main.LocalPlayer.whoAmI)
            {
                for (int i = -1; i < 2; i++)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, Projectile.velocity.RotatedBy(0.2f * i), ModContent.ProjectileType<SplitTopaz>(), (int)(Projectile.damage * 0.85f), Projectile.knockBack, Projectile.owner, 0, 0, 0);
                }
                SoundEngine.PlaySound(SoundID.Item110, Projectile.position);
                ParticleSystem.AddParticle(new BigSparkle() { fadeInTime = 10, lengthMultiplier = 1f, Scale = 1, Rotation = Main.rand.NextFloat(MathHelper.TwoPi) }, Projectile.Center, Vector2.Zero, new Color(255, 180, 0, 0));
                Projectile.Kill();
            }
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 25; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.GemTopaz, Main.rand.NextVector2Circular(4, 4) + Projectile.velocity * 0.5f);
                d.noGravity = true;
            }
            for (int i = 0; i < 15; i++)
            {
                ParticleSystem.AddParticle(new ColorDot() { Size = 0.4f, TimeInWorld = 40, secondaryColor = new Color(128, 128, 128, 0), fadeOut = 0.99f }, Projectile.Center, Main.rand.NextVector2Circular(2, 2), new Color(255, 180, 0, 0)); ;
                ParticleSystem.AddParticle(new ImpactSparkle() { Scale = 0.4f, LifeTime = Main.rand.Next(35), secondaryColor = new Color(1f, 1f, 1f, 0f) }, Projectile.Center, Main.rand.NextVector2Circular(4, 4) + Projectile.velocity * 0.3f, new Color(255, 180, 0, 0));
            }
        }
    }

    public class SplitTopaz : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileSets.CanBeReflected[Type] = true;
        }
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.TopazBolt}";
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.TopazBolt);
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 60;
            Projectile.Size = new Vector2(16);
        }
        public override void AI()
        {
            Projectile.ai[0]++;
            if (Main.rand.NextBool(3))
                ParticleSystem.AddParticle(new ColorDot() { Size = 0.2f, TimeInWorld = 40, Waviness = Main.rand.NextFloat(0.02f), gravity = 0f, secondaryColor = new Color(255, 255, 255, 0) * 0.2f }, Projectile.Center, Projectile.velocity * 0.5f, new Color(255, 180, 0, 0) * Math.Min(Projectile.ai[0] / 10, 1));
            if (Main.rand.NextBool())
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.GemTopaz, Projectile.velocity.RotatedByRandom(0.3f) * 1f);
                d.noGravity = true;
                d.scale = 1f;
            }
            ParticleSystem.AddParticle(new ChlorophyteStyleLaserSegment() { Scale = 0.8f, Rotation = Projectile.velocity.ToRotation(), TimeInWorld = 6 }, Projectile.Center + Projectile.velocity, Vector2.Normalize(Projectile.velocity) * 2, new Color(255, 180, 64, 64) * Math.Min(Projectile.ai[0] / 30, 0.5f));
        }
    }
}
