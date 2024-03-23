using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Global;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Systems.NPCQuests
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
        public virtual string Name => "";
        public virtual string Dialogue => "";
        public virtual string Description => "";
        public virtual byte Difficulty => 0;
        public virtual byte Type => (byte)QuestType.Explorer;
        public virtual int[] NPCs => new int[] { };
        public virtual Item[] Rewards => new Item[] { };
        public virtual Condition[] Conditions => new Condition[] { };

        public byte Status = (byte)QuestStatus.NotStarted;

        public override void Load()
        {
            QuestIndex.quests = QuestIndex.quests.Append(this).ToArray();
        }

        /// <summary>
        /// override to insert custom completion conditions. Returns false by default.
        /// </summary>
        /// <returns></returns>
        public virtual bool QuestCompletion(Player player)
        {
            return false;
        }

        /// <summary>
        /// override to insert custom activation conditions. Returns false by default.
        /// </summary>
        /// <returns></returns>
        public virtual bool QuestActivation(Player player)
        {
            return false;
        }

        protected override void Register()
        {
            ModTypeLookup<Quest>.Register(this);
        }
    }

    public class QuestIndex : ModSystem
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
    }
}
