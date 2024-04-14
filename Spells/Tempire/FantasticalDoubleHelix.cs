using Microsoft.Xna.Framework;
using System;
using Terrafirma.Items.Weapons.Magic.Tempire;
using Terrafirma.Particles;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Spells.Tempire
{
    internal class FantasticalDoubleHelix : Spell
    {
        public override int UseAnimation => 22;
        public override int UseTime => 22;
        public override int ManaCost => 5;
        public override int[] SpellItem => new int[] { ModContent.ItemType<Majesty>() };


        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return true;
        }
    }
    public class FantasticalDoubleHelixProj : ModProjectile
    {
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.AmethystBolt}";
        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(16);
            Projectile.friendly = true;
            Projectile.Opacity = 0;
        }
        public override void AI()
        {
            Projectile.ai[0]++;

            if (Projectile.ai[1] == 0)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Projectile.velocity, Projectile.type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0, 1, 0);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Projectile.velocity, Projectile.type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0, 2, 0);
                Projectile.Kill();
            }
            if (Projectile.ai[1] == 1)
            {
                Dust dust = Dust.NewDustPerfect(Projectile.Center, DustID.AmberBolt, Vector2.Zero, 0, Color.Yellow);
                dust.noGravity = true;
                Projectile.velocity = Projectile.velocity.RotatedBy(Math.Sin((Projectile.ai[0] - 0.07f) * 0.2f + MathHelper.PiOver2) * 0.1f);

                if (Projectile.ai[0] % Main.rand.Next(6, 10) == 0)
                {
                    BigSparkle bigsparkle = new BigSparkle();
                    bigsparkle.fadeInTime = Main.rand.Next(6, 10);
                    bigsparkle.Rotation = Main.rand.NextFloat(-0.1f, 0.1f);
                    bigsparkle.Scale = 1f;
                    ParticleSystem.AddParticle(bigsparkle, Projectile.Center + Main.rand.NextVector2Circular(12, 12), Vector2.Zero, new Color(1f, 0.3f, 0.2f, 0));
                    //LegacyParticleSystem.AddParticle(new BigSparkle(), Projectile.Center + Main.rand.NextVector2Circular(12, 12), Vector2.Zero, new Color(1f, 0.3f, 0.2f, 0), 0, Main.rand.Next(6, 10), 1, 1f, Main.rand.NextFloat(-0.1f, 0.1f));
                }
            }
            if (Projectile.ai[1] == 2)
            {
                Dust dust = Dust.NewDustPerfect(Projectile.Center, DustID.AmberBolt, Vector2.Zero, 0, Color.Pink);
                dust.noGravity = true;
                Projectile.velocity = Projectile.velocity.RotatedBy(-Math.Sin((Projectile.ai[0] - 0.07f) * 0.2f + MathHelper.PiOver2) * 0.1f);

                if (Projectile.ai[0] % Main.rand.Next(6, 10) == 0)
                {
                    BigSparkle bigsparkle = new BigSparkle();
                    bigsparkle.fadeInTime = Main.rand.Next(6, 10);
                    bigsparkle.Rotation = Main.rand.NextFloat(-0.1f, 0.1f);
                    bigsparkle.Scale = 1f;
                    ParticleSystem.AddParticle(bigsparkle, Projectile.Center + Main.rand.NextVector2Circular(12, 12), Vector2.Zero, new Color(1f, 0.3f, 0.2f, 0));
                    //LegacyParticleSystem.AddParticle(new BigSparkle(), Projectile.Center + Main.rand.NextVector2Circular(12, 12), Vector2.Zero, new Color(1f, 0.3f, 0.2f, 0), 0, Main.rand.Next(6, 10), 1, 1f, Main.rand.NextFloat(-0.1f, 0.1f));
                }
            }

        }
    }
    public class GlitterBolt : ModProjectile
    {
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.AmethystBolt}";
        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(16);
            Projectile.friendly = true;
            Projectile.Opacity = 0;
        }
        public override void AI()
        {
            Projectile.ai[0]++;

            if (Projectile.ai[0] % Main.rand.Next(6, 10) == 0)
            {
                BigSparkle bigsparkle = new BigSparkle();
                bigsparkle.fadeInTime = Main.rand.Next(6, 10);
                bigsparkle.Rotation = Main.rand.NextFloat(-0.1f, 0.1f);
                bigsparkle.Scale = 1f;
                ParticleSystem.AddParticle(bigsparkle, Projectile.Center + Main.rand.NextVector2Circular(12, 12), Vector2.Zero, new Color(1f, 0.3f, 0.2f, 0));
                //LegacyParticleSystem.AddParticle(new BigSparkle(), Projectile.Center + Main.rand.NextVector2Circular(12, 12), Vector2.Zero, new Color(1f, 0.3f, 0.2f, 0), 0, Main.rand.Next(6, 10), 1, 1f, Main.rand.NextFloat(-0.1f, 0.1f));
            }

            if (Projectile.ai[1] == 0)
            {
                Projectile.velocity.X *= 0.99f;
                Projectile.velocity.Y = Math.Clamp(Projectile.velocity.Y + 0.1f, -12f, 12f);
            }

            Dust dust = Dust.NewDustPerfect(Projectile.Center, DustID.AmberBolt, Vector2.Zero, 0, Color.Pink);
            dust.noGravity = true;
            Projectile.velocity = Projectile.velocity.RotatedBy(-Math.Sin((Projectile.ai[0] - 0.07f) * 0.2f + MathHelper.PiOver2) * 0.1f);

        }
    }
}
