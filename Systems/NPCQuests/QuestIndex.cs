using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Global.Structs;
using Terrafirma.Items.Equipment.Ranged;
using Terrafirma.Items.Weapons.Ranged.Guns.Hardmode;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Systems.NPCQuests
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

        //Guide Quests
        public static Quest GuideQuest = new Quest();
        //Arms Dealer
        public static Quest BusinessDeal = new Quest();
        public static Quest ExperimentalMarineBiology = new Quest();
        public static Quest ExperimentalMarineBiology2 = new Quest();
        public static Quest ExperimentalMarineBiology3 = new Quest();
        public static Quest OlReliable = new Quest();
        public static Quest HorrorsBeyondGunComprehension = new Quest();

        public override void OnWorldLoad()
        {
            base.OnWorldLoad();

            quests = new Quest[] { };

            //Guide Quests
            GuideQuest = new Quest(
                "Guide Quest",
                dialogue: "Hey Pal, guess I'm a new face for you. All I'll tell ya is that you have an eye for guns and guns is what I sell. I see a great partnership forming between us so let's start with a bang. I have just what you want!",
                taskdescription: "Buy the Minishark and Flintlock pistol from the arms dealer",
                difficulty: 0,
                type: QuestType.Scavenger,
                involvednpcs: new int[] { NPCID.Guide },
                rewards: new Item[] { new Item(ModContent.ItemType<AmmoCan>()) }
                );
            quests = quests.Append(GuideQuest).ToArray();


            //Arms Dealer Quests
            BusinessDeal = new Quest(
                "Business Deal",
                dialogue: "Hey Pal, guess I'm a new face for you. All I'll tell ya is that you have an eye for guns and guns is what I sell. I see a great partnership forming between us so let's start with a bang. I have just what you want!",
                taskdescription: "Buy the Minishark and Flintlock pistol from the arms dealer",
                difficulty: 0,
                type: QuestType.Explorer,
                involvednpcs: new int[] { NPCID.ArmsDealer },
                rewards: new Item[] { new Item(ModContent.ItemType<AmmoCan>()) }
                );
            quests = quests.Append(BusinessDeal).ToArray();

            ExperimentalMarineBiology = new Quest(
                "Experimental Marine Biology",
                dialogue: "Fish are only good for one thing only, Gunsmithing! It seems like you've already gotten your hands on one of my little experiments, so help me with my 'environmentally friendly' research by testing  that minishark on the aquatic wildlife... and don't forget to bring me some of the remians!",
                taskdescription: "kill 30 ocean enemies with the Minishark and bring 3 shark fins",
                difficulty: 0,
                type: QuestType.Slayer,
                involvednpcs: new int[] { NPCID.ArmsDealer },
                rewards: new Item[] { new Item( ItemID.Boomstick ) }
                );
            quests = quests.Append(ExperimentalMarineBiology).ToArray();

            ExperimentalMarineBiology2 = new Quest(
                "Experimental Marine Biology II",
                dialogue: "Hey Pal, guess I'm a new face for you. All I'll tell ya is that you have an eye for guns and guns is what I sell. I see a great partnership forming between us so let's start with a bang. I have just what you want!",
                taskdescription: "Buy the Minishark and Flintlock pistol from the arms dealer",
                difficulty: 0,
                type: QuestType.Slayer,
                involvednpcs: new int[] { NPCID.ArmsDealer },
                rewards: new Item[] { new Item(ModContent.ItemType<Microshark>()) }
                );
            quests = quests.Append(ExperimentalMarineBiology2).ToArray();

            ExperimentalMarineBiology3 = new Quest(
                "Experimental Marine Biology III",
                dialogue: "Hey Pal, guess I'm a new face for you. All I'll tell ya is that you have an eye for guns and guns is what I sell. I see a great partnership forming between us so let's start with a bang. I have just what you want!",
                taskdescription: "Buy the Minishark and Flintlock pistol from the arms dealer",
                difficulty: 0,
                type: QuestType.Special,
                involvednpcs: new int[] { NPCID.ArmsDealer },
                rewards: new Item[] { new Item(ModContent.ItemType<Microshark>()) }
                );
            quests = quests.Append(ExperimentalMarineBiology3).ToArray();

            OlReliable = new Quest(
                "Ol' Reliable",
                dialogue: "Hey Pal, guess I'm a new face for you. All I'll tell ya is that you have an eye for guns and guns is what I sell. I see a great partnership forming between us so let's start with a bang. I have just what you want!",
                taskdescription: "Buy the Minishark and Flintlock pistol from the arms dealer",
                difficulty: 0,
                type: QuestType.Destroyer,
                involvednpcs: new int[] { NPCID.ArmsDealer },
                rewards: new Item[] { new Item(ModContent.ItemType<Microshark>()) }
                );
            quests = quests.Append(OlReliable).ToArray();

            HorrorsBeyondGunComprehension = new Quest(
                "Horrors Beyond Gun Comprehension",
                dialogue: "Hey Pal, guess I'm a new face for you. All I'll tell ya is that you have an eye for guns and guns is what I sell. I see a great partnership forming between us so let's start with a bang. I have just what you want!",
                taskdescription: "Buy the Minishark and Flintlock pistol from the arms dealer",
                difficulty: 0,
                type: QuestType.Collector,
                involvednpcs: new int[] { NPCID.ArmsDealer },
                rewards: new Item[] { new Item(ModContent.ItemType<Microshark>()) }
                );
            quests = quests.Append(HorrorsBeyondGunComprehension).ToArray();
        }

    }
    
}
