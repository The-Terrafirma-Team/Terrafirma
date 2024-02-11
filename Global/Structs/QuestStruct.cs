using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Systems.NPCQuests;
using Terraria;
using Terraria.ID;

namespace Terrafirma.Global.Structs
{

    public enum QuestType
    {
        Explorer = 0,
        Scavenger = 1,
        Collector = 2,
        Slayer = 3,
        Destroyer = 4,
        Final = 5,
        Special = 6
    }

    /// <summary>
    /// A structure type to catalogue NPC quests
    /// </summary>
    public struct Quest
    {
        public string Name;
        public string Dialogue;
        public string TaskDescription;
        public int Difficulty;
        public QuestType Type;
        public int[] InvolvedNPCs;

        public Item[] Rewards;

        public Condition[] conditions;
        public int Completion;
        public Progress CompletionProgress;

        /// <summary>
        /// Create a quest, put this in the QuestIndex.cs file to automatically add this quest when the mod is loaded
        /// </summary>
        /// <param name="Name"> Name of the Quest </param>
        /// <param name="dialogue"> Quest Dialogue</param>
        /// <param name="taskdescription"> Description of what the player needs to do in the quest</param>
        /// <param name="difficulty"> Quest Difficulty, value is clamped from 1 to 5</param>
        /// <param name="involvednpcs"> Array of all NPCs that can show this quest in their quest menu </param>
        /// <param name="rewards"> List of all the quest's rewards </param>
        /// <param name="conditions"> Array of conditions that have to be met for this quest to show in the quest menu</param>
        public Quest(string Name, string dialogue, string taskdescription, int difficulty, int[] involvednpcs, Item[] rewards, QuestType type = QuestType.Explorer, Condition[] conditions = null, int completion = 0)
        {
            this.Name = Name;
            this.Dialogue = dialogue;
            this.Difficulty = Math.Clamp(difficulty, 0, 5); 
            this.TaskDescription = taskdescription;
            this.InvolvedNPCs = involvednpcs;
            this.Rewards = rewards;
            this.conditions = conditions;
            this.Completion = completion;
            this.Type = type;
        }

        public Quest(string Name, string dialogue, string taskdescription, int difficulty, int[] involvednpcs, Item[] rewards, Progress completionprogress, Condition[] conditions = null, int completion = 0)
        {
            this.Name = Name;
            this.Dialogue = dialogue;
            this.Difficulty = Math.Clamp(difficulty, 0, 5);
            this.TaskDescription = taskdescription;
            this.InvolvedNPCs = involvednpcs;
            this.Rewards = rewards;
            this.conditions = conditions;
            this.Completion = completion;
            this.CompletionProgress = completionprogress;
        }

        public Quest(string Name)
        {
            this.Name = Name;
            this.Dialogue = "";
            this.Difficulty = 0;
            this.TaskDescription = "";
            this.InvolvedNPCs = new int[] {NPCID.Guide};
            this.Rewards = new Item[] { new Item(ItemID.CopperShortsword) };
            this.Completion = 0;
        }

    }

    public static class QuestMethods
    {
        /// <summary>
        /// Compares two quests and checks if they match (Completion excluded)
        /// </summary>
        public static bool IsEqualsTo(this Quest quest, Quest comparison)
        {
            if (
                quest.Name == comparison.Name &&
                quest.Dialogue == comparison.Dialogue &&
                quest.TaskDescription == comparison.TaskDescription &&
                quest.Difficulty == comparison.Difficulty &&
                quest.InvolvedNPCs == comparison.InvolvedNPCs &&
                quest.Rewards == comparison.Rewards
                ) return true;
            else return false;

        }

        /// <summary>
        /// Sets the Quest as "Completed"
        /// </summary>
        public static void Complete(this Quest quest, bool GrantQuestRewards = true)
        {
            quest.Completion = 2;

            if (GrantQuestRewards)
            {
                for (int i = 0; i < quest.Rewards.Length; i++)
                {
                    Main.LocalPlayer.QuickSpawnItem(Main.LocalPlayer.GetSource_FromThis(), quest.Rewards[i]);      
                }
            }
            

        }

        /// <summary>
        /// Sets the Quest to "In Progress"
        /// </summary>
        /// <param name="DisableOtherQuests"> Automatically true, will turn all other quests's status in the quest list from "In Progress" to "Not Started"</param>
        public static void SetInProgress(this Quest quest, Quest[] questlist = null, bool DisableOtherQuests = true)
        {
            quest.Completion = 1;
            if (DisableOtherQuests && questlist != null)
            {
                for (int i = 0; i < questlist.Length; i++)
                {
                    if (questlist[i].Completion == 1 && !questlist[i].IsEqualsTo(quest)) questlist[i].Completion = 0;
                }
            }
            
        }

        /// <summary>
        /// Sets the Quest as "Not Started"
        /// </summary>
        /// <param name="quest"></param>
        public static void Uncomplete(this Quest quest)
        {
            quest.Completion = 0;
        }

        public static Quest[] UncompletedQuests(this Quest[] questlist)
        {
            Quest[] uncompletedquests = new Quest[] { };
            for (int i = 0; i < questlist.Length; i++)
            {
                if (questlist[i].Completion == 0) uncompletedquests = uncompletedquests.Append(questlist[i]).ToArray();
            }
            return uncompletedquests;
        }

        public static Quest[] OnGoingQuests(this Quest[] questlist)
        {
            Quest[] ongoingquests = new Quest[] { };
            for (int i = 0; i < questlist.Length; i++)
            {
                if (questlist[i].Completion == 1) ongoingquests = ongoingquests.Append(questlist[i]).ToArray();
            }
            return ongoingquests;
        }

        public static Quest[] FinishedQuests(this Quest[] questlist)
        {
            Quest[] finishedquests = new Quest[] { };
            for (int i = 0; i < questlist.Length; i++)
            {
                if (questlist[i].Completion == 2) finishedquests = finishedquests.Append(questlist[i]).ToArray();
            }
            return finishedquests;
        }

        /// <summary>
        /// Gets the index of the a specific quest inside of a quest array
        /// </summary>
        public static int GetQuestIndex(this Quest[] array, Quest quest)
        {
            for (int i = 0; i < array.Length; i ++)
            {
                if (array[i].IsEqualsTo(quest)) return i;
            }
            return -1;
        }

        /// <summary>
        /// returns if any quest in this list has their status set to "in progress"
        /// </summary>
        public static bool AnyQuestInProgress(this Quest[] questlist)
        {
            for (int i = 0; i < questlist.Length; i++)
            {
                if (questlist[i].Completion == 1) return true;
            }
            return false;
        }
    }
}
