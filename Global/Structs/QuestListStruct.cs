using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TerrafirmaRedux.Systems.NPCQuests;
using Terraria;
using Terraria.ID;

namespace TerrafirmaRedux.Global.Structs
{
    /// <summary>
    /// Creates a list of quests attached to a player, it is meant to be used to track a player's quest progression
    /// </summary>
    public struct QuestList
    {
        public Player QuestPlayer;
        public Quest[] Quests = new Quest[]{};

        /// <summary>
        /// Creates a list of quests attached to a player, it is meant to be used to track a player's quest progression
        /// </summary>
        /// <param name="player"> The Player this quest list is attached to</param>
        /// <param name="quests"> List of Quests, it is recommended to keep this empty and use CreateQuestList()</param>
        public QuestList()
        {
            Quests = new Quest[] {};
        }

        public static void AddQuest( QuestList questlist, Quest quest)
        {
            questlist.Quests = questlist.Quests.Append(quest).ToArray();
        }

        /// <summary>
        /// Sets the completion value of a quest
        /// </summary>
        /// <param name="completion"> Completion value, 0 for uncompleted, 1 for quest in progress, 2 for completed</param>
        public static void SetQuest(QuestList questlist, Quest quest, int completion)
        {
            if ( questlist.Quests.Contains(quest))
            {
                int pos = -1;
                for (int i = 0; i < questlist.Quests.Length; i++)
                {
                    if (questlist.Quests[i].IsEqualsTo(quest))
                    {
                        pos = i; 
                        break;
                    }
                }
                if (pos > -1) questlist.Quests[pos].Completion = completion;
                else Main.NewText("Couldn't Set Quest, Quest doesn't exist in questlist", Main.errorColor);
            }
        }

        /// <summary>
        /// Adds all the quests from QuestIndex.cs into the questlist
        /// </summary>
        /// <param name="questlist"></param>
        public static QuestList ImportQuestList()
        {
            QuestList questlist = new QuestList();

            for (int i = 0; i < QuestIndex.quests.Length; i++)
            {
                questlist.Quests = questlist.Quests.Append(QuestIndex.quests[i]).ToArray();
            }

            return questlist;
        }

        /// <summary>
        /// Gets the completion value of a quest
        /// </summary>
        public static int GetQuestCompletion(QuestList questlist, Quest quest) 
        {
            int pos = 0;
            for (int i = 0; i < questlist.Quests.Length; i++)
            {
                if (questlist.Quests[i].IsEqualsTo(quest))
                {
                    pos = i;
                    break;
                }
            }
            return questlist.Quests[pos].Completion;
        }

        /// <summary>
        /// Gets the playerquests quest list from the local player
        /// </summary>
        /// <returns></returns>
        public static QuestList GetLocalQuestList()
        {
            return Main.LocalPlayer.GetModPlayer<TerrafirmaGlobalPlayer>().playerquests;
        }

    }
}
