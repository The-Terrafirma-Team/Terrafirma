using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terrafirma.Common.Templates.Melee;
using Terrafirma.Particles;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Projectiles.Melee
{
    public class SteelGreatsword : UpDownSwing
    {
        public override string Texture => "Terrafirma/Items/Weapons/Melee/Swords/SteelGreatsword";

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            BigSparkle p = new BigSparkle();
            p.Scale = 2;
            p.fadeInTime = 6;
            p.smallestSize = 0.01f;
            p.secondaryColor = Color.Transparent;
            ParticleSystem.AddParticle(p, target.Hitbox.ClosestPointInRect(Projectile.Center),null,new Color(180,180,250,0));
        }
    }
    public class SteelGreatswordSlash : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.Size = new Vector2(80);
            Projectile.aiStyle = -1;
            Projectile.alpha = 254;
            Projectile.timeLeft = 600;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            for (int i = 0; i < 5; i++)
            {
                Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, Projectile.Center - (Projectile.velocity * i) - Main.screenPosition, null, lightColor * Projectile.Opacity * (0.5f - i * 0.1f), Projectile.rotation, new Vector2(40, 24), Projectile.scale - (i*0.1f), SpriteEffects.None);
            }

            Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, Projectile.Center - Main.screenPosition, null, lightColor * Projectile.Opacity, Projectile.rotation, new Vector2(40, 24), Projectile.scale, SpriteEffects.None);
            
            return false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (!Collision.CanHitLine(Main.player[Projectile.owner].Center,1,1,Projectile.Center,1,1))
                return false;
            return base.CanHitNPC(target);
        }
        public override void AI()
        {
            Projectile.velocity *= 0.93f;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

            if (Projectile.timeLeft > 580)
                Projectile.alpha -= 20;
            else
                Projectile.alpha += 20;
            if (Projectile.alpha > 255)
                Projectile.Kill();
        }
    }
}
