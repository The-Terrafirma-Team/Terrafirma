using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Common.Systems;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace Terrafirma.Content.Particles
{
    public class Smoke : Particle
    {
        private static Asset<Texture2D> Tex;
        private Rectangle Frame;
        public Vector2 Velocity = Vector2.Zero;
        public Color Bright = Color.White;
        public Color Dark = Color.White;
        public float Scale = 1f;
        public float Opacity = 0f;
        public float Rotation = 0f;
        public int FadeInTime = 25;
        public SpriteEffects effect = SpriteEffects.None;
        public Smoke(Vector2 velocity, Color bright, Color dark, float scale = 1f)
        {
            Velocity = velocity;
            Bright = bright;
            Dark = dark;
            Scale = scale;
        }
        public override void OnSpawn()
        {
            Frame = new Rectangle(40, 38 * Main.rand.Next(3), 40, 38);
            Opacity = 0.1f;
            Rotation = Main.rand.NextFloat(MathHelper.TwoPi);
            if (Main.rand.NextBool())
                effect = SpriteEffects.FlipHorizontally;
            FadeInTime = Main.rand.Next(12, 40);
        }
        public override void Update()
        {
            if (TimeInWorld < FadeInTime)
                Opacity += 0.9f / FadeInTime;
            else
            {
                Opacity -= 0.4f / FadeInTime;
            }
            if (Opacity < 0)
                Active = false;

            Rotation += Velocity.X * 0.1f;
            Velocity.X *= 0.99f;
            Velocity.Y -= Main.rand.NextFloat(0.03f) / FadeInTime;

            Position += Velocity;
        }
        public override void Draw(SpriteBatch spriteBatch, Vector2 ScreenPos)
        {
            Color light = new Color(Lighting.GetSubLight(Position));
            if (Tex == null)
                Tex = ModContent.Request<Texture2D>("Terrafirma/Content/Particles/Smoke");
            spriteBatch.Draw(Tex.Value, Position - ScreenPos, Frame with { X = 0}, Dark.MultiplyRGBA(light) * Opacity, Rotation, Frame.Size() / 2, Scale, effect, 0);
            spriteBatch.Draw(Tex.Value, Position - ScreenPos, Frame, Bright.MultiplyRGBA(light) * Opacity, Rotation, Frame.Size() / 2, Scale, effect, 0);
        }
    }
}
