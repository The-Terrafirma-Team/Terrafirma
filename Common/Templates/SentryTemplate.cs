using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace Terrafirma.Common.Templates
{
    public abstract class SentryTemplate : ModProjectile
    {
        /// <summary>
        /// Put code to instantly fire the sentry here
        /// </summary>
        public virtual void OnHitByWrench(Player player, WrenchItem wrench)
        {

        }
    }
}
