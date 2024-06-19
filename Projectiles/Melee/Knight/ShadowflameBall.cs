using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Projectiles.Melee.Knight
{
    public class ShadowflameBall : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 4;
        }
        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(54);
            Projectile.friendly = true;
            Projectile.extraUpdates = 2;
            Projectile.tileCollide = false;
            Projectile.scale = 0.75f;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.localNPCHitCooldown = -1;
            Projectile.usesLocalNPCImmunity = true;
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            target.AddBuff(BuffID.ShadowFlame, 60 * 2);
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.DD2_BetsyFireballImpact, Projectile.position);

            for (int i = 0; i < 24; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Shadowflame, Main.rand.NextVector2Circular(8, 8), 255);
                d.scale *= 1.5f;
                d.noGravity = Main.rand.NextBool();
            }
        }
        public override void AI()
        {
            if (Projectile.ai[1] == 0)
            {
                for (int i = 0; i < 24; i++)
                {
                    Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Shadowflame, Main.rand.NextVector2Circular(8, 8), 255);
                    d.scale *= 1.5f;
                    d.noGravity = Main.rand.NextBool();
                }
            }

            Projectile.ai[1]++;

            if (Projectile.Center.Y > Projectile.ai[0])
                Projectile.tileCollide = true;

            if (Main.rand.NextBool(3))
            {
                Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Shadowflame, Projectile.velocity.X, Projectile.velocity.Y, 255);
                d.scale *= 1.5f;
                d.noGravity = true;
            }
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 10)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
                if (Projectile.frame > 3)
                    Projectile.frame = 0;
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
    }
}
