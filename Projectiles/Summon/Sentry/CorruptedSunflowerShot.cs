using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace Terrafirma.Projectiles.Summon.Sentry
{
    internal class CorruptedSunflowerShot : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.timeLeft = 600;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Corruption, Projectile.velocity);
            d.noGravity = !Main.rand.NextBool(6);
        }

        public override void OnKill(int timeLeft)
        {
            for(int i = 0; i < 10; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Corruption, Main.rand.NextVector2CircularEdge(3, 3));
                d.noGravity = !Main.rand.NextBool(6);
            }
            SoundEngine.PlaySound(SoundID.Dig,Projectile.position);
        }
    }
}
