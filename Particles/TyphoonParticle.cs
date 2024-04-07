using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace Terrafirma.Particles
{
    internal class TyphoonParticle : Particle
    {
        private static Asset<Texture2D> Typhoon;

        public float Scale;
        public float Rotation;
        public override void OnSpawn()
        {

        }
        public override void Update()
        {
            Scale *= 0.9f;
            Rotation = velocity.ToRotation();
            position += velocity;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Typhoon == null)
                Typhoon = ModContent.Request<Texture2D>(Terrafirma.AssetPath + "Particles/Typhoon");
            spriteBatch.Draw(Typhoon.Value, position - Main.screenPosition - velocity * 3f, null, color * (Scale * 2f), Rotation, Typhoon.Size() / 2, Scale * 0.5f, SpriteEffects.None, 0);
            spriteBatch.Draw(Typhoon.Value, position - Main.screenPosition, null, color * Scale, Rotation, Typhoon.Size() / 2, Scale, SpriteEffects.None, 0);
            spriteBatch.Draw(Typhoon.Value, position - Main.screenPosition + velocity * 3f, null, color * (Scale / 2f), Rotation, Typhoon.Size() / 2, Scale * 2f, SpriteEffects.None, 0);
        }
    }
}
