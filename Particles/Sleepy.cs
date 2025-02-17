using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;
using ReLogic.Content;
using Microsoft.Xna.Framework;
using Terraria.GameContent;
using ReLogic.Graphics;

namespace Terrafirma.Particles
{
    public class Sleepy : Particle
    {
        //private static Asset<Texture2D> Tex;
        float opacity = 0f;
        float scale = Main.rand.NextFloat(0.5f, 1f);
        float movement = Main.rand.NextFloat(-1f, 1f);
        //public override void Load()
        //{
        //    Tex = ModContent.Request<Texture2D>(Terrafirma.AssetPath + "Particles/Sleepy");
        //}
        public override void Update()
        {
            scale += 0.5f / 30f;
            if(TimeInWorld > 20)
            {
                opacity -= 0.04f;
            }
            else if(TimeInWorld < 5)
            {
                opacity += 1f / 5f;
            }
            if (opacity < 0)
                Active = false;

            position += velocity;
            velocity.X -= movement * 0.01f;
            velocity.Y += -0.04f;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            for(int i = 0; i < 8; i++)
            {
                spriteBatch.DrawString(FontAssets.DeathText.Value, "Z", position - Main.screenPosition + new Vector2(0,2).RotatedBy(i * MathHelper.PiOver4), new Color(0.2f,0.3f,0.8f) * opacity, 0.3f - TimeInWorld / 90f, new Vector2(4, 10) / 0.4f, scale * 0.4f, SpriteEffects.None, 0);
            }
            spriteBatch.DrawString(FontAssets.DeathText.Value, "Z", position - Main.screenPosition, Color.White * opacity, 0.3f - TimeInWorld / 90f, new Vector2(4,10) / 0.4f, scale * 0.4f, SpriteEffects.None, 0);
            //spriteBatch.Draw(Tex.Value, position - Main.screenPosition, null, Color.White * opacity, 0.3f - TimeInWorld / 90f, Tex.Size() / 2, 1f, SpriteEffects.None, 0); 
        }
    }
}
