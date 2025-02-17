using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Data;
using Terrafirma.Particles;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace Terrafirma.Projectiles.Hostile
{
    public class YinYangRiser : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileSets.CanBeReflected[Type] = true;
        }
        public override string Texture => "Terrafirma/Assets/Bullet";
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.AmethystBolt);
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 60;
        }
        public override void AI()
        {
            if(Projectile.timeLeft > 20)
                Projectile.velocity.Y -= 0.17f;
            ParticleSystem.AddParticle(new ColorDot() { Size = 0.4f, TimeInWorld = 40, Waviness = Main.rand.NextFloat(0.04f), gravity = 0f, secondaryColor = Projectile.ai[0] % 20 == 0 ? Color.Black : Color.White with { A = 0 } }, Projectile.Center, Projectile.velocity * 0.7f, Projectile.ai[0] % 20 == 0? Color.White with { A = 0 } : Color.Black);

        }
    }
}
