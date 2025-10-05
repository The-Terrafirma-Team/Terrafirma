using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Common.Mechanics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.ModLoader.UI;
using Terraria.UI;

namespace Terrafirma.Content.UI
{
    internal class SkillBookButton : UIPanel
    {

        public SoundStyle? HoverSound = null;
        public SoundStyle? ClickSound = null;

        public string TooltipText = null;

        public Color HoverPanelColor = UICommon.DefaultUIBlue;
        public Color HoverBorderColor = UICommon.DefaultUIBorderMouseOver;

        public Color? AltPanelColor = null;
        public Color? AltBorderColor = null;

        public Color? AltHoverPanelColor = null;
        public Color? AltHoverBorderColor = null;

        public Func<bool> UseAltColors = () => false;

        private Color? _panelColor = null;
        private Color? _borderColor = null;

        public Skill skill;
        public TerrafirmaUIImage skillIcon;
        public UIText skillName;

        bool mouseAllow = false;
        bool mouseDrag = false;
        public SkillBookButton(Skill buttonSkill)
        {
            skill = buttonSkill;
            Initialize();
        }

        public override void OnInitialize()
        {
            RemoveAllChildren();
            skillIcon = new TerrafirmaUIImage(SkillsSystem.SkillTextures[skill.ID]);
            skillIcon.Top.Pixels = -5;
            skillIcon.frame = new Rectangle(0, 0, SkillsSystem.SkillTextures[skill.ID].Width(), SkillsSystem.SkillTextures[skill.ID].Width());
            Append(skillIcon);

            skillName = new UIText(SkillsSystem.Skills[skill.ID].DisplayName.Value);
            skillName.Width.Pixels = Width.Pixels - 54;
            skillName.Top.Pixels = 0;
            skillName.Left.Pixels = 54;
            skillName.VAlign = 0.5f;
            skillName.DynamicallyScaleDownToWidth = true;
            Append(skillName);
        }

        public override void Update(GameTime gameTime)
        {
            if (Main.mouseLeft && !IsMouseHovering)
            {
                mouseAllow = false;
            }
            if (Main.mouseLeft && IsMouseHovering && !mouseDrag && mouseAllow)
            {
                mouseDrag = true;
                SkillBook book = Parent.Parent.Parent as SkillBook;
                book.SetIconSkill(skill);
                if (ClickSound != null)
                    SoundEngine.PlaySound(ClickSound.Value);
            }
            if (!Main.mouseLeft)
            {
                mouseDrag = false;
                mouseAllow = true;
            }
            base.Update(gameTime);
        }

        public override void MouseOver(UIMouseEvent evt)
        {
            base.MouseOver(evt);

            if (HoverSound != null)
                SoundEngine.PlaySound(HoverSound.Value);
        }

        public override void LeftClick(UIMouseEvent evt)
        {
            base.LeftClick(evt);
        }

        public override void Recalculate()
        {
            base.Recalculate();

            _panelColor ??= BackgroundColor;
            _borderColor ??= BorderColor;

            AltPanelColor ??= BackgroundColor;
            AltBorderColor ??= BorderColor;

            AltHoverPanelColor ??= HoverPanelColor;
            AltHoverBorderColor ??= HoverBorderColor;
        }

        protected void SetPanelColors()
        {
            bool altCondition = UseAltColors();
            if (mouseDrag)
            {
                BackgroundColor = altCondition ? AltHoverBorderColor.Value : HoverBorderColor;
                BorderColor = altCondition ? AltHoverBorderColor.Value : HoverBorderColor;
            }
            else
            {
                if (IsMouseHovering)
                {
                    BackgroundColor = altCondition ? AltHoverPanelColor.Value : HoverPanelColor;
                    BorderColor = altCondition ? AltHoverBorderColor.Value : HoverBorderColor;
                }
                else
                {
                    BackgroundColor = altCondition ? AltPanelColor.Value : _panelColor.Value;
                    BorderColor = altCondition ? AltBorderColor.Value : _borderColor.Value;
                }
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            DrawChildren(spriteBatch);

            SetPanelColors();

            if (IsMouseHovering && TooltipText != null && TooltipText != "")
            {
                UICommon.TooltipMouseText(TooltipText);
            }
        }
    }
}
