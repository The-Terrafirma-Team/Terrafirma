using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Particles
{
    public class ParticleSystem : ModSystem
    {
        static List<Particle> Particles;
        static int MaxParticles = 3000;
        public override void Load()
        {
            Particles = new List<Particle>(MaxParticles);
            On_Main.DrawCapture += On_Main_DrawCapture;
            On_Main.DrawDust += On_Main_DrawDust;
            On_Main.Draw += On_Main_Draw;
        }

        private void On_Main_Draw(On_Main.orig_Draw orig, Main self, GameTime gameTime)
        {
            orig.Invoke(self, gameTime);
            DrawParticles(true);
        }

        private void On_Main_DrawDust(On_Main.orig_DrawDust orig, Main self)
        {
            orig.Invoke(self);
            DrawParticles();
        }
        private void On_Main_DrawCapture(On_Main.orig_DrawCapture orig, Main self, Rectangle area, Terraria.Graphics.Capture.CaptureSettings settings)
        {
            orig.Invoke(self, area, settings);
            DrawParticles();
        }
        public override void Unload()
        {
            Particles.Clear();
            On_Main.DrawCapture -= On_Main_DrawCapture;
            On_Main.DrawDust -= On_Main_DrawDust;
            On_Main.Draw -= On_Main_Draw;
        }
        public override void PostUpdateDusts()
        {
            for (int i = 0; i < Particles.Count; i++)
            {
                Particle particle = Particles[i];
                if (particle.Active)
                {
                    if (particle.TimeInWorld == 0)
                    {
                        particle.OnSpawn();
                    }
                    particle.TimeInWorld++;
                    particle.Update();
                }
                else
                {
                    Particles.Remove(Particles[i]);
                }
            }
        }
        public static void DrawParticles(bool FrontLayer = false)
        {
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.Transform);

            SpriteBatch spriteBatch = Main.spriteBatch;

            for (int i = 0; i < Particles.Count; i++)
            {
                Particle particle = Particles[i];
                if (particle.Active && particle.FrontLayer == FrontLayer)
                {
                    particle.Draw(spriteBatch);
                }
            }

            for (int i = 0; i < Particles.Count; i++)
            {
                Particle particle = Particles[i];
                if (particle.Active)
                {
                    particle.PostDraw(spriteBatch);
                }
            }

            Main.spriteBatch.End();
        }
        public static Particle AddParticle(Particle type, Vector2 position, Vector2 velocity = default, Color color = default, float AI1 = 0, float AI2 = 0, float AI3 = 0)
        {
            if (Particles.Count == MaxParticles)
            {
                Particles.Remove(Particles[0]);
            }
            Particles.Add(type);
            Particles.Last().Position = position;
            Particles.Last().Velocity = velocity;
            Particles.Last().Color = color;
            Particles.Last().ai1 = AI1;
            Particles.Last().ai2 = AI2;
            Particles.Last().ai3 = AI3;

            return Particles.Last();
        }
    }
    public abstract class Particle
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public int TimeInWorld;
        public float ai1;
        public float ai2;
        public float ai3;
        public bool Active = true;
        public Color Color;
        public bool FrontLayer; // Draws in front of everything
        /// <summary>
        /// You have to manually add Position to Velocity if you want it to move with it.
        /// Set Active to False to kill.
        /// </summary>
        public virtual void Update()
        {
        }
        public virtual void OnSpawn()
        {
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }
        /// <summary>
        /// Unlike other PostDraw hooks this one is called for each particle after all of them called Draw. Good for outlined particles.
        /// </summary>
        public virtual void PostDraw(SpriteBatch spriteBatch)
        {
        }
    }
}
