using Microsoft.Xna.Framework;
using System;
using Terrafirma.Particles;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.VanillaMagic.Spells.PreHardmode.GemStaves.AmethystStaff
{
    internal class HomingAmethystBolt : Spell
    {
        public override int UseAnimation => 43;
        public override int UseTime => 43;
        public override int ManaCost => 6;
        public override int[] SpellItem => new int[] { ItemID.AmethystStaff };

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            type = ModContent.ProjectileType<HomingAmethyst>();
            damage = (int)(damage * 0.8f);

            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0, 0, 0);
            return false;
        }
    }

    public class HomingAmethyst : ModProjectile
    {
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.AmethystBolt}";
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.AmethystBolt);
            Projectile.aiStyle = -1;
            Projectile.Size = new Vector2(16);
            Projectile.timeLeft = 100;
        }
        public override void AI()
        {
            Projectile.ai[0]++;
            ParticleSystem.AddParticle(new ColorDot() { Size = Main.rand.NextFloat(0.4f, 0.8f), TimeInWorld = 40, Waviness = Main.rand.NextFloat(0.04f), gravity = 0f, secondaryColor = new Color(0.1f,0.1f,0.1f,0f) }, Projectile.Center, Projectile.velocity * 0.5f, new Color(200, 64, 255, 64) * Math.Min(Projectile.ai[0] / 10, 1) * 0.5f);

            NPC target = TFUtils.FindClosestNPC(200, Projectile.Center);
            if (target != null && target.active)
            {
                Projectile.velocity = Vector2.Lerp(Projectile.velocity += Projectile.Center.DirectionTo(target.Center) * 0.1f, Projectile.Center.DirectionTo(target.Center) * Projectile.velocity.Length(), 0.04f);
            }
            Projectile.velocity *= 0.99f;
            Projectile.velocity = Projectile.velocity.LengthClamp(5);
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            for (int i = 0; i < 20; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.GemAmethyst, Main.rand.NextVector2Circular(4, 4) + Projectile.velocity * 0.5f);
                d.noGravity = true;
            }
            for (int i = 0; i < 10; i++)
            {
                ParticleSystem.AddParticle(new ColorDot() { Size = 0.4f, gravity = Main.rand.NextFloat(0.04f), TimeInWorld = 40, secondaryColor = new Color(128, 128, 128, 0), fadeOut = 0.99f }, Projectile.Center, Main.rand.NextVector2Circular(2, 2) + Projectile.velocity * 0.5f + new Vector2(0, -2), new Color(200, 64, 255, 64) * Math.Min(Projectile.ai[0] / 10, 1)); ;
            }
        }
    }
}
