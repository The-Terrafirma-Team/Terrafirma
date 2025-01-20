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
    internal class AmethystBolt : Spell
    {
        public override int UseAnimation => 37;
        public override int UseTime => 37;
        public override int ManaCost => 5;
        public override int[] SpellItem => new int[] { ItemID.AmethystStaff };

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<AmethystBoltProj>(), damage, knockback, player.whoAmI, 0, 0, 0);
            return false;
        }
    }
    public class AmethystBoltProj : ModProjectile
    {
        public override string Texture => "Terrafirma/Assets/Bullet";
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.AmethystBolt);
            Projectile.aiStyle = -1;
        }
        public override void AI()
        {
            Projectile.ai[0]++;
            ParticleSystem.AddParticle(new ColorDot() { Size = Main.rand.NextFloat(0.2f, 0.5f), TimeInWorld = 40, Waviness = Main.rand.NextFloat(0.04f), gravity = 0f, secondaryColor = new Color(128, 128, 128, 0) }, Projectile.Center, Projectile.velocity * 0.5f, new Color(200, 0, 255, 64) * Math.Min(Projectile.ai[0] / 10, 1));
            
            //if(Main.rand.NextBool())
            //{
            //    Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.GemAmethyst, Projectile.velocity.RotatedByRandom(0.3f) * 1f);
            //    d.noGravity = true;
            //    d.scale = 1f;
            //}
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            for (int i = 0; i < 25; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.GemAmethyst, Main.rand.NextVector2Circular(4, 4) + Projectile.velocity * 0.5f);
                d.noGravity = true;
            }
            for(int i = 0; i < 15; i++)
            {
                ParticleSystem.AddParticle(new ColorDot() { Size = 0.4f, gravity = Main.rand.NextFloat(0.04f), TimeInWorld = 40, secondaryColor = new Color(128,128,128,0), fadeOut = 0.99f }, Projectile.Center, Main.rand.NextVector2Circular(2,2) + Projectile.velocity * 0.5f + new Vector2(0,-2), new Color(200, 64, 255, 64) * Math.Min(Projectile.ai[0] / 10, 1)); ;
            }
        }
    }
}
