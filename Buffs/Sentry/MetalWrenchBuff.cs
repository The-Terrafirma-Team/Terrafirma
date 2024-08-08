using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Common;
using Terrafirma.Common.Templates;
using Terraria;

namespace Terrafirma.Buffs.Sentry
{
    public class MetalWrenchBuff : SentryBuff
    {
        public override void UpdateSentry(Projectile sentry, SentryStats stats)
        {
            stats.SpeedMultiplier *= 0.8f;
            if (Main.rand.NextBool(5))
            {
                Dust d = Dust.NewDustDirect(sentry.BottomLeft + new Vector2(0, -4), sentry.width, 0, Terraria.ID.DustID.GemDiamond, 0, -sentry.height / 20);
                d.noGravity = true;
                d.velocity.X *= 0.1f;
                d.noLightEmittence = true;
                if (d.velocity.Y > 0)
                    d.velocity *= -1;
            }
        }
    }
}
