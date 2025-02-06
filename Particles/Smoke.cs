using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;
using ReLogic.Content;
using Microsoft.Xna.Framework;

namespace Terrafirma.Particles
{
    public class Smoke : Particle
    {
        public float Scale;
        public float rotation = 0f;
        public Color secondaryColor = Color.SkyBlue;
        bool fade = false;
        float opacity = 1f;
        public bool AffectedByLight = true;
        int Frame = Main.rand.Next(3);

        private static Asset<Texture2D> Tex;
        float Size = 0f;
        public override void Load()
        {
            Tex = ModContent.Request<Texture2D>(Terrafirma.AssetPath + "Particles/Smoke");
        }
        public override void Update()
        {
            if(Size < 1f && !fade)
            {
                Size += Main.rand.NextFloat(0.05f,0.1f);
            }
            else if(!fade)
            {
                fade = true;
            }
            else
            {
                Size -= 0.005f;
                opacity -= 0.01f;
            }
            if (opacity < 0 || Size < 0)
                Active = false;

            position += velocity;
            velocity *= 0.96f;
            rotation += velocity.X * 0.1f;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!AffectedByLight)
            {
                spriteBatch.Draw(Tex.Value, position - Main.screenPosition, Tex.Frame(2, 3, 0, Frame), secondaryColor * opacity, rotation, new Vector2(19), Scale * Size, SpriteEffects.None, 0);
                spriteBatch.Draw(Tex.Value, position - Main.screenPosition, Tex.Frame(2, 3, 1, Frame), color * opacity, rotation, new Vector2(19), Scale * Size, SpriteEffects.None, 0);
            }
            else
            {
                Vector3 vect = Lighting.GetSubLight(position);
                spriteBatch.Draw(Tex.Value, position - Main.screenPosition, Tex.Frame(2, 3, 0, Frame), (secondaryColor.ToVector3() * vect).ToColor() with { A = secondaryColor.A} * opacity,rotation, new Vector2(19), Scale * Size, SpriteEffects.None, 0);
                spriteBatch.Draw(Tex.Value, position - Main.screenPosition, Tex.Frame(2, 3, 1, Frame), (color.ToVector3() * vect).ToColor() with { A = color.A } * opacity, rotation, new Vector2(19), Scale * Size, SpriteEffects.None, 0);
            }
        }
    }
}
