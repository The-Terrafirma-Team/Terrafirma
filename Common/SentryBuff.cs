using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace Terrafirma.Common
{
    public abstract class SentryBuff : ModBuff
    {
        public virtual void Update(Projectile sentry)
        {

        }
        public virtual void ModifyBullet()
        {

        }
    }
}
