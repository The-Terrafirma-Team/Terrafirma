using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Common.Projectiles;
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
                Dust d = Dust.NewDustDirect(sentry.position, sentry.width, sentry.height / 4, Main.rand.NextBool()? DustID.Snow : DustID.Ice, 0);
                d.noGravity = true;
                d.velocity = Main.rand.NextVector2Circular(2, 1);
                d.velocity.Y += 2;
                d.alpha = 128;
                d.noLightEmittence = true;
            }
        }
    }
}
