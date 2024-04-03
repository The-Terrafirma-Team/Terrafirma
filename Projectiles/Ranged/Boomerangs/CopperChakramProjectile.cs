using Terraria;
using Terrafirma.Global.Templates;

namespace Terrafirma.Projectiles.Ranged.Boomerangs
{
    public class CopperChakramProjectile : ChakramTemplate
    {
        protected override int BounceAmount => 0;
        protected override int BounceMode => 0;
        protected override float ReturnSpeed => 14f;

        public override void SetDefaults()
        {
            AttackTime = 40;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 3600;
            Projectile.friendly = true;
            Projectile.damage = 16;
            Projectile.width = 20;
            Projectile.height = 20;
            DrawOffsetX = -5;
            DrawOriginOffsetY = -5;
            base.SetDefaults();
        }
    }
}
