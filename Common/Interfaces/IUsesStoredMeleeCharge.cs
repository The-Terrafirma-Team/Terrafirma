using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace Terrafirma.Common.Interfaces
{
    public interface IUsesStoredMeleeCharge
    {
        void ApplyStoredCharge(Player player, Projectile projectile);
    }
}
