using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace Terrafirma.Common.Templates
{
    public abstract class SentryBuff : ModBuff
    {
        public virtual void UpdateSentry(Projectile sentry, SentryStats sentryStats)
        {

        }
        public virtual void modifyBullet(SentryBulletBuff globalProjectile, Projectile projectile)
        {

        }
    }
}
