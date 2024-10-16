using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Common
{
    public class TerrafirmaGlobalBuff : GlobalBuff
    {
        public override void Update(int type, NPC npc, ref int buffIndex)
        {
            switch (type)
            {
                case BuffID.Silenced:
                    npc.NPCStats().Silenced = true;
                    break;
            }
        }
    }
}
