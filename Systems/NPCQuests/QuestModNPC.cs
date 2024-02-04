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

            if (Main.playerInventory) 
            { 
                ModContent.GetInstance<NPCQuestButtonSystem>().FlushButton(); 
                ModContent.GetInstance<NPCQuestButtonSystem>().FlushSelectorUI(); 
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

        public Progress[] ProgressArray = new Progress[] {};

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {   
            base.OnHitNPC(target, hit, damageDone);

            //Experimental Marine Biology
            if ( target.life <= 0 && target.type == NPCID.BlueSlime && Player.HeldItem.type == ItemID.Minishark)
            {
                for (int i = 0; i < ProgressArray.Length; i++)
                {
                    if (ProgressArray[i].Name == "EMBProgress") ProgressArray[i].CurrentValue += 1f;
                }
            }

        }

        public bool CanQuestBeCompleted(Quest quest, QuestList questlist, Player player)
        {
            //Business Deal
            if (quest.IsEqualsTo(QuestIndex.BusinessDeal))
            {
                if (player.HasItemInAnyInventory(ItemID.Minishark) && player.HasItemInAnyInventory(ItemID.FlintlockPistol)) return true;
            }
            //Experimental Marine Biology
            if (quest.IsEqualsTo(QuestIndex.ExperimentalMarineBiology))
            {
                for (int i = 0; i < ProgressArray.Length; i++)
                {
                    if (ProgressArray[i].Name == "EMBProgress" && ProgressArray[i].IsCompleted()) return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets executed whenever a quest is started
        /// </summary>
        /// <param name="quest"></param>
        public void StartQuest(Quest quest)
        {
            //Experimental Marine Biology
            if (quest.IsEqualsTo(QuestIndex.ExperimentalMarineBiology))
            {
                ProgressArray = ProgressArray.Append(new Progress("EMBProgress", 10, 0, 0)).ToArray();
            }
        }

        /// <summary>
        /// Gets executed whenever a quest is completed or ended
        /// </summary>
        /// <param name="quest"></param>
        public void EndQuest(Quest quest)
        {
            //Experimental Marine Biology
            if (quest.IsEqualsTo(QuestIndex.ExperimentalMarineBiology))
            {
                ProgressArray = new Progress[] { };
            }
        }

    }

}
