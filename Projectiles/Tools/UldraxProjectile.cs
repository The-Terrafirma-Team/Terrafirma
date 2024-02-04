using Terraria.ModLoader;
using Terraria.ID;
using Terraria;

namespace Terrafirma.Projectiles.Tools
{
    public class UldraxProjectile: ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.MythrilDrill);

            DrawOriginOffsetY = 0;

            Projectile.width = 30;
            Projectile.height = 30;
        }

        public override void AI()
        {
            Projectile.gfxOffY = Main.player[Projectile.owner].gfxOffY;
            DrawOriginOffsetX = -1 * Projectile.direction;
        }
    }
}
