using Terraria;
using Terraria.ModLoader;

namespace Terrafirma.Projectiles.Summon.Sentry.PreHardmode
{
    internal class CrimsonHeartSentryInvisProj : ModProjectile
    {
        public override string Texture => "Terrafirma/Projectiles/Summon/Sentry/PreHardmode/CrimsonHeartSentryHeart";
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.height = 6;
            Projectile.width = 6;
            Projectile.DamageType = DamageClass.Summon;

            Projectile.tileCollide = false;
            Projectile.timeLeft = Projectile.SentryLifeTime;
            Projectile.penetrate = 0;
            Projectile.hide = true;
        }

    }
}
