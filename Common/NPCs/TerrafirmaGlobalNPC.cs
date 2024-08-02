using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Buffs.Debuffs;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Common.NPCs
{
    public class TerrafirmaGlobalNPC : GlobalNPC
    {
        public override void SetStaticDefaults()
        {
            for (int i = 0; i < ContentSamples.NpcsByNetId.Count; i++)
            {
                if (ContentSamples.NpcsByNetId[i].knockBackResist == 0)
                {
                    NPCID.Sets.SpecificDebuffImmunity[i][ModContent.BuffType<Inked>()] = true;
                    NPCID.Sets.SpecificDebuffImmunity[i][ModContent.BuffType<ChilledForEnemies>()] = true;
                }
            }
        }
    }
}
