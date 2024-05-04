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
using Terraria.ModLoader;

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
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
        }
        public override void OnSpawn(IEntitySource source)
        {
            player = Main.player[Projectile.owner];
            Projectile.timeLeft = player.itemAnimationMax;
            Projectile.ai[1] = player.itemAnimationMax;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            return targetHitbox.IntersectsConeSlowMoreAccurate(player.MountedCenter, Length * Projectile.scale, Projectile.rotation - MathHelper.PiOver4, 0.2f);
        }
        public override void AI()
        {
            Projectile.scale = player.GetAdjustedItemScale(player.HeldItem);

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
                commonDiagonalItemDrawManualRotation(new Color(lightColor.R, lightColor.G, lightColor.B, 0) * (0.3f - (i*0.05f)), TextureAssets.Projectile[Type], Projectile.scale + extend * 0.2f, Projectile.oldRot[i]);
            }

            commonDiagonalItemDraw(lightColor, TextureAssets.Projectile[Type], Projectile.scale + extend * 0.2f);
            return false;
        }
    }
}
