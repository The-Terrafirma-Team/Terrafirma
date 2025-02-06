using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terrafirma.Common;
using Terrafirma.Data;
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
            Projectile.Size = new Vector2(8,32) / 0.8f;
            Projectile.alpha = 250;
            Projectile.scale = 0.8f;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 3;
            ProjectileID.Sets.TrailCacheLength[Type] = 8;
            ProjectileID.Sets.TrailingMode[Type] = 0;
            ProjectileSets.CanBeReflected[Type] = true;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            return true;
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Tink with { MaxInstances = 10, Volume = Math.Min((Projectile.oldVelocity.Length() * 0.02f) + 0.1f, 1f) });
            for (int i = 0; i < 20; i++)
            {
                Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Granite);
                d.noGravity = Main.rand.NextBool();
                d.velocity.Y *= 0.1f;
                d.velocity.Y -= Projectile.velocity.Y * Main.rand.NextFloat(0.8f);
            }
                for (int i = 0; i < Math.Min(Projectile.velocity.Y * 0.2f,3); i++)
                {
                    float opacity = Main.rand.NextFloat(0.5f);
                    ParticleSystem.AddParticle(new Smoke() { Scale = 1f, secondaryColor = new Color(25, 23, 54) * opacity }, Projectile.Bottom, new Vector2(Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-Projectile.velocity.Y * 0.5f, -1)), new Color(116, 132, 169) * opacity, ParticleLayer.Normal);
                }
            ParticleSystem.AddParticle(new Shockwave() { Scale = new Vector2(Main.rand.NextFloat(0.1f,0.2f),0.3f) * Projectile.oldVelocity.Length() * 0.04f, rotation = Projectile.oldVelocity.ToRotation()}, Projectile.Bottom, null, Color.LightBlue * Math.Min(Projectile.oldVelocity.Length() * 0.04f, 0.7f));
        }
        public override void AI()
        {
            if (Projectile.reflected)
            {
                Projectile.rotation = Projectile.velocity.ToRotation() - MathHelper.PiOver2;
            }

            Projectile.frame = (int)Projectile.ai[0];

            Projectile.ai[1]++;
            if (Projectile.ai[1] > 30 || Projectile.reflected)
            {
                Projectile.velocity.Y += 0.2f;
                Projectile.velocity.Y *= 1.02f;
            }
            if(Projectile.alpha > 0)
            {
                Projectile.alpha -= 10;
            }
            if (Projectile.scale < 1)
            {
                Projectile.scale += 0.025f;
            }
            //Projectile.tileCollide = Projectile.position.Y > Projectile.ai[2];
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
