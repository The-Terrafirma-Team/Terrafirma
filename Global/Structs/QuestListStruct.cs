using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Systems.NPCQuests;
using Terraria;
using Terraria.ID;

namespace Terrafirma.Global.Structs
{
    /// <summary>
    /// Creates a list of quests attached to a player, it is meant to be used to track a player's quest progression
    /// </summary>
    public class QuestList
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

    }

    internal static class QuestListMethods
    {
        public static void AddQuest(this QuestList questlist, Quest quest)
        {
            questlist.Quests = questlist.Quests.Append(quest).ToArray();
        }

        /// <summary>
        /// Sets the completion value of a quest
        /// </summary>
        /// <param name="completion"> Completion value, 0 for uncompleted, 1 for quest in progress, 2 for completed</param>
        public static void SetQuest(this QuestList questlist, Quest quest, int completion)
        {
            if (questlist.Quests.Contains(quest))
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
        /// Gets the completion value of a quest
        /// </summary>
        public static int GetQuestCompletion(this QuestList questlist, Quest quest)
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
        /// Gets a copy of a specific quest out of a QuestList, returns an empty Quest if nothing can be found
        /// </summary>
        public static int GetQuestIndex(this QuestList questlist, Quest quest)
        {
            for (int i = 0; i < questlist.Quests.Length; i++)
            {
                if (questlist.Quests[i].IsEqualsTo(quest))
                {
                    return i;
                }
            }
            return -1;
        }

        public static QuestList UncompletedQuests(this QuestList questlist)
        {
            QuestList uncompletedquests = new QuestList();
            for (int i = 0; i < questlist.Quests.Length; i++)
            {
                if (questlist.Quests[i].Completion == 0) uncompletedquests.Quests = uncompletedquests.Quests.Append(questlist.Quests[i]).ToArray();
            }
            return uncompletedquests;
        }

        public static QuestList OnGoingQuests(this QuestList questlist)
        {
            QuestList ongoingquests = new QuestList();
            for (int i = 0; i < questlist.Quests.Length; i++)
            {
                if (questlist.Quests[i].Completion == 1) ongoingquests.Quests = ongoingquests.Quests.Append(questlist.Quests[i]).ToArray();
            }
            return ongoingquests;
        }

        public static QuestList FinishedQuests(this QuestList questlist)
        {
            QuestList finishedquests = new QuestList();
            for (int i = 0; i < questlist.Quests.Length; i++)
            {
                if (questlist.Quests[i].Completion == 2) finishedquests.Quests = finishedquests.Quests.Append(questlist.Quests[i]).ToArray();
            }
            return finishedquests;
        }

    }
}
