using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Systems.NewNPCQuests;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace Terrafirma.Systems.UIElements
{
    public class UIButton_Terrafirma : UIElement
    {
        Asset<Texture2D> BackgroundTexture;
        Asset<Texture2D> BorderTexture;

        public Color BackgroundColor = Color.White;
        public Color BorderColor = Color.Black;
        public Color BackgroundColor_Pressed = Color.White;
        public Color BorderColor_Pressed = Color.White;
        Color CurrentBGColor = Color.White;
        Color CurrentBorderColor = Color.White;

        public int BorderWidth = 1;

        public string Text = "";
        /// <summary>
        /// The Text's horizontal alignment on the button. 0 is left, 1 is middle and 2 is right.
        /// </summary>
        public byte Text_HAlign = 0;
        public float TextOpacity = 1f;
        public float TextOpacity_Pressed = 1f;
        float TextOpac = 1f;
        Vector2 ScreenPos = Vector2.Zero;

        public Object ButtonData = null;
        public UIButton_Terrafirma(Asset<Texture2D> BGTexture = null, Asset<Texture2D> BGBorderTexture = null, int BorderSize = 12)
        {
            BackgroundTexture = BGTexture == null? Terrafirma.QuestButtonBG : BGTexture;
            BorderTexture = BGBorderTexture == null? Terrafirma.QuestButtonBGBorder : BGBorderTexture;
            BorderWidth = BorderSize;
        }
        
        public override void OnInitialize()
        {
            CurrentBGColor = BackgroundColor;
            CurrentBorderColor = BorderColor;
            TextOpac = TextOpacity;
            base.OnInitialize();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Draw9Patch(spriteBatch, BackgroundTexture, CurrentBGColor);
            Draw9Patch(spriteBatch, BorderTexture, CurrentBorderColor);

            Vector2 Textpos = ScreenPos + new Vector2(10, Height.Pixels/2 + 5);
            float TextHorizontalPosition = 0;
            switch (Text_HAlign)
            {
                case 0: TextHorizontalPosition = 0; break;
                case 1: TextHorizontalPosition = -Width.Pixels/2 + 10 + FontAssets.MouseText.Value.MeasureString(Text).X/2; break;
                case 2: TextHorizontalPosition = -Width.Pixels + 20 + FontAssets.MouseText.Value.MeasureString(Text).X; break;
            }
            Vector2 TextOrigin = new Vector2(TextHorizontalPosition, FontAssets.MouseText.Value.MeasureString(Text).Y / 2);
            for (int i = 0; i < 4; i++)
            {
                spriteBatch.DrawString(FontAssets.MouseText.Value, Text, Textpos + new Vector2(0, 2).RotatedBy(i * MathHelper.PiOver2), Color.Black * TextOpac, 0f, TextOrigin, 1f, SpriteEffects.None, 1f);
            }
            spriteBatch.DrawString(FontAssets.MouseText.Value, Text, Textpos, Color.White * TextOpac, 0f, TextOrigin, 1f, SpriteEffects.None, 1f);
            base.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {

            if (IsMouseHovering)
            {
                CurrentBGColor = BackgroundColor_Pressed;
                CurrentBorderColor = BorderColor_Pressed;
                TextOpac = TextOpacity_Pressed;
            }
            else
            {
                CurrentBGColor = BackgroundColor;
                CurrentBorderColor = BorderColor;
                TextOpac = TextOpacity;
            }


            ScreenPos = Vector2.Zero;
            if (Parent == null || Parent is UIState)
            {
                ScreenPos = new Vector2(Left.Pixels + Main.screenWidth * HAlign, Top.Pixels + Main.screenHeight * VAlign) - new Vector2(Width.Pixels / 2, Height.Pixels / 2);
            }
            else
            {
                //Vector2 ParentPos = new Vector2(Main.screenWidth * Parent.HAlign + Parent.Left.Pixels, Main.screenHeight * Parent.VAlign + Parent.Top.Pixels) - new Vector2(Parent.Width.Pixels / 2, Parent.Height.Pixels / 2);
                //ScreenPos = new Vector2(ParentPos.X + Left.Pixels + Parent.Width.Pixels * HAlign, ParentPos.Y + Top.Pixels + Parent.Height.Pixels * VAlign) - new Vector2(Width.Pixels / 2, Height.Pixels / 2);
                ScreenPos = new Vector2(
                    (Main.screenWidth * Parent.HAlign + Parent.Left.Pixels) + Left.Pixels - (Parent.GetDimensions().Width / 2) + Parent.PaddingLeft + ((Parent.GetInnerDimensions().Width - Width.Pixels) * HAlign)
                    ,
                    (Main.screenHeight * Parent.VAlign + Parent.Top.Pixels) + Top.Pixels - (Parent.GetDimensions().Height / 2) + Parent.PaddingTop + ((Parent.GetInnerDimensions().Height - Height.Pixels) * VAlign)
                    );
            }
            base.Update(gameTime);
        }

        public override void MouseOver(UIMouseEvent evt)
        {
            SoundEngine.PlaySound(SoundID.MenuTick);
            base.MouseOver(evt);
        }

        public override void MouseOut(UIMouseEvent evt)
        {
            base.MouseOut(evt);
        }

        public override void LeftClick(UIMouseEvent evt)
        {
            SoundEngine.PlaySound(SoundID.MenuTick);
            base.LeftClick(evt);
        }

        public void Draw9Patch(SpriteBatch spriteBatch, Asset<Texture2D> Texture, Color color)
        {

            //TL Corner
            spriteBatch.Draw(Texture.Value, ScreenPos, new Rectangle(0, 0, BorderWidth, BorderWidth), color, 0f, new Vector2(0), 1f, SpriteEffects.None, 1f);
            //TR Corner
            spriteBatch.Draw(Texture.Value, ScreenPos + new Vector2(Width.Pixels - BorderWidth, 0), new Rectangle(BackgroundTexture.Width() - BorderWidth, 0, BorderWidth, BorderWidth), color, 0f, new Vector2(0), 1f, SpriteEffects.None, 1f);
            //BL Corner
            spriteBatch.Draw(Texture.Value, ScreenPos + new Vector2(0, Height.Pixels - BorderWidth), new Rectangle(0, BackgroundTexture.Height() - BorderWidth, BorderWidth, BorderWidth + 1), color, 0f, new Vector2(0), 1f, SpriteEffects.None, 1f);
            //BR Corner
            spriteBatch.Draw(Texture.Value, ScreenPos + new Vector2(Width.Pixels - BorderWidth, Height.Pixels - BorderWidth), new Rectangle(BackgroundTexture.Width() - BorderWidth, BackgroundTexture.Height() - BorderWidth, BorderWidth, BorderWidth + 1), color, 0f, new Vector2(0), 1f, SpriteEffects.None, 1f);

            //Top Side
            spriteBatch.Draw(Texture.Value, ScreenPos + new Vector2(BorderWidth, 0), new Rectangle(BorderWidth, 0, BackgroundTexture.Width() - BorderWidth * 2, BorderWidth), color, 0f, new Vector2(0), new Vector2((Width.Pixels - BorderWidth * 2) / (BackgroundTexture.Width() - BorderWidth * 2), 1f), SpriteEffects.None, 1f);
            //Left Side
            spriteBatch.Draw(Texture.Value, ScreenPos + new Vector2(0, BorderWidth), new Rectangle(0, BorderWidth, BorderWidth, BackgroundTexture.Height() - BorderWidth * 2), color, 0f, new Vector2(0), new Vector2(1f, (Height.Pixels - BorderWidth * 2) / (BackgroundTexture.Height() - BorderWidth * 2)), SpriteEffects.None, 1f);
            //Right Side
            spriteBatch.Draw(Texture.Value, ScreenPos + new Vector2(Width.Pixels - BorderWidth, BorderWidth), new Rectangle(BackgroundTexture.Width() - BorderWidth, BorderWidth, BorderWidth, BackgroundTexture.Height() - BorderWidth * 2), color, 0f, new Vector2(0), new Vector2(1f, (Height.Pixels - BorderWidth * 2) / (BackgroundTexture.Height() - BorderWidth * 2)), SpriteEffects.None, 1f);
            //Bottom Side
            spriteBatch.Draw(Texture.Value, ScreenPos + new Vector2(BorderWidth, Height.Pixels - BorderWidth), new Rectangle(BorderWidth, BackgroundTexture.Height() - BorderWidth, BackgroundTexture.Width() - BorderWidth * 2, BorderWidth + 1), color, 0f, new Vector2(0), new Vector2((Width.Pixels - BorderWidth * 2) / (BackgroundTexture.Width() - BorderWidth * 2), 1f), SpriteEffects.None, 1f);

            //Center Fill
            spriteBatch.Draw(Texture.Value, ScreenPos + new Vector2(BorderWidth), new Rectangle(BorderWidth, BorderWidth, BackgroundTexture.Width() - BorderWidth * 2, BackgroundTexture.Height() - BorderWidth * 2), color, 0f, new Vector2(0), new Vector2((Width.Pixels - BorderWidth * 2) / (BackgroundTexture.Width() - BorderWidth * 2), (Height.Pixels - BorderWidth * 2) / (BackgroundTexture.Height() - BorderWidth * 2)), SpriteEffects.None, 1f);
        }
    }
}
