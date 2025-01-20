using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;

namespace Terrafirma.Particles
{
    internal class ChlorophyteStyleLaserSegmentGlowless : ChlorophyteStyleLaserSegment
    {
        private static Asset<Texture2D> Sparkle;
        public override void Load()
        {
            Sparkle = Mod.Assets.Request<Texture2D>("Assets/Particles/GlowlessSparkle");
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sparkle.Value, position - Main.screenPosition, null, color, Rotation + MathHelper.PiOver2, Sparkle.Size() / 2, new Vector2(Scale,0.8f), SpriteEffects.None, 0);
        }
    }
}
