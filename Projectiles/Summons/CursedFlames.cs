using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Projectiles.Summons
{
    internal class CursedFlames : ModProjectile
    {

        Color flamecolor = new Color(96, 248, 2);
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.Flames}";

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(96,248,2,0f);
        }

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 7;
        }
        public override void SetDefaults()
        {
            Projectile.friendly = true;

            Projectile.damage = 55;
            Projectile.height = 30;
            Projectile.width = 30;
            Projectile.DamageType = DamageClass.Summon;

            Projectile.tileCollide = true;
            Projectile.timeLeft = 200;
            Projectile.penetrate = -1;
            Projectile.Opacity = 0.1f;

            DrawOriginOffsetX = 0;
            DrawOriginOffsetY = 0;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.ArmorPenetration = 15;

            //Projectile.sentry = true;
            Projectile.extraUpdates = 1;
            

        }

        public override void AI()
        {
            Projectile.frameCounter++;
            Projectile.ai[0]++;
            Projectile.rotation += 0.1f;

            if (Projectile.penetrate == -2)
            {
                Projectile.Opacity *= 0.9f;
            }

            if (Projectile.timeLeft > 180)
            {
                Projectile.Opacity += 0.05f;
            }

            if (Projectile.frame == 6)
            {
                Projectile.Kill();
            }

            if (Projectile.frameCounter % 4 == 0 && Projectile.frame != 6)
            {
                Dust newdust = Dust.NewDustPerfect(Projectile.Center + new Vector2(Main.rand.Next(-Projectile.width, Projectile.width), Main.rand.Next(-Projectile.height / 2, Projectile.height / 2)), DustID.CursedTorch, Projectile.velocity * 0.3f, 0, Color.White, Main.rand.NextFloat(1.5f,2f));
                Projectile.frame++;
            }
            flamecolor = new Color((96 / 255f) * Math.Clamp(Projectile.ai[0] / 10f, 0.8f, 2f), (248 / 255f) * Math.Clamp(Projectile.ai[0] / 10f, 0.8f, 1.2f), (2 / 255f) * Math.Clamp(Projectile.ai[0] / 10f, 0.8f, 1f), 0.5f) * Projectile.Opacity ;

            
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.penetrate = -2;
            return base.OnTileCollide(oldVelocity);
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (Projectile.penetrate == -2)
            {
                return false;
            }
            return base.CanHitNPC(target);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.penetrate = -2;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D Flame = TextureAssets.Projectile[ProjectileID.Flames].Value;
            for (int i = 0; i < 8; i++)
            {
                Main.EntitySpriteDraw(Flame, Projectile.Center - Main.screenPosition + Main.rand.NextVector2Circular(20f,20f), new Rectangle(0, (Flame.Height / 7) * Projectile.frame, Flame.Width, Flame.Height / 7), flamecolor * 0.2f, Projectile.ai[0] * 0.1f + (float)Math.Pow(i, Projectile.ai[1]), new Vector2(Flame.Width / 2, (Flame.Height / 7) / 2), Projectile.scale * 1.2f - (i/20f), SpriteEffects.None, 0);
            }
            for (int i = 0; i < 4; i++)
            {
                Main.EntitySpriteDraw(Flame, Projectile.Center - Main.screenPosition + Main.rand.NextVector2Circular(20f,20f), new Rectangle(0, (Flame.Height / 7) * Projectile.frame, Flame.Width, Flame.Height / 7), flamecolor, Projectile.ai[0] * 0.1f + (float)Math.Pow(i, Projectile.ai[1]), new Vector2(Flame.Width / 2, (Flame.Height / 7) / 2), Projectile.scale - (i / 20f), SpriteEffects.None, 0);
            }



            return false;
        }

    }
}
