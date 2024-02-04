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

namespace Terrafirma.Projectiles.Summons
{
    internal class SportarShot : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.timeLeft = 600;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            NPC target = Main.npc[(int)Projectile.ai[2]];
            if(target.Center.Y > Projectile.Center.Y || !target.active)
            {
                Projectile.velocity.Y += 0.45f;
            }
            else
            {
                Projectile.velocity.Y += 0.2f;
            }
        }

        public override void OnKill(int timeLeft)
        {
            Projectile.Explode(16 * 6);
            SoundEngine.PlaySound(SoundID.DD2_BetsyFireballImpact,Projectile.position);
            for(int i = 0; i < 16; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.GlowingMushroom);
                d.velocity = Main.rand.NextVector2CircularEdge(24, 24) * Main.rand.NextFloat(0.5f, 1f);
                d.fadeIn = 1f;
                d.noGravity = !Main.rand.NextBool(5);
            }
            for (int i = 0; i < 16; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.GlowingSnail);
                d.velocity = Main.rand.NextVector2CircularEdge(8, 8) * Main.rand.NextFloat(0.5f, 1f);
                d.fadeIn = 1f;
                d.noGravity = !Main.rand.NextBool(5);
            }
        }
    }
}
