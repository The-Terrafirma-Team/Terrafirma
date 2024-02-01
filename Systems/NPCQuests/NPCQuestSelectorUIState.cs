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
        Quest[] Buttonquests = new Quest[]{};
        UIText[] ButtonTexts = new UIText[]{};

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

        QuestList localquestlist;
        public void Create()
        {
            localquestlist = Main.LocalPlayer.GetModPlayer<TerrafirmaGlobalPlayer>().playerquests;
            EmptyStarTexture = ModContent.Request<Texture2D>("TerrafirmaRedux/Systems/NPCQuests/EmptyStar").Value;
            FullStarTexture = ModContent.Request<Texture2D>("TerrafirmaRedux/Systems/NPCQuests/FilledStar").Value;

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
            SideContainerRight.Append(CompleteButton);

            CompleteButtonText = new UIText("Complete", 1.1f, false);
            CompleteButtonText.Width.Pixels = 200f;
            CompleteButtonText.VAlign = 0.5f;
            CompleteButton.Append(CompleteButtonText);

            SimpleTooltip = new UIText("Tooltip", 1.05f);
            SimpleTooltip.MarginTop = Main.MouseScreen.Y + 20f;
            SimpleTooltip.MarginLeft = Main.MouseScreen.X + 20f;
            Append(SimpleTooltip);

        }

        public void Flush() { RemoveAllChildren(); }

        public override void Update(GameTime gameTime)
        {

            SimpleTooltip.MarginTop = Main.MouseScreen.Y + 20f;
            SimpleTooltip.MarginLeft = Main.MouseScreen.X + 20f;

            if (DifficultyText != null && DifficultyText.IsMouseHovering) 
            {
                SimpleTooltip.SetText("");
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
            else 
            { 
                SimpleTooltip.SetText("Difficulty"); 
                ShowSimpleTooltip = false; 
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

            for (int i = 0; i < Buttons.Length; i++)
            {
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

            localquestlist = Main.LocalPlayer.GetModPlayer<TerrafirmaGlobalPlayer>().playerquests;
            if (localquestlist.Quests.Contains(selectedQuest))
            {
                if (localquestlist.Quests[localquestlist.GetQuestIndex(selectedQuest)].Completion == 1)
                {
                    if (localquestlist.Quests[localquestlist.GetQuestIndex(selectedQuest)].CanBeCompleted(localquestlist, Main.LocalPlayer))
                    {
                        CompleteButtonText.SetText("Complete");
                    }
                    else
                    {
                        CompleteButtonText.SetText("In Progress");
                    }
                }
                else if (localquestlist.Quests[localquestlist.GetQuestIndex(selectedQuest)].Completion == 0) CompleteButtonText.SetText("Start Quest");
            }

            if (MainContainer.IsMouseHovering) Main.blockMouse = true;
            else Main.blockMouse = false;
        }
        public override void LeftClick(UIMouseEvent evt)
        {

            if (CompleteButton.IsMouseHovering)
            {

                SoundEngine.PlaySound(SoundID.MenuTick);
                selectedQuest.Completion = 1;
                //localquestlist.Quests[localquestlist.GetQuestIndex(selectedQuest)].SetInProgress(localquestlist);
                localquestlist.Quests[localquestlist.GetQuestIndex(selectedQuest)].Completion = 1;

            }

            for (int i = 0; i < Buttons.Length;i++)
            {
                if (Buttons[i].IsMouseHovering)
                {

                    SoundEngine.PlaySound(SoundID.MenuTick);

                    DifficultyRating = Buttonquests[i].Difficulty;
                    selectedQuest = localquestlist.Quests[i];

                    if (SideContainerRight.HasChild(QuestName)) SideContainerRight.RemoveChild(QuestName);
                    QuestName = new UIText(Buttonquests[i].Name, 0.8f, true);
                    QuestName.VAlign = 0f;
                    QuestName.HAlign = 0.5f;
                    QuestName.Height.Pixels = 30f;
                    QuestName.MarginTop = 10f;
                    SideContainerRight.Append(QuestName);

                    if (SideContainerRight.HasChild(QuestDialogue)) SideContainerRight.RemoveChild(QuestDialogue);
                    QuestDialogue = new UIText(Buttonquests[i].Dialogue, 1f);
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
                    QuestTask = new UIText(Buttonquests[i].TaskDescription, 1f);
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
                        if (Buttonquests[i].Difficulty >= j + 1) DifficultyStars = new UIImage(FullStarTexture);
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

                    if (Buttonquests[i].Rewards != null)
                    {
                        if (SideContainerRight.HasChild(RewardText)) SideContainerRight.RemoveChild(RewardText);
                        RewardText = new UIText("Reward", 1.2f);

                        if (Buttonquests[i].Rewards.Length <= 1) RewardText.SetText("Reward");
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
                    for (int j = 0; j < Buttonquests[i].Rewards.Length; j++)
                    {
                            
                        if (RewardsItemList.Length >= j + 1 && SideContainerRight.HasChild(RewardsItemList[j])) SideContainerRight.RemoveChild(RewardsItemList[j]);
                        
                        Item[] rewardlist = new Item[] { };
                        for (int k = 0; k < Buttonquests[i].Rewards.Length; k++)
                        {
                            rewardlist = rewardlist.Append(new Item(Buttonquests[i].Rewards[k])).ToArray();
                        }
                        
                        RewardItem = new UIItemSlot(rewardlist, j, 22);
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
                Buttonquests = new Quest[] { };

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

                for (int i = 0; i < localquestlist.Quests.Length; i++)
                {
                    bool allconditionsmatch = true;
                    QuestList playerquests = localquestlist;
                    Buttonquests = Buttonquests.Append(playerquests.Quests[i]).ToArray();
                    if (Buttonquests[i].conditions != null)
                    {
                        for (int k = 0; k < Buttonquests[i].conditions.Length; k++)
                        {
                            if (!Buttonquests[i].conditions[k].IsMet()) { allconditionsmatch = false; break; }
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
                        Button.MarginTop = ((Button.Height.Pixels + 5) * i);
                        SideContainerLeft.Append(Button);
                        Buttons = Buttons.Append(Button).ToArray();

                        string ButtonTextText = playerquests.Quests[i].Name;
                        ButtonText = new UIText(ButtonTextText, Math.Clamp(1f / (ButtonTextText.Length / 20f), 0.5f, 1.1f));
                        ButtonText.Width.Pixels = ButtonAmount > 12 ? 220f : 250f;
                        ButtonText.MarginLeft = ButtonText.Text.Length > 15 ? ButtonText.Text.Length / 2 : 0;
                        ButtonText.VAlign = 0.5f;
                        Button.Append(ButtonText);

                        ButtonTexts = ButtonTexts.Append(ButtonText).ToArray();
                    }
                }
            }
        }

    }
}
