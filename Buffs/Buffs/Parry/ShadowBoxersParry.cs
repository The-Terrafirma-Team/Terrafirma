using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace Terrafirma.Buffs.Buffs.Parry
{
    public class ShadowBoxersParry : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            player.PlayerStats().MeleeWeaponScale += player.buffTime[buffIndex] / (60f * 8);
        }
    }
}
