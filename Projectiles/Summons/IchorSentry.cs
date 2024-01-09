using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using static Terraria.GameContent.Animations.IL_Actions.NPCs;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace TerrafirmaRedux.Projectiles.Summons
{
    internal class IchorSentry : ModProjectile
    {
        float sentryrot = 0;
        public override string Texture => "TerrafirmaRedux/Projectiles/Summons/IchorSentryBase";
        public override void SetDefaults()
        {
            Projectile.friendly = true;

            Projectile.height = 38;
            Projectile.width = 36;
            Projectile.DamageType = DamageClass.Summon;

            Projectile.tileCollide = true;
            Projectile.timeLeft = Projectile.SentryLifeTime;
            Projectile.penetrate = -1;

            Projectile.sentry = true;
            

        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }

        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override void AI()
        {
            Projectile.velocity.Y += 0.5f;
            Projectile.ai[0]++;
            if (Projectile.ai[0] % 20 == 0 && Utils.FindClosestNPC(600f, Projectile.Center) != null)
            {
                sentryrot = (Projectile.Center - Utils.FindClosestNPC(600f, Projectile.Center).Center).ToRotation();
                Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center + new Vector2(0, -8) + new Vector2(-32, 0).RotatedBy(sentryrot), -new Vector2(3f, 0f).RotatedBy(sentryrot), ProjectileID.IchorSplash, Projectile.damage, Projectile.knockBack, Projectile.owner, 0, 0, 0);
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Asset<Texture2D> SentryBase = ModContent.Request<Texture2D>("TerrafirmaRedux/Projectiles/Summons/IchorSentryBase");
            Asset<Texture2D> SentryShooter = ModContent.Request<Texture2D>("TerrafirmaRedux/Projectiles/Summons/IchorSentryTentacle");

            Main.EntitySpriteDraw(SentryShooter.Value, Projectile.Center - Main.screenPosition + new Vector2(0, -8), null, Color.White, sentryrot - MathHelper.PiOver2, new Vector2(SentryShooter.Width()/2, SentryShooter.Height()), 1, SpriteEffects.None, 0);
            Main.EntitySpriteDraw(SentryBase.Value, Projectile.Center - Main.screenPosition + new Vector2(0,1), null, Color.White, 0, SentryBase.Size() / 2, 1, SpriteEffects.None, 0);

            return false;
        }

    }
}
