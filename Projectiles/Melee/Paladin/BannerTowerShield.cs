using Terraria.ModLoader;
using Terrafirma.Common.Templates.Melee;
using Terraria;
using Terraria.ID;

namespace Terrafirma.Projectiles.Melee.Paladin
{
    public class BannerTowerShield : TowerShieldProjectile
    {
        public override void PostAI()
        {
            Projectile.frame = (int)Projectile.ai[2];
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 4;
        }
    }
}
