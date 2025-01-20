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
        float RandomTimer;
        public float Size;
        public Color secondaryColor = new Color(255, 255, 255, 0);
        public float Waviness = 0.05f;
        public float gravity = 0.01f;
        public float fadeOut = 0.9f;
        public override void OnSpawn()
        {
            RandomTimer = Main.rand.NextFloat(MathHelper.TwoPi);
        }
        public override void Update()
        {
            RandomTimer += Main.rand.NextFloat(0.3f);
            velocity = velocity.RotatedBy(Math.Sin(RandomTimer - MathHelper.PiOver4) * Waviness) * Main.rand.NextFloat(0.8f, 1.1f);
            velocity.Y += gravity;

            position += velocity;

            float scale = velocity.Length() / 8f;
            if (scale <= 0.03f)
            {
                Active = false;
            }
            if (TimeInWorld > 60)
            {
                velocity *= fadeOut;
                gravity *= fadeOut;
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

            spriteBatch.Draw(texture.Value, DrawPos, null, secondaryColor * MathHelper.Min(scale * 0.4f, 1) * opacityMulti, velocity.ToRotation() + MathHelper.PiOver2, frameOrigin, new Vector2(MathHelper.Min(scale, 1), scale) * 0.8f * Size, SpriteEffects.None, 0);
        }
    }
}
