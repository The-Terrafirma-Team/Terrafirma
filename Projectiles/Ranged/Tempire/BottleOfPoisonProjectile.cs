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

namespace Terrafirma.Projectiles.Ranged.Tempire
{
    internal class BottleOfPoisonProjectile : ModProjectile
    {
        int startdirection = 1;
        public override void SetDefaults()
        {
            Projectile.damage = 8;
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.timeLeft = 600;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;

            DrawOffsetX = 2;
            DrawOriginOffsetY = -4;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Venom, 180);
            base.OnHitNPC(target, hit, damageDone);
        }
        public override void AI()
        {
            Projectile.rotation += ((0.04f * Projectile.velocity.Length() ) + 0.1f) * startdirection;
            

            if (Projectile.ai[0] == 0) startdirection = Projectile.direction;

            if (Projectile.ai[0] > 20) Projectile.velocity.Y += 0.5f;

            Projectile.ai[0]++;
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 6; i++)
            {
                Dust.NewDust(Projectile.Center, 16, 16, DustID.Glass, Main.rand.Next(-2, 2), Main.rand.Next(-2, 3), 0, default, Main.rand.NextFloat(0.9f, 1f));
                
            }
            for (int i = 0; i < 24; i++)
            {
                Dust.NewDust(Projectile.Center, 16, 16, DustID.Water_Corruption, Main.rand.Next(-4, 4) * 2, Main.rand.Next(-4, 4) * 2, 0, new Color(255,200,200), Main.rand.NextFloat(1.1f, 1.5f));
            }
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Shatter, Projectile.position);
        }
    }
}
