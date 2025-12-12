using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terrafirma.Common.Systems;
using Terraria;
using Terraria.ModLoader;

namespace Terrafirma.Content.Particles
{
    public class CrackDecal : Particle
    {
        private static Asset<Texture2D> Tex;
        public float Scale = 1f;
        public float Opacity = 1f;
        public float Rotation = 0f;
        public SpriteEffects effect = SpriteEffects.None;
        public CrackDecal(float scale = 1f)
        {
            Scale = scale;
        }
        public override void OnSpawn()
        {
            Rotation = Main.rand.NextFloat(MathHelper.TwoPi);
            if (Main.rand.NextBool())
                effect = SpriteEffects.FlipHorizontally;
        }
        public override void Update()
        {
            if (TimeInWorld > 60)
                Opacity -= 0.01f;
            if (Opacity < 0f)
                Active = false;
        }
        public override void Draw(SpriteBatch spriteBatch, Vector2 ScreenPos)
        {
            Color light = new Color(Lighting.GetSubLight(Position));
            if (Tex == null)
                Tex = ModContent.Request<Texture2D>("Terrafirma/Content/Particles/CrackDecal");
            spriteBatch.Draw(Tex.Value, Position - ScreenPos, null, light * Opacity * 0.8f, Rotation, new Vector2(38,33), Scale, effect, 0);
        }
    }
}
