using Terraria.ModLoader;
using Terrafirma.Systems.NewNPCQuests;
using System.Linq;
using Terraria;
using System.Collections.Generic;
using Terrafirma.Common.Players;

namespace Terrafirma.Systems.NPCQuests.Quests
{
    public class NewQuestModNPC : ModPlayer
    {
        public Quest[] playerquests = new Quest[] { };
        int[] QuestNPCs = new int[] { };
        bool uiopenswitch = false;
        NPC lasttalkedtoNPC = null;
        public override void PostUpdate()
        {
            //Get all the Quest NPCs
            QuestNPCs = new int[] { };
            for (int i = 0; i < QuestID.quests.Length; i++)
            {
                for (int k = 0; k < QuestID.quests[i].NPCs.Length; k++)
                { if (!QuestNPCs.Contains(QuestID.quests[i].NPCs[k])) QuestNPCs = QuestNPCs.Append(QuestID.quests[i].NPCs[k]).ToArray(); }
            }
            //Set Last talked to NPC when talking to an NPC
            if (Player.TalkNPC != lasttalkedtoNPC && Player.TalkNPC != null) lasttalkedtoNPC = Player.TalkNPC;

            //Create Button when opening NPC Chat UI
            if (Player.TalkNPC != null && QuestNPCs.Contains(Player.TalkNPC.type) && !uiopenswitch)
            {
                ModContent.GetInstance<NPCQuestButtonSystem>().CreateButton();
                uiopenswitch = true;
            }

            //Flush Button if Player stops talking with npc, opens a shop or their inventory
            if ((Player.TalkNPC == null || Main.npcShop == 1 || Main.playerInventory == true) && 
                uiopenswitch && 
                (ModContent.GetInstance<NPCQuestButtonSystem>().Open() == "" || ModContent.GetInstance<NPCQuestButtonSystem>().Open() == "Button"))
            {
                ModContent.GetInstance<NPCQuestButtonSystem>().FlushButton();
                uiopenswitch = false;
            }

            //Flush Selector if Player starts talking with an NPC, opens a shop or their inventory
            if ((Player.TalkNPC != null || Main.npcShop == 1 || Main.playerInventory == true) &&
                uiopenswitch &&
                ModContent.GetInstance<NPCQuestButtonSystem>().Open() == "Selector")
            {
                ModContent.GetInstance<NPCQuestButtonSystem>().FlushSelectorUI();
                uiopenswitch = false;
            }

            //Close all Quest UIs if the Player gets too far from last talked to NPC
            if (lasttalkedtoNPC != null && Player.Distance(lasttalkedtoNPC.Center) > 300f)
            {
                ModContent.GetInstance<NPCQuestButtonSystem>().FlushButton();
                ModContent.GetInstance<NPCQuestButtonSystem>().FlushSelectorUI();
                uiopenswitch = false;
            }

            base.PostUpdate();
        }

        //Give player the Quest list when they are made
        public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
        {
            playerquests = QuestID.quests;
            return base.AddStartingItems(mediumCoreDeath);
        }

        //Check Player and add new changes to player quest list if there are any
        public override void OnEnterWorld()
        {
            playerquests = playerquests.SetupPlayerQuestList();
            base.OnEnterWorld();
        }
    }
}