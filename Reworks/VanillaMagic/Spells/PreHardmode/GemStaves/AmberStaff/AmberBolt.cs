using Microsoft.Xna.Framework;
using System;
using Terrafirma.Particles;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.VanillaMagic.Spells.PreHardmode.GemStaves.AmberStaff
{
    internal class AmberBolt : Spell
    {
        public override int UseAnimation => 28;
        public override int UseTime => 28;
        public override int ManaCost => 7;
        public override int[] SpellItem => [ItemID.AmberStaff];

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<AmberBoltProj>(), damage, knockback, player.whoAmI, 0, 0, 0);
            return false;
        }
    }
    public class AmberBoltProj : ModProjectile
    {
        public override string Texture => "Terrafirma/Assets/Bullet";
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.AmberBolt);
            Projectile.aiStyle = -1;
        }
        public override void AI()
        {
            Projectile.ai[0]++;
            ParticleSystem.AddParticle(new ChlorophyteStyleLaserSegmentGlowless() { Scale = 1, Rotation = Projectile.velocity.ToRotation(), TimeInWorld = Main.rand.Next(6) }, Projectile.Center + Projectile.velocity, Vector2.Normalize(Projectile.velocity) * 2, new Color(255,128,64,64) * Math.Min(Projectile.ai[0] / 10,1));
            if (Main.rand.NextBool(2))
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.AmberBolt, Projectile.velocity.RotatedByRandom(0.3f) * 0.7f);
                d.noGravity = true;
            }
            if (Projectile.ai[0] > 20)
            {
                Projectile.velocity.Y -= 0.07f;
            }
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            for(int i = 0; i < 25; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.AmberBolt, Main.rand.NextVector2Circular(4,4) + Projectile.velocity * 0.5f);
                d.noGravity = true;
            }
        }
    }
}
