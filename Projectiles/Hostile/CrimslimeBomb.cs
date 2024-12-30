using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Buffs.Debuffs;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Projectiles.Hostile
{
    public class CrimslimeBomb : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.QuickDefaults(true, 18);
            Projectile.tileCollide = false;
            Projectile.timeLeft = 60;
        }
        public override void AI()
        {
            Projectile.rotation += Projectile.velocity.X * 0.1f;
            Projectile.velocity *= new Vector2(0.97f,0.98f);
            Projectile.scale = 0.9f + MathF.Sin(Projectile.timeLeft * 0.6f) * (0.2f * (1f - Projectile.timeLeft / 60f));
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.NPCDeath1);

            for(int i = 0; i < 70; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Blood, Main.rand.NextVector2Circular(6, 6));
                d.noGravity = !Main.rand.NextBool(3);
                d.fadeIn = Main.rand.NextFloat(1.3f);
                d.scale = Main.rand.NextFloat(1f, 2f);
            }
            if(Main.LocalPlayer.Center.Distance(Projectile.Center) < 16 * 8)
            {
                Main.LocalPlayer.AddBuff(ModContent.BuffType<Bloodsoaked>(), 60 * 5);
            }
        }
    }
}
