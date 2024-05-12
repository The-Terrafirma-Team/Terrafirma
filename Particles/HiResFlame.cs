using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.ModLoader;

namespace Terrafirma.Particles
{
    public class HiResFlame : Particle
    {
        public static Asset<Texture2D> tex;
        public override void Load()
        {
            tex = Mod.Assets.Request<Texture2D>("Assets/Particles/TinyFlame");
        }
        int Frame = 0;
        int FrameX = 0;
        float Scale = 0;
        float Rotation = 0;
        float variance = 0;
        SpriteEffects effect = SpriteEffects.None;
        bool emitLight;
        public float SizeMultiplier;
        float Size;
        public float gravity = 10;
        public override void OnSpawn()
        {
            Frame = Main.rand.Next(4);
            Scale = Main.rand.NextFloat(0.2f, 0.7f);
            variance = Main.rand.NextFloat(0.01f, 0.03f);
            if (Main.rand.NextBool())
                effect = SpriteEffects.FlipHorizontally;
            emitLight = Main.rand.NextBool(10);
        }
        public override void Update()
        {
            Rotation = -2.3f + velocity.X * 0.1f;
            velocity.Y -= variance * gravity;
            if (TimeInWorld % (60 / 15) == 0)
                FrameX++;
            velocity = velocity.RotatedBy(Math.Sin(Main.timeForVisualEffects) * variance);

            position += velocity;

            if (TimeInWorld > 60)
                Active = false;
            else if (TimeInWorld < 10)
            {
                Size += 0.1f;
            }
            else if (TimeInWorld > 50)
            {
                Size -= 0.1f;
            }
            if (TimeInWorld > 10 && Scale > 0)
                Scale -= 0.02f;
            if(emitLight)
                Lighting.AddLight(position, color.ToVector3() * Scale * SizeMultiplier * Size * 0.3f);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex.Value, position - Main.screenPosition, new Rectangle(FrameX * 51, Frame * tex.Width() / 15, tex.Width() / 15, tex.Width() / 15), Color.Lerp(new Color(0.4f, 1f, 1f, 0), color, SizeMultiplier), Rotation, new Vector2(25.5f), Scale * SizeMultiplier * Size * 0.8f, effect, 0);
        }
    }
}
