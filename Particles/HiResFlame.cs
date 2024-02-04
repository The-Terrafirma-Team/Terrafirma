using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace Terrafirma.Particles
{
    public class HiResFlame : Particle
    {
        int Frame = 0;
        int FrameX = 0;
        float Scale = 0;
        float Rotation = 0;
        float variance = 0;
        SpriteEffects effect = SpriteEffects.None;
        bool emitLight;
        public override void OnSpawn()
        {
            Frame = Main.rand.Next(4);
            Scale = Main.rand.NextFloat(0.2f, 0.7f);
            variance = Main.rand.NextFloat(0.01f,0.03f);
            if(Main.rand.NextBool())
                effect = SpriteEffects.FlipHorizontally;
            emitLight = Main.rand.NextBool(10);
        }
        public override void Update()
        {
            Rotation = -2.3f + Velocity.X * 0.1f;
            Velocity.Y -= variance * 10;
            if (TimeInWorld % (int)(60 / 15) == 0)
                FrameX++;
            Velocity = Velocity.RotatedBy(Math.Sin(Main.timeForVisualEffects) * variance);
            Position += Velocity;
            if(TimeInWorld > 60)
                Active = false;
            else if(TimeInWorld < 10)
            {
                ai3 += 0.1f;
            }
            else if (TimeInWorld > 50)
            {
                ai3 -= 0.1f;
            }
            if (TimeInWorld > 10 && Scale > 0)
                Scale -= 0.02f;
            Lighting.AddLight(Position, Color.ToVector3() * Scale * ai1 * ai3 * 0.3f);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            //Main.spriteBatch.End();
            //Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.Transform);

            //Effect shader = GameShaders.Misc["Terrafirma:FlameShader"].Shader;
            //shader.Parameters["uOpacity"].SetValue(TimeInWorld / 30f);
            //shader.CurrentTechnique.Passes[0].Apply();

            Texture2D tex = ModContent.Request<Texture2D>(Terrafirma.AssetPath + "Particles/TinyFlame").Value;
            //spriteBatch.Draw(tex, Position - Main.screenPosition, new Rectangle(0,Frame * 128,128,128), new Color(1f,1f,1f,0), Rotation, tex.Size() / new Vector2(2,8), Scale, SpriteEffects.None, 0);
            spriteBatch.Draw(tex, Position - Main.screenPosition, new Rectangle(FrameX * 51, Frame * tex.Width / 15, tex.Width / 15, tex.Width / 15), Color.Lerp(new Color(0.4f,1f,1f,0),Color,ai1), Rotation, new Vector2(25.5f), Scale * ai1 * ai3 * 0.8f, effect, 0);
            //spriteBatch.Draw(tex, Position - Main.screenPosition, new Rectangle(0, Frame * 128, 128, 128), new Color(Color.R * 2f, Color.G * 2f, Color.B * 2f, 0) * 0.5f, Rotation, tex.Size() / new Vector2(2, 8), Scale * ai1 * 1.1f, SpriteEffects.None, 0);

            //Main.spriteBatch.End();
            //Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.Transform);
        }
    }
}
