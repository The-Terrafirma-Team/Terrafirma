using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;

namespace Terrafirma.Particles
{
    internal class ImpactSparkle : Particle
    {
        float Rotation;
        public float Scale;
        public int LifeTime;
        Vector2 size;
        public Color secondaryColor;
        public override void Update()
        {
            size.Y = velocity.Length() * 0.4f;
            size.X = MathHelper.Lerp(1,0,Easing.OutPow((float)TimeInWorld / LifeTime,2));

            Rotation = velocity.ToRotation();
            position += velocity;
            velocity *= 0.95f;

            if(TimeInWorld > LifeTime)
            {
                Active = false;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D Sparkle = TextureAssets.Extra[89].Value;
            spriteBatch.Draw(Sparkle, position - Main.screenPosition, new Rectangle(0, 0, Sparkle.Width, Sparkle.Height), color * size.X, Rotation + MathHelper.PiOver2, new Vector2(36,56), size * Scale, SpriteEffects.None, 0);
            spriteBatch.Draw(Sparkle, position - Main.screenPosition, new Rectangle(0, 0, Sparkle.Width, Sparkle.Height), secondaryColor * size.X, Rotation + MathHelper.PiOver2, new Vector2(36, 56), size * Scale, SpriteEffects.None, 0);
        }
    }
}
