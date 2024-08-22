using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace Terrafirma.Projectiles.Melee.Knight
{
    public class Splinter : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(16);
            Projectile.friendly = true;
            Projectile.timeLeft = 60 * 17;
            Projectile.aiStyle = -1;
            Projectile.extraUpdates = 1;
            Projectile.penetrate = -1;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 30;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            return base.PreDraw(ref lightColor);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.ai[0] > -1)
            {
                Projectile.timeLeft = 60 * 3;
                Projectile.tileCollide = false;
                Projectile.ai[0] = -1;
                Projectile.ai[1] = target.whoAmI;
                Projectile.ai[2] = Projectile.rotation - target.rotation;
                Projectile.velocity = Projectile.Center - target.Center;
            }
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (Projectile.ai[0] == -2)
                return null;
            if (Projectile.timeLeft > 60 * 16.5f && Projectile.ai[0] > -1)
                return false;
            return null;
        }
        public override void AI()
        {
            if (Projectile.timeLeft < 20)
                Projectile.alpha += 12;

            if (Projectile.ai[0] == -1)
            {
                NPC target = Main.npc[(int)Projectile.ai[1]];
                Projectile.rotation = Projectile.ai[2] + target.rotation;
                Projectile.velocity.RotatedBy(target.rotation - Projectile.ai[2]);
                Projectile.Center = target.Center + Projectile.velocity.RotatedBy(target.rotation);

                if (!target.active)
                    Projectile.Kill();
            }
            else if (Projectile.tileCollide && Projectile.ai[0] != -1)
            {
                Projectile.velocity.Y += 0.1f;
                Projectile.rotation = Projectile.velocity.ToRotation();
            }
        }
        public override bool ShouldUpdatePosition()
        {
            return Projectile.ai[0] != -1;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity = Vector2.Zero;
            Projectile.ai[0] = -2;
            Projectile.tileCollide = false;
            return false;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = height = 4;
            fallThrough = false;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
        }
    }
}
