using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;
using Terrafirma.Common;
using Terraria;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config.UI;

namespace Terrafirma.Systems.UIElements.ConfigElements
{
    public struct ConfigSpellBorderButton
    {
        public Vector2 position;
        public int index;
        public Color color;

        public ConfigSpellBorderButton(int indx)
        {
            position = Vector2.Zero;
            index = indx;
            color = new Color(44, 57, 105, 255);
        }

        public ConfigSpellBorderButton(Vector2 pos, int indx)
        {
            position = pos;
            index = indx;
            color = new Color(44, 57, 105, 255);
        }

        public bool ContainsPoint(Vector2 pos)
        {
            Rectangle hitbox = new Rectangle((int)position.X - Terrafirma.SpellBorderSelectionBG.Width()/2, (int)position.Y - Terrafirma.SpellBorderSelectionBG.Height()/2, Terrafirma.SpellBorderSelectionBG.Width(), Terrafirma.SpellBorderSelectionBG.Height());
            return hitbox.Contains(pos.ToPoint());
        }
    }
    internal class ConfigSpellBorderSelector : ConfigElement
    {
        public int borderID = 0;
        private float pullBack = 0f;
        private bool open = false;

        private Color flash = Color.White;
        private float flashTimer = 0f;
        private bool clickSwitch = false;

        public ConfigSpellBorderButton[] borderButtons = new ConfigSpellBorderButton[] { };
        public ConfigSpellBorderSelector()
        {          
            MinHeight.Set(1000, 0);
            flash = new Color(0.4f,0.4f,0.8f,1f);
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Terrafirma.SpellBorders.Width() / (int)Terrafirma.SpellBorders.Height(); i++)
            {
                float width = (Terrafirma.SpellBorderSelectionBG.Width() + 5) * i + (Terrafirma.SpellBorderSelectionBG.Width() / 2f + 7);
                float height = Terrafirma.SpellBorderSelectionBG.Height() + Terrafirma.SpellBorderSelectionBG.Height() / 2f + 15;

                float realwidth = width - (((Terrafirma.SpellBorderSelectionBG.Width() + 5) * 7) * (i / 7));
                float realheight = height + ((Terrafirma.SpellBorderSelectionBG.Height() + 5) * (i / 7));

                float multipliedheight = ((realheight - height) * pullBack) + height * pullBack + (Terrafirma.SpellBorderSelectionBG.Height() / 2f * (1 - pullBack));

                if (borderButtons.Length > i) borderButtons[i].position = GetInnerDimensions().ToRectangle().TopLeft() + new Vector2(realwidth, multipliedheight);
                else borderButtons = borderButtons.Append(new ConfigSpellBorderButton(GetInnerDimensions().ToRectangle().TopLeft() + new Vector2(realwidth, multipliedheight), i)).ToArray();
            }

            base.Draw(spriteBatch);

            spriteBatch.Draw(Terrafirma.SpellBorderSelectionBG.Value,
                    GetInnerDimensions().ToRectangle().TopRight() - new Vector2(Terrafirma.SpellBorderSelectionBG.Width() / 2, -Terrafirma.SpellBorderSelectionBG.Height() / 2) - new Vector2(10, -10),
                    Terrafirma.SpellBorderSelectionBG.Frame(),
                    Color.Lerp(new Color(44, 57, 105, 255), flash, flashTimer),
                    0f,
                    Terrafirma.SpellBorderSelectionBG.Size() / 2,
                    1f,
                    SpriteEffects.None,
                    1f);
            spriteBatch.Draw(Terrafirma.SpellBordersGlow.Value,
                    GetInnerDimensions().ToRectangle().TopRight() - new Vector2(Terrafirma.SpellBorderSelectionBG.Width() / 2, -Terrafirma.SpellBorderSelectionBG.Height() / 2) - new Vector2(10, -10),
                    Terrafirma.SpellBordersGlow.Frame(),
                    ModContent.GetInstance<ClientConfig>().SpellRingUiColor with { A = 0 } * 0.5f,
                    0f,
                    Terrafirma.SpellBordersGlow.Size() / 2f,
                    1f,
                    SpriteEffects.None,
                    1f);
            spriteBatch.Draw(Terrafirma.SpellBorders.Value,
                    GetInnerDimensions().ToRectangle().TopRight() - new Vector2(Terrafirma.SpellBorderSelectionBG.Width() / 2, -Terrafirma.SpellBorderSelectionBG.Height() / 2) - new Vector2(10, -10),
                    new Rectangle(ModContent.GetInstance<ClientConfig>().SpellBorder * (Terrafirma.SpellBorders.Width() / borderButtons.Length), 0, Terrafirma.SpellBorders.Height(), Terrafirma.SpellBorders.Height()),
                    Color.White,
                    0f,
                    new Vector2(Terrafirma.SpellBorders.Height(), Terrafirma.SpellBorders.Height()) / 2f,
                    1f,
                    SpriteEffects.None,
                    1f);

            for (int i = 0; i < borderButtons.Length; i++)
            {
                if (pullBack == 0f) break;
                spriteBatch.Draw(Terrafirma.SpellBorderSelectionBG.Value,
                        borderButtons[i].position,
                        Terrafirma.SpellBorderSelectionBG.Frame(),
                        borderButtons[i].index == ModContent.GetInstance<ClientConfig>().SpellBorder ? Main.OurFavoriteColor * pullBack : borderButtons[i].color * pullBack,
                        0f,
                        Terrafirma.SpellBorderSelectionBG.Size() / 2,
                        1f,
                        SpriteEffects.None,
                        1f);
                spriteBatch.Draw(Terrafirma.SpellBordersGlow.Value,
                        borderButtons[i].position,
                        Terrafirma.SpellBordersGlow.Frame(),
                        ModContent.GetInstance<ClientConfig>().SpellRingUiColor with { A = 0 } * 0.5f * pullBack,
                        0f,
                        Terrafirma.SpellBordersGlow.Size() / 2f,
                        1f,
                        SpriteEffects.None,
                        1f);
                spriteBatch.Draw(Terrafirma.SpellBorders.Value,
                        borderButtons[i].position,
                        new Rectangle(borderButtons[i].index * (Terrafirma.SpellBorders.Width() / borderButtons.Length), 0, Terrafirma.SpellBorders.Height(), Terrafirma.SpellBorders.Height()),
                        Color.White * pullBack,
                        0f,
                        new Vector2(Terrafirma.SpellBorders.Height(), Terrafirma.SpellBorders.Height()) / 2f,
                        1f,
                        SpriteEffects.None,
                        1f);
            }
        }

        public override void Update(GameTime gameTime)
        {
            float rowHeight = (Terrafirma.SpellBorderSelectionBG.Height() + 7);
            int rows = (borderButtons.Length / 7) + 1;
            MinHeight.Set(Terrafirma.SpellBorderSelectionBG.Height() + 15 + ((rowHeight * rows) * pullBack), 0);

            //Open UI Mechanic
            if (GetInnerDimensions().ToRectangle().Contains(Main.MouseScreen.ToPoint()) && Main.mouseLeft)
            {
                open = true;
            }
            else if (!GetInnerDimensions().ToRectangle().Contains(Main.MouseScreen.ToPoint()) && Main.mouseLeft)
            {
                open = false;
            }

            //Open UI Update
            if (open)
            {
                pullBack = float.Lerp(pullBack, 1f, 0.2f);
            }
            else
            {
                pullBack = float.Lerp(pullBack, 0f, 0.2f);
            }

            //Update Buttons
            for (int i = 0; i < borderButtons.Length; i++)
            {
                if (borderButtons[i].ContainsPoint(Main.MouseScreen))
                {
                    borderButtons[i].color = Main.OurFavoriteColor;
                    if (PlayerInput.Triggers.JustPressed.MouseLeft && pullBack >= 0.9f)
                    {
                        borderID = borderButtons[i].index;
                        ModContent.GetInstance<ClientConfig>().SpellBorder = borderID;
                        SetObject(borderID);
                        flashTimer = 1f;
                    }
                }
                else borderButtons[i].color = new Color(44, 57, 105, 255);
            }

            flashTimer -= 0.05f;

            base.Update(gameTime);
        }
    }
}
