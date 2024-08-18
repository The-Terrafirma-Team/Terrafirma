using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Terrafirma.Particles
{
    public class LuckyDiceNumber : Particle
    {
        private static Asset<Texture2D> BaseTex;
        public override void Load()
        {
            BaseTex = ModContent.Request<Texture2D>("Terrafirma/Assets/SpriteFonts/LuckyDiceNumbers");
        }

        public override void Update()
        {
            if (timer > 60)
            {
                position.Y -= 0.15f;
                opacity *= 0.96f;
            }

            timer++;
        }

        int timer = 0;
        float opacity = 1f;
        float scale = 1f;
        public int displayNumber = 0;

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BaseTex.Value,
                position - Main.screenPosition,
                new Rectangle(2 + (34 * displayNumber), 3, 28, 43),
                color * opacity * 0.5f,
                0f,
                new Vector2(14, 21.5f),
                scale + (float)((Math.Sin(Main.timeForVisualEffects / 15f) + 1f) / 10f),
                SpriteEffects.None,
                0f);
            spriteBatch.Draw(BaseTex.Value,
                position - Main.screenPosition,
                new Rectangle(2 + (34*displayNumber),3,28,43),
                color * opacity * 0.8f,
                0f,
                new Vector2(14,21.5f),
                scale,
                SpriteEffects.None,
                0f);
        }
    }
}
