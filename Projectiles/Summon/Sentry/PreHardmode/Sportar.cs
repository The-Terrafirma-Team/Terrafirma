using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.ID;

namespace Terrafirma.Projectiles.Summon.Sentry.PreHardmode
{
    public class Sportar : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 3;
        }
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.height = 36;
            Projectile.width = 44;
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
            Projectile.ai[0]++;

            if (Main.rand.NextBool(64)) Dust.NewDustDirect(Projectile.Left, Projectile.width, Projectile.height / 2, DustID.GlowingMushroom);

            Player owner = Main.player[Projectile.owner];
            NPC potentialTarget = Projectile.FindSummonTarget(600 * Projectile.GetSentryRangeMultiplier(), Projectile.Center, false);
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
            int shotTime = 90;
            if (Projectile.ai[0] > shotTime * Projectile.GetSentryAttackCooldownMultiplier() && Projectile.ai[0] <= (shotTime + 15) * Projectile.GetSentryAttackCooldownMultiplier())
            {
                if (Main.LocalPlayer == owner && Projectile.ai[1] == 0)
                {
                    NPC target = Main.npc[(int)Projectile.ai[2]];
                    SoundEngine.PlaySound(SoundID.Item42, Projectile.position);

                    Vector2 relativetarget = new Vector2(Math.Abs(target.Center.X - Projectile.Center.X), Projectile.Center.Y - target.Center.Y);
                    Vector2 ProjVel = new Vector2(
                        target.Center.X > Projectile.Center.X ? 4f : -4f,
                        (relativetarget.Y / relativetarget.X - 0.3f * relativetarget.X) / 8f
                        ) ;

                    Projectile.NewProjectileButWithChangesFromSentryBuffs(Projectile.GetSource_FromThis(), Projectile.Top + new Vector2(0, 6), ProjVel, ModContent.ProjectileType<SportarShot>(), Projectile.damage, Projectile.knockBack, Projectile.owner,target.whoAmI, ai2: target.whoAmI, RangeDoesNotAffectVelocity: true);
                }
                Projectile.frame = 1;
                Projectile.ai[1] = 1;
            }
            else if (Projectile.ai[0] > (shotTime + 15) * Projectile.GetSentryAttackCooldownMultiplier() && Projectile.ai[0] <= (shotTime + 30) * Projectile.GetSentryAttackCooldownMultiplier())
            {
                Projectile.frame = 2;
            }
            else if (Projectile.ai[0] > (shotTime + 30) * Projectile.GetSentryAttackCooldownMultiplier())
            {
                Projectile.ai[0] = 0;
                Projectile.ai[1] = 0;
            }
            else
            {
                Projectile.frame = 0;
            }
        }
    }
}
