﻿using System;
using System.Linq;
using Terrafirma.Items.Equipment;
using Terrafirma.Items.Equipment.Summoner;
using Terrafirma.Items.Materials;
using Terrafirma.Items.Weapons.Ranged.Bows;
using Terrafirma.Items.Weapons.Summoner.Sentry.PreHardmode;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Common
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
            if (NPCID.Sets.Zombies[npc.type])
            {
                npcLoot.Add(ItemDropRule.Common(ItemID.Leather, 12));
            }
            int[] DropsFireShield = new int[] { NPCID.FireImp,NPCID.LavaSlime, NPCID.MeteorHead,NPCID.SkeletonArcher};
            if (DropsFireShield.Contains(npc.type))
            {
                npcLoot.Add(ItemDropRule.StatusImmunityItem(ModContent.ItemType<FireShield>(),100));
            }
            if (npc.type == NPCID.PirateCrossbower)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PirateCrossbow>(), 25, 1, 1));
            }
            if (npc.type == NPCID.GraniteGolem || npc.type == NPCID.GraniteFlyer)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<EnchantedStone>(), 5, 1, 1));
            }
            if (npc.type == NPCID.KingSlime)
            {
                npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<PortableSlimeBakery>(), 5, 1, 1));
            }
            if (npc.type == NPCID.BloodZombie || npc.type == NPCID.Drippler)
            {
                npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<Overclock>(), 149, 75));

            }
        }

    }
    public class BossBags : GlobalItem
    {
        public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
        {
            if(item.type == ItemID.KingSlimeBossBag)
                itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<PortableSlimeBakery>(), 5, 1, 1));
        }
    }
}
