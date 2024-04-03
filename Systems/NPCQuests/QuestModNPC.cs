using Terraria.ModLoader;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terrafirma.Systems.MageClass;
using System.ComponentModel;
using Terrafirma.Global.Structs;
using System.Linq;
using System.Collections.Generic;

namespace Terrafirma.Systems.NPCQuests
{
    internal class QuestModNPC : ModPlayer
    {
        //Quest NPCs
        NPC armsdealer = new NPC();
        NPC guide = new NPC();
        NPC steampunker = new NPC();


        bool JustOpenedUI = false;

        public override void PostUpdate()
        {
            if (ModContent.GetInstance<NPCQuestButtonSystem>() == null) return;
            
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].type == NPCID.ArmsDealer) armsdealer = Main.npc[i];
                if (Main.npc[i].type == NPCID.Steampunker) steampunker = Main.npc[i];
                if (Main.npc[i].type == NPCID.Guide) guide = Main.npc[i];
            }


            if (Player.TalkNPC == armsdealer || Player.TalkNPC == guide || Player.TalkNPC == steampunker)
            {
                if (!JustOpenedUI) 
                { 
                    ModContent.GetInstance<NPCQuestButtonSystem>().CreateButton(Player.TalkNPC); 
                    JustOpenedUI = true; 

                    for (int i = 0; i < Main.npc.Length;i++)
                    {
                        if (Main.npc[i] == Player.TalkNPC)
                        {
                            ModContent.GetInstance<NPCQuestButtonSystem>().UIOpenForNPC = i;
                            break;
                        }

                    }
                }
            }
            else if (JustOpenedUI) 
            { 
                ModContent.GetInstance<NPCQuestButtonSystem>().FlushButton(); 
                if( ModContent.GetInstance<NPCQuestButtonSystem>().Open() != "Selector" )
                {
                    ModContent.GetInstance<NPCQuestButtonSystem>().HideUI();
                }
                JustOpenedUI = false; 
            }

            if (Main.npcShop == 1)
            {
                ModContent.GetInstance<NPCQuestButtonSystem>().FlushButton();
                ModContent.GetInstance<NPCQuestButtonSystem>().FlushSelectorUI();
                ModContent.GetInstance<NPCQuestButtonSystem>().HideUI();
                JustOpenedUI = false;
            }

            if (Main.playerInventory)
            {
                ModContent.GetInstance<NPCQuestButtonSystem>().FlushButton();
                ModContent.GetInstance<NPCQuestButtonSystem>().FlushSelectorUI();
                ModContent.GetInstance<NPCQuestButtonSystem>().HideUI();
                JustOpenedUI = false;
            }

            if (ModContent.GetInstance<NPCQuestButtonSystem>().UIOpenForNPC != -1 &&
                ModContent.GetInstance<NPCQuestButtonSystem>().Open() == "Selector" &&
                Player.Center.Distance(Main.npc[ModContent.GetInstance<NPCQuestButtonSystem>().UIOpenForNPC].Center) > 300f)
            {
                ModContent.GetInstance<NPCQuestButtonSystem>().FlushButton();
                ModContent.GetInstance<NPCQuestButtonSystem>().FlushSelectorUI();
                JustOpenedUI = false;
            }

            base.PostUpdate();

        }

    }

}
