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
        internal static Asset<Texture2D> categoryButtonTex;
        internal static Asset<Texture2D> categoryButtonOverlayTex;

        internal static SkillBookHoverIcon hoverIcon;
        public Skill selectedSkill;
        public SkillCategory selectedCategory = SkillCategory.General;

        Vector2 uiPosition = Vector2.Zero;
        bool mouseDrag = false;
        bool mouseAllow = true;
        Vector2 dragOffset = Vector2.Zero;

        UIPanel mainPanel;
            UIText topTitle;
            UIPanel abilityPanel;
                SkillBookButton[] abilityButtons = new SkillBookButton[]{};
                UIScrollbar abilityPanelScrollbar;
        UIPanel categoryPanel;
            TerrafirmaUIImage[] categoryButtons = new TerrafirmaUIImage[]{};
        TerrafirmaUIImage dragIcon;
        TerrafirmaUIImage closeIcon;

        public override void OnActivate()
        {
            RemoveAllChildren();

            hoverIcon = new SkillBookHoverIcon();
            Append(hoverIcon);

            mainPanel = new UIPanel();
            mainPanel.MinWidth.Pixels = 520;
            mainPanel.MinHeight.Pixels = 450;
            mainPanel.Left.Pixels = uiPosition.X;
            mainPanel.Top.Pixels = uiPosition.Y;

            categoryPanel = new UIPanel();
            categoryPanel.Width.Pixels = 52;
            categoryPanel.Height.Pixels = categoryButtons.Length * 62;
            categoryPanel.Left.Pixels = uiPosition.X - 46;
            categoryPanel.Top.Pixels = uiPosition.Y + 40;
            categoryPanel.BackgroundColor = Color.Transparent;
            categoryPanel.BorderColor = Color.Transparent;

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
            abilityPanel.Left.Pixels = -23;
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
            Append(categoryPanel);
            //Append(dragIcon);

            var dims = mainPanel.GetDimensions();
            uiPosition = Main.ScreenSize.ToVector2() / 2 - new Vector2(dims.Width,dims.Height) / 2;

            GetSkills();
            GetCategories();
            Update(Main.gameTimeCache);
        }

        public override void Update(GameTime gameTime)
        {
            //OnActivate();

            //GetSkills();
            UpdatePositions(gameTime);
            UpdateDrag(gameTime);
            UpdateCategoryButtons(gameTime);

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
            foreach(TerrafirmaUIImage button in categoryButtons)
            {
                if (button.IsMouseHovering)
                {
                    selectedCategory = (SkillCategory)(int)button.customData;
                    GetSkills();
                    SoundEngine.PlaySound(SoundID.MenuTick);
                }
            }
            base.LeftClick(evt);
        }

        public void SetIconSkill(Skill skill)
        {
            hoverIcon.skill = skill;
            hoverIcon.doUpdate = true;
            hoverIcon.Initialize();
        }

        public void GetCategories()
        {

            foreach (TerrafirmaUIImage button in categoryButtons)
            {
                categoryPanel.RemoveChild(button);
                button.Remove();
                button.Deactivate();
            }

            categoryButtons = new TerrafirmaUIImage[]{};

            Array categories = Enum.GetValues(typeof(SkillCategory));
            for (int i = 0; i < categories.Length; i++)
            {
                TerrafirmaUIImage button = new TerrafirmaUIImage(categoryButtonTex, categoryButtonOverlayTex, true);
                button.customOverlayFrame = true;

                int yPos = (SkillCategory)i == selectedCategory ? 62 : 0;

                button.frame = new Rectangle(i * 70, yPos, 68, 62);
                button.overlayFrame = new Rectangle(0, yPos, 68, 62);
                button.Top.Pixels = (categoryButtonOverlayTex.Height() / 2) * i - 12;
                button.Left.Pixels = -12;
                button.MinWidth.Pixels = 46;
                button.MinHeight.Pixels = 60;
                button.customData = (int)((SkillCategory)i);
                button.showOverlay = false;

                categoryButtons = categoryButtons.Append(button).ToArray();
                categoryPanel.Append(button);
            }
        }

        public void GetSkills()
        {

            foreach (SkillBookButton button in abilityButtons){
                abilityPanel.RemoveChild(button);
                button.Remove();
                button.Deactivate();
            }

            abilityButtons = new SkillBookButton[]{};

            Skill[] skills = SkillsSystem.GetSkillsOfCategory(selectedCategory);
            for (int i = 0; i < skills.Length; i++)
            {
                SkillBookButton newButton = new SkillBookButton(skills[i]);
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
            mainPanel.MinWidth.Pixels = 520;
            mainPanel.MinHeight.Pixels = 450;

            categoryPanel.Width.Pixels = 52;
            categoryPanel.Height.Pixels = categoryButtons.Length * 62;
            categoryPanel.Left.Pixels = uiPosition.X - 46;
            categoryPanel.Top.Pixels = uiPosition.Y + 54;

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

            abilityPanel.Width.Percent = 0.98f;
            abilityPanel.Width.Pixels = -abilityPanelScrollbar.Width.Pixels - 5;
            abilityPanel.BorderColor = Color.Black;

            abilityPanelScrollbar.Top.Pixels = 36;
            abilityPanelScrollbar.Height.Percent = 1.0f;
            abilityPanelScrollbar.Height.Pixels = -42;
            abilityPanel.HAlign = 1.0f;
            abilityPanel.Left.Pixels = -23;
            abilityPanelScrollbar.SetView(abilityPanel.GetDimensions().Height, buttonsHeight);
        }

        public void UpdateDrag(GameTime gameTime)
        {
            bool mouseHover = false;
            var dims = mainPanel.GetDimensions();
            if (Main.MouseScreen.X >= dims.X && Main.MouseScreen.X <= uiPosition.X + dims.Height)
                mouseHover = Main.LocalPlayer.mouseInterface = Main.MouseScreen.Y > dims.Y && Main.MouseScreen.Y < dims.Y + 40;

            if (Main.mouseLeft && !mouseHover)
            {
                mouseAllow = false;
            }
            if (mouseHover && Main.mouseLeft && !mouseDrag && mouseAllow)
            {
                mouseDrag = true;
                dragOffset = Main.MouseScreen - mainPanel.GetDimensions().Position();
            }
            if (!Main.mouseLeft)
            {
                mouseDrag = false;
                mouseAllow = true;
            }

            if (mouseDrag)
            {
                uiPosition = Main.MouseScreen - dragOffset;
            }
            uiPosition = Vector2.Clamp(uiPosition, new Vector2(60, 20), new Vector2(Main.ScreenSize.X - mainPanel.GetDimensions().Width - 50, Main.ScreenSize.Y - mainPanel.GetDimensions().Height - 50));

            if (mouseDrag || dragIcon.IsMouseHovering) dragIcon.frame = new Rectangle(44, 0, 44, 44);
            else dragIcon.frame = new Rectangle(0, 0, 44, 44);
        }

        public void UpdateCategoryButtons(GameTime gameTime)
        {
            foreach (TerrafirmaUIImage button in categoryButtons)
            {
                button.showOverlay = button.IsMouseHovering;
                if (button.IsMouseHovering)
                    Main.LocalPlayer.mouseInterface = true;
                int yPos = selectedCategory == (SkillCategory)(int)button.customData ? 62 : 0;
                Rectangle baseFrame = button.frame.Value;
                button.frame = new Rectangle(baseFrame.X, yPos, baseFrame.Width, baseFrame.Height);
                Rectangle overlayFrame = button.overlayFrame.Value;
                button.overlayFrame = new Rectangle(overlayFrame.X, yPos, overlayFrame.Width, overlayFrame.Height);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            mainPanel.Draw(spriteBatch);
            //dragIcon.Draw(spriteBatch);
            abilityPanel.Draw(spriteBatch);
            categoryPanel.Draw(spriteBatch);
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
        public static ModKeybind OpenSkillbook { get; set; }
        public override void Load()
        {
            OpenSkillbook = KeybindLoader.RegisterKeybind(Mod, "Open Skillbook", Keys.P);
            SkillBook.dragIconTex = Mod.Assets.Request<Texture2D>("Assets/UI/DragIcon");
            SkillBook.closeIconTex = Mod.Assets.Request<Texture2D>("Assets/UI/CloseButton");
            SkillBook.skillBorderTex = Mod.Assets.Request<Texture2D>("Assets/UI/SkillIconBorder");
            SkillBook.categoryButtonTex = Mod.Assets.Request<Texture2D>("Assets/UI/SkillCategoryButtons");
            SkillBook.categoryButtonOverlayTex = Mod.Assets.Request<Texture2D>("Assets/UI/SkillCategoryButtonOverlay");
            skillBook = new();
            skillsBookInterface = new();
            skillsBookInterface.SetState(null);
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
            if (OpenSkillbook.JustPressed)
            {
                Flip();
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
