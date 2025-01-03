using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;

namespace Terrafirma.Particles
{
    internal class ChlorophyteStyleLaserSegment : Particle
    {
        public float Rotation;
        public float Scale;
        public override void OnSpawn()
        {
            
        }
        public override void Update()
        {
            position += velocity;
            Scale += TimeInWorld < 5? 0.1f : -0.1f;
            if (Scale < 0.05f)
            {
                Active = false;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D Sparkle = TextureAssets.Extra[89].Value;
            spriteBatch.Draw(Sparkle, position - Main.screenPosition, null, color, Rotation + MathHelper.PiOver2, Sparkle.Size() / 2, new Vector2(Scale,0.8f), SpriteEffects.None, 0);
           
        }
        public override void DrawInUI(SpriteBatch spriteBatch, Vector2 linePos)
        {
            Texture2D Sparkle = TextureAssets.Extra[89].Value;
        }
    }
}
