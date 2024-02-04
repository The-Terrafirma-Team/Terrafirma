using Terraria.ID;
using Terraria;
using Terrafirma.Global.Templates;
using Microsoft.Xna.Framework;

namespace Terrafirma.Projectiles.Melee
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
            Dust d = Dust.NewDustPerfect(Projectile.Center + new Vector2(-7,5).RotatedBy(Projectile.rotation), DustID.Ichor, Main.rand.NextVector2Circular(1,1) + Projectile.velocity * 3);
            d.noGravity = true;
            d.fadeIn = Main.rand.NextFloat(0, 1.5f);
        }
        public override void OnTurnAround(Player player)
        {
            if (Main.LocalPlayer == player)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.oldPosition + Projectile.Size/2, Projectile.velocity * 2.5f, ProjectileID.IchorSplash, Projectile.damage, Projectile.knockBack, Projectile.owner);
            }
        }
    }
}
