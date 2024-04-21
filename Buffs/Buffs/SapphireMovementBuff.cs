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
    internal class SapphireMovementBuff: ModBuff
    { 
        public override void Update(Player player, ref int buffIndex)
        {
            player.moveSpeed += 0.33f;
            player.jumpSpeedBoost += 2;
        }
    }
}

