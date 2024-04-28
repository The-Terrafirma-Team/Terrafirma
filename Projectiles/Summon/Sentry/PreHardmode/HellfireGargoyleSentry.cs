using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.ID;
using Terraria.GameContent;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Terrafirma.Projectiles.Summon.Sentry.PreHardmode
{
    public class HellfireGargoyleSentry : ModProjectile
    {
        int shoottime;
        Asset<Texture2D> texglow;
        Asset<Texture2D> tex ;

        public override void Load()
        {                     
            base.Load();
        }
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.height = 68;
            Projectile.width = 40;
            Projectile.DamageType = DamageClass.Summon;

            Projectile.tileCollide = true;
            Projectile.timeLeft = Projectile.SentryLifeTime;
            Projectile.penetrate = -1;
            Projectile.sentry = true;
        }

        public override void AI()
        {
            Projectile.velocity.Y += 0.5f;
            float maxrange = 600f * TFUtils.GetSentryRangeMultiplier(Projectile);
            shoottime = (int)(5 * TFUtils.GetSentryAttackCooldownMultiplier(Projectile));

            int highestHP = 0;
            NPC targetnpc = null;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Projectile.Center.Distance(Main.npc[i].Center) < maxrange &&
                    Main.npc[i].lifeMax > highestHP &&
                    Main.npc[i].active &&
                    Collision.CanHitLine(Projectile.Center, 8, 8, Main.npc[i].position, Main.npc[i].width, Main.npc[i].height))
                {
                    highestHP = Main.npc[i].lifeMax;
                    targetnpc = Main.npc[i];
                }
            }

            if (Projectile.ai[0] / 8 % shoottime == 0 && targetnpc != null)
            {
                Vector2 DustPos = Projectile.ai[2] == 1 ? Projectile.Center + new Vector2(18, 0) : Projectile.Center - new Vector2(22, 0);
                for (int i = 0; i < 5; i++)
                {
                    Dust dust = Dust.NewDustDirect(DustPos, 4, 4, DustID.Torch, 8f * Projectile.ai[2], Main.rand.NextFloat(-1f,3f), 0, Color.White, 2.3f);
                    dust.noGravity = true;
                }
                for (int i = 0; i < 10; i++)
                {
                    Dust dust = Dust.NewDustDirect(DustPos, 4, 4, DustID.Torch, 16f * Projectile.ai[2], Main.rand.NextFloat(-1f, 3f), 0, Color.White, 1f);
                    dust.noGravity = true;
                }

                if (Projectile.owner == Main.LocalPlayer.whoAmI && targetnpc != null)
                {
                    Projectile.NewProjectileButWithChangesFromSentryBuffs(Projectile.GetSource_FromThis(), DustPos, Projectile.DirectionTo(targetnpc.Center) * 6f, ModContent.ProjectileType<HellfireBall>(), Projectile.damage, Projectile.knockBack, Projectile.owner, ai2: targetnpc.whoAmI);
                }

                SoundEngine.PlaySound(SoundID.DD2_BetsyFireballShot, Projectile.Center);
            }

            if (targetnpc != null)
            {
                if (Projectile.Center.Distance(targetnpc.Center) >= maxrange || !targetnpc.active)
                {
                    Projectile.ai[0] = 0;
                    targetnpc = null;
                }
            }
            if (targetnpc != null) Projectile.ai[0]++;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            tex = TextureAssets.Projectile[Type];
            texglow = ModContent.Request<Texture2D>("Terrafirma/Projectiles/Summon/Sentry/PreHardmode/HellfireGargoyleSentryGlow");
            Rectangle TexFrame = new Rectangle(0,70 * (int)((Projectile.ai[0] + 2) / 8 % shoottime),64,68);
            Main.EntitySpriteDraw(tex.Value, Projectile.Center - Main.screenPosition + new Vector2(0,2), TexFrame, lightColor, 0, TexFrame.Size()/2, 1, Projectile.ai[2] == 1? SpriteEffects.None: SpriteEffects.FlipHorizontally);
            Main.EntitySpriteDraw(texglow.Value, Projectile.Center - Main.screenPosition + new Vector2(0, 2), TexFrame, Color.White, 0, TexFrame.Size() / 2, 1, Projectile.ai[2] == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally);

            return false;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
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
    }
}
