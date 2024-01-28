using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerrafirmaRedux.Global.Structs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Systems.NPCQuests
{
    internal class QuestIndex : ModSystem
    {
        public static Quest[] quests = new Quest[] { };

        public override void OnModLoad()
        {
            quests = quests.Append(new Quest(
                "Quest",
                dialogue: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",
                taskdescription: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.", 
                difficulty: 0,
                involvednpcs: new int[] {NPCID.ArmsDealer}, 
                rewards: new Item[] {new Item(ItemID.CopperShortsword)},
                null
                ) ).ToArray();
            quests = quests.Append(new Quest(
                "Quest2",
                dialogue: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Mauris vitae ultricies leo integer malesuada nunc vel risus commodo.",
                taskdescription: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Mauris vitae ultricies leo integer malesuada nunc vel risus commodo.",
                difficulty: 3,
                involvednpcs: new int[] { NPCID.ArmsDealer },
                rewards: new Item[] { new Item(ItemID.CopperShortsword), new Item(ItemID.CopperShortsword) },
                new Condition[] { Condition.TimeNight }
                )).ToArray();
            quests = quests.Append(new Quest(
                "Quest3 WOW WOW INSANE",
                dialogue: "Lorem ipsum dolor sit amet, consectetur adipiscing el",
                taskdescription: "Eat Ass",
                difficulty: 5,
                involvednpcs: new int[] { NPCID.ArmsDealer },
                rewards: new Item[] { new Item(ItemID.TerraBlade), new Item(ItemID.TerraBladeChronicles) },
                new Condition[] { Condition.TimeNight }
                )).ToArray();


            base.OnModLoad();
        }
    }
}
