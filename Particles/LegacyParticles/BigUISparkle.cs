﻿using Microsoft.Xna.Framework;
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
    internal class BigUISparkle : LegacyParticle
    {
        public override void OnSpawn()
        {
            if (ai1 == 0) ai1 = 0.1f;
            if (ai3 == 0) ai3 = 0.1f;
        }
        public override void Update()
        {
            if (TimeInWorld < ai2)
            {
                ai1 *= 1.1f;
            }
            else
            {
                ai1 *= 0.95f;
            }

            if (ai1 < ai3)
            {
                Active = false;
            }

            Velocity *= 0.96f;
        }

        public override void DrawInUI(SpriteBatch spriteBatch, Vector2 linePos)
        {
            Texture2D Sparkle = TextureAssets.Extra[89].Value;
            spriteBatch.Draw(Sparkle, Position + linePos + (TimeInWorld * Velocity), new Rectangle(0, 0, Sparkle.Width, Sparkle.Height), Color, 0 + Rotation, Sparkle.Size() / 2, ai1 * Scale, SpriteEffects.None, 0);
            spriteBatch.Draw(Sparkle, Position + linePos + (TimeInWorld * Velocity), new Rectangle(0, 0, Sparkle.Width, Sparkle.Height), Color, MathHelper.PiOver2 + Rotation, Sparkle.Size() / 2, ai1 * Scale, SpriteEffects.None, 0);
            spriteBatch.Draw(Sparkle, Position + linePos + (TimeInWorld * Velocity), new Rectangle(0, 0, Sparkle.Width, Sparkle.Height), new Color(1f, 1f, 1f, 0f) * 0.5f, 0 + Rotation, Sparkle.Size() / 2, ai1 * 0.5f * Scale, SpriteEffects.None, 0);
            spriteBatch.Draw(Sparkle, Position + linePos + (TimeInWorld * Velocity), new Rectangle(0, 0, Sparkle.Width, Sparkle.Height), new Color(1f, 1f, 1f, 0f) * 0.5f, MathHelper.PiOver2 + Rotation, Sparkle.Size() / 2, ai1 * 0.5f * Scale, SpriteEffects.None, 0);
        }
    }
}
