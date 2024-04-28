using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terrafirma.Projectiles.Ranged.Boomerangs;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.GameContent;

namespace Terrafirma.Projectiles.Summon.Sentry.PreHardmode
{
    internal class HellfireBall : ModProjectile
    {
        public override string Texture => "Terrafirma/Projectiles/Summon/Sentry/PreHardmode/BigFireball";
        static Asset<Texture2D> bigtex;
        static Asset<Texture2D> tex;

        public override void Load()
        {
            bigtex = ModContent.Request<Texture2D>(Texture);
            tex = TextureAssets.Projectile[ProjectileID.Flamelash];
            base.Load();
        }
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.height = 20;
            Projectile.width = 20;
            Projectile.DamageType = DamageClass.Summon;

            Projectile.tileCollide = true;
            Projectile.timeLeft = 60 * 5;
            Projectile.penetrate = -1;
            Projectile.sentry = true;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (target == Main.npc[(int)Projectile.ai[2]]) Projectile.Kill();
            base.OnHitNPC(target, hit, damageDone);
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() - MathHelper.PiOver2;
            Projectile.ai[0]++;
            if (Projectile.ai[0] % 2 == 0)
            {
                Dust dust = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.Torch, 0, 0, 0, Color.White, 2f);
                dust.noGravity = true;
            }
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.Center, 4, 4, DustID.Torch, Main.rand.Next(-5,5), Main.rand.Next(-5, 5), 0, Color.White, 2f);
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Rectangle texBounds = new Rectangle(0,(tex.Height() / 6) * (int)((Projectile.ai[0] / 4) % 6), tex.Width(), tex.Height()/6);
            Main.EntitySpriteDraw(tex.Value,
                Projectile.Center - Main.screenPosition,
                texBounds,
                Color.White * 0.4f,
                Projectile.rotation,
                new Vector2(tex.Width() / 2, tex.Height() / 12),
                1.6f,
                SpriteEffects.None);
            Main.EntitySpriteDraw(tex.Value,
                Projectile.Center - Main.screenPosition,
                texBounds,
                Color.White,
                Projectile.rotation,
                new Vector2(tex.Width() / 2, tex.Height() / 12),
                1.3f,
                SpriteEffects.None);

            return false;
        }
    }
}
