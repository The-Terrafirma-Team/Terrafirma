using Microsoft.Xna.Framework;
using System;
using Terrafirma.Particles;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.VanillaMagic.Spells.PreHardmode.GemStaves.EmeraldStaff
{
    internal class EmeraldBolt : Spell
    {
        public override int UseAnimation => 32;
        public override int UseTime => 32;
        public override int ManaCost => 6;
        public override int[] SpellItem => new int[] { ItemID.EmeraldStaff };

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<EmeraldBoltProj>(), damage, knockback, player.whoAmI, 0, 0, 0);
            return false;
        }

        public override void Update(Item item, Player player)
        {
            item.UseSound = SoundID.Item8;
            item.channel = false;
            base.Update(item, player);
        }
    }
    public class EmeraldBoltProj : ModProjectile
    {
        public override string Texture => "Terrafirma/Assets/Bullet";
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.EmeraldBolt);
            Projectile.aiStyle = -1;
        }
        public override void AI()
        {
            Projectile.ai[0]++;
            ParticleSystem.AddParticle(new ColorDot() { Size = 0.5f, TimeInWorld = 40, Waviness = Main.rand.NextFloat(0.04f), gravity = 0f, secondaryColor = new Color(30, 180, 120, 0) * 0.1f }, Projectile.Center, Projectile.velocity * 0.5f, new Color(30, 180, 120, 0) * Math.Min(Projectile.ai[0] / 10, 1) * 0.3f);

            if (Main.rand.NextBool(6))
            {
                ParticleSystem.AddParticle(new BigSparkle() { fadeInTime = 7, Scale = 1f, fadeOutMultiplier = 0.99f, Rotation = Main.rand.NextFloat(MathHelper.TwoPi), secondaryColor = new Color(0.3f,0.3f,0.3f,0f)}, Projectile.Center + Main.rand.NextVector2Circular(5,5), Vector2.Zero, new Color(30, 180, 120, 0) * 0.3f);
            }
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            for (int i = 0; i < 25; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.GemEmerald, Main.rand.NextVector2Circular(4, 4) + Projectile.velocity * 0.5f);
                d.noGravity = true;
            }
            ParticleSystem.AddParticle(new BigSparkle() { fadeInTime = 15, fadeOutMultiplier = 0.99f, lengthMultiplier = 2f, Scale = 0.1f, Rotation = Main.rand.NextFloat(MathHelper.TwoPi) }, Projectile.Center + Main.rand.NextVector2Circular(5, 5), Vector2.Zero, new Color(30, 180, 120, 0));
            for (int i = 0; i < 15; i++)
            {
                ParticleSystem.AddParticle(new ColorDot() { Size = 0.1f, gravity = Main.rand.NextFloat(0.04f), TimeInWorld = 40, secondaryColor = new Color(30, 180, 120, 0), fadeOut = 0.99f }, Projectile.Center, Main.rand.NextVector2Circular(2, 2) + Projectile.velocity * 0.5f + new Vector2(0, -2), new Color(30, 180, 120, 0) * Math.Min(Projectile.ai[0] / 10, 1)); ;
            }
        }
    }
}
