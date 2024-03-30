using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terrafirma.Global.Structs;
using Terraria.GameContent.UI.Elements;
using Terrafirma.Global;
using System.Collections.Generic;
using System;
using System.Linq;
using Terraria.DataStructures;
using Microsoft.CodeAnalysis.Text;
using Terraria.Localization;
using Microsoft.Xna.Framework.Input;
using static Terraria.GameContent.UI.States.UIVirtualKeyboard;
using Terraria.ModLoader.UI;
using Terrafirma.Global.Templates;
using Terrafirma.Systems.UIElements;

namespace Terrafirma.Systems.Cooking
{
    [Autoload(Side = ModSide.Client)]
    internal class CookingPotUI : UIState
    {   

        Item[] PotSlots = new Item[] { new Item(0), new Item(0), new Item(0) };
        UIItemSlot Itemslot = new UIItemSlot(new Item[] { }, 0, 3);
        UIItemSlot[] ItemslotArray = new UIItemSlot[] { };

        UITextBox AmountInput = new UITextBox("1", 1f, false);

        Texture2D PotBGTex = ModContent.Request<Texture2D>("Terrafirma/Systems/Cooking/CookingPotBG", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
        UIImage PotBG;

        UIPanel Button = new UIPanel();
        UIText ButtonText = new UIText("Cook");
        Vector2 tileposition = Vector2.Zero;

        UITickBox MinigameTickBox = new UITickBox();

        Texture2D PotMinigameBGTex = ModContent.Request<Texture2D>("Terrafirma/Systems/Cooking/CookingPotMinigameBG", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
        UIImage PotMinigameBG;

        CookingPotClickableUIElement PotMinigameClickable;
        CookingPotClickableUIElement[] PotMinigameClickableArray = new CookingPotClickableUIElement[] {};

        Dictionary<Keys, bool> KeyPresses = new Dictionary<Keys, bool>();

        int UIMode = 0;
        float TransitionFloat = -0.01f;
        int MinigameTimer = 60 * 6;
        int MinigameScore = 0;

        bool Active = false;

        public void Flush()
        {
            for (int i = 0; i < PotSlots.Length; i++)
            {
                if (PotSlots[i] != new Item(0))
                {
                    Main.LocalPlayer.QuickSpawnItemDirect(Main.LocalPlayer.GetSource_FromThis(), PotSlots[i], PotSlots[i].stack);
                    PotSlots[i] = new Item(0);
                }
            }

            RemoveAllChildren();
        }

        public void SwitchUIMode()
        {
            if (UIMode == 0)
            {
                UIMode = 1;

                //RemoveChild(PotBG);
                //for (int i = 0; i < ItemslotArray.Length; i++)
                //{
                //    RemoveChild(ItemslotArray[i]);
                //}
                //RemoveChild(AmountInput);

                //Append(PotMinigameBG);

            }
            else
            {
                UIMode = 0;

                //Append(PotBG);
                //for (int i = 0; i < ItemslotArray.Length; i++)
                //{
                //    Append(ItemslotArray[i]);
                //}
                //Append(AmountInput);
                //RemoveChild(Button);
                //Append(Button);

                //RemoveChild(PotMinigameBG);
            }
        }

        public void Create(Vector2 tilepos)
        {
            Flush();
            UIMode = 0;
            ItemslotArray = new UIItemSlot[] { };
            tileposition = tilepos - new Vector2(40, 60) * Main.UIScale;
 
            PotBG = new UIImage(PotBGTex);
            PotBG.Width.Pixels = 60f;
            PotBG.Height.Pixels = 35f;
            PotBG.HAlign = 0.5f;
            PotBG.VAlign = 0.38f;
            PotBG.Left.Pixels = - PotBG.Width.Pixels;
            PotBG.Top.Pixels = -PotBG.Height.Pixels - 70f;
            PotBG.Color = new Color(58,127,215) * 0.4f;

            for (int i = 0; i < 3; i++)
            {
                Itemslot = new UIItemSlot(PotSlots, i, 3);
                Itemslot.Width.Pixels = 43;
                Itemslot.Height.Pixels = 43;
                Itemslot.HAlign = 0.5f;
                Itemslot.VAlign = 0.38f;
                Itemslot.Left.Pixels = (i - 1) * 47;
                ItemslotArray = ItemslotArray.Append(Itemslot).ToArray();

            }

            Button = new UIPanel(ModContent.Request<Texture2D>("Terrafirma/Systems/NPCQuests/QuestButtonPanel"), ModContent.Request<Texture2D>("Terrafirma/Systems/NPCQuests/QuestButtonPanel"), 20);
            Button.Width.Pixels = 60f;
            Button.Height.Pixels = 35f;
            Button.HAlign = 0.5f;
            Button.VAlign = 0.38f;
            Button.Top.Pixels = -70;
            Button.BackgroundColor = new Color(0.4f, 0.5f, 1f, 1f);
            Button.BorderColor = new Color(0.4f, 0.5f, 1f);

            MinigameTickBox.HAlign = 0.5f;
            MinigameTickBox.VAlign = 0.38f;
            MinigameTickBox.Top.Pixels = -68;
            MinigameTickBox.Left.Pixels = 60;
            MinigameTickBox.State = TickBoxState.Checked;
            MinigameTickBox.Initialize();

            ButtonText.Width.Pixels = 60f;
            ButtonText.VAlign = 0.5f;
            ButtonText.HAlign = 0.5f;

            AmountInput = new UITextBox("1", 1f, false);
            AmountInput.Width.Pixels = 40f;
            AmountInput.Height.Pixels = 35f;
            AmountInput.HAlign = 0.5f;
            AmountInput.VAlign = 0.38f;
            AmountInput.Top.Pixels = 50;

            Append(PotBG);
            for (int i = 0; i < ItemslotArray.Length; i++)
            {
                Append(ItemslotArray[i]);
            }
            Append(MinigameTickBox);
            Append(AmountInput);
            Append(Button);
            Button.Append(ButtonText);

            PotMinigameBG = new UIImage(PotMinigameBGTex);
            PotMinigameBG.Width.Pixels = 160f;
            PotMinigameBG.Height.Pixels = 160f;
            PotMinigameBG.HAlign = 0.5f;
            PotMinigameBG.VAlign = 0.38f;
            PotMinigameBG.Top.Pixels = -30f;

            Active = true;

        } 
       
        public override void Update(GameTime gameTime)
        {
            //if (tileposition != Vector2.Zero)
            //{
            //    for (int i = 0; i < ItemslotArray.Length; i++)
            //    {
            //        ItemslotArray[i].Top.Pixels = (tileposition.Y - Main.screenPosition.Y) / Main.UIScale;
            //        ItemslotArray[i].Left.Pixels = (tileposition.X + ItemslotArray[i].Width.Pixels * i - Main.screenPosition.X) / Main.UIScale;
            //    }
            //}
            //Button.Top.Pixels = (tileposition.Y - Main.screenPosition.Y) / Main.UIScale;
            //Button.Left.Pixels = (tileposition.X - (Button.Width.Pixels / 2) - Main.screenPosition.X) / Main.UIScale;


            if (Button.IsMouseHovering)
            {
                Button.BackgroundColor = new Color(1f, 0.8f, 0.1f, 1f);
                Button.BorderColor = new Color(1f, 0.8f, 0.1f);
                Main.blockMouse = true;
            }
            else
            {
                Button.BackgroundColor = new Color(0.4f, 0.5f, 1f, 1f);
                Button.BorderColor = new Color(0.4f, 0.5f, 1f);
            }

            if (PotMinigameBG != null && PotMinigameBG.IsMouseHovering)
            {
                Main.blockMouse = true;
            }

            if (AmountInput.IsMouseHovering)
            {
                Main.instance.MouseText("Amount");
                Main.blockMouse = true;
            }

            if (MinigameTickBox.IsMouseHovering) 
            { 
                Main.instance.MouseText("Toggle Cooking Minigame when Crafting");
                Main.blockMouse = true;
            }

            for (int i = 0; i < PotMinigameClickableArray.Length; i++)
            {
                if (PotMinigameClickableArray[i].IsMouseHovering)
                {
                    Main.blockMouse = true;
                }
                PotMinigameClickableArray[i].Update(gameTime);
                if (PotMinigameClickableArray[i].Clicked)
                {
                    PotMinigameClickableArray[i].Clicked = false;
                    MinigameScore++;
                }
            }
  
            if (Main.keyState.GetPressedKeys().Length > 0)
            {
                for (int i = 0; i < Main.keyState.GetPressedKeys().Length; i++)
                {
                    Keys KeyPress = Keys.None;
                    
                    if (!KeyPresses.ContainsKey(Main.keyState.GetPressedKeys()[i]))
                    {
                        KeyPresses.Add(Main.keyState.GetPressedKeys()[i], true);
                        KeyPress = Main.keyState.GetPressedKeys()[i];
                    }
                    if (Main.keyState.IsKeyDown( Main.keyState.GetPressedKeys()[i] ) && !KeyPresses[Main.keyState.GetPressedKeys()[i]] )
                    {
                        KeyPresses[Main.keyState.GetPressedKeys()[i]] = true;
                        KeyPress = Main.keyState.GetPressedKeys()[i];
                    }

                    if ((byte)KeyPress >= 48 && (byte)KeyPress <= 57 && AmountInput.Text.Length < 3)
                    {
                        AmountInput.Write(KeyPress.ToString().Substring(1));
                    }
                    if (KeyPress == Keys.Back && AmountInput.Text.Length > 0)
                    {
                        AmountInput.SetText(AmountInput.Text.Remove(AmountInput.Text.Length - 1, 1));
                    }
                }
            }

            for (int i = 0; i < KeyPresses.Count; i++) 
            { 
                if (Main.keyState.IsKeyUp(KeyPresses.ElementAt(i).Key)) KeyPresses[KeyPresses.ElementAt(i).Key] = false;
            }

            if (Active)
            {
                if (UIMode == 0 )
                {
                    if (TransitionFloat > 0f)
                    {
                        TransitionFloat = Math.Clamp(TransitionFloat - 0.1f, 0f, 1f);
                        Append(PotBG);

                        for (int i = PotMinigameClickableArray.Length - 1; i >= 0; i--)
                        {
                            RemoveChild(PotMinigameClickableArray[i]);
                            PotMinigameClickableArray[i].Deactivate();
                        }

                    }
                    else if (TransitionFloat == 0f)
                    {
                        TransitionFloat = -0.01f;       
                        for (int i = 0; i < ItemslotArray.Length; i++)
                        {
                            Append(ItemslotArray[i]);
                        }
                        Append(AmountInput);
                        RemoveChild(Button);
                        Append(Button);
                        Append(MinigameTickBox);
                        Button.Top.Pixels = -70;
                        ButtonText.SetText("Cook");
                        RemoveChild(PotMinigameBG);

                        CookingRecipe[] possibleresults = new CookingRecipe[] { };
                        for (int i = 0; i < CookingRecipeIndex.cookingrecipes.Count; i++)
                        {
                            if (CookingRecipeIndex.cookingrecipes[i].CanCraft(PotSlots.ToList())) possibleresults = possibleresults.Append(CookingRecipeIndex.cookingrecipes[i]).ToArray();
                        }

                        int CraftAmount = 0;
                        if (Int32.TryParse(AmountInput.Text, out CraftAmount))
                        {
                            for (int i = 0; i < CraftAmount; i++) CraftAttempt(possibleresults);
                        }
                        if (AmountInput.Text == "")
                        {
                            CraftAttempt(possibleresults);
                        }

                        MinigameScore = 0;
                    }
                }
                else
                {
                    if (TransitionFloat < 1f)
                    {
                        TransitionFloat = Math.Clamp(TransitionFloat + 0.1f, 0f, 1f);
                        Append(PotMinigameBG);
                    }
                    else if (TransitionFloat == 1f)
                    {
                        TransitionFloat = 1.01f;
                        RemoveChild(PotBG);
                        for (int i = 0; i < ItemslotArray.Length; i++)
                        {
                            RemoveChild(ItemslotArray[i]);
                        }
                        RemoveChild(AmountInput);
                        RemoveChild(MinigameTickBox);
                        Button.Top.Pixels = -120;
                        ButtonText.SetText("Skip");

                    }
                    else
                    {
                        MinigameTimer--;
                        if (MinigameTimer > 0)
                        {   
                            SetupMinigame();
                        }
                        else
                        {
                            UIMode = 0;
                        }
                    }
                }

                PotBG.Color = new Color(58, 127, 215) * 0.4f * (1f - TransitionFloat);
                PotMinigameBG.Color = Color.White * TransitionFloat;
            }

            

        }

        //Left Click button
        public override void LeftClick(UIMouseEvent evt)
        {
            if (Button.IsMouseHovering)
            {

                CookingRecipe[] possibleresults = new CookingRecipe[] { };
                for (int i = 0; i < CookingRecipeIndex.cookingrecipes.Count; i++)
                {
                    if (CookingRecipeIndex.cookingrecipes[i].CanCraft(PotSlots.ToList())) possibleresults = possibleresults.Append(CookingRecipeIndex.cookingrecipes[i]).ToArray();
                }

                if (possibleresults.Length > 0 && MinigameTickBox.State == TickBoxState.Checked) SwitchUIMode();
                else
                {
                    int CraftAmount = 0;
                    if (Int32.TryParse(AmountInput.Text, out CraftAmount))
                    {
                        for (int i = 0; i < CraftAmount; i++) CraftAttempt(possibleresults);
                    }
                    if (AmountInput.Text == "")
                    {
                        CraftAttempt(possibleresults);
                    }
                }
            }

            if (MinigameTickBox.IsMouseHovering)
            {
                MinigameTickBox.State = MinigameTickBox.State == TickBoxState.Checked ? TickBoxState.FalseChecked : TickBoxState.Checked;
            }
        }

        //Crafting Method
        internal void CraftAttempt(CookingRecipe[] possibleresults)
        {
            if (possibleresults.Length > 0)
            {
                if (possibleresults.Length > 1)
                {

                    MinigameTimer = 60 * 7;

                    int randitem = Main.rand.Next(possibleresults.Length);
                    for (int j = 0; j < PotSlots.Length; j++)
                    {
                        if (PotSlots[j].stack == 1) PotSlots[j] = new Item(0);
                        else PotSlots[j].stack -= 1;

                    }

                    Item ResultItem = new Item(possibleresults[randitem].Result);
                    if (MinigameScore >= 9) ResultItem.GetGlobalItem<FoodTemplateGlobalItem>().grade = 2;
                    else if (MinigameScore >= 5) ResultItem.GetGlobalItem<FoodTemplateGlobalItem>().grade = 1;
                    else ResultItem.GetGlobalItem<FoodTemplateGlobalItem>().grade = -1;

                    Main.LocalPlayer.QuickSpawnItem(Main.LocalPlayer.GetSource_FromThis(), ResultItem, possibleresults[randitem].Stack);
                }
                else
                {    

                    MinigameTimer = 60 * 7;

                    for (int j = 0; j < PotSlots.Length; j++)
                    {
                        if (PotSlots[j].stack == 1) PotSlots[j] = new Item(0);
                        else PotSlots[j].stack -= 1;
                    }

                    Item ResultItem = new Item(possibleresults[0].Result);
                    if (MinigameScore >= 9) ResultItem.GetGlobalItem<FoodTemplateGlobalItem>().grade = 2;
                    else if (MinigameScore >= 5) ResultItem.GetGlobalItem<FoodTemplateGlobalItem>().grade = 1;
                    else ResultItem.GetGlobalItem<FoodTemplateGlobalItem>().grade = -1;

                    Main.LocalPlayer.QuickSpawnItem(Main.LocalPlayer.GetSource_FromThis(), ResultItem, possibleresults[0].Stack);
                }
            }
        }

        public void SetupMinigame()
        {

            if (MinigameTimer % 30 == 0 && MinigameTimer > 60)
            {
                PotMinigameClickable = new CookingPotClickableUIElement();
                PotMinigameClickable.HAlign = 0.5f;
                PotMinigameClickable.VAlign = 0.38f;

                Vector2 RandDistance = Main.rand.NextVector2Circular(45f,45f);

                PotMinigameClickable.Top.Pixels = -16f + RandDistance.X;
                PotMinigameClickable.Left.Pixels = RandDistance.Y;
                PotMinigameClickable.Initialize();
                Append(PotMinigameClickable);
                PotMinigameClickableArray = PotMinigameClickableArray.Append(PotMinigameClickable).ToArray();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            Main.inventoryScale = 0.755f;
            base.Draw(spriteBatch);
        }

        

    }
}
