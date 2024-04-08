using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terrafirma.Common.Players;

namespace Terrafirma.Buffs.Buffs
{
    internal class Engineering: ModBuff
    { 
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<PlayerStats>().SentryRangeMultiplier += 0.12f;
            player.GetModPlayer<PlayerStats>().SentrySpeedMultiplier -= 0.12f;
        }
    }
}

