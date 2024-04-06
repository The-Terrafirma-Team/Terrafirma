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
using Terraria.DataStructures;
using Terrafirma.Particles;

namespace Terrafirma.Projectiles.Summon.Sentry.PreHardmode
{
    internal class CrimsonHeartSentryHeart : ModProjectile
    {
        public Projectile BaseProj = null;
        public override string Texture => "Terrafirma/Projectiles/Summon/Sentry/PreHardmode/CrimsonHeartSentryHeart";
        private static Asset<Texture2D> HeartTex;
        private static Asset<Texture2D> BaseTex;
        private float a;
        private float b;
        private Vector2 c;

        public override void SetStaticDefaults()
        {
            HeartTex = ModContent.Request<Texture2D>("Terrafirma/Projectiles/Summon/Sentry/PreHardmode/CrimsonHeartSentryHeart");
            BaseTex = ModContent.Request<Texture2D>("Terrafirma/Projectiles/Summon/Sentry/PreHardmode/CrimsonHeartSentryBase");
        }
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.height = 30;
            Projectile.width = 36;
            Projectile.DamageType = DamageClass.Summon;

            Projectile.tileCollide = true;
            Projectile.timeLeft = Projectile.SentryLifeTime;
            Projectile.penetrate = -1;
            Projectile.sentry = true;
            Projectile.hide = true;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            behindProjectiles.Add(index);
            base.DrawBehind(index, behindNPCsAndTiles, behindNPCs, behindProjectiles, overPlayers, overWiresUI);
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            fallThrough = false;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
        }

        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }

        public override void OnSpawn(IEntitySource source)
        {
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                if (Main.projectile[i].owner == Projectile.owner && Main.projectile[i].type == ModContent.ProjectileType<CrimsonHeartSentry>())
                {
                    BaseProj = Main.projectile[i];
                }
            }

            if (BaseProj != null)
            {
                c = new Vector2(BaseProj.Center.X + (Projectile.Center.X - BaseProj.Center.X) * 0.5f, Math.Max(Projectile.Center.Y, BaseProj.Center.Y) + 40);
                Vector2 PosA = Projectile.Center - c;
                Vector2 PosB = BaseProj.Center - c;
                a = (PosA.Y * PosB.X - PosB.Y * PosA.X) / (PosA.X * PosB.X * (PosA.X * PosB.X));
                b = (PosB.Y - a * (float)Math.Pow(PosB.X, 2)) / PosB.X;
            } 
            base.OnSpawn(source);
        }
        public override void AI()
        {

            if (BaseProj == null || BaseProj.active == false) Projectile.Kill();

            NPC ClosestNPC = TFUtils.FindClosestNPC(120f, Projectile.Center);

            if (Projectile.ai[0] % 24 == 0 && ClosestNPC != null) 
            { 
                ParticleSystem.AddParticle(new HeartWaveParticle(), Projectile.Center); 
                for (int i = 0; i < 8; i++)
                {
                    Dust dust = Dust.NewDustDirect(Projectile.position, 16, 16, DustID.Blood, Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-0.5f, 1f));
                }
            }

            if (ClosestNPC != null) Projectile.ai[0]++;
            else if (Projectile.ai[0] % 24 != 0)  Projectile.ai[0]++;
  

            Projectile.ai[1]++;

        }

        public override bool PreDraw(ref Color lightColor)
        {

            if (BaseProj != null)
            {
                for (int i = 0; i < Math.Abs(Projectile.Center.X - BaseProj.Center.X) / 10; i++)
                {
                    Main.EntitySpriteDraw(BaseTex.Value,
                        BaseProj.Center - Main.screenPosition + 
                        new Vector2(i * 10, (a * (float)Math.Pow(BaseProj.Center.X + (i * 10) - c.X, 2) + b * (BaseProj.Center.X + (i * 10) - c.X)) * -1),
                        i % 2 == 0 ? new Rectangle(24, 0, 14, 10) : new Rectangle(40, 0, 14, 10),
                        lightColor * 0.3f,
                        0,
                        new Vector2(7, 5),
                        1,
                        SpriteEffects.None,
                        0);
                }
            }

            Main.EntitySpriteDraw(HeartTex.Value, Projectile.Center - Main.screenPosition, new Rectangle(0, HeartTex.Height() / 4 * (int)((Projectile.ai[0] / 6) % 4), HeartTex.Width(), HeartTex.Height() / 4), lightColor * 0.3f, 0, new Vector2(HeartTex.Width(), HeartTex.Height() / 4) / 2, 1.1f + ((float)Math.Sin(Main.timeForVisualEffects / 20) + 1) * 0.1f, SpriteEffects.None, 0);
            Main.EntitySpriteDraw(HeartTex.Value, Projectile.Center - Main.screenPosition, new Rectangle(0, HeartTex.Height() / 4 * (int)((Projectile.ai[0] / 6) % 4), HeartTex.Width(), HeartTex.Height()/4), lightColor, 0, new Vector2(HeartTex.Width(), HeartTex.Height() / 4) / 2, 1, SpriteEffects.None, 0);
            Main.EntitySpriteDraw(HeartTex.Value, Projectile.Center - Main.screenPosition, new Rectangle(0, HeartTex.Height() / 4 * (int)((Projectile.ai[0] / 6) % 4), HeartTex.Width(), HeartTex.Height() / 4), new Color(50,30,50,0), 0, new Vector2(HeartTex.Width(), HeartTex.Height() / 4) / 2, 1, SpriteEffects.None, 0);

            return false;
        }
    }
}
