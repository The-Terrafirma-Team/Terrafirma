using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;

namespace TerrafirmaRedux.Global.Structs
{
    /// <summary>
    /// A structure type to catalogue NPC quests
    /// </summary>
    public struct Quest
    {
        public string Name;
        public string Dialogue;
        public string TaskDescription;
        public int Difficulty;
        public int[] InvolvedNPCs;
        public Item[] Rewards;
        public Condition[] conditions;

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
        public Quest(string Name, string dialogue, string taskdescription, int difficulty, int[] involvednpcs, Item[] rewards, Condition[] conditions = null)
        {
            this.Name = Name;
            this.Dialogue = dialogue;
            this.Difficulty = Math.Clamp(difficulty, 1, 5); 
            this.TaskDescription = taskdescription;
            this.InvolvedNPCs = involvednpcs;
            this.Rewards = rewards;
            this.conditions = conditions;
        }

        public Quest(string Name)
        {
            this.Name = Name;
            this.Dialogue = "";
            this.Difficulty = 0;
            this.TaskDescription = "";
            this.InvolvedNPCs = new int[] {NPCID.Guide};
            this.Rewards = new Item[] { new Item(ItemID.CopperShortsword,1) };
        }

    }
}
