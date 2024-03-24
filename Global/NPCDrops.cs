using System;
using System.Linq;
using Terrafirma.Items.Equipment;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Global
{
    public class NPCDrops : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            int[] DropsLeather = new int[] {NPCID.FireImp,NPCID.Demon,NPCID.FaceMonster,NPCID.DesertGhoul,NPCID.DesertGhoulCorruption,NPCID.DesertGhoulCrimson,NPCID.DesertGhoulHallow};
            if (DropsLeather.Contains(npc.type))
            {
                npcLoot.Add(ItemDropRule.Common(ItemID.Leather, 5));
            }
            int[] DropsFireShield = new int[] { NPCID.FireImp,NPCID.LavaSlime, NPCID.MeteorHead,NPCID.SkeletonArcher};
            if (DropsFireShield.Contains(npc.type))
            {
                npcLoot.Add(ItemDropRule.StatusImmunityItem(ModContent.ItemType<FireShield>(),100));
            }
        }
    }
}
