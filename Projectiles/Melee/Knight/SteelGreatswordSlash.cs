using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Particles;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Projectiles.Melee.Knight
{
    public class SteelGreatswordSlash : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.QuickDefaults(false, 64);
            Projectile.timeLeft = 80;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.damage -= Projectile.damage / 10;
            for (int i = 0; i < 20; i++)
            {
                ParticleSystem.AddParticle(new ImpactSparkle() { Scale = Main.rand.NextFloat(0.4f, 0.9f), LifeTime = Main.rand.Next(15, 25), secondaryColor = new Color(1f, 1f, 0.3f, 0f)}, target.Hitbox.ClosestPointInRect(Projectile.Center), (Vector2.Normalize(Projectile.velocity) * Main.rand.NextFloat(2,6)).RotatedByRandom(1.2f), new Color(1f, Main.rand.NextFloat(0.7f), 0f, 0f));
            }
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 2;
            height = 2;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
        }
        public override void AI()
        {
            if (Projectile.ai[0] == 0)
            {
                SoundEngine.PlaySound(SoundID.DD2_BetsyFlameBreath with { Pitch = 0.2f, Volume = 2},Projectile.position);
            }
            Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch);
            d.velocity = Projectile.velocity * Main.rand.NextFloat(0.5f,1.2f);
            d.noGravity = !Main.rand.NextBool(4);
            d.customData = 0;
            d.scale = Main.rand.NextFloat(0.4f, 2f);

            Projectile.ai[0]++;
            Projectile.velocity *= 0.99f;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

            if (Projectile.timeLeft < 20)
            {
                Projectile.Opacity *= 0.9f;
                Projectile.scale *= 0.95f;
            }
            else
            {
                Projectile.scale += MathF.Sin(Projectile.ai[0] * 0.2f) * 0.01f;
                Projectile.Opacity += MathF.Sin(Projectile.ai[0] * 0.2f) * 0.04f;
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(1f, 1f, 1f, 0f) * Projectile.Opacity;
        }
    }
}
