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
    internal class Healer : ModBuff
    { 
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<PlayerStats>().OutgoingHealingMultiplier += 0.2f;
            player.GetDamage(DamageClass.Generic) -= 0.1f;
        }
    }
}

