using Terraria.ModLoader;
using Terrafirma.Common.Templates.Melee;

namespace Terrafirma.Projectiles.Melee.Paladin
{
    public class EaterOfSkulls : PaladinHammer
    {
        public override int ThrownProjectile => ModContent.ProjectileType<EaterOfSkullsThrown>();
    }
    public class EaterOfSkullsThrown : PaladinHammerThrown
    {
        public override string Texture => "Terrafirma/Projectiles/Melee/Paladin/EaterOfSkulls";
    }
}
