using System;
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
    internal class SlapBuff: ModBuff
    { 
        public override void Update(Player player, ref int buffIndex)
        {
            Dust.NewDust(player.position, player.width, player.height, DustID.Honey);
            player.moveSpeed += 2;
        }
    }
}

