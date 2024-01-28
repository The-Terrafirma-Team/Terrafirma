using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using TerrafirmaRedux.Projectiles.Ranged.Boomerangs;
using System.Collections.Generic;
using Terraria.ID;

namespace TerrafirmaRedux.Projectiles.Summons
{
    internal class MechanicsPocketSentry : ModProjectile
    {
        Vector2 Glowrand = Vector2.Zero;

        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.height = 46;
            Projectile.width = 44;
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
            Projectile.ai[0]++;

            if (Projectile.ai[0] >= 30 * Projectile.GetSentryAttackCooldownMultiplier() && Main.myPlayer == Projectile.owner && TFUtils.FindClosestNPC(350f * Projectile.GetSentryRangeMultiplier(), Projectile.Center) != null)
            {
                SoundEngine.PlaySound(SoundID.DD2_LightningBugZap, Projectile.Center);
                Projectile.ai[0] = 0;
                Projectile newproj = Projectile.NewProjectileButWithChangesFromSentryBuffs(Projectile.GetSource_FromThis(), Projectile.Center + new Vector2(14,-8), Vector2.Zero, ModContent.ProjectileType<MechanicsPocketSentryLightning>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0, TFUtils.FindClosestNPC(350f * Projectile.GetSentryRangeMultiplier(),Projectile.Center).whoAmI, 0);
            }

            if (Projectile.ai[0] % 3 == 0) Glowrand = Main.rand.NextVector2Circular(1, 1);


        }

        public override bool PreDraw(ref Color lightColor)
        {
            Asset<Texture2D> SentryBase = ModContent.Request<Texture2D>("TerrafirmaRedux/Projectiles/Summons/MechanicsPocketSentry");
            Asset<Texture2D> SentryGlow = ModContent.Request<Texture2D>("TerrafirmaRedux/Projectiles/Summons/MechanicsPocketSentryGlow");

            Main.EntitySpriteDraw(SentryBase.Value, Projectile.Center - Main.screenPosition , null, lightColor, 0, SentryBase.Size() / 2, 1, SpriteEffects.None, 0);
            Main.EntitySpriteDraw(SentryGlow.Value, Projectile.Center - Main.screenPosition + Glowrand, null, new Color(255,255,255,0) * Main.rand.NextFloat(0.6f,1f), 0, SentryBase.Size() / 2, 1, SpriteEffects.None, 0);
            Main.EntitySpriteDraw(SentryGlow.Value, Projectile.Center - Main.screenPosition + new Vector2(-4, 4) + Main.rand.NextVector2Circular(2, 2), null, new Color(255, 255, 255, 0) * Main.rand.NextFloat(0.2f, 0.4f), 0, SentryBase.Size() / 2, 1.3f, SpriteEffects.None, 0);

            return false;
        }
    }
}
