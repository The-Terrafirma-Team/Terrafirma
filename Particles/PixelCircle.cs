using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace Terrafirma.Particles
{
    public class PixelCircle : Particle
    {
        private static Asset<Texture2D> tex;

        public Color outlineColor = Color.Black;
        public float scale;
        public float gravity = 0;
        public bool affectedByLight = false;
        public bool outlineAffectedByLight = false;
        public override void Update()
        {
            position += velocity;
            velocity *= 0.96f;
            velocity.Y += gravity;
            if (TimeInWorld > 60)
                Active = false;
            scale -= 0.1f;
            if (scale < 1)
                Active = false;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (tex == null)
                tex = ModContent.Request<Texture2D>(Terrafirma.AssetPath + "Particles/PixelCircle");

            Color drawColor = outlineColor;
            if (outlineAffectedByLight)
                drawColor = Lighting.GetColor((position / 16).ToPoint(), outlineColor);

            spriteBatch.Draw(tex.Value, (position / 2).ToPoint().ToVector2() * 2 - Main.screenPosition, new Rectangle(0, tex.Width() * (int)(scale), tex.Width(), tex.Width()), drawColor, 0,new Vector2(tex.Width() / 2),1,SpriteEffects.None,0);
        }
        public override void DrawAfter(SpriteBatch spriteBatch)
        {
            Color drawColor = color;
            if (affectedByLight)
                drawColor = Lighting.GetColor((position / 16).ToPoint(), color);
            spriteBatch.Draw(tex.Value, (position / 2).ToPoint().ToVector2() * 2 - Main.screenPosition, new Rectangle(0, tex.Width() * (int)(scale - 1), tex.Width(), tex.Width()), drawColor, 0, new Vector2(tex.Width() / 2), 1, SpriteEffects.None, 0);
        }
        public override bool HasPartsDrawnAfterOtherParticles => true;
    }
}
