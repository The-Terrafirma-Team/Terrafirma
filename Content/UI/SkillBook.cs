using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Common;
using Terrafirma.Common.Mechanics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.UI;
using Terraria.UI;
using Terraria.UI.Chat;
using static System.Net.Mime.MediaTypeNames;

namespace Terrafirma.Content.UI
{
    public class SkillBook : UIState
    {
        internal static Asset<Texture2D> dragIconTex;
        internal static Asset<Texture2D> closeIconTex;
        internal static Asset<Texture2D> skillBorderTex;

        Vector2 uiPosition = Vector2.Zero;
        bool mouseDrag = false;
        bool mouseAllow = true;
        Vector2 dragOffset = Vector2.Zero;

        UIPanel mainPanel;
            UIText topTitle;
            UIPanel abilityPanel;
                SkillBookButton[] abilityButtons = new SkillBookButton[]{};
                UIScrollbar abilityPanelScrollbar;

        TerrafirmaUIImage dragIcon;
        TerrafirmaUIImage closeIcon;

        public override void OnActivate()
        {
            RemoveAllChildren();
            uiPosition = Main.ScreenSize.ToVector2() / 2;

            mainPanel = new UIPanel();
            mainPanel.MinWidth.Pixels = 500;
            mainPanel.MinHeight.Pixels = 450;
            mainPanel.Left.Pixels = uiPosition.X;
            mainPanel.Top.Pixels = uiPosition.Y;

            topTitle = new UIText("Skill Book", 1.0f, false);
            topTitle.Height.Pixels = 40;
            topTitle.Width.Percent = 1.0f;
            topTitle.Top.Pixels = 5;
            topTitle.HAlign = 0.5f;
            mainPanel.Append(topTitle);

            abilityPanel = new UIPanel();
            abilityPanel.Width.Percent = 1.0f;
            abilityPanel.Height.Pixels = -30;
            abilityPanel.Height.Percent = 1.0f;
            abilityPanel.Top.Pixels = 30;
            abilityPanel.OverflowHidden = true;
            abilityPanel.BackgroundColor = Color.Transparent;
            abilityPanel.BorderColor = Color.Transparent;
            mainPanel.Append(abilityPanel);

            abilityPanelScrollbar = new UIScrollbar();
            abilityPanelScrollbar.Top.Pixels = 36;
            abilityPanelScrollbar.Height.Percent = 1.0f;
            abilityPanelScrollbar.Height.Pixels = -42;
            abilityPanelScrollbar.HAlign = 1.0f;
            mainPanel.Append(abilityPanelScrollbar);


            dragIcon = new TerrafirmaUIImage(dragIconTex);
            dragIcon.frame = new Rectangle(0, 0, 44, 44);
            dragIcon.Left.Pixels = uiPosition.X + mainPanel.Width.Pixels + 5;
            dragIcon.Top.Pixels = uiPosition.Y + mainPanel.Height.Pixels + 5;
            dragIcon.Width.Pixels = 44;
            dragIcon.Height.Pixels = 44;

            closeIcon = new TerrafirmaUIImage(closeIconTex);
            closeIcon.frame = new Rectangle(0, 0, 32, 32);
            closeIcon.Left.Percent = 0.92f;
            closeIcon.Top.Pixels = -8;
            closeIcon.Width.Pixels = 32;
            closeIcon.Height.Pixels = 32;
            mainPanel.Append(closeIcon);

            Append(mainPanel);
            Append(dragIcon);

            GetSkills();
        }

        public override void Update(GameTime gameTime)
        {
            //OnActivate();

            //GetSkills();
            UpdatePositions(gameTime);
            UpdateDrag(gameTime);

            if (closeIcon.IsMouseHovering) closeIcon.frame = new Rectangle(32, 0, 32, 32);
            else closeIcon.frame = new Rectangle(0, 0, 32, 32);

            base.Update(gameTime);
        }

        public override void LeftClick(UIMouseEvent evt)
        {
            if (closeIcon.IsMouseHovering)
            {
                SkillBookSystem.Hide();
            }
            base.LeftClick(evt);
        }

        public void GetSkills()
        {

            foreach (SkillBookButton button in abilityButtons){
                abilityPanel.RemoveChild(button);
                button.Remove();
                button.Deactivate();
            }

            abilityButtons = new SkillBookButton[]{};

            for (int i = 0; i < SkillsSystem.Skills.Length; i++)
            {
                SkillBookButton newButton = new SkillBookButton(SkillsSystem.Skills[i]);
                newButton.ClickSound = SoundID.MenuTick;
                newButton.Width.Percent = 0.5f;
                newButton.Width.Pixels = -4;
                newButton.Left.Percent = (i % 2) * 0.5f;
                newButton.Left.Pixels = 2;
                newButton.Top.Pixels = 64 * (i / 2);
                newButton.Height.Pixels = 60;

                abilityButtons = abilityButtons.Append(newButton).ToArray();
                abilityPanel.Append(newButton);
            }
        }

        public void UpdatePositions(GameTime gameTime)
        {
            mainPanel.Left.Pixels = uiPosition.X;
            mainPanel.Top.Pixels = uiPosition.Y;
            mainPanel.MinWidth.Pixels = 500;
            mainPanel.MinHeight.Pixels = 450;

            dragIcon.Left.Pixels = uiPosition.X + mainPanel.GetDimensions().Width + 5;
            dragIcon.Top.Pixels = uiPosition.Y;
            dragIcon.Width.Pixels = 44;
            dragIcon.Height.Pixels = 44;

            closeIcon.Left.Percent = 0.92f;
            closeIcon.Top.Pixels = -8;
            closeIcon.Width.Pixels = 32;
            closeIcon.Height.Pixels = 32;

            float buttonsHeight = 0.0f;
            for (int i = 0; i < abilityButtons.Length; i++)
            {
                if (i%2 == 0) buttonsHeight += 64;
                abilityButtons[i].Top.Pixels = 64 * (i / 2) - abilityPanelScrollbar.ViewPosition;
            }
            buttonsHeight -= 64 - (64 * 1.5f);

            abilityPanel.Width.Percent = 1.0f;
            abilityPanel.Width.Pixels = -abilityPanelScrollbar.Width.Pixels - 5;
            abilityPanel.Width.Percent = 1.0f;
            abilityPanel.BorderColor = Color.Black;

            abilityPanelScrollbar.Top.Pixels = 36;
            abilityPanelScrollbar.Height.Percent = 1.0f;
            abilityPanelScrollbar.Height.Pixels = -42;
            abilityPanelScrollbar.SetView(abilityPanel.GetDimensions().Height, buttonsHeight);
        }

        public void UpdateDrag(GameTime gameTime)
        {
            if (Main.mouseLeft && !dragIcon.IsMouseHovering)
            {
                mouseAllow = false;
            }
            if (dragIcon.IsMouseHovering && Main.mouseLeft && !mouseDrag && mouseAllow)
            {
                mouseDrag = true;
                dragOffset = Main.MouseScreen - dragIcon.GetDimensions().Position();
            }
            if (!Main.mouseLeft)
            {
                mouseDrag = false;
                mouseAllow = true;
            }

            if (mouseDrag)
            {
                uiPosition = Main.MouseScreen - (dragIcon.GetDimensions().Position() + dragOffset - mainPanel.GetDimensions().Position());
            }
            uiPosition = Vector2.Clamp(uiPosition, new Vector2(20, 20), new Vector2(Main.ScreenSize.X - mainPanel.GetDimensions().Width - 50, Main.ScreenSize.Y - mainPanel.GetDimensions().Height - 50));

            if (mouseDrag || dragIcon.IsMouseHovering) dragIcon.frame = new Rectangle(44, 0, 44, 44);
            else dragIcon.frame = new Rectangle(0, 0, 44, 44);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            mainPanel.Draw(spriteBatch);
            dragIcon.Draw(spriteBatch);
            abilityPanel.Draw(spriteBatch);
            foreach (UIElement element in Children)
            {
                if (element is SkillBookHoverIcon) element.Draw(spriteBatch);
            }
        }
    }

    [Autoload(Side = ModSide.Client)]
    public class SkillBookSystem : ModSystem
    {
        private static UserInterface skillsBookInterface;
        internal static SkillBook skillBook;
        public override void Load()
        {
            SkillBook.dragIconTex = Mod.Assets.Request<Texture2D>("Assets/UI/DragIcon");
            SkillBook.closeIconTex = Mod.Assets.Request<Texture2D>("Assets/UI/CloseButton");
            SkillBook.skillBorderTex = Mod.Assets.Request<Texture2D>("Assets/UI/SkillIconBorder");
            skillBook = new();
            skillsBookInterface = new();
            skillsBookInterface.SetState(skillBook);
        }

        public static void Hide()
        {
            skillsBookInterface.SetState(null);
            SoundEngine.PlaySound(SoundID.MenuClose);
        }

        public static void Show()
        {
            skillBook.Initialize();
            skillsBookInterface.SetState(skillBook);
            SoundEngine.PlaySound(SoundID.MenuOpen);
        }

        public static void Flip()
        {
            if (skillsBookInterface.CurrentState == null) Show();
            else Hide();
        }
        public override void UpdateUI(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.P))
            {
                Show();
            }
            skillsBookInterface?.Update(gameTime);
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            if (ModContent.GetInstance<ServerConfig>().CombatReworkEnabled)
            {
                int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Hotbar"));
                if (mouseTextIndex != -1)
                {
                    layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                        "Terrafirma: Skill Book",
                        delegate
                        {
                            skillsBookInterface?.Draw(Main.spriteBatch, new GameTime());
                            return true;
                        },
                        InterfaceScaleType.UI)
                    );
                }
            }
        }
    }
}
