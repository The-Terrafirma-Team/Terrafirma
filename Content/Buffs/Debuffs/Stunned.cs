using Terrafirma.Common.Mechanics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Terrafirma.Content.Buffs.Debuffs
{
    public class Stunned : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.NPCStats().NoFlight = true;
            npc.NPCStats().Immobile = true;
        }
    }
}
