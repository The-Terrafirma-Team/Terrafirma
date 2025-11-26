using Terraria;

namespace Terrafirma.Utilities
{
    public static class ProjectileUtils
    {
        public static void QuickDefaults(this Projectile proj, bool hostile = false, int size = 8, int aiStyle = -1)
        {
            proj.aiStyle = aiStyle;
            proj.hostile = hostile;
            proj.friendly = !hostile;
            proj.width = size;
            proj.height = size;
        }
    }
}
