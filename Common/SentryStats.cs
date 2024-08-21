using Terraria.ModLoader;
using Terraria;
using Terraria.ModLoader.IO;
using System.IO;
using Terraria.ID;
using Microsoft.CodeAnalysis;
using Microsoft.Xna.Framework;
using Terrafirma.Particles;
using Terrafirma.Projectiles.Summon.Sentry;
using Microsoft.Xna.Framework.Graphics;
using Terrafirma.Projectiles.Summon.Sentry.Hardmode;
using Terrafirma.Projectiles.Summon.Sentry.PreHardmode;
using ReLogic.Content;
using Terrafirma.Common.Templates;

namespace Terrafirma.Common
{
    public class SentryItemChanges : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.sentry;
        }
        public override bool CanUseItem(Item item, Player player)
        {
            Projectile proj = new Projectile();
            proj.SetDefaults(item.shoot);
            if (proj.GetGlobalProjectile<SentryStats>().SentrySlots > player.maxTurrets)
            {
                return false;
            }
            return base.CanUseItem(item, player);
        }
    }
    public class SentryStats : Terraria.ModLoader.GlobalProjectile
    {
        private static Asset<Texture2D> Bookmark;

        public float SentrySlots = 1f;
        public int ClockworkSentryBonus;
        public bool Priority = false;
        public override bool InstancePerEntity => true;

        public override void SetStaticDefaults()
        {
            Bookmark = ModContent.Request<Texture2D>(Terrafirma.AssetPath + "Bookmark");
        }
        public override void SetDefaults(Projectile entity)
        {
            if (entity.type == ModContent.ProjectileType<ClockworkTurret>()) SentrySlots = 2f;
            if (entity.type == ModContent.ProjectileType<IchorSentry>()) SentrySlots = 1.5f;
            if (entity.type == ModContent.ProjectileType<CursedFlameSentry>()) SentrySlots = 1.5f;
            if (entity.type == ModContent.ProjectileType<RoyalJellyDispenser>()) SentrySlots = 2f;
            if (entity.type == ModContent.ProjectileType<BarbedWireCanisterSentry>()) SentrySlots = 0.5f;
            if (entity.type == ModContent.ProjectileType<GREATSentry>()) SentrySlots = 4f;
            Priority = false;
        }
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return entity.sentry;
        }
        public int BuffTime = 0;
        public SentryBuff BuffType = null;

        public float SpeedMultiplier = 1f;
        public float RangeMultiplier = 1f;
        public float DamageMultiplier = 1f;
        void ResetEffects(Projectile entity)
        {
            SpeedMultiplier = 1f;
            RangeMultiplier = 1f;
            DamageMultiplier = 1f;
        }
        public void UpdateBuffs(Projectile projectile)
        {
            ClockworkSentryBonus--;

            if (ClockworkSentryBonus > 0)
                SpeedMultiplier *= 0.8f;

            BuffTime--;
            if(BuffType != null)
            {
                BuffType.UpdateSentry(projectile,this);
            }
            if(BuffTime <= 0)
            {
                BuffType = null;
            }
            //if (BuffTime[SentryBuffID.InflictShadowflame] > 0 && Main.rand.NextBool(5))
            //{
            //    Dust d = Dust.NewDustDirect(projectile.BottomLeft + new Vector2(0, -4), projectile.width, 0, DustID.Shadowflame, 0, -projectile.height / 10);
            //    d.noGravity = true;
            //    d.velocity.X *= 0.1f;
            //    d.scale *= 1.5f;
            //    d.alpha = 128;
            //    if (d.velocity.Y > 0)
            //        d.velocity *= -1;
            //}
            //if (BuffTime[SentryBuffID.CrimtaneWrench] > 0)
            //{
            //    DamageMultiplier += 0.15f;

            //    if (Main.rand.NextBool(5))
            //    {
            //        Dust d = Dust.NewDustDirect(projectile.BottomLeft + new Vector2(0, -4), projectile.width, 0, DustID.Crimson, 0, -projectile.height / 10);
            //        d.noGravity = true;
            //        d.velocity.X *= 0.1f;
            //        d.scale *= 1.5f;
            //        d.alpha = 128;
            //        if (d.velocity.Y > 0)
            //            d.velocity *= -1;
            //    }
            //}
            //if (BuffTime[SentryBuffID.CoolWrench] > 0)
            //{
            //    RangeMultiplier += 0.6f;
            //    if (Main.rand.NextBool(5))
            //    {
            //        Dust d = Dust.NewDustDirect(projectile.Center - projectile.Size / 4, projectile.width / 2, projectile.height / 2, DustID.Ice, 0, -projectile.height / 20);
            //        d.noGravity = true;
            //        d.velocity = Main.rand.NextVector2Circular(5, 5);
            //        d.noLightEmittence = true;
            //        if (d.velocity.Y > 0)
            //            d.velocity *= -1;
            //    }
            //}
            //if (BuffTime[SentryBuffID.ClockworkTurret] > 0)
            //{
            //    SpeedMultiplier *= 0.8f;
            //    if (Main.rand.NextBool(5))
            //    {
            //        Dust d = Dust.NewDustDirect(projectile.BottomLeft + new Vector2(0, -4), projectile.width, 0, DustID.TreasureSparkle, 0, -projectile.height / 20);
            //        d.noGravity = true;
            //        d.velocity.X *= 0.1f;
            //        d.noLightEmittence = true;
            //        if (d.velocity.Y > 0)
            //            d.velocity *= -1;
            //    }
            //}
            //if (BuffTime[SentryBuffID.MetalWrench] > 0)
            //{
            //    SpeedMultiplier *= 0.8f;
            //    if (Main.rand.NextBool(5))
            //    {
            //        Dust d = Dust.NewDustDirect(projectile.BottomLeft + new Vector2(0, -4), projectile.width, 0, DustID.GemDiamond, 0, -projectile.height / 20);
            //        d.noGravity = true;
            //        d.velocity.X *= 0.1f;
            //        d.noLightEmittence = true;
            //        if (d.velocity.Y > 0)
            //            d.velocity *= -1;
            //    }
            //}
        }
        public override void PostDraw(Projectile projectile, Color lightColor)
        {
            if (projectile.GetGlobalProjectile<SentryStats>().Priority == true)
            {
                Main.EntitySpriteDraw(Bookmark.Value, projectile.Bottom - Main.screenPosition, new Rectangle(0, 0, Bookmark.Width(), Bookmark.Height()), Color.White, 0, new Vector2(Bookmark.Width() / 2, 0), 1, SpriteEffects.None);
            }
        }
        public override bool PreAI(Projectile projectile)
        {
            ResetEffects(projectile);
            UpdateBuffs(projectile);
            return base.PreAI(projectile);
        }
    }
    public class SentryBulletBuff : Terraria.ModLoader.GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        public bool Shadowflame = false;
        public override void PostAI(Projectile projectile)
        {
            if (Shadowflame)
            {
                Dust d = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.Shadowflame, projectile.velocity.X, projectile.velocity.Y);
                d.noGravity = true;
                d.fadeIn = Main.rand.NextFloat(2);
                d.alpha = 128;
            }
        }
        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Shadowflame && Main.rand.NextBool(2))
            {
                target.AddBuff(BuffID.ShadowFlame, 60 * 3);
            }
        }
    }

    public static class SentryBuffID
    {
        public static SentryBuff[] sentrybuffs = new SentryBuff[]{};
    }
}
