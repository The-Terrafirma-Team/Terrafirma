using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terrafirma.Common.Systems;
using Terraria;
using Terraria.ModLoader;

namespace Terrafirma.Content.Particles
{
    public class StarSparkle : Particle
    {
        private static Asset<Texture2D> Tex;
        public int BurstTime = 14;
        public float Rotation = 0;
        public float Scale = 1f;
        public Color Color;
        public Vector2 Velocity = Vector2.Zero;

        public StarSparkle(int burstTime, float rotation, float scale, Color color, Vector2 velocity)
        {
            BurstTime = burstTime;
            Rotation = rotation;
            Scale = scale;
            Color = color;
            Velocity = velocity;
        }
        public override void Update()
        {
            if (TimeInWorld == 0)
                TimeInWorld += 3;
            Position += Velocity;
            Velocity *= 0.95f;
            if (TimeInWorld > BurstTime)
                Active = false;
        }
        public override void Draw(SpriteBatch spriteBatch, Vector2 ScreenPos)
        {
            if (Tex == null)
                Tex = ModContent.Request<Texture2D>("Terrafirma/Content/Particles/StarSparkle");

            spriteBatch.Draw(Tex.Value, Position - ScreenPos, null, Color * (1f - (float)TimeInWorld / BurstTime * 0.6f) * 2, Rotation, Tex.Size() / 2, (float)Math.Sin((TimeInWorld) * (MathHelper.Pi / BurstTime)) * Scale, SpriteEffects.None, 0);
            spriteBatch.Draw(Tex.Value, Position - ScreenPos, null, new Color(255, 255, 255, 64) * (1f - (float)TimeInWorld / BurstTime * 0.6f) * 0.5f, Rotation, Tex.Size() / 2, (float)Math.Sin((TimeInWorld) * (MathHelper.Pi / BurstTime)) * Scale * 0.83f, SpriteEffects.None, 0);
        }
    }
}
