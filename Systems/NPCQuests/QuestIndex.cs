using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerrafirmaRedux.Global.Structs;
using TerrafirmaRedux.Items.Equipment.Ranged;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Systems.NPCQuests
{
    /// <summary>
    /// Class for all the Quests that are loaded into the game.
    /// Do not try to alter anything in here, instead, if you want to alter parameters like completion of a quest for a player do it inside
    /// TerrafirmaGlobalPlayer.playerquests
    /// <para>This Class always gets automatically imported for all players</para>
    /// </summary>
    public class QuestIndex : ModSystem
    {
        //To Add a quest just make a new public static Quest variable
        //Then set it in OnModLoad() and append it to the quests array
        

        public static Quest[] quests = new Quest[] { };

        public static Quest BusinessDeal = new Quest();
        public static Quest quest2 = new Quest();
        public static Quest quest3 = new Quest();

        public override void OnModLoad()
        {
            base.OnModLoad();

            BusinessDeal = new Quest(
                "Business Deal",
                dialogue: "Hey Pal, guess I'm a new face for you. All I'll tell ya is that you have an eye for guns and guns is what I sell. I see a great partnership forming between us so let's start with a bang. I have just what you want!",
                taskdescription: "Buy the Minishark and Flintlock pistol from the arms dealer", 
                difficulty: 0,
                involvednpcs: new int[] {NPCID.ArmsDealer}, 
                rewards: new int[] { ModContent.ItemType<AmmoCan>() }
                );
            quests = quests.Append(BusinessDeal).ToArray();

            quest2 = new Quest(
                "Quest2",
                dialogue: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Mauris vitae ultricies leo integer malesuada nunc vel risus commodo.",
                taskdescription: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Mauris vitae ultricies leo integer malesuada nunc vel risus commodo.",
                difficulty: 3,
                involvednpcs: new int[] { NPCID.ArmsDealer },
                rewards: new int[] { ItemID.Dirt1Echo  },
                new Condition[] { Condition.TimeNight }
                );
            quests = quests.Append(quest2).ToArray();

            quest3 = new Quest(
                "Quest3 Wow wow insane",
                dialogue: "Lorem ipsum dolor sit amet, consectetur adipiscing el",
                taskdescription: "Eat Ass",
                difficulty: 5,
                involvednpcs: new int[] { NPCID.ArmsDealer },
                rewards: new int[] { ItemID.Dirt1Echo },
                new Condition[] { Condition.TimeNight }
                );
            quests = quests.Append(quest3).ToArray();

        }
    }
    
}
