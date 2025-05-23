﻿using Microsoft.Xna.Framework;
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
    public class ShadowflameWrenchBuff : SentryBuff
    {
        public override void UpdateSentry(Projectile sentry, SentryStats stats)
        {
            if (Main.rand.NextBool(5))
            {
                Dust d = Dust.NewDustDirect(sentry.BottomLeft + new Vector2(0, -4), sentry.width, 0, DustID.Shadowflame, 0, -sentry.height / 10);
                d.noGravity = true;
                d.velocity.X *= 0.1f;
                d.scale *= 1.5f;
                d.alpha = 128;
                if (d.velocity.Y > 0)
                    d.velocity *= -1;
            }
        }
        public override void modifyBullet(SentryBulletBuff globalProjectile, Projectile projectile)
        {
            globalProjectile.Shadowflame = true;
        }
    }
}
