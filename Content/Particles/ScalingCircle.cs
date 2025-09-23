using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terrafirma.Common.Systems;
using Terraria;
using Terraria.ModLoader;

namespace Terrafirma.Content.Particles
{
    public class ScalingCircle : Particle
    {
        private static Asset<Texture2D> Tex;
        public Vector2 StartRadius = Vector2.Zero;
        public Vector2 EndRadius = Vector2.Zero;
        public int Duration = 20;
        public float Rotation = 0f;
        public Color Color;
        public Entity FollowedEntity = null;

        public ScalingCircle(Vector2 startRadius, float endRadius, int duration, Color color, float rotation = 0, Entity followedEntity = null)
        {
            StartRadius = startRadius;
            EndRadius = new Vector2(endRadius);
            Duration = duration;
            Rotation = rotation;
            Color = color;
            FollowedEntity = followedEntity;
        }
        public ScalingCircle(float startRadius, Vector2 endRadius, int duration, Color color, float rotation = 0, Entity followedEntity = null)
        {
            StartRadius = new Vector2(startRadius);
            EndRadius = endRadius;
            Duration = duration;
            Rotation = rotation;
            Color = color;
            FollowedEntity = followedEntity;
        }
        public ScalingCircle(float startRadius, float endRadius, int duration, Color color, float rotation = 0, Entity followedEntity = null)
        {
            StartRadius = new Vector2(startRadius);
            EndRadius = new Vector2(endRadius);
            Duration = duration;
            Rotation = rotation;
            Color = color;
            FollowedEntity = followedEntity;
        }
        public ScalingCircle(Vector2 startRadius, Vector2 endRadius, int duration, Color color, float rotation = 0, Entity followedEntity = null)
        {
            StartRadius = startRadius;
            EndRadius = endRadius;
            Duration = duration;
            Rotation = rotation;
            Color = color;
            FollowedEntity = followedEntity;
        }
        public override void Update()
        {
            if (TimeInWorld > Duration)
                Active = false;
            if(FollowedEntity != null)
            {
                Position = FollowedEntity.Center;
            }
        }
        public override void Draw(SpriteBatch spriteBatch, Vector2 ScreenPos)
        {
            if (Tex == null)
                Tex = ModContent.Request<Texture2D>("Terrafirma/Content/Particles/ScalingCircle");
            float scalePercent = TimeInWorld / (float)Duration;
            Vector2 scale = Vector2.Lerp(StartRadius, EndRadius, scalePercent) / 256;
            float opacity = MathF.Sin(scalePercent * MathHelper.Pi);
            spriteBatch.Draw(Tex.Value, Position - ScreenPos, null, Color * opacity, Rotation, Tex.Size() / 2, scale, SpriteEffects.None, 0);
        }
    }
}
