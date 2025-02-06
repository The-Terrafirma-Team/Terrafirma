using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terrafirma.Data;
using Terrafirma.Particles;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Projectiles.Ranged
{
    internal class TyphoonBulletProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileSets.CanBeReflected[Type] = true;
        }
        public override void SetDefaults()
        {
            Projectile.width = 8; 
            Projectile.height = 8; 

            Projectile.friendly = true; 
            Projectile.hostile = false; 
            Projectile.DamageType = DamageClass.Ranged; 
            Projectile.penetrate = -1; 

            Projectile.timeLeft = 600; 

            Projectile.ignoreWater = true; 
            Projectile.tileCollide = true; 
            Projectile.extraUpdates = 1;

            AIType = ProjectileID.Bullet;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;

            Projectile.extraUpdates = 20;
            
        }

        public override void AI()
        {
            if (Projectile.ai[0] == 0) 
            { 
                Projectile.velocity *= 0.25f; 
                Projectile.Opacity = 0;
            }


            Projectile.ai[0]++;
            
            Dust newdust = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.Water_Jungle, -Projectile.velocity.X / 5f, -Projectile.velocity.Y / 5f, 0, new Color(190,200,215,1), Main.rand.NextFloat(1.2f,2f));
            newdust.velocity.Y = -2;
            newdust.noGravity = true;
            if (Projectile.ai[0] % 20 == 0)
            {
                TyphoonParticle p = new TyphoonParticle();
                p.Scale = (1000f - Projectile.Center.Distance(Main.player[Projectile.owner].MountedCenter)) / 500f;
                p.Rotation = Projectile.rotation;
                ParticleSystem.AddParticle(p, Projectile.Center, -Projectile.velocity * 2f);
            }
            if (Projectile.ai[0] % 80 == 0) SoundEngine.PlaySound(SoundID.Item21, Projectile.Center);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            return false;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

            Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
            }

            return true;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            TyphoonParticle p = new TyphoonParticle();
            p.Scale = 1; p.Rotation = Projectile.rotation;
            ParticleSystem.AddParticle(p, Projectile.Center, -Projectile.velocity * 2f);
            for (int i = 0; i < 10; i++)
            {
                Dust newdust = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.Water_Jungle, Main.rand.NextVector2Circular(10,10).X, Main.rand.NextVector2Circular(10, 10).Y, 0, new Color(190, 200, 215, 1), Main.rand.NextFloat(1.2f, 2f));
            }
            Projectile.damage -= 2;
            base.OnHitNPC(target, hit, damageDone);
        }
        public override void OnKill(int timeLeft)
        {
            Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        }

    }
}
