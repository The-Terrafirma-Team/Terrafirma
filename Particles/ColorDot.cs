using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System;
using ReLogic.Content;

namespace Terrafirma.Particles
{
    public class ColorDot : Particle
    {
        private static Asset<Texture2D> texture;
        float ai2;
        public float Size;
        public override void Update()
        {
            ai2 += Main.rand.NextFloat(3);
            velocity = velocity.RotatedBy(Math.Sin(ai2 * 0.1f - MathHelper.PiOver2) * 0.05f) * Main.rand.NextFloat(0.8f, 1.1f);
            velocity.Y += 0.01f;

            position += velocity;

            float scale = velocity.Length() / 8f;
            if (scale <= 0.01f)
            {
                Active = false;
            }
            if (TimeInWorld > 60)
            {
                velocity *= 0.9f;
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (texture == null)
                texture = ModContent.Request<Texture2D>(Terrafirma.AssetPath + "Particles/GlowCircle");

            Vector2 frameOrigin = texture.Size() / 2;
            Vector2 DrawPos = position - Main.screenPosition;

            float scale = velocity.Length() / 1f;
            if (scale > 3)
                scale = 3;
            float opacityMulti = MathHelper.Min(TimeInWorld / 40f, 1);

            spriteBatch.Draw(texture.Value, DrawPos, null, color * MathHelper.Min(scale * 0.4f, 1) * opacityMulti, velocity.ToRotation() + MathHelper.PiOver2, frameOrigin, new Vector2(MathHelper.Min(scale, 1), scale) * Size, SpriteEffects.None, 0);

            spriteBatch.Draw(texture.Value, DrawPos, null, new Color(255, 255, 255, 0) * MathHelper.Min(scale * 0.4f, 1) * opacityMulti, velocity.ToRotation() + MathHelper.PiOver2, frameOrigin, new Vector2(MathHelper.Min(scale, 1), scale) * 0.8f * Size, SpriteEffects.None, 0);
        }
    }
}
