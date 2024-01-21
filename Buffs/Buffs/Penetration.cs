using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using TerrafirmaRedux.Global;

namespace TerrafirmaRedux.Buffs.Buffs
{
    internal class Penetration: ModBuff
    { 
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<PlayerStats>().ExtraWeaponPierceMultiplier += 1;
        }
    }
}

