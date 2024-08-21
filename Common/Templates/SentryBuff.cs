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
        public int ID { get => GetID(); }
        public virtual void UpdateSentry(Projectile sentry, SentryStats sentryStats)
        {

        }
        public virtual void modifyBullet(SentryBulletBuff globalProjectile, Projectile projectile)
        {

        }
        public override void Load()
        {
            SentryBuffID.sentrybuffs = SentryBuffID.sentrybuffs.Append(this).ToArray();
        }

        public int GetID()
        {
            for (int i = 0; i < SentryBuffID.sentrybuffs.Length; i++)
            {
                if (SentryBuffID.sentrybuffs[i].Name == Name) return i;
            }
            return -1;
        }
    }
}
