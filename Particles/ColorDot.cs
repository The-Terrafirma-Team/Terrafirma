using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System;

namespace Terrafirma.Particles
{
    public class ColorDot : Particle
    {
        public override void Update()
        {
            ai2 += Main.rand.NextFloat(3);
            Velocity = Velocity.RotatedBy(Math.Sin((ai2 * 0.1f) - MathHelper.PiOver2) * 0.05f) * Main.rand.NextFloat(0.8f,1.1f);
            Velocity.Y += 0.01f;

            Position += Velocity;

            float scale = Velocity.Length() / 8f;
            if(scale <= 0.01f)
            {
                Active = false;
            }
            if(TimeInWorld > 60)
            {
                Velocity *= 0.9f;
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D texture = (Texture2D)ModContent.Request<Texture2D>(Terrafirma.AssetPath + "Particles/GlowCircle");
            Rectangle frame = new Rectangle(0, 0, texture.Width, texture.Height);
            Vector2 frameOrigin = new Vector2(texture.Width) / 2;
            Vector2 DrawPos = Position - Main.screenPosition;

            float scale = Velocity.Length() / 1f;
            if (scale > 3)
                scale = 3;
            float opacityMulti = MathHelper.Min(TimeInWorld / 40f, 1);

            spriteBatch.Draw(texture, DrawPos, frame, Color * MathHelper.Min(scale * 0.4f, 1) * opacityMulti, Velocity.ToRotation() + MathHelper.PiOver2, frameOrigin, new Vector2(MathHelper.Min(scale, 1), scale) * ai1, SpriteEffects.None, 0);

            spriteBatch.Draw(texture, DrawPos, frame, new Color(255, 255, 255, 0) * MathHelper.Min(scale * 0.4f, 1) * opacityMulti, Velocity.ToRotation() + MathHelper.PiOver2, frameOrigin, new Vector2(MathHelper.Min(scale,1),scale) * 0.8f * ai1, SpriteEffects.None, 0);
        }
    }
}
