using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace Terrafirma.Projectiles.Summon.Sentry.PreHardmode
{
    internal class SportarShot : ModProjectile
    {
        NPC target;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 4;
        }
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
            Projectile.velocity.Y += 0.3f;

            Projectile.frameCounter++;
            if(Projectile.frameCounter > 5)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
                if (Projectile.frame == 4)
                    Projectile.frame = 0;
            }
        }
        public override void OnKill(int timeLeft)
        {
            Projectile.Explode(16 * 6);
            SoundEngine.PlaySound(SoundID.DD2_BetsyFireballImpact,Projectile.position);
            for (int i = 0; i < 16; i++)
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
