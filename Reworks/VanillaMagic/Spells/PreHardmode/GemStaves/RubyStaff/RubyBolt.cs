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

namespace Terrafirma.Reworks.VanillaMagic.Spells.PreHardmode.GemStaves.RubyStaff
{
    internal class RubyBolt : Spell
    {
        public override int UseAnimation => 28;
        public override int UseTime => 28;
        public override int ManaCost => 7;
        public override int[] SpellItem => new int[] { ItemID.RubyStaff };

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<RubyBoltProj>(), damage, knockback, player.whoAmI, 0, 0, 0);
            return false;
        }
    }
    public class RubyBoltProj : ModProjectile
    {
        public override string Texture => "Terrafirma/Assets/Bullet";
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.AmethystBolt);
            Projectile.aiStyle = -1;
        }
        public override void SetStaticDefaults()
        {
            ProjectileSets.CanBeReflected[Type] = true;
        }
        public override void AI()
        {
            Projectile.ai[0]++;
            ParticleSystem.AddParticle(new ColorDot() { Size = Main.rand.NextFloat(0.4f, 0.7f), TimeInWorld = 40, Waviness = Main.rand.NextFloat(0.01f), gravity = 0f, secondaryColor = new Color(54, 54, 54, 0) }, Projectile.Center, Projectile.velocity * 0.5f, new Color(255, 0, 0, 0) * Math.Min(Projectile.ai[0] / 10, 1));

            if (Main.rand.NextBool())
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.GemRuby, Projectile.velocity.RotatedByRandom(0.3f) * 1f);
                d.noGravity = true;
                d.scale = 1f;
            }
            if (Projectile.ai[0] > 20)
            {
                Projectile.velocity.Y += 0.1f;
            }
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            for (int i = 0; i < 25; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.GemRuby, Main.rand.NextVector2Circular(4, 4) + Projectile.velocity * 0.5f);
                d.noGravity = true;
            }
            for (int i = 0; i < 15; i++)
            {
                ParticleSystem.AddParticle(new ColorDot() { Size = 0.4f, gravity = 0f, TimeInWorld = 40, secondaryColor = new Color(128, 128, 128, 0), fadeOut = 0.99f, Waviness = Main.rand.NextFloat(0.1f) }, Projectile.Center, Main.rand.NextVector2Circular(2, 2) + Projectile.velocity * 0.5f, new Color(255, 0, 0, 0) * Math.Min(Projectile.ai[0] / 10, 1)); ;
            }
        }
    }
}
