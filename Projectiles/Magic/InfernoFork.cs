using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace TerrafirmaRedux.Projectiles.Magic
{
    public class InfernoFork : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.Size = new Vector2(16);
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 600;
            DrawOriginOffsetY = -12;
            DrawOffsetX = -12;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(1f, 1f, 1f, 0f) * Projectile.Opacity;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver4;
            Dust d = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(4,4), DustID.InfernoFork, Projectile.velocity * 0.1f);
            d.noGravity = true;
        }

        public override void OnKill(int timeLeft)
        {
            Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ProjectileID.InfernoFriendlyBlast, Projectile.damage = (int)(Projectile.damage * 0.4f), 0, Projectile.owner);
        }
    }
}
