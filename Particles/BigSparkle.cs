using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;

namespace Terrafirma.Particles
{
    internal class BigSparkle : Particle
    {
        public float Rotation;
        public float Scale;
        float sizeMultiplier;
        public float lengthMultiplier = 1f;
        public float fadeInTime;
        public float smallestSize = 0.1f;
        public int TimeLeft = -1;
        public float fadeOutMultiplier = 0.9f;
        public Color secondaryColor = new Color(255,255,255,0);
        public override void OnSpawn()
        {
            sizeMultiplier = 0.1f;
        }
        public override void Update()
        {
            //velocity = velocity.RotatedBy(0.01f);
            position += velocity;
            if (TimeInWorld < fadeInTime)
            {
                sizeMultiplier *= 1.3f;
                lengthMultiplier *= 1.05f;
            }
            else
            {
                sizeMultiplier *= fadeOutMultiplier;
                lengthMultiplier *= fadeOutMultiplier * 0.97f;
                color *= fadeOutMultiplier * 0.99f;
                secondaryColor *= fadeOutMultiplier * 0.98f;
            }

            if (sizeMultiplier < smallestSize)
            {
                Active = false;
            }

            if (TimeLeft > 0) TimeLeft--;
            else if (TimeLeft == 0) Active = false;
            {
                
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D Sparkle = TextureAssets.Extra[89].Value;
            spriteBatch.Draw(Sparkle, position - Main.screenPosition, new Rectangle(0, 0, Sparkle.Width, Sparkle.Height), color, Rotation, Sparkle.Size() / 2, new Vector2(sizeMultiplier, sizeMultiplier * lengthMultiplier) * Scale, SpriteEffects.None, 0);
            spriteBatch.Draw(Sparkle, position - Main.screenPosition, new Rectangle(0, 0, Sparkle.Width, Sparkle.Height), color, MathHelper.PiOver2 + Rotation, Sparkle.Size() / 2, new Vector2(sizeMultiplier, sizeMultiplier * lengthMultiplier) * Scale, SpriteEffects.None, 0);
            spriteBatch.Draw(Sparkle, position - Main.screenPosition, new Rectangle(0, 0, Sparkle.Width, Sparkle.Height), secondaryColor, Rotation, Sparkle.Size() / 2, new Vector2(sizeMultiplier, sizeMultiplier * lengthMultiplier) * 0.5f * Scale, SpriteEffects.None, 0);
            spriteBatch.Draw(Sparkle, position - Main.screenPosition, new Rectangle(0, 0, Sparkle.Width, Sparkle.Height), secondaryColor, MathHelper.PiOver2 + Rotation, Sparkle.Size() / 2, new Vector2(sizeMultiplier, sizeMultiplier * lengthMultiplier) * 0.5f * Scale, SpriteEffects.None, 0);
        }
        public override void DrawInUI(SpriteBatch spriteBatch, Vector2 linePos)
        {
            Texture2D Sparkle = TextureAssets.Extra[89].Value;
            spriteBatch.Draw(Sparkle, position + linePos , new Rectangle(0, 0, Sparkle.Width, Sparkle.Height), color, Rotation, Sparkle.Size() / 2, sizeMultiplier * Scale, SpriteEffects.None, 0);
            spriteBatch.Draw(Sparkle, position + linePos, new Rectangle(0, 0, Sparkle.Width, Sparkle.Height), color, MathHelper.PiOver2 + Rotation, Sparkle.Size() / 2, sizeMultiplier * Scale, SpriteEffects.None, 0);
            spriteBatch.Draw(Sparkle, position + linePos, new Rectangle(0, 0, Sparkle.Width, Sparkle.Height), new Color(1f, 1f, 1f, 0f), Rotation, Sparkle.Size() / 2, sizeMultiplier * 0.5f * Scale, SpriteEffects.None, 0);
            spriteBatch.Draw(Sparkle, position + linePos, new Rectangle(0, 0, Sparkle.Width, Sparkle.Height), new Color(1f, 1f, 1f, 0f), MathHelper.PiOver2 + Rotation, Sparkle.Size() / 2, sizeMultiplier * 0.5f * Scale, SpriteEffects.None, 0);
        }
    }
}
