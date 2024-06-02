using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Items.Equipment.Ranged;
using Terrafirma.Items.Weapons.Ranged.Guns.PreHardmode;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Systems.NewNPCQuests.Quests.ArmsDealer
{
    internal class Pistolero : Quest
    {
        public override byte Difficulty => (byte)QuestDifficulty.Easy;
        public override byte Type => (byte)QuestType.Destroyer;
        public override int[] NPCs => new int[] { NPCID.ArmsDealer };
        public override Item[] Rewards => new Item[] { new Item(ModContent.ItemType<BigIron>()) };

        public override bool Condition()
        {
            return true;
        }
        public override bool CanBeCompleted()
        {
            return false;
        }
    }
}
