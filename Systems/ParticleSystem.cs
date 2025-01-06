﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace Terrafirma.Particles
{
    public enum ParticleLayer : byte
    {
        normal = 0,
        UI = 1,
        BehindTiles = 2
    }
    public class ParticleSystem : ModSystem
    {
        static List<Particle> Particles;
        static List<Particle> PreTileParticles;
        const int MaxParticles = 3000;
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
        public override void Load()
        {
            TooltipParticles = new List<Particle>(MaxParticles);
            PreTileParticles = new List<Particle>(MaxParticles);
            Particles = new List<Particle>(MaxParticles);

            On_Main.DrawDust += On_Main_DrawDust;
            On_Main.DrawBackGore += On_Main_DrawBackGore;
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
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.Transform);
            DrawParticles(Particles);
            Main.spriteBatch.End();
        }
        public override void PostUpdateDusts()
        {
            UpdateParticles(Particles);
            UpdateParticles(PreTileParticles);
            UpdateParticles(TooltipParticles);
        }

        public static void AddParticle(Particle particle, Vector2 Position, Vector2? Velocity = null, Color? color = null, ParticleLayer layer = ParticleLayer.normal)
        {
            particle.position = Position;
            particle.velocity = (Vector2)(Velocity == null ? Vector2.Zero : Velocity);
            particle.color = (Color)(color == null ? Color.White : color);
            switch (layer)
            {
                case ParticleLayer.normal:
                    {
                        if (Particles.Count == MaxParticles)
                        {
                            Particles.Remove(Particles[0]);
                        }
                        Particles.Add(particle);
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
