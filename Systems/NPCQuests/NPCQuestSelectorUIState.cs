using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using Terraria.Audio;
using Terraria.ID;
using Terraria.GameContent.UI.Elements;
using Terraria.UI.Chat;
using Terraria.GameContent;
using ReLogic.Graphics;
using Microsoft.Xna.Framework.Audio;
using System.Linq;
using Terrafirma.Items.Weapons.Ranged.Guns.PreHardmode;
using Terrafirma.Common.Structs;
using Terrafirma.Systems.NPCQuests;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terrafirma.Items.Equipment.Ranged;
using Terrafirma.Common.Players;

namespace Terrafirma.Systems.NPCQuests
{
    [Autoload(Side = ModSide.Client)]


    // This code needs some major clean up, it really does
    // Also Add a warning window for when you start a new quest
    internal class NPCQuestSelectorUIState : UIState
    {

        public Quest selectedQuest;

        UIPanel MainContainer;
        UIPanel SideContainerLeft;
        UIPanel SideContainerRight;
        UIScrollbar VerticalSlider;

        UIPanel Button;
        UIText ButtonText;
        UIPanel[] Buttons = new UIPanel[]{};
        Quest[] ButtonQuests;
        UIText[] ButtonTexts = new UIText[]{};
        UIImage QuestTypeImage;
        UIImage[] QuestTypeImages = new UIImage[]{};

        UIPanel CompleteButton;
        UIText CompleteButtonText;

        UIText QuestName;
        UIPanel QuestDialoguePanel;
        UIText QuestDialogue;
        UIText QuestTaskTitle;
        UIPanel QuestTaskPanel;
        UIText QuestTask;

        UIText DifficultyText;
        UIImage DifficultyStars;
        UIImage[] DifficultyStarsList = new UIImage[]{};
        public int DifficultyRating = 0;

        UIText RewardText;
        UIItemSlot RewardItem;
        UIItemSlot[] RewardsItemList = new UIItemSlot[]{};
        public Item[] RewardItems = new Item[] {};

        UIText SimpleTooltip;
        bool ShowSimpleTooltip;

        int ButtonAmount;

        Texture2D EmptyStarTexture;
        Texture2D FullStarTexture;
        Texture2D[] QuestTypeIcons;

        Quest[] localquestlist;

        NPC selectednpc = null;

        /// <summary>
        /// Creates the UI
        /// </summary>
        /// <param name="npc">The NPC that the Player is currently talking to</param>
        public override void OnActivate()
        {
            //Set Textures
            EmptyStarTexture = ModContent.Request<Texture2D>("Terrafirma/Systems/NPCQuests/EmptyStar", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            FullStarTexture = ModContent.Request<Texture2D>("Terrafirma/Systems/NPCQuests/FilledStar", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;

            QuestTypeIcons = new Texture2D[]
            {
                ModContent.Request<Texture2D>("Terrafirma/Systems/NPCQuests/QuestType_Explorer", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value,
                ModContent.Request<Texture2D>("Terrafirma/Systems/NPCQuests/QuestType_Scavenger", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value,
                ModContent.Request<Texture2D>("Terrafirma/Systems/NPCQuests/QuestType_Collector", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value,
                ModContent.Request<Texture2D>("Terrafirma/Systems/NPCQuests/QuestType_Slayer", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value,
                ModContent.Request<Texture2D>("Terrafirma/Systems/NPCQuests/QuestType_Destroyer", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value,
                ModContent.Request<Texture2D>("Terrafirma/Systems/NPCQuests/QuestType_Final", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value,
                ModContent.Request<Texture2D>("Terrafirma/Systems/NPCQuests/QuestType_Special", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value
            };

            base.OnActivate();
        }
        public void Create(NPC npc)
        {
            //Selected NPC for getting the right quest list later
            localquestlist = Main.LocalPlayer.GetModPlayer<TerrafirmaModPlayer>().playerquests;
            selectednpc = npc;  

            //Other Vars
            DifficultyRating = 2;
            RewardItems = new Item[] { new Item(ModContent.ItemType<GunSword>(), 1), new Item(ItemID.GoldCoin, 5) };
            ButtonAmount = 30;

            //Main Container - UI Element
            MainContainer = new UIPanel();
            MainContainer.HAlign = 0.5f;
            MainContainer.VAlign = 0.5f;
            MainContainer.Width.Pixels = 900f;
            MainContainer.Height.Pixels = 600f;
            MainContainer.BorderColor = Color.Black;
            Append(MainContainer);

            //Left Side Container - UI Element
            SideContainerLeft = new UIPanel();
            SideContainerLeft.HAlign = 0.5f;
            SideContainerLeft.VAlign = 0.5f;
            SideContainerLeft.BackgroundColor = new Color(50f / 255f, 0f, 120f / 255f, 1f) * 0.4f;
            SideContainerLeft.MarginRight = (MainContainer.Width.Pixels / 10) * 6.9f - 10f;
            SideContainerLeft.Width.Pixels = (MainContainer.Width.Pixels / 10) * 3.1f - 10f;
            SideContainerLeft.Height.Pixels = MainContainer.Height.Pixels - 20f;
            SideContainerLeft.BorderColor = Color.Black;
            SideContainerLeft.OverflowHidden = true;
            MainContainer.Append(SideContainerLeft);

            //Right Side Container - UI Element
            SideContainerRight = new UIPanel();
            SideContainerRight.HAlign = 0.5f;
            SideContainerRight.VAlign = 0.5f;
            SideContainerRight.BackgroundColor = new Color(50f / 255f, 0f, 120f / 255f, 1f) * 0.4f;
            SideContainerRight.MarginRight = (-MainContainer.Width.Pixels / 10) * 3.1f;
            SideContainerRight.Width.Pixels = (MainContainer.Width.Pixels / 10) * 6.9f - 20f;
            SideContainerRight.Height.Pixels = MainContainer.Height.Pixels - 20f;
            SideContainerRight.BorderColor = Color.Black;
            MainContainer.Append(SideContainerRight);

            //If The Amount of buttons is above 12 => Create Vertical Scrollbar on the Left Side Container
            if (ButtonAmount > 12)
            {
                VerticalSlider = new UIScrollbar();
                VerticalSlider.Width.Pixels = 50f;
                VerticalSlider.Height.Pixels = SideContainerLeft.Height.Pixels - 40f;
                VerticalSlider.MarginTop = 6f;
                VerticalSlider.HAlign = 1f;
                VerticalSlider.SetView( (45f * ButtonAmount) - (45f * (ButtonAmount - 12)), 45f * ButtonAmount - 5f);
                SideContainerLeft.Append(VerticalSlider);
            }

            //Completion Status Button - UI Element
            CompleteButton = new UIPanel(ModContent.Request<Texture2D>("Terrafirma/Systems/NPCQuests/QuestButtonPanel"), ModContent.Request<Texture2D>("Terrafirma/Systems/NPCQuests/QuestButtonPanel"), 20);
            CompleteButton.Width.Pixels = ButtonAmount > 12 ? 220f : 250f;
            CompleteButton.Height.Pixels = 40f;
            CompleteButton.HAlign = 1f;
            CompleteButton.VAlign = 1f;
            CompleteButton.BackgroundColor = new Color(0.4f, 0.5f, 1f, 1f);
            CompleteButton.BorderColor = new Color(0.4f, 0.5f, 1f);

            //Completion Status Button Text - UI Element
            CompleteButtonText = new UIText("Complete", 1.1f, false);
            CompleteButtonText.Width.Pixels = 200f;
            CompleteButtonText.VAlign = 0.5f;
            CompleteButton.Append(CompleteButtonText);

            //Mouse Tooltip - UI Element
            SimpleTooltip = new UIText("Tooltip", 1.05f);
            SimpleTooltip.MarginTop = Main.MouseScreen.Y + 20f;
            SimpleTooltip.MarginLeft = Main.MouseScreen.X + 20f;
            Append(SimpleTooltip);

            UIProgressBar progress = new UIProgressBar();
            progress.HAlign = 0.5f;
            progress.VAlign = 0.5f;
            Append(progress);

            UpdateQuests();
            UpdateCompleteButton();
            UpdateQuestButton();

            //Set selected quest to NoQuest so nothing is displayed when the UI is open
            selectedQuest = null;

        }

        /// <summary>
        /// Deletes the UI
        /// </summary>
        public void Flush() { RemoveAllChildren(); }

        /// <summary>
        /// Tick Update
        /// </summary>
        public override void Update(GameTime gameTime)
        {

            UpdateCompleteButton();
            UpdateQuestButton();

            ShowSimpleTooltip = false;

            SimpleTooltip.MarginTop = Main.MouseScreen.Y + 20f;
            SimpleTooltip.MarginLeft = Main.MouseScreen.X + 20f;

            for (int i = 0; i < DifficultyStarsList.Length; i++)
            {
                if (DifficultyStarsList != null && DifficultyStarsList[i].IsMouseHovering)
                {
                    switch (DifficultyRating)
                    {
                        case 0: SimpleTooltip.SetText("Effortless"); break;
                        case 1: SimpleTooltip.SetText("Easy"); break;
                        case 2: SimpleTooltip.SetText("Intermediate"); break;
                        case 3: SimpleTooltip.SetText("Difficult"); break;
                        case 4: SimpleTooltip.SetText("Intense"); break;
                        case 5: SimpleTooltip.SetText("Deranged"); break;
                    }

                    ShowSimpleTooltip = true;
                }
            }

            if (ShowSimpleTooltip) SimpleTooltip.TextColor = Color.White;
            else SimpleTooltip.TextColor = new Color(0f, 0f, 0f, 0f);

            if (ButtonAmount > 12)
            {
                for (int i = 0; i < Buttons.Length; i++)
                {
                    Buttons[i].MarginTop = -VerticalSlider.GetValue() + ((Button.Height.Pixels + 5) * i);
                }
            }

            if (CompleteButton.IsMouseHovering)
            {
                CompleteButton.BackgroundColor = new Color(1f, 0.8f, 0.1f, 1f);
                CompleteButton.BorderColor = new Color(1f, 0.8f, 0.1f);
            }
            else
            {
                CompleteButton.BackgroundColor = new Color(0.4f, 0.5f, 1f, 1f);
                CompleteButton.BorderColor = new Color(0.4f, 0.5f, 1f);
            }

            if (MainContainer.IsMouseHovering) Main.blockMouse = true;
            else Main.blockMouse = false;
        }

        /// <summary>
        /// Checks for Button Clicks
        /// </summary>
        public override void LeftClick(UIMouseEvent evt)
        {
            //
            //Completion Status Button Left Click
            //
            if (CompleteButton.IsMouseHovering)
            {

                SoundEngine.PlaySound(SoundID.MenuTick);

                //If the current quest has not been started yet
                if (localquestlist[localquestlist.GetQuestIndex(selectedQuest)].Status == (byte)QuestStatus.NotStarted)
                {
                    localquestlist[localquestlist.GetQuestIndex(selectedQuest)].Status = (byte)QuestStatus.InProgress;

                    //Loop through all Quests and set their status to 0 => When A quest is started, all other quests are automatically ended
                    for (int i = 0; i < localquestlist.Length; i++)
                    {
                        if (localquestlist[i].Status == (byte)QuestStatus.InProgress && localquestlist[i].Name != selectedQuest.Name)
                        {
                            localquestlist[i].Status = (byte)QuestStatus.NotStarted;
                            //Loop through all Button Quests and set their completion
                            for (int j = 0; j < ButtonQuests.Length; j++)
                            {
                                if (!( ButtonQuests[j].Name == localquestlist[localquestlist.GetQuestIndex(selectedQuest)].Name)) 
                                { 
                                    ButtonQuests[j].Status = (byte)QuestStatus.NotStarted;
                                    
                                }
                            }
                        }


                    }

                    for (int i = 0; i < localquestlist.Length; i++)
                    {
                        if (localquestlist[i].Name == localquestlist[localquestlist.GetQuestIndex(selectedQuest)].Name ) localquestlist[i].Status = (byte)QuestStatus.InProgress;
                    }
                }
                //If the current quest is in progress and the current quest can be completed
                else if (localquestlist[localquestlist.GetQuestIndex(selectedQuest)].Status == (byte)QuestStatus.InProgress && localquestlist[localquestlist.GetQuestIndex(selectedQuest)].QuestCompletion(Main.LocalPlayer))
                {
                    localquestlist[localquestlist.GetQuestIndex(selectedQuest)].Status = (byte)QuestStatus.Completed;

                    //Loop through all Button Quests and set their completion
                    for (int i = 0; i < ButtonQuests.Length; i++)
                    {
                        if (ButtonQuests[i].Name == localquestlist[localquestlist.GetQuestIndex(selectedQuest)].Name) 
                        {
                            ButtonQuests[i].Status = (byte)QuestStatus.Completed; 
                        }
                    }

                    //Give Player the reward
                    for (int i = 0; i < localquestlist[localquestlist.GetQuestIndex(selectedQuest)].Rewards.Length; i++)
                    {
                        Main.LocalPlayer.QuickSpawnItem(Main.LocalPlayer.GetSource_FromThis(), 
                            localquestlist[localquestlist.GetQuestIndex(selectedQuest)].Rewards[i], 
                            localquestlist[localquestlist.GetQuestIndex(selectedQuest)].Rewards[i].stack);
                    }

                    //Remove Status Button as the quest has been completed
                    SideContainerRight.RemoveChild(CompleteButton);
                }

                UpdateQuests();

            }

            //
            // Quest Buttons Left Click
            //
            for (int i = 0; i < Buttons.Length;i++)
            {
                if (Buttons[i].IsMouseHovering)
                {
                    //Set Textures and Other Stuff
                    EmptyStarTexture = ModContent.Request<Texture2D>("Terrafirma/Systems/NPCQuests/EmptyStar").Value;
                    FullStarTexture = ModContent.Request<Texture2D>("Terrafirma/Systems/NPCQuests/FilledStar").Value;

                    SoundEngine.PlaySound(SoundID.MenuTick);

                    DifficultyRating = ButtonQuests[i].Difficulty;
                    selectedQuest = ButtonQuests[i];

                    // Create Info UI on the Right Side Panel

                    // Quest Name - UI Element
                    if (SideContainerRight.HasChild(QuestName)) SideContainerRight.RemoveChild(QuestName);
                    QuestName = new UIText(ButtonQuests[i].Name, 0.8f, true);
                    QuestName.VAlign = 0f;
                    QuestName.HAlign = 0.5f;
                    QuestName.Height.Pixels = 30f;
                    QuestName.MarginTop = 10f;
                    SideContainerRight.Append(QuestName);

                    // Quest Dialogue Panel - UI Element
                    if (SideContainerRight.HasChild(QuestDialoguePanel)) SideContainerRight.RemoveChild(QuestDialoguePanel);
                    QuestDialoguePanel = new UIPanel();
                    QuestDialoguePanel.VAlign = 0f;
                    QuestDialoguePanel.HAlign = 0.5f;
                    QuestDialoguePanel.BackgroundColor = new Color(20, 21, 51) * 0.4f;
                    QuestDialoguePanel.Width.Pixels = SideContainerRight.Width.Pixels - 20f;
                    QuestDialoguePanel.Height.Pixels = ((ButtonQuests[i].Dialogue.Length / 45) + 1) * 20f + 20f;
                    QuestDialoguePanel.MarginTop = QuestName.Height.Pixels + 24f;
                    SideContainerRight.Append(QuestDialoguePanel);

                    // Quest Dialogue - UI Element
                    if (QuestDialoguePanel.HasChild(QuestDialogue)) QuestDialoguePanel.RemoveChild(QuestDialogue);
                    QuestDialogue = new UIText(ButtonQuests[i].Dialogue, 1f);
                    QuestDialogue.VAlign = 0f;
                    QuestDialogue.HAlign = 0f;
                    QuestDialogue.Height.Pixels = ((QuestDialogue.Text.Length / 45) + 1) * 20f;
                    QuestDialogue.Width.Pixels = SideContainerRight.Width.Pixels;
                    QuestDialogue.IsWrapped = true;
                    QuestDialoguePanel.Append(QuestDialogue);

                    // Quest Task Title - UI Element
                    if (SideContainerRight.HasChild(QuestTaskTitle)) SideContainerRight.RemoveChild(QuestTaskTitle);
                    QuestTaskTitle = new UIText("Task", 1.2f);
                    QuestTaskTitle.VAlign = 0f;
                    QuestTaskTitle.HAlign = 0f;
                    QuestTaskTitle.Width.Pixels = 45f;
                    QuestTaskTitle.Height.Pixels = 25f;
                    QuestTaskTitle.IsWrapped = true;
                    QuestTaskTitle.MarginTop = QuestName.Height.Pixels + (QuestDialoguePanel.Height.Pixels + 20f) + 25f;
                    SideContainerRight.Append(QuestTaskTitle);

                    // Quest Task Panel - UI Element
                    if (SideContainerRight.HasChild(QuestTaskPanel)) SideContainerRight.RemoveChild(QuestTaskPanel);
                    QuestTaskPanel = new UIPanel();
                    QuestTaskPanel.VAlign = 0f;
                    QuestTaskPanel.HAlign = 0.5f;
                    QuestTaskPanel.BackgroundColor = new Color(20, 21, 51) * 0.4f;
                    QuestTaskPanel.Width.Pixels = SideContainerRight.Width.Pixels - 20f;
                    QuestTaskPanel.Height.Pixels = ((ButtonQuests[i].Description.Length / 45) + 1) * 20f + 20f;
                    QuestTaskPanel.MarginTop = QuestName.Height.Pixels + (QuestDialoguePanel.Height.Pixels + 20f) + 50f;
                    SideContainerRight.Append(QuestTaskPanel);

                    // Quest Task Description - UI Element
                    if (QuestTaskPanel.HasChild(QuestTask)) QuestTaskPanel.RemoveChild(QuestTask);
                    QuestTask = new UIText(ButtonQuests[i].Description, 1f);
                    QuestTask.VAlign = 0f;
                    QuestTask.HAlign = 0f;
                    QuestTask.Width.Pixels = QuestTaskPanel.Width.Pixels;
                    QuestTask.Height.Pixels = ((QuestTask.Text.Length / 45) + 1) * 20f;
                    QuestTask.IsWrapped = true;
                    QuestTaskPanel.Append(QuestTask);

                    // Difficulty Title - UI Element
                    if (SideContainerRight.HasChild(DifficultyText)) SideContainerRight.RemoveChild(DifficultyText);
                    DifficultyText = new UIText("Difficulty", 1.2f);
                    DifficultyText.VAlign = 0f;
                    DifficultyText.HAlign = 0f;
                    DifficultyText.Width.Pixels = 90f;
                    DifficultyText.Height.Pixels = 25f;
                    DifficultyText.IsWrapped = true;
                    DifficultyText.MarginTop = QuestName.Height.Pixels + (QuestDialoguePanel.Height.Pixels + 20f) + (QuestTaskPanel.Height.Pixels + 20f) + 60f;
                    SideContainerRight.Append(DifficultyText);

                    // Quest Stars - UI Element

                    // Loop through the DifficultyStarsList and delete every previous star UI Element (Refresh UI)
                    for (int j = 0; j < 5; j++)
                    {
                        if (DifficultyStarsList.Length >= j + 1 && SideContainerRight.HasChild(DifficultyStarsList[j])) SideContainerRight.RemoveChild(DifficultyStarsList[j]);
                    }

                    DifficultyStarsList = new UIImage[] { };

                    //Loop through 5 times for each star
                        for (int j = 4; j >= 0; j--)
                    {
                        if (ButtonQuests[i].Difficulty >= j + 1) DifficultyStars = new UIImage(FullStarTexture); // Set Texture based on the quest's difficulty int
                        else DifficultyStars = new UIImage(EmptyStarTexture);

                        DifficultyStars.VAlign = 0f;
                        DifficultyStars.HAlign = 0f;
                        DifficultyStars.Width.Pixels = 30f;
                        DifficultyStars.Height.Pixels = 30f;
                        DifficultyStars.Color = new Color(1f, 1f - (j / 7f), 1f - (j / 9f), 1f);
                        DifficultyStars.MarginTop = QuestName.Height.Pixels + (QuestDialoguePanel.Height.Pixels + 20f) + (QuestTaskPanel.Height.Pixels + 20f) + DifficultyText.Height.Pixels + 75f;
                        DifficultyStars.MarginLeft = j * 20f;
                        DifficultyStarsList = DifficultyStarsList.Append(DifficultyStars).ToArray();
                        SideContainerRight.Append(DifficultyStars);
                        
                    }

                    // Reward Title - UI Element
                    if (ButtonQuests[i].Rewards != null)
                    {
                        if (SideContainerRight.HasChild(RewardText)) SideContainerRight.RemoveChild(RewardText);
                        RewardText = new UIText("Reward", 1.2f);

                        if (ButtonQuests[i].Rewards.Length <= 1) RewardText.SetText("Reward");
                        else RewardText.SetText("Rewards");

                        RewardText.VAlign = 0f;
                        RewardText.HAlign = 0.5f;
                        RewardText.Width.Pixels = 100f;
                        RewardText.Height.Pixels = 25f;
                        RewardText.IsWrapped = true;
                        RewardText.MarginTop = QuestName.Height.Pixels + (QuestDialoguePanel.Height.Pixels + 20f) + (QuestTaskPanel.Height.Pixels + 20f) + 60f;
                        SideContainerRight.Append(RewardText);


                    }

                    // Reward Item slots - UI Element

                    //Loop through RewardsItemList and delete every previous Item Slot (Refresh UI)
                    for (int j = 0; j < 100; j++)
                    {
                        if (RewardsItemList.Length >= j + 1 && SideContainerRight.HasChild(RewardsItemList[j])) SideContainerRight.RemoveChild(RewardsItemList[j]);
                        else break;
                    }

                    RewardsItemList = new UIItemSlot[] { };

                        //Loop through the length of the Rewards Array's length for each item slot
                    for (int j = 0; j < ButtonQuests[i].Rewards.Length; j++)
                    {
                            
                        if (RewardsItemList.Length >= j + 1 && SideContainerRight.HasChild(RewardsItemList[j])) SideContainerRight.RemoveChild(RewardsItemList[j]);

                        RewardItem = new UIItemSlot(ButtonQuests[i].Rewards, j, 22);
                        RewardItem.VAlign = 0f;
                        RewardItem.HAlign = 0.5f;
                        RewardItem.MarginTop = QuestName.Height.Pixels + (QuestDialoguePanel.Height.Pixels + 20f) + (QuestTaskPanel.Height.Pixels + 20f) + DifficultyText.Height.Pixels + 70f + (((float)Math.Floor((double)j / 6)) * 55);
                        RewardItem.MarginLeft = -30f + ((j % 6) * 110f);
                        RewardItem.Width.Pixels = 60f;
                        RewardItem.Height.Pixels = 60f;
                        SideContainerRight.Append(RewardItem);
                        RewardsItemList = RewardsItemList.Append(RewardItem).ToArray();
                        Main.inventoryScale = 1f; 
                        
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// Updates the list of quests available in the Quest UI
        /// </summary>
        public void UpdateQuests()
        {
            if (localquestlist.Length > 0)
            {
                

                //Delete everything in the Buttons and ButtonTexts Array + the Vertical Scrollbar (Refresh UI)
                for (int i = 0; i < Buttons.Length; i++)
                {
                    RemoveChild(Buttons[i]);
                    RemoveChild(ButtonTexts[i]);
                    RemoveChild(QuestTypeImages[i]);
                }

                if (VerticalSlider != null) RemoveChild(VerticalSlider);

                Buttons = new UIPanel[] { };
                ButtonTexts = new UIText[] { };
                QuestTypeImages = new UIImage[] { };
                ButtonQuests = new Quest[] {};
                int ButtonIndex = 0;

                ButtonAmount = localquestlist.Length;

                //If Amount of quest buttons is higher than 12 => Create Vertical Scrollbar
                if (ButtonAmount > 12)
                {
                    VerticalSlider = new UIScrollbar();
                    VerticalSlider.Width.Pixels = 50f;
                    VerticalSlider.Height.Pixels = SideContainerLeft.Height.Pixels - 40f;
                    VerticalSlider.MarginTop = 6f;
                    VerticalSlider.HAlign = 1f;
                    VerticalSlider.SetView((45f * ButtonAmount) - (45f * (ButtonAmount - 12)), 45f * ButtonAmount - 5f);
                    SideContainerLeft.Append(VerticalSlider);
                }

                //
                //Create Buttons for all available quests
                //For loop for each Button completion status (Order of the for loops matter)
                //In Progress quest are put to the top, quests that have not been started yet in the middle and completed quests at the bottom of the list
                //

                for (int p = 0; p < 3; p++)
                {
                    Quest[] Checkedlist = new Quest[] {};
                    switch (p)
                    {
                        case 0: Checkedlist = localquestlist.OnGoingQuests(); break;
                        case 1: Checkedlist = localquestlist.UncompletedQuests(); break;
                        case 2: Checkedlist = localquestlist.FinishedQuests(); break;
                    }

                    for (int i = 0; i < Checkedlist.Length; i++)
                    {
                        //Check if conditions match
                        bool allconditionsmatch = true;
                        if (Checkedlist[i].Conditions != null)
                        {
                            for (int k = 0; k < Checkedlist[i].Conditions.Length; k++)
                            {
                                if (!Checkedlist[i].Conditions[k].IsMet()) { allconditionsmatch = false; break; }
                            }
                        }
                        //Check if the quest NPC matches the NPC that is being talked to
                        if (!Checkedlist[i].NPCs.Contains(selectednpc.type))
                        {
                            allconditionsmatch = false;
                        }
                        if (!Checkedlist[i].QuestActivation(Main.LocalPlayer))
                        {
                            allconditionsmatch = false;
                        }
                        if (allconditionsmatch)
                        {

                            //Button - UI Element
                            Button = new UIPanel(ModContent.Request<Texture2D>("Terrafirma/Systems/NPCQuests/QuestButtonPanel"), ModContent.Request<Texture2D>("Terrafirma/Systems/NPCQuests/QuestButtonPanel"), 20);
                            Button.Width.Pixels = ButtonAmount > 12 ? 220f : 250f;
                            Button.Height.Pixels = 40f;
                            Button.HAlign = 0f;
                            Button.BackgroundColor = new Color(0.4f, 0.5f, 1f, 1f);
                            Button.BorderColor = new Color(0.4f, 0.5f, 1f);
                            Button.MarginTop = ((Button.Height.Pixels + 5) * ButtonIndex);
                            ButtonIndex++;

                            Buttons = Buttons.Append(Button).ToArray();

                            //Button Text - UI Element
                            string ButtonTextText = Checkedlist[i].Name;
                            ButtonText = new UIText(ButtonTextText, Math.Clamp(1f / (ButtonTextText.Length / 20f), 0.5f, 1.1f));
                            ButtonText.Width.Pixels = ButtonAmount > 12 ? 220f : 250f;
                            ButtonText.MarginLeft = ButtonText.Text.Length > 15 ? ButtonText.Text.Length / 2 : 0;
                            ButtonText.VAlign = 0.5f;
                            Button.Append(ButtonText);

                            ButtonTexts = ButtonTexts.Append(ButtonText).ToArray();

                            //Button Icon - UI Element
                            QuestTypeImage = new UIImage(QuestTypeIcons[(int)Checkedlist[i].Type]);
                            QuestTypeImage.Width.Pixels = 32f;
                            QuestTypeImage.Height.Pixels = 32f;
                            QuestTypeImage.HAlign = 0f;
                            QuestTypeImage.VAlign = 0f;
                            QuestTypeImage.MarginTop = -QuestTypeImage.Height.Pixels / 2;
                            QuestTypeImage.MarginLeft = -QuestTypeImage.Width.Pixels / 2;
                            Button.Append(QuestTypeImage);

                            QuestTypeImages = QuestTypeImages.Append(QuestTypeImage).ToArray();

                            //Add the Button's Quest to the ButtonQuests Array
                            ButtonQuests = ButtonQuests.Append(Checkedlist[i]).ToArray();

                        }
                    }

                }

                //Loop through the whole Buttons array and append each Button to the Left Side COntainer
                for (int i = 0; i < Buttons.Length; i++)
                {
                    SideContainerLeft.Append(Buttons[i]);
                    Buttons[i].Append(QuestTypeImages[i]);
                }
            }
        }

        /// <summary>
        /// Updates the Status Button in the bottom right corner to display the right info based on quest status
        /// </summary>
        public void UpdateCompleteButton()
        {
            localquestlist = Main.LocalPlayer.GetModPlayer<TerrafirmaModPlayer>().playerquests;

            //Check if the selected quest is in the Player's quest list && Check if the selected quest is not "NoQuest"
            if (localquestlist.Contains(selectedQuest) && selectedQuest.Name != null)
            {
                //If The Quest's Completion status is "Completed"
                if (localquestlist[localquestlist.GetQuestIndex(selectedQuest)].Status == 2)
                {
                    if (SideContainerRight.HasChild(CompleteButton)) SideContainerRight.RemoveChild(CompleteButton);
                }

                //If The Quest's Completion status is "In Progress"
                else if (localquestlist[localquestlist.GetQuestIndex(selectedQuest)].Status == 1)
                {
                    //If the Quest can be Completed
                    if ( localquestlist[localquestlist.GetQuestIndex(selectedQuest)].QuestCompletion(Main.LocalPlayer) )
                    {
                        CompleteButtonText.SetText("Claim Reward");
                        if (!SideContainerRight.HasChild(CompleteButton)) SideContainerRight.Append(CompleteButton);
                    }
                    //If the Quest cannot yet be Completed
                    else
                    {
                        CompleteButtonText.SetText("In Progress");
                        if (!SideContainerRight.HasChild(CompleteButton)) SideContainerRight.Append(CompleteButton);
                    }
                }

                //If The Quest's Completion status is "Not Started"
                else if (localquestlist[localquestlist.GetQuestIndex(selectedQuest)].Status == 0)
                {
                    CompleteButtonText.SetText("Start Quest");
                    if (!SideContainerRight.HasChild(CompleteButton)) SideContainerRight.Append(CompleteButton);
                }
            }
        }

        /// <summary>
        /// Updates the Quest Buttons on the left panel to display the right color based on the Quest's status
        /// </summary>
        public void UpdateQuestButton()
        {
            for (int i = 0; i < ButtonQuests.Length; i++)
            {
                switch (ButtonQuests[i].Status)
                {
                    //If The Quest's Completion status is "Completed"
                    case 0:
                        // Blue
                        if (Buttons[i].IsMouseHovering)
                        { Buttons[i].BackgroundColor = new Color(1f, 0.8f, 0.1f, 1f); Buttons[i].BorderColor = new Color(1f, 0.8f, 0.1f); }
                        else
                        { Buttons[i].BackgroundColor = new Color(0.4f, 0.5f, 1f, 1f); Buttons[i].BorderColor = new Color(0.4f, 0.5f, 1f); }
                        break;
                    //If The Quest's Completion status is "In Progress"
                    case 1:
                        // If the Quest can be completed
                        // Master Mode Color
                        if (ButtonQuests[i].QuestCompletion(Main.LocalPlayer))
                        {
                            if (Buttons[i].IsMouseHovering)
                            { Buttons[i].BackgroundColor = new Color(1f, 0.8f, 0.1f, 1f); Buttons[i].BorderColor = new Color(1f, 0.8f, 0.1f); }
                            else
                            { Buttons[i].BackgroundColor = new Color(1f, Main.masterColor, 0f); Buttons[i].BorderColor = new Color(1f, Main.masterColor, 0f); }
                        }
                        // If the Quest cannot yet be completed
                        // Pink
                        else
                        {
                            if (Buttons[i].IsMouseHovering)
                            { Buttons[i].BackgroundColor = new Color(1f, 0.8f, 0.1f, 1f); Buttons[i].BorderColor = new Color(1f, 0.8f, 0.1f); }
                            else
                            { Buttons[i].BackgroundColor = new Color(255, 150, 255); Buttons[i].BorderColor = new Color(255, 150, 255); }
                        }
                        break;
                    //If The Quest's Completion status is "Completed"
                    case 2:
                        // Faded Out Blue
                        if (Buttons[i].IsMouseHovering)
                        { Buttons[i].BackgroundColor = new Color(1f, 0.8f, 0.1f, 1f); Buttons[i].BorderColor = new Color(1f, 0.8f, 0.1f); }
                        else
                        { Buttons[i].BackgroundColor = new Color(0.4f, 0.5f, 1f, 1f) * 0.2f; Buttons[i].BorderColor = new Color(0.4f, 0.5f, 1f) * 0.2f; }
                        break;
                }
            }
        }

    }
}
