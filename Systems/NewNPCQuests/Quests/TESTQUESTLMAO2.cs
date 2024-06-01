using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Items.Equipment.Ranged;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Systems.NewNPCQuests.Quests
{
    internal class TESTQUESTLMAO2 : Quest
    {
        public override byte Difficulty => (byte)QuestDifficulty.Deranged;
        public override byte Type => (byte)QuestType.Explorer;
        public override int[] NPCs => new int[] { NPCID.ArmsDealer };
        public override Item[] Rewards => new Item[] { new Item(ModContent.ItemType<AmmoCan>()), new Item(ItemID.TerraBlade), new Item(ItemID.TerraFartMinecart) };

        public override bool QuestActivation()
        {
            return true;
        }
        public override bool QuestCompletion()
        {
            return true;
        }
    }
}
