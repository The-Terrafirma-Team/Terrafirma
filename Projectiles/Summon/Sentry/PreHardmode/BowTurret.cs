using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Steamworks;
using Terraria.GameContent;
using Terrafirma.Common.Templates;

namespace Terrafirma.Projectiles.Summon.Sentry.PreHardmode
{
    public class BowTurret : SentryTemplate
    {
        Texture2D tex;
        NPC targetNPC = null;
        float pullback = 0f;
        float rot = 0f;

        Item selectedarrow = null;
        int arrowslot = 0;

        public override void SetStaticDefaults()
        {
            Texture2D tex = ModContent.Request<Texture2D>(Texture).Value;
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            tex = ModContent.Request<Texture2D>(Texture).Value;

            Projectile.friendly = true;
            Projectile.height = 44;
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
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            fallThrough = false;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override void OnHitByWrench(Player player, WrenchItem wrench)
        {
            Projectile.ai[0] = (int)(60 * TFUtils.GetSentryAttackCooldownMultiplier(Projectile));
        }
        public override void AI()
        {
            Projectile.velocity.Y += 0.5f;

            targetNPC = TFUtils.FindSummonTarget(Projectile, 500f, Projectile.Center, false);
            if (targetNPC != null && (!targetNPC.active || targetNPC.Distance(Projectile.Center) > 500f)) targetNPC = null;

            selectedarrow = null;
            for (int i = 57; i >= 54; i--)
            {
                if (Main.player[Projectile.owner].inventory[i].ammo == AmmoID.Arrow && Main.player[Projectile.owner].inventory[i].stack > 0) 
                {
                    arrowslot = i;
                    selectedarrow = Main.player[Projectile.owner].inventory[i]; 
                }
            }

            if (Projectile.ai[0] % (int)(60 * TFUtils.GetSentryAttackCooldownMultiplier(Projectile)) == 0 && Projectile.owner == Main.myPlayer && targetNPC != null && selectedarrow != null)
            {
                if (!Main.player[Projectile.owner].IsAmmoFreeThisShot(ContentSamples.ItemsByType[ItemID.WoodenBow], selectedarrow, selectedarrow.shoot)) Main.player[Projectile.owner].inventory[arrowslot].stack--;
                Projectile.NewProjectileButWithChangesFromSentryBuffs(Projectile.GetSource_FromThis(), Projectile.Center - new Vector2(-11, 5), (Projectile.Center - new Vector2(-11, 5)).DirectionTo(targetNPC.Center) * 10f, selectedarrow.shoot, Projectile.damage + selectedarrow.damage, selectedarrow.knockBack, Projectile.owner);
                pullback = -10f;
                SoundEngine.PlaySound(SoundID.Item5, Projectile.Center);
            }

            if (targetNPC != null) rot = Utils.AngleLerp(rot, (Projectile.Center - new Vector2(1, 5)).DirectionTo(targetNPC.Center).ToRotation(), 0.12f);
            else rot *= 0.9f;

            pullback *= 0.95f;
            Projectile.ai[0]++;

        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D arrowtex = selectedarrow != null ? TextureAssets.Item[selectedarrow.type].Value : null;

            Main.EntitySpriteDraw(tex,
                Projectile.Center - Main.screenPosition + new Vector2(0,8),
                new Rectangle(0,0,36,32),
                lightColor,
                0f,
                new Vector2(18,16),
                1f,
                SpriteEffects.None);           
            Main.EntitySpriteDraw(tex,
                Projectile.Center - Main.screenPosition - new Vector2(1, 5) - new Vector2(pullback,0).RotatedBy(rot),
                new Rectangle(40, 0, 14, 42),
                lightColor,
                rot,
                new Vector2(7, 21),
                1f,
                SpriteEffects.None);

            if (arrowtex != null && (Projectile.ai[0] % (int)(60 * TFUtils.GetSentryAttackCooldownMultiplier(Projectile)) > 12 || targetNPC == null))
            {
                Main.EntitySpriteDraw(arrowtex,
                    Projectile.Center - Main.screenPosition - new Vector2(1, 5) - new Vector2(pullback, 0).RotatedBy(rot),
                    arrowtex.Bounds,
                    lightColor,
                    rot - MathHelper.PiOver2,
                    arrowtex.Size()/2,
                    1f,
                    SpriteEffects.None);
            }

            return false;
        }
    }
}
