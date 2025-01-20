using Terraria.ModLoader;
using Terrafirma.Common.Templates.Melee;
using Microsoft.Xna.Framework;

namespace Terrafirma.Projectiles.Melee.Paladin
{
    public class IronThrowingHammer : PaladinHammer
    {
        public override int ThrownProjectile => ModContent.ProjectileType<IronThrowingHammerThrown>();
    }
    public class IronThrowingHammerThrown : PaladinHammerThrown
    {
        public override int FlightDuration => 40;
        public override string Texture => "Terrafirma/Projectiles/Melee/Paladin/IronThrowingHammer";
    }
}
