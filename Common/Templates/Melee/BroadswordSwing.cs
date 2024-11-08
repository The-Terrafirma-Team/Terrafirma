using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Common.Players;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;

namespace Terrafirma.Common.Templates.Melee
{
    public abstract class BroadswordSwing : HeldProjectile
    {
        public override void SetDefaults()
        {
        }
        public override void OnSpawn(IEntitySource source)
        {
            Projectile.timeLeft = player.HeldItem.useTime;
            Projectile.ai[1] = player.PlayerStats().TimesHeldWeaponHasBeenSwung;
        }
        public override void AI()
        {
            Projectile.ai[0] = player.HeldItem.useTime;

            Projectile.scale = player.HeldItem.scale;

            if (Projectile.ai[1] == 1)
                Projectile.rotation = MathHelper.SmoothStep(1.4f, 0.3f, Projectile.timeLeft / Projectile.ai[0]);
            else if (Projectile.ai[1] == 2)
                Projectile.rotation = MathHelper.SmoothStep(0.3f, 1.4f, Projectile.timeLeft / Projectile.ai[0]);
            else
            {
                float extend = MathF.Sin((Projectile.timeLeft / Projectile.ai[0]) * MathHelper.Pi);
                Projectile.rotation = MathHelper.SmoothStep(1.9f, -2f, Projectile.timeLeft / Projectile.ai[0]);
                player.PlayerStats().TimesHeldWeaponHasBeenSwung = 0;
            }

            player.heldProj = Projectile.whoAmI;

            PlayerAnimation.ArmPointToDirection(Projectile.rotation - MathHelper.PiOver4, player);
            Projectile.Center = player.MountedCenter.ToPoint().ToVector2() + new Vector2(0, player.gfxOffY) + player.getFrontArmPosition();

            if (Projectile.ai[1] != 3)
            {
                float extend = (MathF.Sin((Projectile.timeLeft / Projectile.ai[0]) * MathHelper.Pi) * 15) - 12;
                Projectile.Center += new Vector2(extend * player.direction, -extend).RotatedBy(Projectile.rotation);
            }

            if (player.direction == -1)
                Projectile.rotation = MathHelper.Pi - Projectile.rotation + MathHelper.PiOver2;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            float extend = MathF.Sin((Projectile.timeLeft / Projectile.ai[0]) * MathHelper.Pi);
            if (Projectile.ai[1] == 3)
                commonDiagonalItemDraw(lightColor, TextureAssets.Projectile[Type], Projectile.scale + extend*0.1f);
            else
            {
                commonDiagonalItemDraw(lightColor, TextureAssets.Projectile[Type], new Vector2(0.5f, 1.1f) * Projectile.scale);
            }
            return false;
        }
    }
}
