using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using Terraria.Audio;
using Terraria.ID;
using Terraria.GameContent.UI.Elements;
using TerrafirmaRedux.Global;
using Terraria.UI.Chat;
using Terraria.GameContent;
using ReLogic.Graphics;
using Microsoft.Xna.Framework.Audio;
using System.Linq;
using TerrafirmaRedux.Items.Weapons.Ranged.Guns.PreHardmode;
using TerrafirmaRedux.Global.Structs;
using TerrafirmaRedux.Systems.NPCQuests;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using TerrafirmaRedux.Items.Equipment.Ranged;

namespace TerrafirmaRedux.Systems.MageClass
{
    [Autoload(Side = ModSide.Client)]
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
        UIText QuestDialogue;
        UIText QuestTaskTitle;
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

        QuestList localquestlist;

        //Creates the UI
        public void Create()
        {
            localquestlist = Main.LocalPlayer.GetModPlayer<TerrafirmaGlobalPlayer>().playerquests;
            EmptyStarTexture = ModContent.Request<Texture2D>("TerrafirmaRedux/Systems/NPCQuests/EmptyStar").Value;
            FullStarTexture = ModContent.Request<Texture2D>("TerrafirmaRedux/Systems/NPCQuests/FilledStar").Value;

            QuestTypeIcons = new Texture2D[]
            {
                ModContent.Request<Texture2D>("TerrafirmaRedux/Systems/NPCQuests/QuestType_Explorer").Value,
                ModContent.Request<Texture2D>("TerrafirmaRedux/Systems/NPCQuests/QuestType_Scavenger").Value,
                ModContent.Request<Texture2D>("TerrafirmaRedux/Systems/NPCQuests/QuestType_Collector").Value,
                ModContent.Request<Texture2D>("TerrafirmaRedux/Systems/NPCQuests/QuestType_Slayer").Value,
                ModContent.Request<Texture2D>("TerrafirmaRedux/Systems/NPCQuests/QuestType_Destroyer").Value,
                ModContent.Request<Texture2D>("TerrafirmaRedux/Systems/NPCQuests/QuestType_Final").Value
            };

            DifficultyRating = 2;
            RewardItems = new Item[] { new Item(ModContent.ItemType<GunSword>(), 1), new Item(ItemID.GoldCoin, 5) };
            ButtonAmount = 30;

            MainContainer = new UIPanel();
            MainContainer.HAlign = 0.5f;
            MainContainer.VAlign = 0.5f;
            MainContainer.Width.Pixels = 900f;
            MainContainer.Height.Pixels = 600f;
            MainContainer.BorderColor = Color.Black;
            Append(MainContainer);

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


            SideContainerRight = new UIPanel();
            SideContainerRight.HAlign = 0.5f;
            SideContainerRight.VAlign = 0.5f;
            SideContainerRight.BackgroundColor = new Color(50f / 255f, 0f, 120f / 255f, 1f) * 0.4f;
            SideContainerRight.MarginRight = (-MainContainer.Width.Pixels / 10) * 3.1f;
            SideContainerRight.Width.Pixels = (MainContainer.Width.Pixels / 10) * 6.9f - 20f;
            SideContainerRight.Height.Pixels = MainContainer.Height.Pixels - 20f;
            SideContainerRight.BorderColor = Color.Black;
            MainContainer.Append(SideContainerRight);

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

            CompleteButton = new UIPanel(ModContent.Request<Texture2D>("TerrafirmaRedux/Systems/NPCQuests/QuestButtonPanel"), ModContent.Request<Texture2D>("TerrafirmaRedux/Systems/NPCQuests/QuestButtonPanel"), 20);
            CompleteButton.Width.Pixels = ButtonAmount > 12 ? 220f : 250f;
            CompleteButton.Height.Pixels = 40f;
            CompleteButton.HAlign = 1f;
            CompleteButton.VAlign = 1f;
            CompleteButton.BackgroundColor = new Color(0.4f, 0.5f, 1f, 1f);
            CompleteButton.BorderColor = new Color(0.4f, 0.5f, 1f);

            CompleteButtonText = new UIText("Complete", 1.1f, false);
            CompleteButtonText.Width.Pixels = 200f;
            CompleteButtonText.VAlign = 0.5f;
            CompleteButton.Append(CompleteButtonText);

            SimpleTooltip = new UIText("Tooltip", 1.05f);
            SimpleTooltip.MarginTop = Main.MouseScreen.Y + 20f;
            SimpleTooltip.MarginLeft = Main.MouseScreen.X + 20f;
            Append(SimpleTooltip);

            selectedQuest = new Quest("NoQuest");

        }

        //Deletes the UI
        public void Flush() { RemoveAllChildren(); }

        //Tick Update
        public override void Update(GameTime gameTime)
        {

            UpdateCompleteButton();
            UpdateQuestButton();

            ShowSimpleTooltip = false;

            SimpleTooltip.MarginTop = Main.MouseScreen.Y + 20f;
            SimpleTooltip.MarginLeft = Main.MouseScreen.X + 20f;

            if (DifficultyText != null && DifficultyText.IsMouseHovering)
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

            //for (int i = 0; i < QuestTypeImages.Length; i++)
            //{
            //    if (QuestTypeImage.IsMouseHovering)
            //    {
                    
            //        switch ((int)ButtonQuests[i].Type)
            //        {
            //            case 0: SimpleTooltip.SetText("Explorer"); break;
            //            case 1: SimpleTooltip.SetText("Scavenger"); break;
            //            case 2: SimpleTooltip.SetText("Collector"); break;
            //            case 3: SimpleTooltip.SetText("Slayer"); break;
            //            case 4: SimpleTooltip.SetText("Destroyer"); break;
            //            case 5: SimpleTooltip.SetText("Final"); break;
            //        }

            //        ShowSimpleTooltip = true;
            //    }
            //}

            if (ShowSimpleTooltip) SimpleTooltip.TextColor = Color.White;
            else SimpleTooltip.TextColor = new Color(0f, 0f, 0f, 0f);

            if (ButtonAmount > 12)
            {
                for (int i = 0; i < Buttons.Length; i++)
                {
                    Buttons[i].MarginTop = -VerticalSlider.GetValue() + ((Button.Height.Pixels + 5) * i);
                }
            }
           

            if (MainContainer.IsMouseHovering) Main.blockMouse = true;
            else Main.blockMouse = false;
        }

        //Checks for Button Clicks
        public override void LeftClick(UIMouseEvent evt)
        {
            // Status Button
            if (CompleteButton.IsMouseHovering)
            {

                SoundEngine.PlaySound(SoundID.MenuTick);
                selectedQuest.Completion = 1;
                //localquestlist.Quests[localquestlist.GetQuestIndex(selectedQuest)].SetInProgress(localquestlist);
                if (localquestlist.Quests[localquestlist.GetQuestIndex(selectedQuest)].Completion == 0)
                {
                    localquestlist.Quests[localquestlist.GetQuestIndex(selectedQuest)].Completion = 1;
                    for (int i = 0; i < localquestlist.Quests.Length; i++)
                    {
                        if (localquestlist.Quests[i].Completion == 1 && !localquestlist.Quests[i].IsEqualsTo(selectedQuest))
                        {
                            localquestlist.Quests[i].Completion = 0;
                            for (int j = 0; j < ButtonQuests.Length; j++)
                            {
                                if (!ButtonQuests[j].IsEqualsTo(localquestlist.Quests[localquestlist.GetQuestIndex(selectedQuest)])) ButtonQuests[j].Completion = 0;
                            }
                        }


                    }

                    for (int i = 0; i < localquestlist.Quests.Length; i++)
                    {
                        if (localquestlist.Quests[i].IsEqualsTo(localquestlist.Quests[localquestlist.GetQuestIndex(selectedQuest)])) localquestlist.Quests[i].Completion = 1;
                    }
                }
                else if (localquestlist.Quests[localquestlist.GetQuestIndex(selectedQuest)].Completion == 1 && localquestlist.Quests[localquestlist.GetQuestIndex(selectedQuest)].CanBeCompleted(localquestlist, Main.LocalPlayer))
                {
                    localquestlist.Quests[localquestlist.GetQuestIndex(selectedQuest)].Completion = 2;
                    localquestlist.Quests[localquestlist.GetQuestIndex(selectedQuest)].Complete();

                    for (int i = 0; i < ButtonQuests.Length; i++)
                    {
                        if (ButtonQuests[i].IsEqualsTo(localquestlist.Quests[localquestlist.GetQuestIndex(selectedQuest)])) ButtonQuests[i].Completion = 2;
                    }

                    SideContainerRight.RemoveChild(CompleteButton);
                }

                UpdateQuests();

            }


            // Quest Button
            for (int i = 0; i < Buttons.Length;i++)
            {
                if (Buttons[i].IsMouseHovering)
                {
                    
                    EmptyStarTexture = ModContent.Request<Texture2D>("TerrafirmaRedux/Systems/NPCQuests/EmptyStar").Value;
                    FullStarTexture = ModContent.Request<Texture2D>("TerrafirmaRedux/Systems/NPCQuests/FilledStar").Value;

                    SoundEngine.PlaySound(SoundID.MenuTick);

                    DifficultyRating = ButtonQuests[i].Difficulty;
                    selectedQuest = ButtonQuests[i];

                    if (SideContainerRight.HasChild(QuestName)) SideContainerRight.RemoveChild(QuestName);
                    QuestName = new UIText(ButtonQuests[i].Name, 0.8f, true);
                    QuestName.VAlign = 0f;
                    QuestName.HAlign = 0.5f;
                    QuestName.Height.Pixels = 30f;
                    QuestName.MarginTop = 10f;
                    SideContainerRight.Append(QuestName);

                    if (SideContainerRight.HasChild(QuestDialogue)) SideContainerRight.RemoveChild(QuestDialogue);
                    QuestDialogue = new UIText(ButtonQuests[i].Dialogue, 1f);
                    QuestDialogue.VAlign = 0f;
                    QuestDialogue.HAlign = 0f;
                    QuestDialogue.MarginTop = QuestName.Height.Pixels + 24f;
                    QuestDialogue.Height.Pixels = ((QuestDialogue.Text.Length / 45) + 1) * 20f;
                    QuestDialogue.Width.Pixels = SideContainerRight.Width.Pixels;
                    QuestDialogue.IsWrapped = true;
                    SideContainerRight.Append(QuestDialogue);

                    if (SideContainerRight.HasChild(QuestTaskTitle)) SideContainerRight.RemoveChild(QuestTaskTitle);
                    QuestTaskTitle = new UIText("Task", 1.2f);
                    QuestTaskTitle.VAlign = 0f;
                    QuestTaskTitle.HAlign = 0f;
                    QuestTaskTitle.Width.Pixels = 45f;
                    QuestTaskTitle.Height.Pixels = 25f;
                    QuestTaskTitle.IsWrapped = true;
                    QuestTaskTitle.MarginTop = QuestName.Height.Pixels + QuestDialogue.Height.Pixels + 25f;
                    SideContainerRight.Append(QuestTaskTitle);

                    if (SideContainerRight.HasChild(QuestTask)) SideContainerRight.RemoveChild(QuestTask);
                    QuestTask = new UIText(ButtonQuests[i].TaskDescription, 1f);
                    QuestTask.VAlign = 0f;
                    QuestTask.HAlign = 0f;
                    QuestTask.Width.Pixels = SideContainerRight.Width.Pixels;
                    QuestTask.Height.Pixels = ((QuestTask.Text.Length / 45) + 1) * 20f;
                    QuestTask.IsWrapped = true;
                    QuestTask.MarginTop = QuestName.Height.Pixels + QuestDialogue.Height.Pixels + 50f;
                    SideContainerRight.Append(QuestTask);

                    if (SideContainerRight.HasChild(DifficultyText)) SideContainerRight.RemoveChild(DifficultyText);
                    DifficultyText = new UIText("Difficulty", 1.2f);
                    DifficultyText.VAlign = 0f;
                    DifficultyText.HAlign = 0f;
                    DifficultyText.Width.Pixels = 90f;
                    DifficultyText.Height.Pixels = 25f;
                    DifficultyText.IsWrapped = true;
                    DifficultyText.MarginTop = QuestName.Height.Pixels + QuestDialogue.Height.Pixels + QuestTask.Height.Pixels + 60f;
                    SideContainerRight.Append(DifficultyText);

                    for (int j = 0; j < 5; j++)
                    {
                        if (DifficultyStarsList.Length >= j + 1 && SideContainerRight.HasChild(DifficultyStarsList[j])) SideContainerRight.RemoveChild(DifficultyStarsList[j]);
                    }
                    DifficultyStarsList = new UIImage[] { };
                        for (int j = 4; j >= 0; j--)
                    {
                        if (ButtonQuests[i].Difficulty >= j + 1) DifficultyStars = new UIImage(FullStarTexture);
                        else DifficultyStars = new UIImage(EmptyStarTexture);

                        DifficultyStars.VAlign = 0f;
                        DifficultyStars.HAlign = 0f;
                        DifficultyStars.Width.Pixels = 10f;
                        DifficultyStars.Height.Pixels = 10f;
                        DifficultyStars.Color = new Color(1f, 1f - (j / 7f), 1f - (j / 9f), 1f);
                        DifficultyStars.MarginTop = QuestName.Height.Pixels + QuestDialogue.Height.Pixels + QuestTask.Height.Pixels + DifficultyText.Height.Pixels + 75f;
                        DifficultyStars.MarginLeft = j * 20f;
                        DifficultyStarsList = DifficultyStarsList.Append(DifficultyStars).ToArray();
                        SideContainerRight.Append(DifficultyStars);
                        
                    }

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
                        RewardText.MarginTop = QuestName.Height.Pixels + QuestDialogue.Height.Pixels + QuestTask.Height.Pixels + 60f;
                        SideContainerRight.Append(RewardText);


                    }

                    for (int j = 0; j < 100; j++)
                    {
                        if (RewardsItemList.Length >= j + 1 && SideContainerRight.HasChild(RewardsItemList[j])) SideContainerRight.RemoveChild(RewardsItemList[j]);
                        else break;
                    }
                    RewardsItemList = new UIItemSlot[] { };
                    for (int j = 0; j < ButtonQuests[i].Rewards.Length; j++)
                    {
                            
                        if (RewardsItemList.Length >= j + 1 && SideContainerRight.HasChild(RewardsItemList[j])) SideContainerRight.RemoveChild(RewardsItemList[j]);

                        RewardItem = new UIItemSlot(ButtonQuests[i].Rewards, j, 22);
                        RewardItem.VAlign = 0f;
                        RewardItem.HAlign = 0.5f;
                        RewardItem.MarginTop = QuestName.Height.Pixels + QuestDialogue.Height.Pixels + QuestTask.Height.Pixels + DifficultyText.Height.Pixels + 85f + (((float)Math.Floor((double)j / 6)) * 55);
                        RewardItem.MarginLeft = -30f + ((j % 6) * 110f);
                        RewardItem.Width.Pixels = 10f;
                        RewardItem.Height.Pixels = 10f;
                        SideContainerRight.Append(RewardItem);
                        RewardsItemList = RewardsItemList.Append(RewardItem).ToArray();
                        Main.inventoryScale = 1f; 
                        
                    }
                    break;
                }
            }
        }

        //Updates the list of quests available in the Quest UI
        public void UpdateQuests()
        {
            if (localquestlist.Quests.Length > 0)
            {
                

                for (int i = 0; i < Buttons.Length; i++)
                {
                    RemoveChild(Buttons[i]);
                    RemoveChild(ButtonTexts[i]);
                }

                RemoveChild(VerticalSlider);

                Buttons = new UIPanel[] { };
                ButtonTexts = new UIText[] { };
                QuestTypeImages = new UIImage[] { };
                ButtonQuests = new Quest[] {};
                int ButtonIndex = 0;

                ButtonAmount = localquestlist.Quests.Length;

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

                //OnGoingQuests
                for (int i = 0; i < localquestlist.OnGoingQuests().Quests.Length; i++)
                {
                    bool allconditionsmatch = true;
                    if (localquestlist.OnGoingQuests().Quests[i].conditions != null)
                    {
                        for (int k = 0; k < localquestlist.OnGoingQuests().Quests[i].conditions.Length; k++)
                        {
                            if (!localquestlist.OnGoingQuests().Quests[i].conditions[k].IsMet()) { allconditionsmatch = false; break; }
                        }
                    }
                    if (allconditionsmatch)
                    {
                        

                        Button = new UIPanel(ModContent.Request<Texture2D>("TerrafirmaRedux/Systems/NPCQuests/QuestButtonPanel"), ModContent.Request<Texture2D>("TerrafirmaRedux/Systems/NPCQuests/QuestButtonPanel"), 20);
                        Button.Width.Pixels = ButtonAmount > 12 ? 220f : 250f;
                        Button.Height.Pixels = 40f;
                        Button.HAlign = 0f;
                        Button.BackgroundColor = new Color(0.4f, 0.5f, 1f, 1f);
                        Button.BorderColor = new Color(0.4f, 0.5f, 1f);
                        Button.MarginTop = ((Button.Height.Pixels + 5) * ButtonIndex);
                        ButtonIndex++;

                        Buttons = Buttons.Append(Button).ToArray();

                        string ButtonTextText = localquestlist.OnGoingQuests().Quests[i].Name;
                        ButtonText = new UIText(ButtonTextText, Math.Clamp(1f / (ButtonTextText.Length / 20f), 0.5f, 1.1f));
                        ButtonText.Width.Pixels = ButtonAmount > 12 ? 220f : 250f;
                        ButtonText.MarginLeft = ButtonText.Text.Length > 15 ? ButtonText.Text.Length / 2 : 0;
                        ButtonText.VAlign = 0.5f;
                        Button.Append(ButtonText);

                        ButtonTexts = ButtonTexts.Append(ButtonText).ToArray();

                        QuestTypeImage = new UIImage(QuestTypeIcons[(int)localquestlist.OnGoingQuests().Quests[i].Type]);
                        QuestTypeImage.Width.Pixels = 32f;
                        QuestTypeImage.Height.Pixels = 32f;
                        QuestTypeImage.HAlign = 0f;
                        QuestTypeImage.VAlign = 0f;
                        QuestTypeImage.MarginTop = -QuestTypeImage.Height.Pixels / 2;
                        QuestTypeImage.MarginLeft = -QuestTypeImage.Width.Pixels / 2;
                        Button.Append(QuestTypeImage);

                        QuestTypeImages = QuestTypeImages.Append(QuestTypeImage).ToArray();

                        ButtonQuests = ButtonQuests.Append(localquestlist.OnGoingQuests().Quests[i]).ToArray();
                        
                    }
                }

                //UncompletedQuests
                for (int i = 0; i < localquestlist.UncompletedQuests().Quests.Length; i++)
                {
                    bool allconditionsmatch = true;
                    if (localquestlist.UncompletedQuests().Quests[i].conditions != null)
                    {
                        for (int k = 0; k < localquestlist.UncompletedQuests().Quests[i].conditions.Length; k++)
                        {
                            if (!localquestlist.UncompletedQuests().Quests[i].conditions[k].IsMet()) { allconditionsmatch = false; break; }
                        }
                    }
                    if (allconditionsmatch)
                    {


                        Button = new UIPanel(ModContent.Request<Texture2D>("TerrafirmaRedux/Systems/NPCQuests/QuestButtonPanel"), ModContent.Request<Texture2D>("TerrafirmaRedux/Systems/NPCQuests/QuestButtonPanel"), 20);
                        Button.Width.Pixels = ButtonAmount > 12 ? 220f : 250f;
                        Button.Height.Pixels = 40f;
                        Button.HAlign = 0f;
                        Button.BackgroundColor = new Color(0.4f, 0.5f, 1f, 1f);
                        Button.BorderColor = new Color(0.4f, 0.5f, 1f);
                        Button.MarginTop = ((Button.Height.Pixels + 5) * ButtonIndex);
                        ButtonIndex++;

                        Buttons = Buttons.Append(Button).ToArray();

                        string ButtonTextText = localquestlist.UncompletedQuests().Quests[i].Name;
                        ButtonText = new UIText(ButtonTextText, Math.Clamp(1f / (ButtonTextText.Length / 20f), 0.5f, 1.1f));
                        ButtonText.Width.Pixels = ButtonAmount > 12 ? 220f : 250f;
                        ButtonText.MarginLeft = ButtonText.Text.Length > 15 ? ButtonText.Text.Length / 2 : 0;
                        ButtonText.VAlign = 0.5f;
                        Button.Append(ButtonText);

                        ButtonTexts = ButtonTexts.Append(ButtonText).ToArray();

                        QuestTypeImage = new UIImage(QuestTypeIcons[(int)localquestlist.UncompletedQuests().Quests[i].Type]);
                        QuestTypeImage.Width.Pixels = 32f;
                        QuestTypeImage.Height.Pixels = 32f;
                        QuestTypeImage.HAlign = 0f;
                        QuestTypeImage.VAlign = 0f;
                        QuestTypeImage.MarginTop = -QuestTypeImage.Height.Pixels / 2;
                        QuestTypeImage.MarginLeft = -QuestTypeImage.Width.Pixels / 2;
                        Button.Append(QuestTypeImage);

                        QuestTypeImages = QuestTypeImages.Append(QuestTypeImage).ToArray();

                        ButtonQuests = ButtonQuests.Append(localquestlist.UncompletedQuests().Quests[i]).ToArray();
                    }
                }

                //FinishedQuests
                for (int i = 0; i < localquestlist.FinishedQuests().Quests.Length; i++)
                {
                    bool allconditionsmatch = true;
                    if (localquestlist.FinishedQuests().Quests[i].conditions != null)
                    {
                        for (int k = 0; k < localquestlist.FinishedQuests().Quests[i].conditions.Length; k++)
                        {
                            if (!localquestlist.FinishedQuests().Quests[i].conditions[k].IsMet()) { allconditionsmatch = false; break; }
                        }
                    }
                    if (allconditionsmatch)
                    {


                        Button = new UIPanel(ModContent.Request<Texture2D>("TerrafirmaRedux/Systems/NPCQuests/QuestButtonPanel"), ModContent.Request<Texture2D>("TerrafirmaRedux/Systems/NPCQuests/QuestButtonPanel"), 20);
                        Button.Width.Pixels = ButtonAmount > 12 ? 220f : 250f;
                        Button.Height.Pixels = 40f;
                        Button.HAlign = 0f;
                        Button.BackgroundColor = new Color(0.4f, 0.5f, 1f, 1f);
                        Button.BorderColor = new Color(0.4f, 0.5f, 1f);
                        Button.MarginTop = ((Button.Height.Pixels + 5) * ButtonIndex);
                        ButtonIndex++;

                        Buttons = Buttons.Append(Button).ToArray();

                        string ButtonTextText = localquestlist.FinishedQuests().Quests[i].Name;
                        ButtonText = new UIText(ButtonTextText, Math.Clamp(1f / (ButtonTextText.Length / 20f), 0.5f, 1.1f));
                        ButtonText.Width.Pixels = ButtonAmount > 12 ? 220f : 250f;
                        ButtonText.MarginLeft = ButtonText.Text.Length > 15 ? ButtonText.Text.Length / 2 : 0;
                        ButtonText.VAlign = 0.5f;
                        Button.Append(ButtonText);

                        ButtonTexts = ButtonTexts.Append(ButtonText).ToArray();

                        QuestTypeImage = new UIImage(QuestTypeIcons[(int)localquestlist.FinishedQuests().Quests[i].Type]);
                        QuestTypeImage.Width.Pixels = 32f;
                        QuestTypeImage.Height.Pixels = 32f;
                        QuestTypeImage.HAlign = 0f;
                        QuestTypeImage.VAlign = 0f;
                        QuestTypeImage.MarginTop = -QuestTypeImage.Height.Pixels / 2;
                        QuestTypeImage.MarginLeft = -QuestTypeImage.Width.Pixels / 2;
                        Button.Append(QuestTypeImage);

                        QuestTypeImages = QuestTypeImages.Append(QuestTypeImage).ToArray();

                        ButtonQuests = ButtonQuests.Append(localquestlist.FinishedQuests().Quests[i]).ToArray();
                    }
                }

                for (int i = 0; i < Buttons.Length; i++)
                {
                    SideContainerLeft.Append(Buttons[i]);
                    Buttons[i].Append(QuestTypeImages[i]);
                }
            }
        }

        //Updates the Status Button in the bottom right corner to display the right info based on quest status
        public void UpdateCompleteButton()
        {
            localquestlist = Main.LocalPlayer.GetModPlayer<TerrafirmaGlobalPlayer>().playerquests;
            if (localquestlist.Quests.Contains(selectedQuest) && !selectedQuest.IsEqualsTo(new Quest("NoQuest")))
            {
                if (localquestlist.Quests[localquestlist.GetQuestIndex(selectedQuest)].Completion == 2)
                {
                    if (SideContainerRight.HasChild(CompleteButton)) SideContainerRight.RemoveChild(CompleteButton);
                }
                else if (localquestlist.Quests[localquestlist.GetQuestIndex(selectedQuest)].Completion == 1)
                {
                    if (localquestlist.Quests[localquestlist.GetQuestIndex(selectedQuest)].CanBeCompleted(localquestlist, Main.LocalPlayer))
                    {
                        CompleteButtonText.SetText("Claim Reward");
                        if (!SideContainerRight.HasChild(CompleteButton)) SideContainerRight.Append(CompleteButton);
                    }
                    else
                    {
                        CompleteButtonText.SetText("In Progress");
                        if (!SideContainerRight.HasChild(CompleteButton)) SideContainerRight.Append(CompleteButton);
                    }
                }
                else if (localquestlist.Quests[localquestlist.GetQuestIndex(selectedQuest)].Completion == 0)
                {
                    CompleteButtonText.SetText("Start Quest");
                    if (!SideContainerRight.HasChild(CompleteButton)) SideContainerRight.Append(CompleteButton);
                }
            }
        }

        //Updates the Quest Buttons on the left panel to display the right color based on the Quest's status
        public void UpdateQuestButton()
        {
            for (int i = 0; i < ButtonQuests.Length; i++)
            {
                switch (ButtonQuests[i].Completion)
                {
                    case 0:
                        if (Buttons[i].IsMouseHovering)
                        {
                            Buttons[i].BackgroundColor = new Color(1f, 0.8f, 0.1f, 1f);
                            Buttons[i].BorderColor = new Color(1f, 0.8f, 0.1f);
                        }
                        else
                        {
                            Buttons[i].BackgroundColor = new Color(0.4f, 0.5f, 1f, 1f);
                            Buttons[i].BorderColor = new Color(0.4f, 0.5f, 1f);
                        }
                        break;
                    case 1:
                        if (ButtonQuests[i].CanBeCompleted(localquestlist, Main.LocalPlayer))
                        {
                            if (Buttons[i].IsMouseHovering)
                            {
                                Buttons[i].BackgroundColor = new Color(1f, 0.8f, 0.1f, 1f);
                                Buttons[i].BorderColor = new Color(1f, 0.8f, 0.1f);
                            }
                            else
                            {
                                Buttons[i].BackgroundColor = new Color(1f, Main.masterColor, 0f);
                                Buttons[i].BorderColor = new Color(1f, Main.masterColor, 0f);
                            }
                        }
                        else
                        {
                            if (Buttons[i].IsMouseHovering)
                            {
                                Buttons[i].BackgroundColor = new Color(1f, 0.8f, 0.1f, 1f);
                                Buttons[i].BorderColor = new Color(1f, 0.8f, 0.1f);
                            }
                            else
                            {
                                Buttons[i].BackgroundColor = new Color(255, 150, 255);
                                Buttons[i].BorderColor = new Color(255, 150, 255);
                            }
                        }
                        break;
                    case 2:
                        if (Buttons[i].IsMouseHovering)
                        {
                            Buttons[i].BackgroundColor = new Color(1f, 0.8f, 0.1f, 1f);
                            Buttons[i].BorderColor = new Color(1f, 0.8f, 0.1f);
                        }
                        else
                        {
                            Buttons[i].BackgroundColor = new Color(0.4f, 0.5f, 1f, 1f) * 0.2f;
                            Buttons[i].BorderColor = new Color(0.4f, 0.5f, 1f) * 0.2f;
                        }
                        break;
                }
            }
        }

    }
}
