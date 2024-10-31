using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Buffs.Debuffs;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Terrafirma.Common.NPCs
{
    public class TerrafirmaGlobalNPC : GlobalNPC
    {
        public override void SetStaticDefaults()
        {
            for (int i = -65; i < ContentSamples.NpcsByNetId.Count - 65; i++)
            {
                if (ContentSamples.NpcsByNetId[i].knockBackResist == 0)
                {
                    NPCID.Sets.SpecificDebuffImmunity[i][ModContent.BuffType<Inked>()] = true;
                    NPCID.Sets.SpecificDebuffImmunity[i][ModContent.BuffType<ChilledForEnemies>()] = true;
                }
            }
        }
        public override void OnKill(NPC npc)
        {
            if (NPC.downedBoss2)
                return;
            if(npc.type is >= NPCID.EaterofWorldsHead and <= NPCID.EaterofWorldsTail && npc.boss)
            {
                TFUtils.SendImportantStatusMessage("Mods.Terrafirma.Misc.EbonstoneWeak", new Color(50, 255, 130));
            }
            else if(npc.type == NPCID.BrainofCthulhu)
            {
                TFUtils.SendImportantStatusMessage("Mods.Terrafirma.Misc.CrimstoneWeak", new Color(50, 255, 130));
            }
        }
    }
}
