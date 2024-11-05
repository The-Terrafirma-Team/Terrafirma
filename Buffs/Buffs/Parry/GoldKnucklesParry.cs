using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace Terrafirma.Buffs.Buffs.Parry
{
    public class GoldKnucklesParry : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetAttackSpeed(DamageClass.Melee) += player.buffTime[buffIndex] / (60f * 8);
        }
    }
}
