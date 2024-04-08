using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terrafirma.Common;

namespace Terrafirma.Buffs.Buffs
{
    internal class KnockbackResist: ModBuff
    { 
        public override void Update(Player player, ref int buffIndex)
        {
            player.noKnockback = true;
        }
    }
}

