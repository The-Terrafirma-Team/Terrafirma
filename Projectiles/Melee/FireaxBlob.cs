using Terraria.ID;
using Terraria;
using Terrafirma.Common.Templates;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terrafirma.Particles;
using Terraria.Audio;

namespace Terrafirma.Projectiles.Melee
{
    public class FireaxeBlob: ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.hide = true;
            Projectile.penetrate = 15;
            Projectile.tileCollide = true;
            Projectile.friendly = true;
        }
        public override void AI()
        {
            PixelCircle p = new PixelCircle();
            p.scale = Main.rand.NextFloat(3,5);
            p.gravity = Main.rand.NextFloat(0.02f,0.3f);
            p.outlineColor = Color.OrangeRed;
            if (Projectile.ai[0] == 0)
            ParticleSystem.AddParticle(p,Projectile.Center,Projectile.velocity.RotatedByRandom(0.1f),Color.Lerp(Color.Gold,Color.Red,Main.rand.NextFloat(0.2f)),ParticleLayer.BehindTiles);
            else
            {
                p.gravity *= 0.4f;
                p.scale *= (Projectile.timeLeft / 200f) + 1f;
                ParticleSystem.AddParticle(p, Projectile.Center + new Vector2(0,15), Main.rand.NextVector2Circular(4,2) + new Vector2(0,-1), Color.Lerp(Color.Gold, Color.Red, Main.masterColor * 0.5f), ParticleLayer.BehindTiles);

                if (Main.rand.NextBool(6))
                {
                    PixelCircle p2 = new PixelCircle();
                    p2.scale = Main.rand.NextFloat(2, 3);
                    p2.gravity = -Main.rand.NextFloat(0.02f, 0.3f);
                    p2.outlineColor = Color.OrangeRed;
                    ParticleSystem.AddParticle(p2, Projectile.Center, Main.rand.NextVector2Circular(7, 2) + new Vector2(0, -1), Color.Lerp(Color.Gold, Color.Red, Main.masterColor * 0.5f), ParticleLayer.BehindTiles);
                }
            }

            Projectile.velocity.Y += 0.3f;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            hitboxCenterFrac.Y -= 2;
            width = 3;
            height = 3;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.timeLeft > 200)
            {
                SoundEngine.PlaySound(SoundID.Item74);
                Projectile.timeLeft = 200;
            }

            Projectile.velocity *= 0.1f;
            Projectile.ai[0] = 1;
            return false;
        }
    }
}
