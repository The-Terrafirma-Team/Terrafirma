﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace TerrafirmaRedux.Buffs.Buffs
{
    internal class OrdnanceBuff: ModBuff
    { 
        public override void Update(Player player, ref int buffIndex)
        {
            player.maxTurrets += 1;
        }
    }
}

