using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;
using ReLogic.Content;
using Microsoft.Xna.Framework;

namespace Terrafirma.Particles
{
    public class Shockwave : Particle
    {
        public Vector2 Scale = Vector2.One;
        public float rotation = 0f;
        private static Asset<Texture2D> Tex;
        float Size = 0f;
        float opacity = 1f;
        float upscaleRate = 0.1f;
        public override void Load()
        {
            Tex = ModContent.Request<Texture2D>(Terrafirma.AssetPath + "Particles/Shockwave");
        }
        public override void Update()
        {
            Size += upscaleRate;
            if(Size > 1)
            {
                upscaleRate *= 0.9f;
                opacity -= 0.05f;
            }
            if(opacity < 0f)
            {
                Active = false;
            }
            base.Update();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Tex.Value, position - Main.screenPosition, null, color * opacity, rotation, Tex.Size() / 2, Scale * Size, SpriteEffects.None, 0); 
        }
    }
}
