using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;

namespace Terrafirma.Common.Templates.Melee
{
    public abstract class UpDownSwing : HeldProjectile
    {
        public virtual int Length => 106;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 6;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.penetrate = 3;
            Projectile.stopsDealingDamageAfterPenetrateHits = true;
        }
        public override void OnSpawn(IEntitySource source)
        {
            player = Main.player[Projectile.owner];
            Projectile.timeLeft = player.HeldItem.useTime;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            return targetHitbox.IntersectsConeSlowMoreAccurate(player.MountedCenter, Length * Projectile.scale, Projectile.rotation - MathHelper.PiOver4, 0.2f);
        }
        public override void AI()
        {
            Projectile.ai[1] = player.HeldItem.useTime;

            Projectile.scale = player.HeldItem.scale;

            float extend = MathF.Sin(Projectile.timeLeft / Projectile.ai[1] * MathHelper.Pi);
            if (Projectile.ai[0] == 0)
                Projectile.rotation = MathHelper.SmoothStep(2.3f, -2f, Projectile.timeLeft / Projectile.ai[1]);
            else
                Projectile.rotation = MathHelper.SmoothStep(-2f, 2.3f, Projectile.timeLeft / Projectile.ai[1]);

            player.heldProj = Projectile.whoAmI;

            PlayerAnimation.ArmPointToDirection(Projectile.rotation - MathHelper.PiOver4, player);
            if (Projectile.ai[0] == 0)
            {
                if (Projectile.timeLeft / Projectile.ai[1] > 0.6f)
                    player.bodyFrame.Y = 56;
                if (Projectile.timeLeft / Projectile.ai[1] < 0.2f)
                    player.bodyFrame.Y = 0;
            }
            else
            {
                if (Projectile.timeLeft / Projectile.ai[1] > 0.6f)
                    player.bodyFrame.Y = 56 * PlayerAnimation.PointDownRight;
                if (Projectile.timeLeft / Projectile.ai[1] < 0.28f)
                    player.bodyFrame.Y = 56;
            }

            Projectile.Center = player.MountedCenter.ToPoint().ToVector2() + new Vector2(0, player.gfxOffY) + player.getFrontArmPosition();

            if (player.direction == -1)
                Projectile.rotation = MathHelper.Pi - Projectile.rotation + MathHelper.PiOver2;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            float extend = MathF.Sin((Projectile.timeLeft / Projectile.ai[1]) * MathHelper.Pi);

            for(int i = 0; i < 6; i++)
            {
                if(Projectile.oldRot[i] != 0)
                commonDiagonalItemDrawManualRotation(new Color(1f,1f,1f,0f) * (0.3f - (i*0.05f)), TextureAssets.Projectile[Type], Projectile.scale + extend * 0.2f, Projectile.oldRot[i]);
            }

            commonDiagonalItemDraw(lightColor, TextureAssets.Projectile[Type], Projectile.scale + extend * 0.2f);
            return false;
        }
    }
}
