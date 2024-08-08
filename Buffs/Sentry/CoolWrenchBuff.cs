using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Common;
using Terrafirma.Common.Templates;
using Terraria;
using Terraria.ID;

namespace Terrafirma.Buffs.Sentry
{
    public class CoolWrenchBuff : SentryBuff
    {
        public override void UpdateSentry(Projectile sentry, SentryStats stats)
        {
            stats.RangeMultiplier += 0.6f;
            if (Main.rand.NextBool(5))
            {
                Dust d = Dust.NewDustDirect(sentry.Center - sentry.Size / 4, sentry.width / 2, sentry.height / 2, DustID.Ice, 0, -sentry.height / 20);
                d.noGravity = true;
                d.velocity = Main.rand.NextVector2Circular(5, 5);
                d.noLightEmittence = true;
                if (d.velocity.Y > 0)
                    d.velocity *= -1;
            }
        }
    }
}
