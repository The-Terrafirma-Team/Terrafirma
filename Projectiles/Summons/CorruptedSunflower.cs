using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.ID;
using Terraria.GameContent;
using Microsoft.Xna.Framework.Graphics;

namespace TerrafirmaRedux.Projectiles.Summons
{
    public class CorruptedSunflower : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.height = 66;
            Projectile.width = 34;
            Projectile.DamageType = DamageClass.Summon;

            Projectile.tileCollide = true;
            Projectile.timeLeft = Projectile.SentryLifeTime;
            Projectile.penetrate = -1;
            Projectile.sentry = true;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Type].Value;
            Rectangle StemFrame = new Rectangle(2, 4, 32, 44);
            Rectangle FlowerFrame = new Rectangle(36, 2, 34, 34);
            Rectangle EyeFrame = new Rectangle(48, 38, 10, 10);
            Main.EntitySpriteDraw(tex, Projectile.Bottom - Main.screenPosition, StemFrame, lightColor,0,new Vector2(StemFrame.Width / 2, StemFrame.Height),1,SpriteEffects.None);

            Main.EntitySpriteDraw(tex, Projectile.Center + new Vector2(-1,-14) - Main.screenPosition, FlowerFrame, lightColor, (float)Math.Sin(Main.timeForVisualEffects * 0.1f) * 0.1f, FlowerFrame.Size() / 2, 1, SpriteEffects.None);

            Main.EntitySpriteDraw(tex, Projectile.Center + new Vector2(-1, -14) - Main.screenPosition, EyeFrame, lightColor, Projectile.rotation, EyeFrame.Size() / 2, 1, SpriteEffects.None);

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
        public override void AI()
        {
            Projectile.velocity.Y += 0.5f;
            Projectile.ai[0]++;

            Vector2 EyePos = Projectile.Center + new Vector2(-1, -14);

            if (Main.rand.NextBool(70))
            {
                Dust d = Dust.NewDustPerfect(Main.rand.NextVector2FromRectangle(Projectile.Hitbox), DustID.Corruption);
                d.velocity *= 0.5f;
                d.alpha = 128;
            }

            Player owner = Main.player[Projectile.owner];
            NPC potentialTarget = TFUtils.FindClosestNPC(1000 * Projectile.GetSentryRangeMultiplier(), Projectile.Center);
            if (owner.HasMinionAttackTargetNPC)
                Projectile.ai[2] = owner.MinionAttackTargetNPC;
            else if (potentialTarget != null)
            {
                Projectile.ai[2] = potentialTarget.whoAmI;
            }
            else
            {
                Projectile.ai[2] = -1;
                Projectile.ai[0] = 0;
            }

            if (Projectile.ai[2] != -1)
            {
                NPC target = Main.npc[(int)Projectile.ai[2]];
                Projectile.rotation = Utils.AngleLerp(EyePos.DirectionTo(target.Center).ToRotation(),Projectile.rotation,0.9f);
            }
            else
            {
                Projectile.rotation = Utils.AngleLerp(EyePos.DirectionTo(owner.Center).ToRotation(), Projectile.rotation, 0.96f);
            }

            int shotTime = 90;
            if (Projectile.ai[0] > shotTime * Projectile.GetSentryAttackCooldownMultiplier())
            {
                Projectile.ai[0] = 0;
                if (Main.LocalPlayer == owner)
                {
                    NPC target = Main.npc[(int)Projectile.ai[2]];
                    for(int i = 0; i < 2; i++)
                    Projectile.NewProjectileButWithChangesFromSentryBuffs(Projectile.GetSource_FromThis(), EyePos, EyePos.DirectionTo(target.Center + (target.velocity * MathHelper.Clamp(target.Center.Distance(EyePos) * 0.1f,0,4))).RotatedByRandom(0.1f) * 10f, ModContent.ProjectileType<CorruptedSunflowerShot>(), Projectile.damage,Projectile.knockBack,Projectile.owner);
                    SoundEngine.PlaySound(SoundID.Item42, Projectile.position);
                }
            }
        }
    }
}
