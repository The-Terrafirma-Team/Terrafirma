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
using Terrafirma.Global.Items;

namespace Terrafirma.Projectiles.Summon.Sentry
{
    internal class ClockworkTurret : ModProjectile
    {
        float turretradius = 300f;
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.height = 62;
            Projectile.width = 54;
            Projectile.DamageType = DamageClass.Summon;

            Projectile.tileCollide = true;
            Projectile.timeLeft = Projectile.SentryLifeTime;
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
            turretradius = 300f * Projectile.GetSentryRangeMultiplier();
            Projectile.ai[0]++;
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                if (Projectile.Center.Distance(Main.projectile[i].Center) < turretradius + 5f && Main.projectile[i].sentry)
                {
                    Main.projectile[i].GetGlobalProjectile<SentryStats>().BuffTime[SentryBuffID.ClockworkTurret] = 10;
                }
            }

            if (Projectile.ai[0] % 6 == 0) Projectile.frame = Projectile.frame == 0 ? 1 : 0;
            if (Projectile.ai[0] % 4 == 0)
            {
                Dust newdust = Dust.NewDustPerfect(Projectile.Center + new Vector2(-16,-24), DustID.Smoke, new Vector2(Main.rand.NextFloat(-0.2f,0.2f), Main.rand.NextFloat(-4f, -0.5f)), 1, Color.White, 1f);
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Asset<Texture2D> SentryBase = ModContent.Request<Texture2D>("Terrafirma/Projectiles/Summon/Sentry/ClockworkTurret");
            Asset<Texture2D> SentryBorder = ModContent.Request<Texture2D>("Terrafirma/Projectiles/Summon/Sentry/ClockworkTurretBorder");

            Main.EntitySpriteDraw(SentryBase.Value, Projectile.Center - Main.screenPosition + new Vector2(0,SentryBase.Height()/4) , new Rectangle(0, (SentryBase.Height() / 2 * Projectile.frame), SentryBase.Width(),SentryBase.Height()/2), lightColor, 0, SentryBase.Size() / 2, 1, SpriteEffects.None, 0);
            
            for (int i = 0; i < (int)(turretradius / 8); i++)
            {
                float maxi = turretradius / 8;
                float rotationvar = 200f;
                Main.EntitySpriteDraw(SentryBorder.Value, Projectile.Center - Main.screenPosition + new Vector2(turretradius, 0).RotatedBy((((365 / maxi) * i) * (Math.PI / 180)) + (Projectile.ai[0] / rotationvar)), new Rectangle(0, 0, SentryBorder.Width(), SentryBorder.Height()), new Color(150, 100, 0, 0), ((365 / maxi) * i) * (float)(Math.PI / 180) + (Projectile.ai[0] / rotationvar), SentryBorder.Size() / 2, 0.75f + (i % 2 == 0 ? 0.25f : 0f), SpriteEffects.None, 0) ;
            }

            return false;
        }
    }
}
