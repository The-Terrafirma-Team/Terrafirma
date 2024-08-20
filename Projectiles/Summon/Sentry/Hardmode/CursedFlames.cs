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

namespace Terrafirma.Projectiles.Summon.Sentry.Hardmode
{
    internal class CursedFlames : ModProjectile
    {

        Color flamecolor = new Color(96, 248, 2);
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.Flames}";

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(96, 248, 2, 0f);
        }

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 7;
        }
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.height = 30;
            Projectile.width = 30;
            Projectile.DamageType = DamageClass.Summon;

            Projectile.tileCollide = true;
            Projectile.timeLeft = 15;
            Projectile.penetrate = -1;
            Projectile.Opacity = 0.1f;
            Projectile.scale = 0.1f;

            DrawOriginOffsetX = 0;
            DrawOriginOffsetY = 0;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.ArmorPenetration = 5;
            Projectile.stopsDealingDamageAfterPenetrateHits = true;

            //Projectile.sentry = true;


        }

        public override void AI()
        {
            Projectile.frameCounter++;
            Projectile.ai[0]++;
            Projectile.rotation += 0.3f;

            if (Projectile.timeLeft <= 5) Projectile.Opacity -= 0.5f;
            else Projectile.Opacity += 0.15f;
            if (Projectile.scale < 1f) Projectile.scale += 0.2f;

            if (Projectile.frame == 6) Projectile.frame = 0;

            if (Projectile.frameCounter % 4 == 0 && Projectile.frame != 6)
            {
                Dust newdust = Dust.NewDustPerfect(Projectile.Center + new Vector2(Main.rand.Next(-Projectile.width / 2, Projectile.width / 2), Main.rand.Next(-Projectile.height / 2, Projectile.height / 2)), DustID.CursedTorch, (Projectile.velocity * 0.1f).RotatedByRandom(0.4f), 0, Color.White, Main.rand.NextFloat(1.5f, 2f));
                Projectile.frame++;
            }
            flamecolor = new Color(96 / 255f * Math.Clamp(Projectile.ai[0] / 10f, 0.8f, 2f), 248 / 255f * Math.Clamp(Projectile.ai[0] / 10f, 0.8f, 1.2f), 2 / 255f * Math.Clamp(Projectile.ai[0] / 10f, 0.8f, 1f), 0.5f);


        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return base.OnTileCollide(oldVelocity);
        }
        public override bool? CanHitNPC(NPC target)
        {
            return base.CanHitNPC(target);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.CursedInferno, 120);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D Flame = TextureAssets.Projectile[ProjectileID.Flames].Value;
            for (int i = 0; i < 4; i++)
            {
                for (int k = 0; k < 4; k++)
                {
                    Main.EntitySpriteDraw(Flame,
                        Projectile.Center - Main.screenPosition - new Vector2(30 * i - 10 * k, 0).RotatedBy(Projectile.velocity.ToRotation()),
                        new Rectangle(0, Flame.Height / 7 * Projectile.frame, Flame.Width, Flame.Height / 7),
                        flamecolor * (1f - (0.3f * i) - (0.3f * k)) * Projectile.Opacity,
                        Projectile.rotation + (0.5f * k),
                        new Vector2(Flame.Width / 2, Flame.Height / 7 / 2),
                        Projectile.scale + (0.5f * k),
                        SpriteEffects.None,
                        0);
                }
            }



            return false;
        }

    }
}
