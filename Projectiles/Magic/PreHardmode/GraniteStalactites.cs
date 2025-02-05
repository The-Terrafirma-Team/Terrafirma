using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terrafirma.Common;
using Terrafirma.Particles;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Projectiles.Magic.PreHardmode
{
    public class GraniteStalactites : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.Size = new Vector2(8,32);
            Projectile.alpha = 250;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 3;
            ProjectileID.Sets.TrailCacheLength[Type] = 8;
            ProjectileID.Sets.TrailingMode[Type] = 0;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            return true;
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Tink with { MaxInstances = 10});
            for (int i = 0; i < 20; i++)
            {
                Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Granite);
                d.noGravity = Main.rand.NextBool();
                d.velocity.Y *= 0.1f;
                d.velocity.Y -= Projectile.velocity.Y * Main.rand.NextFloat(0.8f);
            }
        }
        public override void AI()
        {
            Projectile.frame = (int)Projectile.ai[0];

            Projectile.ai[1]++;
            if (Projectile.ai[1] > 30)
            {
                Projectile.velocity.Y += 0.2f;
                Projectile.velocity.Y *= 1.02f;
            }
            if(Projectile.alpha > 0)
            {
                Projectile.alpha -= 10;
            }
            Projectile.tileCollide = Projectile.position.Y > Projectile.ai[2];
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (Projectile.ai[1] < 30)
                modifiers.SourceDamage /= 2;
            else
                modifiers.SourceDamage += Projectile.velocity.Y * 0.05f;
        }
    }
}
