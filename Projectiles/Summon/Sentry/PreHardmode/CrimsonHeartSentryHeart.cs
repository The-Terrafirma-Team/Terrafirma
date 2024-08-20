using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
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
        private float a;
        private float b;
        private Vector2 c;

        public override void SetStaticDefaults()
        {
            HeartTex = ModContent.Request<Texture2D>("Terrafirma/Projectiles/Summon/Sentry/PreHardmode/CrimsonHeartSentryHeart");
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
        public override void AI()
        {

            NPC ClosestNPC = TFUtils.FindClosestNPC(120f * TFUtils.GetSentryRangeMultiplier(Projectile), Projectile.Center);

            if (Projectile.ai[0] % (int)(24 * TFUtils.GetSentryAttackCooldownMultiplier(Projectile)) == 0 && ClosestNPC != null) 
            {
                HeartWaveParticle HeartWave = new HeartWaveParticle();
                HeartWave.timeleft = (int)(60 * TFUtils.GetSentryRangeMultiplier(Projectile));
                ParticleSystem.AddParticle(HeartWave, Projectile.Center);
                SoundEngine.PlaySound(new SoundStyle("Terrafirma/Sounds/CrimsonHeart") { PitchVariance = 0.1f,MaxInstances = 10},Projectile.position);
                NPC[] NpcArray = TFUtils.GetAllNPCsInArea(120f * TFUtils.GetSentryRangeMultiplier(Projectile), Projectile.Center);
                for (int i = 0; i < NpcArray.Length; i++)
                {
                    TFUtils.NewProjectileButWithChangesFromSentryBuffs(Projectile, Projectile.GetSource_FromThis(), NpcArray[i].Center, Vector2.Zero, ModContent.ProjectileType<CrimsonHeartSentryInvisProj>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                }

                for (int i = 0; i < 8; i++)
                {
                    Dust dust = Dust.NewDustDirect(Projectile.position, 16, 16, DustID.Blood, Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-0.5f, 1f));
                }
            }

            if (ClosestNPC != null) 
            {
                Projectile.ai[0]++;
            }
            else if (Projectile.ai[0] % (int)(24 * TFUtils.GetSentryAttackCooldownMultiplier(Projectile)) != 0) Projectile.ai[0]++;

            Projectile.velocity.Y = (float)Math.Sin(Projectile.ai[1] / 40f) / 5f;
            Projectile.ai[1]++;

        }

        public override bool PreDraw(ref Color lightColor)
        {

            //if (BaseProj != null)
            //{
            //    for (int i = 0; i < Math.Abs(Projectile.Center.X - BaseProj.Center.X) / 10; i++)
            //    {
            //        Main.EntitySpriteDraw(BaseTex.Value,
            //            BaseProj.Center - Main.screenPosition + 
            //            new Vector2(i * 10, (a * (float)Math.Pow(BaseProj.Center.X + (i * 10) - c.X, 2) + b * (BaseProj.Center.X + (i * 10) - c.X)) * -1),
            //            i % 2 == 0 ? new Rectangle(24, 0, 14, 10) : new Rectangle(40, 0, 14, 10),
            //            lightColor * 0.3f,
            //            0,
            //            new Vector2(7, 5),
            //            1,
            //            SpriteEffects.None,
            //            0);
            //    }
            //}

            Main.EntitySpriteDraw(HeartTex.Value, Projectile.Center - Main.screenPosition, new Rectangle(0, HeartTex.Height() / 4 * (int)((Projectile.ai[0] / 6) % (int)(4 * TFUtils.GetSentryAttackCooldownMultiplier(Projectile))), HeartTex.Width(), HeartTex.Height() / 4), lightColor * 0.3f, 0, new Vector2(HeartTex.Width(), HeartTex.Height() / 4) / 2, 1.1f + ((float)Math.Sin(Main.timeForVisualEffects / 20) + 1) * 0.1f, SpriteEffects.None, 0);
            Main.EntitySpriteDraw(HeartTex.Value, Projectile.Center - Main.screenPosition, new Rectangle(0, HeartTex.Height() / 4 * (int)((Projectile.ai[0] / 6) % (int)(4 * TFUtils.GetSentryAttackCooldownMultiplier(Projectile))), HeartTex.Width(), HeartTex.Height()/4), lightColor, 0, new Vector2(HeartTex.Width(), HeartTex.Height() / 4) / 2, 1, SpriteEffects.None, 0);
            Main.EntitySpriteDraw(HeartTex.Value, Projectile.Center - Main.screenPosition, new Rectangle(0, HeartTex.Height() / 4 * (int)((Projectile.ai[0] / 6) % (int)(4 * TFUtils.GetSentryAttackCooldownMultiplier(Projectile))), HeartTex.Width(), HeartTex.Height() / 4), new Color(50,30,50,0), 0, new Vector2(HeartTex.Width(), HeartTex.Height() / 4) / 2, 1, SpriteEffects.None, 0);

            return false;
        }
    }
}
