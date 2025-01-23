using Microsoft.Xna.Framework;
using System;
using Terrafirma.Particles;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.VanillaMagic.Spells.PreHardmode.GemStaves.RubyStaff
{
    internal class ExplosiveRubyBolt : Spell
    {
        public override int UseAnimation => 60;
        public override int UseTime => 60;
        public override int ManaCost => 9;
        public override int[] SpellItem => new int[] { ItemID.RubyStaff };

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            velocity *= 0.7f;
            damage = (int)(damage * 0.85f);
            type = ModContent.ProjectileType<ExplodingRuby>();
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0, 0, 0);
            return false;
        }
    }
    public class ExplodingRuby : ModProjectile
    {
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.RubyBolt}";
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.RubyBolt);
            Projectile.aiStyle = -1;
            Projectile.penetrate = 1;
            Projectile.Size = new Vector2(16);
        }

        public override void AI()
        {
            Projectile.ai[0]++;
            ParticleSystem.AddParticle(new ColorDot() { Size = Main.rand.NextFloat(0.7f, 1f), TimeInWorld = 40, Waviness = Main.rand.NextFloat(0.04f), fadeOut = 0.995f, gravity = 0f, secondaryColor = new Color(54, 54, 54, 0) }, Projectile.Center, Projectile.velocity * 0.5f, new Color(255, 0, 0, 0) * Math.Min(Projectile.ai[0] / 10, 1));

            Projectile.velocity *= 0.985f;

            if (Projectile.velocity.Length() < 1)
            {
                Projectile.Kill();
            }

        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 20; i++)
            {
                Dust newdust = Dust.NewDustPerfect(Projectile.position, DustID.GemRuby, new Vector2(Main.rand.NextFloat(-5.8f, 5.8f), Main.rand.NextFloat(-5.8f, 5.8f)), 0, Color.White, Main.rand.NextFloat(1f, 1.5f));
                newdust.noGravity = true;

                ParticleSystem.AddParticle(new ColorDot() { Size = 0.4f, gravity = 0f, TimeInWorld = 40, secondaryColor = new Color(128, 128, 128, 0) * Math.Min(Projectile.ai[0] / 30, 1), fadeOut = 0.99f, Waviness = Main.rand.NextFloat(0.1f) }, Projectile.Center, Main.rand.NextVector2Circular(8, 8) + Projectile.velocity * 0.5f, new Color(255, 0, 0, 0) * Math.Min(Projectile.ai[0] / 30, 1)); ;
            }
            ParticleSystem.AddParticle(new BigSparkle() { fadeInTime = 5, lengthMultiplier = 0.75f, Scale = 8, Rotation = Main.rand.NextFloat(MathHelper.TwoPi) }, Projectile.Center, Vector2.Zero, new Color(255, 0, 0, 0));
            Projectile.Explode(100);
            SoundEngine.PlaySound(SoundID.Item110, Projectile.position);
        }
    }
}
