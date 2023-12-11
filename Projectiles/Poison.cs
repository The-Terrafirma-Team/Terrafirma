using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using SteelSeries.GameSense;
using TerrafirmaRedux.Dusts;

namespace TerrafirmaRedux.Projectiles
{
    internal class Poison : ModProjectile
    {
        float Timer = 0;
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.timeLeft = 60;
        }

        public override void AI()
        {
        
            Timer += 1;

            if (Timer <= 60)
            {
                int randint = Main.rand.Next(0, 9);
                if (randint <= 6)
                {
                    Dust.NewDust(Projectile.position, 4, 4, ModContent.DustType<PoisonDust>(), Projectile.velocity.X * (Main.rand.NextFloat(1f, 1f) * Main.rand.Next(-1, 1)), Projectile.velocity.Y * 0.6f, 0, default, 1.3f);
                }
                else
                {
                    Dust.NewDust(Projectile.position, 4, 4, ModContent.DustType<PoisonDust>(), Projectile.velocity.X * (Main.rand.NextFloat(1f, 1f) * Main.rand.Next(-1, 1)), Projectile.velocity.Y * 0.6f, 150, default, 3f);
                }
            }

            if (Timer > 30)
            {
                Projectile.velocity.Y += 0.15f;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.rand.Next(0,9) < 9 )
            {
                target.AddBuff(BuffID.Poisoned, 60);
            }
        }
    }
}
