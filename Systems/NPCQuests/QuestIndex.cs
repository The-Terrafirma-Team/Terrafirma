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
    /// <summary>
    /// <para>Class for all the Quests that are loaded into the game. </para>
    /// <para>Do not try to alter anything in here, instead, if you want to alter parameters like completion of a quest for a player do it inside</para>
    /// <para>TerrafirmaGlobalPlayer.playerquests </para>
    /// </summary>
    public class QuestIndex : ModSystem
    {
        //To Add a quest just make a new public static Quest variable
        //Then set it in OnModLoad() and append it to the quests array
        

        public static Quest[] quests = new Quest[] { };

        public static Quest quest1 = new Quest();
        public static Quest quest2 = new Quest();
        public static Quest quest3 = new Quest();

        public override void OnModLoad()
        {
            
            quest1 = new Quest(
                "Quest",
                dialogue: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",
                taskdescription: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.", 
                difficulty: 0,
                involvednpcs: new int[] {NPCID.ArmsDealer}, 
                rewards: new Item[] {new Item(ItemID.CopperShortsword)},
                null
                );
            quests = quests.Append(quest1).ToArray();

            quest2 = new Quest(
                "Quest2",
                dialogue: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Mauris vitae ultricies leo integer malesuada nunc vel risus commodo.",
                taskdescription: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Mauris vitae ultricies leo integer malesuada nunc vel risus commodo.",
                difficulty: 3,
                involvednpcs: new int[] { NPCID.ArmsDealer },
                rewards: new Item[] { new Item(ItemID.CopperShortsword), new Item(ItemID.CopperShortsword) },
                new Condition[] { Condition.TimeNight }
                );
            quests = quests.Append(quest2).ToArray();

            quest3 = new Quest(
                "Quest3 Wow wow insane",
                dialogue: "Lorem ipsum dolor sit amet, consectetur adipiscing el",
                taskdescription: "Eat Ass",
                difficulty: 5,
                involvednpcs: new int[] { NPCID.ArmsDealer },
                rewards: new Item[] { new Item(ItemID.TerraBlade), new Item(ItemID.TerraBladeChronicles) },
                new Condition[] { Condition.TimeNight }
                );
            quests = quests.Append(quest3).ToArray();


            base.OnModLoad();
        }
    }
}
