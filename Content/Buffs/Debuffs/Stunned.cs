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
using Terrafirma.Common;
using Terrafirma.Utilities;

namespace Terrafirma.Content.Buffs.Debuffs
{
    public class Stunned : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            DataSets.RegisterStunDebuff(Type);
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            NPCStats stats = npc.NPCStats();
            stats.NoFlight = true;
            stats.Immobile = true;
            stats.StunStarEffects = true;
        }
    }
}
