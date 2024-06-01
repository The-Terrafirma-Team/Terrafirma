using Terraria.ModLoader;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terrafirma.Systems.MageClass;
using System.ComponentModel;
using Terrafirma.Common.Structs;
using System.Linq;
using System.Collections.Generic;
using Terrafirma.Systems.NewNPCQuests;

namespace Terrafirma.Systems.NPCQuests
{
    internal class QuestModNPC : ModPlayer
    {

    //    bool uiopenswitch = false;

    //    public override void PostUpdate()
    //    {
    //        if (ModContent.GetInstance<NPCQuestButtonSystem>() == null) return;

    //        int[] QuestNPCs = new int[] { };
    //        for (int i = 0; i < QuestID.quests.Length; i++)
    //        {
    //            for (int k = 0; k < QuestID.quests[i].NPCs.Length; k++)
    //            {
    //                if (!QuestNPCs.Contains(QuestID.quests[i].NPCs[k])) QuestNPCs = QuestNPCs.Append(QuestID.quests[i].NPCs[k]).ToArray();
    //            }
    //        }

    //        if (Player.TalkNPC != null && QuestNPCs.Contains(Player.TalkNPC.type))
    //        {
    //            if (!uiopenswitch)
    //            {
    //                ModContent.GetInstance<NPCQuestButtonSystem>().CreateButton(Player.TalkNPC);
    //                uiopenswitch = true;

    //                for (int i = 0; i < Main.npc.Length; i++)
    //                {
    //                    if (Main.npc[i] == Player.TalkNPC)
    //                    {
    //                        ModContent.GetInstance<NPCQuestButtonSystem>().UIOpenForNPC = i;
    //                        break;
    //                    }

    //                }
    //            }
    //        }
    //        else if (uiopenswitch)
    //        {
    //            ModContent.GetInstance<NPCQuestButtonSystem>().FlushButton();
    //            if (ModContent.GetInstance<NPCQuestButtonSystem>().Open() != "Selector")
    //            {
    //                ModContent.GetInstance<NPCQuestButtonSystem>().HideUI();
    //            }
    //            uiopenswitch = false;
    //        }

    //        if (Main.npcShop == 1)
    //        {
    //            ModContent.GetInstance<NPCQuestButtonSystem>().FlushButton();
    //            ModContent.GetInstance<NPCQuestButtonSystem>().FlushSelectorUI();
    //            ModContent.GetInstance<NPCQuestButtonSystem>().HideUI();
    //            uiopenswitch = false;
    //        }

    //        if (Main.playerInventory)
    //        {
    //            ModContent.GetInstance<NPCQuestButtonSystem>().FlushButton();
    //            ModContent.GetInstance<NPCQuestButtonSystem>().FlushSelectorUI();
    //            ModContent.GetInstance<NPCQuestButtonSystem>().HideUI();
    //            uiopenswitch = false;
    //        }

    //        if (ModContent.GetInstance<NPCQuestButtonSystem>().UIOpenForNPC != -1 &&
    //            ModContent.GetInstance<NPCQuestButtonSystem>().Open() == "Selector" &&
    //            Player.Center.Distance(Main.npc[ModContent.GetInstance<NPCQuestButtonSystem>().UIOpenForNPC].Center) > 300f)
    //        {
    //            ModContent.GetInstance<NPCQuestButtonSystem>().FlushButton();
    //            ModContent.GetInstance<NPCQuestButtonSystem>().FlushSelectorUI();
    //            uiopenswitch = false;
    //        }

    //        base.PostUpdate();

    //    }

    }

}
