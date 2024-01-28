using System;
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
        public Dictionary<Quest, int> Quests = new Dictionary<Quest, int>();

        /// <summary>
        /// Creates a list of quests attached to a player, it is meant to be used to track a player's quest progression
        /// </summary>
        /// <param name="player"> The Player this quest list is attached to</param>
        /// <param name="quests"> List of Quests, it is recommended to keep this empty and use CreateQuestList()</param>
        public QuestList()
        {
            Quests = new Dictionary<Quest, int>();

        }

        public static void AddQuest( QuestList questlist, Quest quest, int completion = 0)
        {
            questlist.Quests.Add(quest, completion);
        }

        public static void SetQuest(QuestList questlist, Quest quest, int completion = 1)
        {
            if ( questlist.Quests.ContainsKey(quest) )
            {
                questlist.Quests[quest] = completion;
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
                questlist.Quests.Add(QuestIndex.quests[i], 0);
            }

            return questlist;
        }

        

    }
}
