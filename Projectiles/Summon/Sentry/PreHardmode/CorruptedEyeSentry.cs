using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.ID;
using Terraria.GameContent;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;

namespace Terrafirma.Projectiles.Summon.Sentry.PreHardmode
{
    public class CorruptedEyeSentry : ModProjectile
    {
        NPC TargetNPC = null;
        Vector2 LastRecordedPos = Vector2.Zero;
        float AttackTimer = 60f;
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.height = 36;
            Projectile.width = 24;
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
            Projectile.velocity.Y += 0.5f;
            AttackTimer = 60f * TFUtils.GetSentryAttackCooldownMultiplier(Projectile);

            NPC ClosestNPC = TFUtils.FindClosestNPC(600f * TFUtils.GetSentryRangeMultiplier(Projectile), Projectile.Center, TargetThroughWalls: true);
            if (TargetNPC == null &&
                ClosestNPC != null)               
            {
                TargetNPC = ClosestNPC;
                LastRecordedPos = Vector2.Zero;
            }
            if (TargetNPC != null && Projectile.Center.Distance(TargetNPC.Center) > 600f * TFUtils.GetSentryRangeMultiplier(Projectile) || TargetNPC != null && !TargetNPC.active)
            {
                LastRecordedPos = TargetNPC.Center;
                TargetNPC = null;
                Projectile.ai[1] = 1f;
            }
            if (TargetNPC != null)
            {
                Projectile.ai[0] = Projectile.ai[0] > AttackTimer ? 0 : Projectile.ai[0] += 1;
                if (!Collision.CanHitLine(Projectile.Center, 8, 8, TargetNPC.position, TargetNPC.width, TargetNPC.height))
                {
                    TargetNPC = null;
                }
            }
            if (Projectile.ai[0] >= AttackTimer && TargetNPC != null)
            {
                Vector2 Dist = (Projectile.Center - new Vector2(2, 6)).DirectionTo(TargetNPC.Center) * ((Projectile.Center - new Vector2(2, 6)).Distance(TargetNPC.Center) * 0.05f);
                for (int i = 0; i < 8; i++)
                {
                    Dust dust = Dust.NewDustDirect(Projectile.Center + Dist, 4, 4, DustID.GemAmethyst, Main.rand.NextFloat(-0.5f, 0.5f), Main.rand.NextFloat(-0.5f, 0.5f), 0, new Color(180, 20, 255, 0));
                    dust.noGravity = true;
                }    
                if (Main.LocalPlayer.whoAmI == Projectile.owner) Projectile.NewProjectileButWithChangesFromSentryBuffs(Projectile.GetSource_FromThis(), Projectile.Center + Dist, Vector2.Zero, ModContent.ProjectileType<PsychicRing>(), Projectile.damage, Projectile.knockBack, Projectile.owner, TargetNPC.whoAmI);
                SoundEngine.PlaySound(new SoundStyle("Terrafirma/Sounds/CorruptEye") { Pitch = MathF.Sin((float)Main.timeForVisualEffects * 0.03f) * 0.3f, MaxInstances = 10, Volume = 0.5f }, Projectile.position);
                Projectile.ai[0] = 0;
            }
            if(TargetNPC == null)
                Projectile.ai[1] *= 0.95f;
        }
        
        public override bool PreDraw(ref Color lightColor)
        {
            Color GlowColor = new Color(180, 20, 255,0);

            Asset<Texture2D> tex = ModContent.Request<Texture2D>("Terrafirma/Projectiles/Summon/Sentry/PreHardmode/CorruptedEyeSentry", AssetRequestMode.ImmediateLoad);
            // Body
            Main.EntitySpriteDraw(tex.Value,
                Projectile.Center - Main.screenPosition,
                new Rectangle(0, 0, 24, 36),
                lightColor,
                0,
                new Vector2(14, 18),
                1f,
                SpriteEffects.None);

            if (TargetNPC != null && (Projectile.Center - new Vector2(2, 6)).Distance(TargetNPC.Center) < 200f)
            {
                Vector2 Dist = TargetNPC != null ? (Projectile.Center - new Vector2(2, 6)).DirectionTo(TargetNPC.Center) * ((Projectile.Center - new Vector2(2, 6)).Distance(TargetNPC.Center) * 0.05f) : Vector2.Zero;
                // Head forwards
                Main.EntitySpriteDraw(tex.Value,
                    Projectile.Center - Main.screenPosition - new Vector2(2, 6) + Dist,
                    new Rectangle(54, 0, 36, 30),
                    lightColor,
                    0,
                    new Vector2(18, 22),
                    1f,
                    SpriteEffects.None);
                // Head forwards glow
                Main.EntitySpriteDraw(tex.Value,
                    Projectile.Center - Main.screenPosition - new Vector2(2, 6) + Dist,
                    new Rectangle(54, 32, 36, 30),
                    GlowColor * (Projectile.ai[0] / AttackTimer),
                    0,
                    new Vector2(18, 22),
                    1f,
                    SpriteEffects.None);
                // Head forwards big glow
                Main.EntitySpriteDraw(tex.Value,
                    Projectile.Center - Main.screenPosition - new Vector2(2, 6) + Dist,
                    new Rectangle(54, 32, 36, 30),
                    GlowColor * (Projectile.ai[0] / AttackTimer) * 0.3f,
                    0,
                    new Vector2(18, 22),
                    1.5f,
                    SpriteEffects.None);
            }
            else if (TargetNPC != null)
            {
                float rot = Projectile.Center.DirectionTo(TargetNPC.Center).ToRotation();
                // Head side
                Main.EntitySpriteDraw(tex.Value,
                    Projectile.Center - Main.screenPosition - new Vector2(0, 8),
                    new Rectangle(26, 0, 22, 30),
                    lightColor,
                    rot,
                    Math.Abs(rot) > (float)MathHelper.PiOver2 ? new Vector2(6, 12) : new Vector2(6, 19),
                    1f,
                    Math.Abs(rot) > (float)MathHelper.PiOver2 ? SpriteEffects.FlipVertically : SpriteEffects.None);
                // Head side glow
                Main.EntitySpriteDraw(tex.Value,
                    Projectile.Center - Main.screenPosition - new Vector2(0, 8),
                    new Rectangle(26, 32, 22, 30),
                    GlowColor * (Projectile.ai[0] / AttackTimer),
                    rot,
                    Math.Abs(rot) > (float)MathHelper.PiOver2 ? new Vector2(6, 12) : new Vector2(6, 19),
                    1f,
                    Math.Abs(rot) > (float)MathHelper.PiOver2 ? SpriteEffects.FlipVertically : SpriteEffects.None);
                // Head side big glow
                Main.EntitySpriteDraw(tex.Value,
                    Projectile.Center - Main.screenPosition - new Vector2(0, 8),
                    new Rectangle(26, 32, 22, 30),
                    GlowColor * (Projectile.ai[0] / AttackTimer) * 0.3f,
                    rot,
                    Math.Abs(rot) > (float)MathHelper.PiOver2 ? new Vector2(6, 12) : new Vector2(6, 19),
                    1.2f,
                    Math.Abs(rot) > (float)MathHelper.PiOver2 ? SpriteEffects.FlipVertically : SpriteEffects.None);
            }
            else
            {
                Vector2 Dist = (Projectile.Center - new Vector2(2, 6)).DirectionTo(LastRecordedPos) * ((Projectile.Center - new Vector2(2, 6)).Distance(LastRecordedPos) * 0.02f);   
                Main.EntitySpriteDraw(tex.Value,
                    Projectile.Center - Main.screenPosition - new Vector2(2, 6) + Vector2.Lerp(Dist, Vector2.Zero, 1f - MathHelper.Clamp(Projectile.ai[1],0,1)),
                    new Rectangle(54, 0, 36, 30),
                    lightColor,
                    0,
                    new Vector2(18, 22),
                    1f,
                    SpriteEffects.None);
            }

            return false;
        }
    }
}
