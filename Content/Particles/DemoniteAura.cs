using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terrafirma.Common.Systems;
using Terraria;
using Terraria.ModLoader;

namespace Terrafirma.Content.Particles
{
    public class DemoniteAura : Particle
    {
        private static Asset<Texture2D> Tex;
        public float Scale = 1f;
        public float Opacity = 1f;
        public float Rotation = 0f;
        public int Frame = 0;
        public SpriteEffects effect = SpriteEffects.None;
        public float Red = 1f;
        public DemoniteAura(float scale = 1f)
        {
            Scale = scale;
        }
        public override void OnSpawn()
        {
            Rotation = Main.rand.NextFloat(MathHelper.TwoPi);
            if (Main.rand.NextBool())
                effect = SpriteEffects.FlipHorizontally;
            Red = Main.rand.NextFloat(0.25f,1f);
        }
        public override void Update()
        {
            if(TimeInWorld < 160)
            {
                Opacity = TimeInWorld / 160f;
            }
            else
            {
                Opacity = 1f - (Frame / 17f);
            }

            if (TimeInWorld > 160 && TimeInWorld % 8 == 0)
                Frame++;

            if (Frame >= 17)
                Active = false;
            Scale *= 1.004f;
            Position += Position.DirectionTo(Main.LocalPlayer.Center) * 0.0005f * TimeInWorld;
            //Position += Main.rand.NextVector2Circular(TimeInWorld, TimeInWorld) / 120f;
        }
        public override void Draw(SpriteBatch spriteBatch, Vector2 ScreenPos)
        {
            if (Tex == null)
                Tex = ModContent.Request<Texture2D>("Terrafirma/Content/Particles/DemoniteAura");
            spriteBatch.Draw(Tex.Value, Position - ScreenPos, Tex.Frame(1,17,0,Frame), new Color(Opacity * Red,0f,1f,0f) * Opacity * 0.3f, Rotation, new Vector2(31,34), Scale, effect, 0);
        }
    }
}
