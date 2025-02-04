using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace Terrafirma.Particles
{
    public enum ParticleLayer : byte
    {
        NormalPixel = 0,
        UI = 1,
        BehindTiles = 2,
        Normal = 0,
    }
    public class ParticleSystem : ModSystem
    {
        static List<Particle> PixelParticles;
        static List<Particle> Particles;
        static List<Particle> PreTileParticles;
        const int MaxParticles = 700;
        static List<Particle> TooltipParticles;

        private static void DrawParticles(List<Particle> list)
        {
            List<Particle> postParticles = new List<Particle>();
            SpriteBatch spriteBatch = Main.spriteBatch;
            for (int i = 0; i < list.Count; i++)
            {
                Particle particle = list[i];
                if (particle.Active)
                {
                    if (particle.HasPartsDrawnAfterOtherParticles)
                    {
                        postParticles.Add(particle);
                    }
                    particle.Draw(spriteBatch);
                }
            }
            for (int i = 0; i < postParticles.Count; i++)
            {
                Particle particle = postParticles[i];
                particle.DrawAfter(spriteBatch);
            }
            postParticles.Clear();
        }
        private static void UpdateParticles(List<Particle> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Particle particle = list[i];
                if (particle.Active)
                {
                    particle.TimeInWorld++;
                    particle.Update();
                }
                else
                {
                    list.Remove(particle);
                }
            }
        }
        private static RenderTarget2D pixelTarget;
        public override void Load()
        {
            TooltipParticles = new List<Particle>(MaxParticles);
            PreTileParticles = new List<Particle>(MaxParticles);
            PixelParticles = new List<Particle>(MaxParticles);
            Particles = new List<Particle>(MaxParticles);
            On_Main.DrawDust += On_Main_DrawDust;
            On_Main.DrawBackGore += On_Main_DrawBackGore;
            Main.OnPreDraw += DrawPixelTarget;
            Main.QueueMainThreadAction(setRenderTarget);
        }

        private void DrawPixelTarget(GameTime obj)
        {
            var oldTargets = Main.instance.GraphicsDevice.GetRenderTargets();
            Main.instance.GraphicsDevice.SetRenderTarget(pixelTarget);
            Main.instance.GraphicsDevice.Clear(Color.Transparent);
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.Transform * Matrix.CreateScale(0.25f / Main.GameZoomTarget));
            DrawParticles(PixelParticles);
            //Main.spriteBatch.Draw(TextureAssets.Item[1].Value, Main.LocalPlayer.Center - Main.screenPosition, Color.White);
            Main.spriteBatch.End();
            Main.instance.GraphicsDevice.SetRenderTargets(oldTargets);
        }

        private void setRenderTarget()
        {
            pixelTarget = new RenderTarget2D(Main.instance.GraphicsDevice, Main.screenWidth, Main.screenHeight,false,SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.PreserveContents);
        }
        private void On_Main_DrawBackGore(On_Main.orig_DrawBackGore orig, Main self)
        {
            orig.Invoke(self);
            DrawParticlesBehindTiles();
        }
        private void On_Main_DrawDust(On_Main.orig_DrawDust orig, Main self)
        {
            orig.Invoke(self);
            DrawParticles();
        }
        public static void DrawUIParticles(Vector2 Linepos)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.SamplerStateForCursor, null, null, null, Main.UIScaleMatrix);
            for (int i = 0; i < TooltipParticles.Count; i++)
            {
                Particle particle = TooltipParticles[i];
                if (particle.Active)
                {
                    particle.DrawInUI(Main.spriteBatch, Linepos);
                }
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.SamplerStateForCursor, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.UIScaleMatrix);
        }
        public static void DrawParticlesBehindTiles()
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.Transform);
            DrawParticles(PreTileParticles);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.Transform);
        }
        public static void DrawParticles()
        {
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer);
            Main.spriteBatch.Draw(pixelTarget, new Rectangle(0,0, (int)(Main.screenWidth * 4 * Main.GameZoomTarget), (int)(Main.screenHeight * 4 * Main.GameZoomTarget)), Color.White);
            DrawParticles(Particles);
            Main.spriteBatch.End();
        }
        public override void PostUpdateDusts()
        {
            UpdateParticles(PixelParticles);
            UpdateParticles(PreTileParticles);
            UpdateParticles(TooltipParticles);
        }

        public static void AddParticle(Particle particle, Vector2 Position, Vector2? Velocity = null, Color? color = null, ParticleLayer layer = ParticleLayer.NormalPixel)
        {
            particle.position = Position;
            particle.velocity = (Vector2)(Velocity == null ? Vector2.Zero : Velocity);
            particle.color = (Color)(color == null ? Color.White : color);
            switch (layer)
            {
                case ParticleLayer.NormalPixel:
                    {
                        if (PixelParticles.Count == MaxParticles)
                        {
                            PixelParticles.Remove(PixelParticles[0]);
                        }
                        PixelParticles.Add(particle);
                        break;
                    }
                case ParticleLayer.UI:
                    {
                        TooltipParticles.Add(particle);
                        break;
                    }
                case ParticleLayer.BehindTiles:
                    {
                        if (PreTileParticles.Count == MaxParticles)
                        {
                            PreTileParticles.Remove(PreTileParticles[0]);
                        }
                        PreTileParticles.Add(particle);
                        break;
                    }
            }
            particle.OnSpawn();
        }
    }
    public abstract class Particle : ModType
    {
        public Vector2 position;
        public Vector2 velocity;
        public Color color;
        public int TimeInWorld;
        public bool Active = true;
        public virtual bool HasPartsDrawnAfterOtherParticles => false;
        protected override void Register()
        {
            ModTypeLookup<Particle>.Register(this);
        }
        public virtual void Update()
        {
        }
        public virtual void OnSpawn()
        {
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }
        public virtual void DrawAfter(SpriteBatch spriteBatch)
        {
        }
        public virtual void DrawInUI(SpriteBatch spriteBatch, Vector2 linePos)
        {
        }
    }
}
