using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using TerrafirmaRedux.Global.Templates;

namespace TerrafirmaRedux.Projectiles.Melee
{
    public class CarmineHookProjectile: SpearTemplate
    {
        protected override float HoldoutRangeMax => 150;
        protected override float HoldoutRangeMin => 40;

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Spear);
            DrawOriginOffsetY = 100;
        }

        public override void PostAI()
        {
            int S = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Ichor);
            Main.dust[S].noGravity = true;
            Main.dust[S].velocity = Projectile.velocity * 2;
            Main.dust[S].fadeIn = Main.rand.NextFloat(0, 1.5f);
            int H = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.IchorTorch);
            Main.dust[H].noGravity = true;
            Main.dust[H].velocity = Projectile.velocity * -3;
            Main.dust[H].fadeIn = Main.rand.NextFloat(0, 1.5f);

            Player player = Main.player[Projectile.owner];
            if (Projectile.timeLeft == (int)(player.itemAnimationMax * 1.3f) && Main.LocalPlayer == player)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Projectile.velocity * 2.5f,
                                        ProjectileID.IchorSplash, Projectile.damage, Projectile.knockBack, Projectile.owner);
            }
        }
    }
}
