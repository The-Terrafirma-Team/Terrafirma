using Microsoft.Xna.Framework;
using System;
using Terrafirma.Particles;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.VanillaMagic.Spells.PreHardmode.GemStaves.DiamondStaff
{
    internal class DiamondBolt : Spell
    {
        public override int UseAnimation => 26;
        public override int UseTime => 26;
        public override int ManaCost => 8;
        public override int[] SpellItem => new int[] { ItemID.DiamondStaff };

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity * 0.2f, ModContent.ProjectileType<DiamondBoltProj>(), damage, knockback, player.whoAmI, 0, 0, 0);
            return false;
        }
    }
    public class DiamondBoltProj : ModProjectile
    {
        public override string Texture => "Terrafirma/Assets/Bullet";
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.AmberBolt);
            Projectile.aiStyle = -1;
            Projectile.Size = new Vector2(20);
        }
        public override void AI()
        {
            Projectile.ai[0]++;
            if (Projectile.ai[1] == 0)
            {
                for (int i = -1; i <= 1; i += 2)
                {
                    ParticleSystem.AddParticle(new ChlorophyteStyleLaserSegmentGlowless() { Scale = 1, Rotation = Projectile.velocity.ToRotation(), TimeInWorld = 5 }, Projectile.Center + Projectile.velocity + new Vector2(0, i * Math.Min(Projectile.ai[0] / 4, 6)).RotatedBy(Projectile.velocity.ToRotation()), Vector2.Normalize(Projectile.velocity) * 4, Color.Lerp(new Color(1f, 1f, 1f, 0f), Main.DiscoColor, 0.2f) * Math.Min(Projectile.ai[0] / 60, 1));
                }
                if (Projectile.ai[0] < 40)
                {
                    Projectile.velocity *= 1.07f;
                }
            }
            else
            {
                ParticleSystem.AddParticle(new ChlorophyteStyleLaserSegmentGlowless() { Scale = 1, Rotation = Projectile.velocity.ToRotation(), TimeInWorld = 5 }, Projectile.Center + Projectile.velocity, Vector2.Normalize(Projectile.velocity) * 4, Color.Lerp(new Color(1f, 1f, 1f, 0f), Main.DiscoColor, 0.2f) * Math.Min(Projectile.ai[0] / 20, 1));
            }
            
            if (Main.rand.NextBool(2))
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.GemDiamond, Projectile.velocity.RotatedByRandom(0.3f) * 0.7f);
                d.noGravity = true;
            }
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            for (int i = 0; i < 25; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.GemDiamond, Main.rand.NextVector2Circular(4, 4) + Projectile.velocity * 0.5f);
                d.noGravity = true;
            }
        }
    }
}
