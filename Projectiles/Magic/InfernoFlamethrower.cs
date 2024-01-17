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

namespace TerrafirmaRedux.Projectiles.Magic
{
    internal class InfernoFlamethrower : ModProjectile
    {

        Color flamecolor = new Color(255, 255, 2, 128);
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.Flames}";

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255,248,2, 128);
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
            Projectile.DamageType = DamageClass.Magic;

            Projectile.tileCollide = true;
            Projectile.timeLeft = 600;
            Projectile.penetrate = -1;
            Projectile.Opacity = 0.1f;

            DrawOriginOffsetX = 0;
            DrawOriginOffsetY = 0;

            Projectile.ArmorPenetration = 15;

            Projectile.sentry = true;
            Projectile.extraUpdates = 1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }

        public override void AI()
        {
            Projectile.frameCounter++;
            if (Projectile.ai[0] == 0)
                Projectile.rotation = Main.rand.NextFloat(MathHelper.TwoPi);
            Projectile.ai[0]++;
            Projectile.rotation += 0.1f;

            if (Projectile.penetrate == -2)
            {
                Projectile.Opacity *= 0.9f;
            }
            Projectile.Opacity += 0.01f;
            if (Projectile.frame == 6)
            {
                Projectile.Kill();
            }

            if (Projectile.frameCounter == 8 && Projectile.frame != 6)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
            if(Projectile.frame > 3)
            {
                flamecolor = Color.Lerp(flamecolor, Color.Black * 0.4f, 0.1f);
                Projectile.Opacity -= 0.4f;
                if (Main.rand.NextBool(8))
                {
                    Dust newdust = Dust.NewDustPerfect(Projectile.Center + new Vector2(Main.rand.Next(-Projectile.width, Projectile.width), Main.rand.Next(-Projectile.height / 2, Projectile.height / 2)), DustID.Smoke, Projectile.velocity.RotatedByRandom(1) * 2f, 128, Color.Black, Main.rand.NextFloat(1f, 2f));
                    newdust.noGravity = true;
                    newdust.velocity.Y -= 4;
                }
            }
            else
            {
                if (Main.rand.NextBool(3))
                {
                    Dust newdust = Dust.NewDustPerfect(Projectile.Center + new Vector2(Main.rand.Next(-Projectile.width, Projectile.width), Main.rand.Next(-Projectile.height / 2, Projectile.height / 2)), DustID.InfernoFork, Projectile.velocity.RotatedByRandom(1) * 2f, 0, Color.White, Main.rand.NextFloat(0.5f, 1.5f) + Projectile.Opacity);
                    newdust.noGravity = true;
                }
                flamecolor = Color.Lerp(flamecolor, new Color(255, 64, 0, 128), 0.1f);
            }

        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.penetrate = -2;
            return false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (Projectile.frame > 4)
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
            Main.EntitySpriteDraw(Flame, Projectile.Center - Main.screenPosition, new Rectangle(0, (Flame.Height / 7) * Projectile.frame, Flame.Width, Flame.Height / 7), flamecolor, Projectile.rotation, new Vector2(Flame.Width / 2, (Flame.Height / 7) / 2), Projectile.scale * 1.2f, SpriteEffects.None, 0);
            Main.EntitySpriteDraw(Flame, Projectile.Center - Main.screenPosition, new Rectangle(0, (Flame.Height / 7) * Projectile.frame, Flame.Width, Flame.Height / 7), new Color(flamecolor.R * 1, flamecolor.G * 1f,flamecolor.B * 2, 0f) * Projectile.Opacity, Projectile.rotation * 0.5f, new Vector2(Flame.Width / 2, (Flame.Height / 7) / 2), Projectile.scale * 0.7f, SpriteEffects.None, 0);

            return false;
        }
    }
}
