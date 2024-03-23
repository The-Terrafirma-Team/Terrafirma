using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Items.Equipment.Ranged;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Systems.NPCQuests.Quests
{
    internal class BusinessDeal : Quest
    {
        public override string Name => "Business Deal";
        public override string Dialogue => "Dialogue Text";
        public override string Description => "Description Text";
        public override byte Difficulty => (byte)QuestDifficulty.Effortless;
        public override byte Type => (byte)QuestType.Explorer;
        public override int[] NPCs => new int[] {NPCID.ArmsDealer};
        public override Item[] Rewards => new Item[] {new Item(ModContent.ItemType<AmmoCan>())};

        public override bool QuestActivation(Player player)
        {
            return true;
        }
        public override bool QuestCompletion(Player player)
        {
            return true;
        }
    }
}
