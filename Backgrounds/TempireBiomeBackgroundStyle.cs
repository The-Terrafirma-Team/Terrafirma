using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Graphics.Capture;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using SubworldLibrary;
using Terrafirma.Subworlds.Tempire;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection;
using Terraria.GameContent;

namespace Terrafirma.Backgrounds
{
    internal class TempireBiomeBackgroundStyle : ModSurfaceBackgroundStyle
    {
        // Use this to keep far Backgrounds like the mountains.
        public override bool PreDrawCloseBackground(SpriteBatch spriteBatch)
        {
            Color ColorOfSurfaceBackgroundsModified = (Color)typeof(Main).GetField("ColorOfSurfaceBackgroundsModified", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);

            Texture2D Close = ModContent.Request<Texture2D>("Terrafirma/Backgrounds/TempireBiomeMidBackground1").Value;
            Texture2D Far = ModContent.Request<Texture2D>("Terrafirma/Backgrounds/TempireBiomeFarBackground1").Value;
            Texture2D Sky = ModContent.Request<Texture2D>("Terrafirma/Backgrounds/TempireSkyBack").Value;
            Texture2D Clouds = ModContent.Request<Texture2D>("Terrafirma/Backgrounds/TempireClouds").Value;
            Texture2D Fog = ModContent.Request<Texture2D>("Terrafirma/Backgrounds/TempireFog").Value;
            spriteBatch.Draw(Sky, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight),ColorOfSurfaceBackgroundsModified);
            //rawBackgroundLayer(Clouds, 0.07f, (int)(Main.timeForVisualEffects * 1f), 1350, spriteBatch, ColorOfSurfaceBackgroundsModified * 0.5f);
            DrawBackgroundLayer(Far, 0.08f, 0, 1450, spriteBatch, ColorOfSurfaceBackgroundsModified * 0.7f);
            DrawBackgroundLayer(Clouds, 0.1f, (int)(Main.timeForVisualEffects * 0.2f), 1450, spriteBatch, ColorOfSurfaceBackgroundsModified * 0.2f);
            DrawBackgroundLayer(Far, 0.15f, 0, 1450, spriteBatch, ColorOfSurfaceBackgroundsModified);
            DrawBackgroundLayer(Fog, 0.14f, (int)(Main.timeForVisualEffects * 0.4f), 1250, spriteBatch, ColorOfSurfaceBackgroundsModified * 0.3f);
            DrawBackgroundLayer(Close, 0.2f, 0, 1250, spriteBatch, ColorOfSurfaceBackgroundsModified);
            return false;
        }
        public void DrawBackgroundLayer(Texture2D tex, float Parallax, int XOffset, int YOffset, SpriteBatch spriteBatch, Color color)
        {
            Vector2 backgroundPos = new Vector2(MathHelper.Lerp(0, tex.Width, (-((Main.screenPosition.X + XOffset) * Parallax) % tex.Width * 2) / (float)tex.Width), -((Main.screenPosition.Y - (TempireSubworld.WorldHeight * 8) + YOffset) * Parallax * 1.5f));
            spriteBatch.Draw(tex, backgroundPos, new Rectangle(0, 0, tex.Width, tex.Height), color, 0, Vector2.Zero, 2, SpriteEffects.None, 0);
            spriteBatch.Draw(tex, backgroundPos + new Vector2(tex.Width * 2, 0), new Rectangle(0, 0, tex.Width, tex.Height), color, 0, Vector2.Zero, 2, SpriteEffects.None, 0);
        }
        public override void ModifyFarFades(float[] fades, float transitionSpeed)
        {
            for (int i = 0; i < fades.Length; i++)
            {
                if (i == Slot)
                {
                    fades[i] += transitionSpeed;
                    if (fades[i] > 1f)
                    {
                        fades[i] = 1f;
                    }
                }
                else
                {
                    fades[i] -= transitionSpeed;
                    if (fades[i] < 0f)
                    {
                        fades[i] = 0f;
                    }
                }
            }
        }
        public override int ChooseFarTexture()
        {
            return ModContent.GetModBackgroundSlot("Terrafirma/Backgrounds/TempireBiomeCloseBackground0");
        }
        public override int ChooseMiddleTexture()
        {
            return ModContent.GetModBackgroundSlot("Terrafirma/Backgrounds/TempireBiomeCloseBackground0");
        }
        public override int ChooseCloseTexture(ref float scale, ref double parallax, ref float a, ref float b)
        {
            return ModContent.GetModBackgroundSlot("Terrafirma/Backgrounds/TempireBiomeCloseBackground0");
        }
    }
}

