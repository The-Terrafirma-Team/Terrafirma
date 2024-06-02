using Humanizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Common;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Terrafirma.Systems.NewNPCQuests
{
    enum QuestStatus : byte
    {
        NotStarted = 0,
        InProgress = 1,
        Completed = 2
    }

    enum QuestType : byte
    {
        Explorer = 0,
        Scavenger = 1,
        Collector = 2,
        Slayer = 3,
        Destroyer = 4,
        Final = 5,
        Special = 6
    }
    enum QuestDifficulty : byte
    {
        Effortless = 0,
        Easy = 1,
        Intermediate = 2,
        Difficult = 3,
        Intense = 4,
        Deranged = 5
    }
    public abstract class Quest : ModType
    {
        public virtual byte Difficulty => 0;
        public virtual byte Type => (byte)QuestType.Explorer;
        public virtual int[] NPCs => new int[] { };
        public virtual Item[] Rewards => new Item[] { };

        public byte Status = (byte)QuestStatus.NotStarted;

        public override void Load()
        {
            QuestID.quests = QuestID.quests.Append(this).ToArray();

            Language.GetOrRegister("Mods.Terrafirma.Quests." + $"{this.GetType().Name}" + ".Name", CreateQuestName);
            Language.GetOrRegister("Mods.Terrafirma.Quests." + $"{this.GetType().Name}" + ".Dialogue", CreateQuestDialogue);
            Language.GetOrRegister("Mods.Terrafirma.Quests." + $"{this.GetType().Name}" + ".Description", CreateQuestDescription);
        }

        string CreateQuestName() => this.GetType().Name.Titleize();
        static string CreateQuestDialogue() => "";
        static string CreateQuestDescription() => "";

        public string GetQuestName()
        {
            return Language.GetTextValue("Mods.Terrafirma.Quests." + $"{this.GetType().Name}" + ".Name");
        }
        public string GetQuestDialogue()
        {
            return Language.GetTextValue("Mods.Terrafirma.Quests." + $"{this.GetType().Name}" + ".Dialogue");
        }
        public string GetQuestDescription()
        {
            return Language.GetTextValue("Mods.Terrafirma.Quests." + $"{this.GetType().Name}" + ".Description");
        }

        /// <summary>
        /// override to insert custom completion conditions. Returns false by default.
        /// </summary>
        /// <returns></returns>
        public virtual bool CanBeCompleted()
        {
            return false;
        }

        /// <summary>
        /// override to insert custom appearance conditions. Returns false by default.
        /// </summary>
        /// <returns></returns>
        public virtual bool Condition()
        {
            return false;
        }

        /// <summary>
        /// Triggers whenever this quest is activated
        /// </summary>
        public virtual void OnQuestActivation()
        {

        }

        protected override void Register()
        {
            ModTypeLookup<Quest>.Register(this);
        }
    }

    public class QuestID : ModSystem
    {
        public static Quest[] quests = new Quest[] { };
    }

    public static class QuestArrayMethods
    {
        public static Quest[] UncompletedQuests(this Quest[] questlist)
        {
            Quest[] uncompletedquests = new Quest[] { };
            for (int i = 0; i < questlist.Length; i++)
            {
                if (questlist[i].Status == 0) uncompletedquests = uncompletedquests.Append(questlist[i]).ToArray();
            }
            return uncompletedquests;
        }

        public static Quest[] OnGoingQuests(this Quest[] questlist)
        {
            Quest[] ongoingquests = new Quest[] { };
            for (int i = 0; i < questlist.Length; i++)
            {
                if (questlist[i].Status == 1) ongoingquests = ongoingquests.Append(questlist[i]).ToArray();
            }
            return ongoingquests;
        }

        public static Quest[] FinishedQuests(this Quest[] questlist)
        {
            Quest[] finishedquests = new Quest[] { };
            for (int i = 0; i < questlist.Length; i++)
            {
                if (questlist[i].Status == 2) finishedquests = finishedquests.Append(questlist[i]).ToArray();
            }
            return finishedquests;
        }

        public static int GetQuestIndex(this Quest[] array, Quest quest)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].Name == quest.Name) return i;
            }
            return -1;
        }

        public static Quest[] SetupPlayerQuestList(this Quest[] questarray)
        {
            string[] questarraynames = new string[] { };
            for (int i = 0; i < questarray.Length; i++)
            {
                questarraynames = questarraynames.Append(questarray[i].FullName).ToArray();
            }
            
            for (int i = 0; i < QuestID.quests.Length; i++)
            {
                if (!questarraynames.Contains(QuestID.quests[i].FullName))
                {
                    questarray = questarray.Append(QuestID.quests[i]).ToArray();
                }
            }

            return questarray;
        }

        public static Quest[] GetSpecificToNPCQuestArray(this Quest[] questarray, int npcid, bool checkcondition = false)
        {
            Quest[] returnquestarray = new Quest[] { };
            for (int i = 0; i < questarray.Length; i++) {
                if (checkcondition)
                {
                    if (questarray[i].Condition() && questarray[i].NPCs.Contains(npcid)) returnquestarray = returnquestarray.Append(questarray[i]).ToArray();
                }
                else if(questarray[i].NPCs.Contains(npcid)) returnquestarray = returnquestarray.Append(questarray[i]).ToArray();
            }
            return returnquestarray;
        }
    }
}
