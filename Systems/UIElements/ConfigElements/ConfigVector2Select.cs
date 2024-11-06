using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using ReLogic.Content;
using SteelSeries.GameSense;
using System;
using System.Linq;
using Terrafirma.Common;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.UI.States;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.Config.UI;
using Terraria.UI;

public struct ConfigSnapPoint
{
    public Vector2 percentPoint;
    public Vector2 setOffset = Vector2.Zero;
    public Color color;
    public Vector2 scale;

    public ConfigSnapPoint()
    {
        percentPoint = Vector2.Zero;
        scale = new Vector2(20, 20);
        color = Color.White;
    }

    public ConfigSnapPoint(Vector2 percPoint)
    {
        percentPoint = percPoint;
        scale = new Vector2(20, 20);
        color = Color.White;
    }

    public ConfigSnapPoint(Vector2 percPoint, Vector2 offset)
    {
        setOffset = offset;
        percentPoint = percPoint;
        scale = new Vector2(20, 20);
        color = Color.White;
    }
}

namespace Terrafirma.Systems.UIElements.ConfigElements
{

    // This custom config UI element uses vanilla config elements paired with custom drawing.
    public abstract class ConfigVector2Select : ConfigElement
    {
        // This custom config UI element uses vanilla config elements paired with custom drawing.

        private Rectangle uiBounds;
        private Color selectionColor = Color.White;
        private float selectionScale = 1f;
        private bool open = false;

        public abstract ref Vector2 SetPos { get; }

        private float screenScale = 0.05f;
        public virtual float minScreenScale => 0.05f;
        public virtual float maxScreenScale => 0.276f;

        public virtual Vector2 maxScreenOffset => new Vector2(0, 20);

        //Add ConfigSnapPoints to this array if you want snapPoints to appear in the config Element
        public ConfigSnapPoint[] snapPoints = new ConfigSnapPoint[] { };        
        public virtual Texture2D selectionTexture => Terrafirma.ExtraSpellUIConfigPosition.Value;
        public ConfigVector2Select()
        {
            MinHeight.Set(Main.screenHeight * maxScreenScale + 20 + maxScreenOffset.Y, 0);
            snapPoints = new ConfigSnapPoint[]
            {
                new ConfigSnapPoint(new Vector2(0.8f,0f), new Vector2(0,0)),
                new ConfigSnapPoint(new Vector2(0.5f,0.5f), new Vector2(0,15)),
                new ConfigSnapPoint(new Vector2(0f,1f), new Vector2(20,-8))
            };

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 pos = new Vector2(GetInnerDimensions().ToRectangle().Right - Main.screenWidth * screenScale, GetInnerDimensions().Y) + new Vector2(-10, 10) + (maxScreenOffset * (screenScale / maxScreenScale));
            uiBounds = new Rectangle((int)pos.X, (int)pos.Y, (int)(Main.screenWidth * screenScale), (int)(Main.screenHeight * screenScale));

            base.Draw(spriteBatch);
            spriteBatch.Draw(Main.screenTarget,
                pos,
                new Rectangle(0, 0, Main.screenWidth, Main.screenHeight),
                Color.White,
                0f,
                Vector2.Zero,
                screenScale,
                SpriteEffects.None,
                1f);
            spriteBatch.Draw(selectionTexture,
                pos + ModContent.GetInstance<ClientConfig>().ExtraSpellUiPosition * screenScale,
                Terrafirma.ExtraSpellUIConfigPosition.Frame(),
                selectionColor,
                0f,
                Terrafirma.ExtraSpellUIConfigPosition.Size() / 2,
                selectionScale,
                SpriteEffects.None,
                1f);


            for (int i = 0; i < snapPoints.Length; i++)
            {
                Rectangle selectionbounds = GetSnapPointRect(pos, snapPoints[i].percentPoint);

                Draw9Patch(spriteBatch,
                selectionbounds,
                Terrafirma.BasicSelectionBox,
                8,
                snapPoints[i].color);
            }

            Draw9Patch(spriteBatch,
                new Rectangle(uiBounds.X - 4, uiBounds.Y - 4, uiBounds.Width + 8, uiBounds.Height + 8) ,
                Terrafirma.BasicBorder,
                8,
                Color.White);
            
        }

        public Rectangle GetSnapPointRect(Vector2 topleft, Vector2 position)
        {
            int scale = (int)Math.Clamp((screenScale / maxScreenScale) * 20, 10, 1000);
            return new Rectangle(
                (int)Math.Clamp(topleft.X + position.X * uiBounds.Width - scale/2, topleft.X, topleft.X + uiBounds.Width - scale),
                (int)Math.Clamp(topleft.Y + position.Y * uiBounds.Height - scale / 2, topleft.Y, topleft.Y + uiBounds.Height - scale),
                scale,
                scale);
        }

        public void Draw9Patch(SpriteBatch spriteBatch, Rectangle bounds, Asset<Texture2D> Texture, int BorderWidth, Color color)
        {

            //TL Corner
            spriteBatch.Draw(Texture.Value, bounds.TopLeft(), new Rectangle(0, 0, BorderWidth, BorderWidth), color, 0f, new Vector2(0), 1f, SpriteEffects.None, 1f);
            //TR Corner
            spriteBatch.Draw(Texture.Value, bounds.TopLeft() + new Vector2(bounds.Width - BorderWidth, 0), new Rectangle(Texture.Width() - BorderWidth, 0, BorderWidth, BorderWidth), color, 0f, new Vector2(0), 1f, SpriteEffects.None, 1f);
            //BL Corner
            spriteBatch.Draw(Texture.Value, bounds.TopLeft() + new Vector2(0, bounds.Height - BorderWidth), new Rectangle(0, Texture.Height() - BorderWidth, BorderWidth, BorderWidth + 1), color, 0f, new Vector2(0), 1f, SpriteEffects.None, 1f);
            //BR Corner
            spriteBatch.Draw(Texture.Value, bounds.TopLeft() + new Vector2(bounds.Width - BorderWidth, bounds.Height - BorderWidth), new Rectangle(Texture.Width() - BorderWidth, Texture.Height() - BorderWidth, BorderWidth, BorderWidth + 1), color, 0f, new Vector2(0), 1f, SpriteEffects.None, 1f);

            //Top Side
            spriteBatch.Draw(Texture.Value, bounds.TopLeft() + new Vector2(BorderWidth, 0), new Rectangle(BorderWidth, 0, Texture.Width() - BorderWidth * 2, BorderWidth), color, 0f, new Vector2(0), new Vector2((bounds.Width - BorderWidth * 2f) / (Texture.Width() - BorderWidth * 2f), 1f), SpriteEffects.None, 1f);
            //Left Side
            spriteBatch.Draw(Texture.Value, bounds.TopLeft() + new Vector2(0, BorderWidth), new Rectangle(0, BorderWidth, BorderWidth, Texture.Height() - BorderWidth * 2), color, 0f, new Vector2(0), new Vector2(1f, (bounds.Height - BorderWidth * 2f) / (Texture.Height() - BorderWidth * 2f)), SpriteEffects.None, 1f);
            //Right Side
            spriteBatch.Draw(Texture.Value, bounds.TopLeft() + new Vector2(bounds.Width - BorderWidth, BorderWidth), new Rectangle(Texture.Width() - BorderWidth, BorderWidth, BorderWidth, Texture.Height() - BorderWidth * 2), color, 0f, new Vector2(0), new Vector2(1f, (bounds.Height - BorderWidth * 2f) / (Texture.Height() - BorderWidth * 2f)), SpriteEffects.None, 1f);
            //Bottom Side
            spriteBatch.Draw(Texture.Value, bounds.TopLeft() + new Vector2(BorderWidth, bounds.Height - BorderWidth), new Rectangle(BorderWidth, Texture.Height() - BorderWidth, Texture.Width() - BorderWidth * 2, BorderWidth + 1), color, 0f, new Vector2(0), new Vector2((bounds.Width - BorderWidth * 2f) / (Texture.Width() - BorderWidth * 2f), 1f), SpriteEffects.None, 1f);

            //Center Fill
            spriteBatch.Draw(Texture.Value, bounds.TopLeft() + new Vector2(BorderWidth), new Rectangle(BorderWidth, BorderWidth, Texture.Width() - BorderWidth * 2, Texture.Height() - BorderWidth * 2), color, 0f, new Vector2(0), new Vector2((bounds.Width - BorderWidth * 2) / (Texture.Width() - BorderWidth * 2f), (bounds.Height - BorderWidth * 2f) / (Texture.Height() - BorderWidth * 2)), SpriteEffects.None, 1f);
        }

        public override void Update(GameTime gameTime)
        {
            MinHeight.Set(Main.screenHeight * (screenScale / maxScreenScale) + 20 + (maxScreenOffset.Y * (screenScale / maxScreenScale)), 0);

            //if Mouse is inside element and also holding
            if (Main.mouseLeft && uiBounds.Contains(Main.MouseScreen.ToPoint()))
            {
                bool snapPointClicked = false;

                for (int i = 0; i < snapPoints.Length; i++)
                {
                    ConfigSnapPoint point = snapPoints[i];
                    Rectangle selectionbounds = GetSnapPointRect(uiBounds.Location.ToVector2(), point.percentPoint);

                    if (selectionbounds.Contains(Main.MouseScreen.ToPoint()))
                    {
                        SetPos = (point.percentPoint * uiBounds.Size() + point.setOffset) / screenScale;
                        SetObject((point.percentPoint * uiBounds.Size() + point.setOffset) / screenScale);
                        snapPointClicked = true;
                    }
                }

                if (!snapPointClicked)
                {
                    SetPos = (Main.MouseScreen - uiBounds.TopLeft()) / screenScale;
                    SetObject((Main.MouseScreen - uiBounds.TopLeft()) / screenScale);
                }
                selectionScale = float.Lerp(selectionScale, 0.7f, 0.05f);
            }
            else
            {
                selectionScale = float.Lerp(selectionScale, 0.5f, 0.05f);
            }

            //Set SnapPoint Color
            if (GetInnerDimensions().ToRectangle().Contains(Main.MouseScreen.ToPoint()))
            {
                for (int i = 0; i < snapPoints.Length; i++)
                {
                    Rectangle selectionbounds = GetSnapPointRect(uiBounds.Location.ToVector2(), snapPoints[i].percentPoint);
                    if (selectionbounds.Contains(Main.MouseScreen.ToPoint())) snapPoints[i].color = Color.Lerp(snapPoints[i].color, Color.White, 0.05f);
                    else snapPoints[i].color = Color.Lerp(snapPoints[i].color, Color.White * 0.5f, 0.05f);

                }
            }
            else
            {
                for (int i = 0; i < snapPoints.Length; i++)
                {
                    snapPoints[i].color = Color.Lerp(snapPoints[i].color, Color.White * 0.02f, 0.05f);
                }
            }

            //If mouse is over screen or outside of the element
            if (uiBounds.Contains(Main.MouseScreen.ToPoint()) || !GetInnerDimensions().ToRectangle().Contains(Main.MouseScreen.ToPoint()))
            {
                selectionColor = Color.Lerp(selectionColor, Color.White, 0.05f);

            }
            else
            {
                selectionColor = Color.Lerp(selectionColor, Color.White * 0.5f, 0.05f);
            }

            //If mouse is inside element
            if (GetInnerDimensions().ToRectangle().Contains(Main.MouseScreen.ToPoint()) && Main.mouseLeft)
            {
                open = true;
                SoundEngine.PlaySound(SoundID.MenuTick);
            }
            else if (!GetInnerDimensions().ToRectangle().Contains(Main.MouseScreen.ToPoint()) && Main.mouseLeft)
            {
                open = false;
                SoundEngine.PlaySound(SoundID.MenuClose);
            }

            if (open) screenScale = float.Lerp(screenScale, maxScreenScale, 0.2f);
            else screenScale = float.Lerp(screenScale, minScreenScale, 0.2f);

            base.Update(gameTime);
        }
    }
}
