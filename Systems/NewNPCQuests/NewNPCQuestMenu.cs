using Humanizer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Common.Players;
using Terrafirma.Systems.NPCQuests.Quests;
using Terrafirma.Systems.UIElements;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.UI;
using Terraria.UI;

namespace Terrafirma.Systems.NewNPCQuests
{
    internal class NewNPCQuestMenu : UIState
    {
        //Base Vars
        Quest SelectedQuest = null;
        Quest[] AvailableQuests = null;
        public int talknpc = NPCID.Guide;

        UIText UITitle;

        //Scroll Panel
        UIPanel ScrollPanel;
        UIScrollbar ScrollPanel_ScrollBar;

        UIPanel InfoPanel;
        UIScrollbar InfoPanel_ScrollBar = new UIScrollbar();

        UIButton_Terrafirma[] QuestButtonList = new UIButton_Terrafirma[] {};

        //Info Panel
        UIText QuestName = new UIText("");
        UIText QuestDialogue = new UIText("");
        UIText QuestTask = new UIText("");

        UIText QuestDifficultyTitle = new UIText("");
        UIImage[] QuestDifficultyStars = new UIImage[]{};
        UIText QuestRewardsTitle = new UIText("");
        UIItemSlot_Terrafirma[] QuestRewardSlots = new UIItemSlot_Terrafirma[]{};

        UIButton_Terrafirma QuestStartButton = new UIButton_Terrafirma();

        //Popup Warning Panel
        UIPanel WarningPanel;
        UIText WarningPanelText;
        UIButton_Terrafirma YesButton = new UIButton_Terrafirma();
        UIButton_Terrafirma NoButton = new UIButton_Terrafirma();

        //OnActivate
        public override void OnActivate()
        {
            base.OnActivate();
        }

        //Create
        public void Create()
        {
            Flush();
            SelectedQuest = null;

            //Create Base UI Elements

            ScrollPanel = new UIPanel();
            ScrollPanel.HAlign = ScrollPanel.VAlign = 0.5f;
            ScrollPanel.Width.Pixels = 250;
            ScrollPanel.Height.Pixels = 500;
            ScrollPanel.Left.Pixels = -(ScrollPanel.Width.Pixels / 2) - 100;
            ScrollPanel.OverflowHidden = true;
            Append(ScrollPanel);

            ScrollPanel_ScrollBar = new UIScrollbar();       
            ScrollPanel_ScrollBar.HAlign = 1f;
            ScrollPanel_ScrollBar.VAlign = 0.5f;
            ScrollPanel_ScrollBar.Width.Pixels = 50;
            ScrollPanel_ScrollBar.Height.Pixels = ScrollPanel.Height.Pixels - 40;
            ScrollPanel_ScrollBar.SetView(10, 15);
            ScrollPanel.Append(ScrollPanel_ScrollBar);

            InfoPanel = new UIPanel();
            InfoPanel.HAlign = InfoPanel.VAlign = 0.5f;
            InfoPanel.Width.Pixels = 450;
            InfoPanel.Height.Pixels = 500;
            InfoPanel.Left.Pixels = (InfoPanel.Width.Pixels / 2) - 100;
            InfoPanel.OverflowHidden = true;
            Append(InfoPanel);

            UITitle = new UIText("Quests", 1f, true);
            UITitle.HAlign = UITitle.VAlign = 0.5f;
            UITitle.Top.Pixels = -290;
            Append(UITitle);

            WarningPanel = new UIPanel();
            WarningPanel.HAlign = 10f;
            WarningPanel.VAlign = 0.5f;
            WarningPanel.Width.Pixels = 400;
            WarningPanel.Height.Pixels = 150;
            WarningPanel.BackgroundColor = new Color(40, 40, 120) * 0.9f;
            Append(WarningPanel);

            WarningPanelText = new UIText("Do you want to start this quest? \nDoing this will reset the currently active quest.");
            WarningPanelText.VAlign = 0f;
            WarningPanelText.HAlign = 0.5f;
            WarningPanelText.Width.Pixels = 400;
            WarningPanel.Append(WarningPanelText);

            YesButton = new UIButton_Terrafirma();
            YesButton.Text = "Yes";
            YesButton.Text_HAlign = 1;
            YesButton.HAlign = 0.2f;
            YesButton.VAlign = 1f;
            YesButton.Width.Pixels = 100;
            YesButton.Height.Pixels = 50;
            YesButton.BackgroundColor = new Color(90, 150, 210);
            YesButton.BackgroundColor_Pressed = Main.OurFavoriteColor;
            YesButton.BorderColor_Pressed = Main.OurFavoriteColor;
            WarningPanel.Append(YesButton);

            NoButton = new UIButton_Terrafirma();
            NoButton.Text = "No";
            NoButton.Text_HAlign = 1;
            NoButton.HAlign = 0.8f;
            NoButton.VAlign = 1f;
            NoButton.Width.Pixels = 100;
            NoButton.Height.Pixels = 50;
            NoButton.BackgroundColor = new Color(90, 150, 210);
            NoButton.BackgroundColor_Pressed = Main.OurFavoriteColor;
            NoButton.BorderColor_Pressed = Main.OurFavoriteColor;
            WarningPanel.Append(NoButton);

            UpdateQuestButtons();
            UpdateInfoScroll();
        }

        //Flush
        public void Flush() { RemoveAllChildren(); }

        //Update Quests
        public void UpdateQuests()
        {           
            AvailableQuests = new Quest[] { };
            AvailableQuests = Main.LocalPlayer.GetModPlayer<NewQuestModNPC>().playerquests.GetSpecificToNPCQuestArray(talknpc);
        }

        //Update Scroll List
        public void UpdateScrollList()
        {
            if (QuestButtonList.Length < 8) 
            {
                ScrollPanel_ScrollBar.HAlign = 20f;
                for (int i = 0; i < QuestButtonList.Length; i++) QuestButtonList[i].Width.Pixels = 226;
            }
            else
            {
                ScrollPanel_ScrollBar.HAlign = 1f;
                ScrollPanel_ScrollBar.SetView(8, QuestButtonList.Length);
                for (int i = 0; i < QuestButtonList.Length; i++) QuestButtonList[i].Width.Pixels = 200;
            }
        }

        //Update Info Scroll
        public void UpdateInfoScroll()
        {
            DynamicSpriteFont font = FontAssets.MouseText.Value;
            Vector2 InfoPanelSize = new Vector2(InfoPanel.Width.Pixels - 20, InfoPanel.Height.Pixels - 20);
            Vector2 TextSize =
                font.MeasureString(font.CreateWrappedText(QuestName.Text, InfoPanelSize.X)) * 1.2f +
                new Vector2(0, 10) +
                font.MeasureString(font.CreateWrappedText(QuestDialogue.Text, InfoPanelSize.X)) +
                new Vector2(0, 10) +
                font.MeasureString(font.CreateWrappedText(QuestTask.Text, InfoPanelSize.X)) +
                new Vector2(0, 10) +
                font.MeasureString(font.CreateWrappedText(QuestDifficultyTitle.Text, InfoPanelSize.X)) * 1.2f +
                new Vector2(0, 44 * (int)Math.Ceiling((decimal)QuestRewardSlots.Length / 5)) +
                new Vector2(0, 10) +
                new Vector2(0,QuestStartButton.Height.Pixels) +
                new Vector2(0, 55);

            if (TextSize.Y > InfoPanelSize.Y)
            {
                InfoPanel_ScrollBar.HAlign = 1f;
                InfoPanel_ScrollBar.SetView(InfoPanelSize.Y, TextSize.Y);
                if (font.MeasureString(font.CreateWrappedText(QuestDialogue.Text, InfoPanelSize.X)).X > InfoPanelSize.X - 50) QuestDialogue.Width.Pixels = InfoPanelSize.X - 30;
                if (font.MeasureString(font.CreateWrappedText(QuestTask.Text, InfoPanelSize.X)).X > InfoPanelSize.X - 50) QuestTask.Width.Pixels = InfoPanelSize.X - 30;
            }
            else
            {
                InfoPanel_ScrollBar.HAlign = 20f;
            }
        }

        //Update Quest Buttons
        public void UpdateQuestButtons()
        {
            UpdateQuests();

            for (int i = 0; i < QuestButtonList.Length; i ++)
            {
                RemoveChild(QuestButtonList[i]);
                QuestButtonList[i].Remove();
            }

            QuestButtonList = new UIButton_Terrafirma[] { };

            for (int k = 0; k < 3; k++)
            {
                Quest[] Checklist = new Quest[] { };
                switch (k)
                {
                    case 0: Checklist = AvailableQuests.OnGoingQuests(); break;
                    case 1: Checklist = AvailableQuests.UncompletedQuests(); break;
                    case 2: Checklist = AvailableQuests.FinishedQuests(); break;
                }
                for (int i = 0; i < Checklist.Length; i++)
                {

                    if (Checklist[i].QuestActivation())
                    {
                        UIButton_Terrafirma QuestButton = new UIButton_Terrafirma();
                        QuestButton.Text = Checklist[i].GetQuestName();
                        QuestButton.Width.Pixels = 200;
                        QuestButton.Height.Pixels = 50;
                        QuestButton.Top.Pixels = (QuestButton.Height.Pixels + 5) * i;

                        switch (k)
                        {
                            case 0:
                                QuestButton.BackgroundColor = new Color(255, 140, 220);
                                break;
                            case 1:
                                QuestButton.BackgroundColor = new Color(90, 150, 210);
                                break;
                            case 2:
                                QuestButton.BackgroundColor = new Color(90, 150, 210, 1) * 0.2f;
                                QuestButton.TextOpacity = 0.6f;
                                break;
                        }
                        QuestButton.BorderColor = Color.Black;
                        QuestButton.BackgroundColor_Pressed = Main.OurFavoriteColor;
                        QuestButton.BorderColor_Pressed = Main.OurFavoriteColor;
                        QuestButton.Initialize();

                        QuestButton.ButtonData = Checklist[i];
                        QuestButtonList = QuestButtonList.Append(QuestButton).ToArray();
                        ScrollPanel.Append(QuestButton);
                    }

                }
                UpdateScrollList();
            }
        }

        //Update Quest Info
        public void UpdateQuestInfo(Quest quest)
        {
            InfoPanel.RemoveAllChildren();
            QuestDifficultyStars = new UIImage[] { };
            QuestRewardSlots = new UIItemSlot_Terrafirma[] { };

            DynamicSpriteFont font = FontAssets.MouseText.Value;
            Vector2 InfoPanelSize = new Vector2(InfoPanel.Width.Pixels - 20, InfoPanel.Height.Pixels - 50);

            //Quest Name
            QuestName = new UIText(quest.GetQuestName(), 0.7f, true);
            QuestName.HAlign = 0.5f;
            QuestName.Top.Pixels = 5;
            InfoPanel.Append(QuestName);
            Vector2 QuestNameSize = font.MeasureString(font.CreateWrappedText(QuestName.Text, InfoPanelSize.X)) * 1.2f;

            //Quest Dialogue
            QuestDialogue = new UIText(quest.GetQuestDialogue().ReplaceLineEndings(""), 1f);
            QuestDialogue.IsWrapped = true;
            QuestDialogue.HAlign = 0f;      
            QuestDialogue.Width.Pixels = FontAssets.MouseText.Value.MeasureString(QuestDialogue.Text).X > InfoPanel.Width.Pixels ? InfoPanel.Width.Pixels : FontAssets.MouseText.Value.MeasureString(QuestDialogue.Text).X;
            QuestDialogue.Top.Pixels = QuestNameSize.Y + 10;
            InfoPanel.Append(QuestDialogue);
            Vector2 QuestDialogueSize = font.MeasureString(font.CreateWrappedText(QuestDialogue.Text, InfoPanelSize.X));

            //Quest Task
            QuestTask = new UIText(quest.GetQuestDescription().ReplaceLineEndings(""), 1f);
            QuestTask.IsWrapped = true;
            QuestTask.HAlign = 0f;
            QuestTask.Width.Pixels = FontAssets.MouseText.Value.MeasureString(QuestTask.Text).X > InfoPanel.Width.Pixels ? InfoPanel.Width.Pixels : FontAssets.MouseText.Value.MeasureString(QuestTask.Text).X;
            QuestTask.Top.Pixels = QuestNameSize.Y + 10 + QuestDialogueSize.Y + 10;
            InfoPanel.Append(QuestTask);
            Vector2 QuestTaskSize = font.MeasureString(font.CreateWrappedText(QuestTask.Text, InfoPanelSize.X));

            //Quest Difficulty Title
            QuestDifficultyTitle = new UIText("Difficulty", 1.2f);
            QuestDifficultyTitle.HAlign = 0f;
            QuestDifficultyTitle.Width.Pixels = 50;
            QuestDifficultyTitle.Top.Pixels = QuestNameSize.Y + 10 + QuestDialogueSize.Y + 10 + QuestTaskSize.Y + 10;
            Vector2 QuestDifficultyTitleSize = font.MeasureString(font.CreateWrappedText(QuestDifficultyTitle.Text, InfoPanelSize.X));
            InfoPanel.Append(QuestDifficultyTitle);

            //Difficulty Stars
            Asset<Texture2D> EmptyStar = Terrafirma.QuestDifficultyStarEmpty;
            Asset<Texture2D> FullStar = Terrafirma.QuestDifficultyStarFull;
            for (int i = 0; i < 6; i++)
            {
                float StarColorInterp = ((6 - i) / 6f) * 1.3f;
                UIImage DifficultyStar;
                if (i <= SelectedQuest.Difficulty)
                {
                    DifficultyStar = new UIImage(FullStar);
                    DifficultyStar.Color = new Color(1f * StarColorInterp * 4f, 1f * StarColorInterp, 1f * StarColorInterp);
                }
                else
                {
                    DifficultyStar = new UIImage(EmptyStar);
                }               
                DifficultyStar.Width.Pixels = 26;
                DifficultyStar.Height.Pixels = 26;
                DifficultyStar.Top.Pixels = QuestNameSize.Y + 10 + QuestDialogueSize.Y + 10 + QuestTaskSize.Y + 10 + QuestDifficultyTitleSize.Y + 10;
                DifficultyStar.Left.Pixels = 18 * i;
                QuestDifficultyStars = QuestDifficultyStars.Append(DifficultyStar).ToArray();
                InfoPanel.Append(DifficultyStar);

            }

            //Quest Reward Title
            QuestRewardsTitle = new UIText("Rewards", 1.2f);
            QuestRewardsTitle.HAlign = 0.5f;
            QuestRewardsTitle.Width.Pixels = 50;
            QuestRewardsTitle.Top.Pixels = QuestNameSize.Y + 10 + QuestDialogueSize.Y + 10 + QuestTaskSize.Y + 10;
            InfoPanel.Append(QuestRewardsTitle);

            //Quest Reward Slots
            for (int i = 0; i < SelectedQuest.Rewards.Length; i++)
            {
                int LeftPlacement = (44 * i) - ( 44 * 5 * (int)Math.Floor((decimal)i / 5));
                int TopPlacement = (int)Math.Floor((decimal)i / 5) * 44;
                Main.inventoryScale = 0.8f;
                UIItemSlot_Terrafirma RewardSlot = new UIItemSlot_Terrafirma(SelectedQuest.Rewards, i, 22, Color.White);
                RewardSlot.HAlign = 0.5f;
                RewardSlot.Width.Pixels = 40;
                RewardSlot.Height.Pixels = 40;
                RewardSlot.Top.Pixels = QuestNameSize.Y + 10 + QuestDialogueSize.Y + 10 + QuestTaskSize.Y + 10 + QuestDifficultyTitleSize.Y + 10 + TopPlacement;
                RewardSlot.Left.Pixels = LeftPlacement - (RewardSlot.Width.Pixels / 2);
                RewardSlot.RightMouse = false;
                RewardSlot.LeftMouse = false;
                QuestRewardSlots = QuestRewardSlots.Append(RewardSlot).ToArray();
                InfoPanel.Append(RewardSlot);
            }

            //Info Panel Scrollbar
            InfoPanel_ScrollBar = new UIScrollbar();
            InfoPanel_ScrollBar.HAlign = 1f;
            InfoPanel_ScrollBar.VAlign = 0f;
            InfoPanel_ScrollBar.Top.Pixels = 10;
            InfoPanel_ScrollBar.Height.Pixels = InfoPanel.Height.Pixels - 100;
            InfoPanel.Append(InfoPanel_ScrollBar);

            //Quest Start Button
            QuestStartButton = new UIButton_Terrafirma();
            QuestStartButton.Text = "Start Quest";
            QuestStartButton.Width.Pixels = 150;
            QuestStartButton.Height.Pixels = 50;
            QuestStartButton.VAlign = 1f;
            QuestStartButton.HAlign = 1f;
            QuestStartButton.BorderColor = Color.Black;
            QuestStartButton.BackgroundColor = new Color(90, 150, 210);
            QuestStartButton.BackgroundColor_Pressed = Main.OurFavoriteColor;
            QuestStartButton.BorderColor_Pressed = Main.OurFavoriteColor;
            QuestStartButton.Initialize();
            InfoPanel.Append(QuestStartButton);
        }

        //Update
        public override void Update(GameTime gameTime)
        {
            //Update Quest Button
            for (int i = 0; i < QuestButtonList.Length; i++)
            {
                QuestButtonList[i].Top.Pixels = (QuestButtonList[i].Height.Pixels + 5) * i - (QuestButtonList[i].Height.Pixels + 5) * ScrollPanel_ScrollBar.GetValue();
                Quest buttondata = QuestButtonList[i].ButtonData as Quest;
                if (buttondata.Status == (byte)QuestStatus.InProgress && buttondata.QuestCompletion())
                {
                    QuestButtonList[i].BackgroundColor = new Color(1f, Main.masterColor, 0f);
                }
                else if (buttondata.Status == (byte)QuestStatus.InProgress && !buttondata.QuestCompletion())
                {
                    QuestButtonList[i].BackgroundColor = new Color(255, 140, 220);
                }
            }

            //Update Some Positions
            Vector2 InfoPanelSize = new Vector2(InfoPanel.Width.Pixels - 20, InfoPanel.Height.Pixels - 50);
            DynamicSpriteFont font = FontAssets.MouseText.Value;

            QuestName.Top.Pixels = 5 - InfoPanel_ScrollBar.GetValue();
            Vector2 QuestNameSize = font.MeasureString(font.CreateWrappedText(QuestName.Text, InfoPanelSize.X)) * 1.2f;

            QuestDialogue.Top.Pixels = QuestNameSize.Y + 10 - InfoPanel_ScrollBar.GetValue();
            Vector2 QuestDialogueSize = font.MeasureString(font.CreateWrappedText(QuestDialogue.Text, InfoPanelSize.X));

            QuestTask.Top.Pixels = QuestNameSize.Y + 10 + QuestDialogueSize.Y + 35 - InfoPanel_ScrollBar.GetValue();
            Vector2 QuestTaskSize = font.MeasureString(font.CreateWrappedText(QuestTask.Text, InfoPanelSize.X));

            QuestDifficultyTitle.Top.Pixels = QuestNameSize.Y + 10 + QuestDialogueSize.Y + 35 + QuestTaskSize.Y + 35 - InfoPanel_ScrollBar.GetValue();
            QuestRewardsTitle.Top.Pixels = QuestNameSize.Y + 10 + QuestDialogueSize.Y + 35 + QuestTaskSize.Y + 35 - InfoPanel_ScrollBar.GetValue();
            Vector2 QuestDifficultyTitleSize = font.MeasureString(font.CreateWrappedText(QuestDifficultyTitle.Text, InfoPanelSize.X));

            for (int i = 0; i < QuestDifficultyStars.Length; i++) QuestDifficultyStars[i].Top.Pixels = QuestNameSize.Y + 10 + QuestDialogueSize.Y + 35 + QuestTaskSize.Y + 35 + QuestDifficultyTitleSize.Y  + 10 - InfoPanel_ScrollBar.GetValue();
            for (int i = 0; i < QuestRewardSlots.Length; i++)
            {
                int TopPlacement = (int)Math.Floor((decimal)i / 5) * 44;
                QuestRewardSlots[i].Top.Pixels = QuestNameSize.Y + 10 + QuestDialogueSize.Y + 35 + QuestTaskSize.Y + 35 + QuestDifficultyTitleSize.Y + 10 + TopPlacement - InfoPanel_ScrollBar.GetValue();
            }

            //Block Mouse when focusing on UI
            Main.blockMouse = ScrollPanel.IsMouseHovering || InfoPanel.IsMouseHovering || WarningPanel.IsMouseHovering;

            //Display Mosue Text when hovering over Stars
            for (int i = 0; i < QuestDifficultyStars.Length; i++)
            {
                if (QuestDifficultyStars[i].IsMouseHovering)
                {                  
                    Main.instance.MouseText(Enum.GetName(typeof(QuestDifficulty),SelectedQuest.Difficulty));
                }
            }

            if(SelectedQuest != null)
            {
                if (SelectedQuest.Status == (byte)QuestStatus.NotStarted) QuestStartButton.Text = "Start Quest";
                else if (SelectedQuest.Status == (byte)QuestStatus.InProgress && !SelectedQuest.QuestCompletion()) QuestStartButton.Text = "In Progress";
                else if (SelectedQuest.Status == (byte)QuestStatus.InProgress && SelectedQuest.QuestCompletion()) QuestStartButton.Text = "Complete Quest";
                else if (SelectedQuest.Status == (byte)QuestStatus.Completed) QuestStartButton.Remove();
            }


            base.Update(gameTime);
        }

        //Left Click
        public override void LeftClick(UIMouseEvent evt)
        {
            for (int i = 0; i < QuestButtonList.Length; i++)
            {
                if (QuestButtonList[i].IsMouseHovering)
                {
                    WarningPanel.HAlign = 10f;
                    if (QuestButtonList[i].ButtonData is Quest && SelectedQuest != QuestButtonList[i].ButtonData as Quest)
                    {
                        SelectedQuest = QuestButtonList[i].ButtonData as Quest;
                        UpdateQuestInfo(QuestButtonList[i].ButtonData as Quest);
                        UpdateInfoScroll();
                    }                                   
                }
            }
            
            if (QuestStartButton.IsMouseHovering)
            {
                byte ActiveQuests = 0;
                for (int i = 0; i < AvailableQuests.Length; i++)
                {
                    if (AvailableQuests[i].Status == (byte)QuestStatus.InProgress)
                    {
                        ActiveQuests++;
                    }
                }

                if(ActiveQuests > 0 && SelectedQuest.Status == (byte)QuestStatus.NotStarted)
                {
                    WarningPanel.HAlign = 0.5f;
                }
                else if (ActiveQuests == 0 && SelectedQuest.Status == (byte)QuestStatus.NotStarted)
                {
                    UpdateQuestStatus(SelectedQuest, QuestStatus.InProgress);
                    UpdateQuests();
                    UpdateQuestButtons();
                }
                else if (SelectedQuest.Status == (byte)QuestStatus.InProgress && SelectedQuest.QuestCompletion())
                {
                    UpdateQuestStatus(SelectedQuest, QuestStatus.Completed);
                    UpdateQuests();
                    UpdateQuestButtons();
                    for (int i = 0; i < SelectedQuest.Rewards.Length; i++)
                    {
                        Main.LocalPlayer.QuickSpawnItem(Main.LocalPlayer.GetSource_FromThis(), SelectedQuest.Rewards[i], SelectedQuest.Rewards[i].stack);
                    }
                }

            }

            if (NoButton.IsMouseHovering) WarningPanel.HAlign = 10f;
            if (YesButton.IsMouseHovering)
            {
                for (int i = 0; i < AvailableQuests.Length; i++)
                {
                    if (AvailableQuests[i].Status == (byte)QuestStatus.InProgress) UpdateQuestStatus(AvailableQuests[i], QuestStatus.NotStarted);
                }
                if (SelectedQuest.Status == (byte)QuestStatus.NotStarted) UpdateQuestStatus(SelectedQuest, QuestStatus.InProgress);
                UpdateQuests();
                UpdateQuestButtons();
                WarningPanel.HAlign = 10f;
            }
            base.LeftClick(evt);
        }

        public void UpdateQuestStatus(Quest quest, QuestStatus status)
        {
            quest.Status = (byte)status;
            for (int i = 0; i < Main.LocalPlayer.GetModPlayer<NewQuestModNPC>().playerquests.Length; i++)
            {
                if (Main.LocalPlayer.GetModPlayer<NewQuestModNPC>().playerquests[i].FullName == quest.FullName)
                {
                    Main.LocalPlayer.GetModPlayer<NewQuestModNPC>().playerquests[i].Status = (byte)status;
                    break;
                }
            }
            
        }
    }
}
