using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Content.Buffs.Debuffs;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Common
{
    public class ApplyImmunitesToStuns : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < NPCLoader.NPCCount; i++)
            {
                if (!DataSets.NPCWhitelistedForStun[i])
                {
                    NPCID.Sets.SpecificDebuffImmunity[i][ModContent.BuffType<Stunned>()] = true;
                }
            }
        }
    }
}
