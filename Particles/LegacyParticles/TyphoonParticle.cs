using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace Terrafirma.Particles.LegacyParticles
{
    internal class TyphoonParticle : LegacyParticle
    {
        public override void OnSpawn()
        {

        }
        public override void Update()
        {
            Scale *= 0.9f;
            Rotation = Velocity.ToRotation();
            Position += Velocity;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D Typhoon = ModContent.Request<Texture2D>("Terrafirma/Particles/SquishedTyphoonDust").Value;
            spriteBatch.Draw(Typhoon, Position - Main.screenPosition - Velocity * 3f, new Rectangle(0, 0, Typhoon.Width, Typhoon.Height), Color * (Scale * 2f), Rotation, Typhoon.Size() / 2, Scale * 0.5f, SpriteEffects.None, 0);
            spriteBatch.Draw(Typhoon, Position - Main.screenPosition, new Rectangle(0, 0, Typhoon.Width, Typhoon.Height), Color * Scale, Rotation, Typhoon.Size() / 2, Scale, SpriteEffects.None, 0);
            spriteBatch.Draw(Typhoon, Position - Main.screenPosition + Velocity * 3f, new Rectangle(0, 0, Typhoon.Width, Typhoon.Height), Color * (Scale / 2f), Rotation, Typhoon.Size() / 2, Scale * 2f, SpriteEffects.None, 0);
        }
    }
}
