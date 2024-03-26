using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;

namespace Terrafirma.Particles.LegacyParticles
{
    internal class BigSparkle : LegacyParticle
    {
        public override void OnSpawn()
        {
            ai1 = 0.1f;
        }
        public override void Update()
        {
            if (TimeInWorld < ai2)
            {
                ai1 *= 1.3f;
            }
            else
            {
                ai1 *= 0.9f;
            }

            if (ai1 < 0.1f)
            {
                Active = false;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D Sparkle = TextureAssets.Extra[89].Value;
            spriteBatch.Draw(Sparkle, Position - Main.screenPosition, new Rectangle(0, 0, Sparkle.Width, Sparkle.Height), Color, 0 + Rotation, Sparkle.Size() / 2, ai1 * Scale, SpriteEffects.None, 0);
            spriteBatch.Draw(Sparkle, Position - Main.screenPosition, new Rectangle(0, 0, Sparkle.Width, Sparkle.Height), Color, MathHelper.PiOver2 + Rotation, Sparkle.Size() / 2, ai1 * Scale, SpriteEffects.None, 0);
            spriteBatch.Draw(Sparkle, Position - Main.screenPosition, new Rectangle(0, 0, Sparkle.Width, Sparkle.Height), new Color(1f, 1f, 1f, 0f), 0 + Rotation, Sparkle.Size() / 2, ai1 * 0.5f * Scale, SpriteEffects.None, 0);
            spriteBatch.Draw(Sparkle, Position - Main.screenPosition, new Rectangle(0, 0, Sparkle.Width, Sparkle.Height), new Color(1f, 1f, 1f, 0f), MathHelper.PiOver2 + Rotation, Sparkle.Size() / 2, ai1 * 0.5f * Scale, SpriteEffects.None, 0);
        }
    }
}
