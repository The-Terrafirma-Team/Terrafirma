//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Terrafirma.Particles.LegacyParticles;
//using Terrafirma.Systems.NPCQuests;
//using Terraria;
//using Terraria.ModLoader;

//namespace Terrafirma.Particles
//{
//    public class ParticleSystem : ModSystem
//    {
//        static List<Particle> Particles;
//        const int MaxParticles = 3000;
//        static List<Particle> TooltipParticles;
//        public override void Load()
//        {
//            TooltipParticles = new List<Particle>(MaxParticles);
//            Particles = new List<Particle>(MaxParticles);
//            On_Main.DrawCapture += On_Main_DrawCapture;
//            On_Main.DrawDust += On_Main_DrawDust;
//        }
//        private void On_Main_DrawDust(On_Main.orig_DrawDust orig, Main self)
//        {
//            orig.Invoke(self);
//            DrawParticles();
//        }
//        private void On_Main_DrawCapture(On_Main.orig_DrawCapture orig, Main self, Rectangle area, Terraria.Graphics.Capture.CaptureSettings settings)
//        {
//            orig.Invoke(self, area, settings);
//            DrawParticles();
//        }
//        public static void DrawUIParticle(Vector2 Linepos)
//        {
//            Main.spriteBatch.End();
//            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.SamplerStateForCursor, null, null, null, Main.UIScaleMatrix);
//            for (int i = 0; i < TooltipParticles.Count; i++)
//            {
//                Particle particle = TooltipParticles[i];
//                if (particle.Active)
//                {
//                    particle.DrawInUI(Main.spriteBatch, Linepos);
//                }
//            }
//            Main.spriteBatch.End();
//            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.SamplerStateForCursor, null, null, null, Main.UIScaleMatrix);
//        }
//        public static void DrawParticles()
//        {
//            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.Transform);

//            SpriteBatch spriteBatch = Main.spriteBatch;

//            for (int i = 0; i < Particles.Count; i++)
//            {
//                Particle particle = Particles[i];
//                if (particle.Active)
//                {
//                    particle.Draw(spriteBatch);
//                }
//            }

//            Main.spriteBatch.End();
//        }
//        public override void PostUpdateDusts()
//        {
//            for (int i = 0; i < Particles.Count; i++)
//            {
//                Particle particle = Particles[i];
//                if (particle.Active)
//                {
//                    particle.TimeInWorld++;
//                    particle.Update();
//                }
//                else
//                {
//                    Particles.Remove(Particles[i]);
//                }
//            }

//            for (int i = 0; i < TooltipParticles.Count; i++)
//            {
//                Particle particle = TooltipParticles[i];
//                if (particle.Active)
//                {
//                    particle.TimeInWorld++;
//                    particle.Update();
//                }
//                else
//                {
//                    TooltipParticles.Remove(TooltipParticles[i]);
//                }
//            }
//        }

//        public void AddParticle(Particle particle, Vector2 Position, bool TooltipParticle = false)
//        {
//            particle.Position = Position;
//            if (!TooltipParticle)
//            {
//                if (Particles.Count == MaxParticles)
//                {
//                    Particles.Remove(Particles[0]);
//                }
//                Particles.Add(particle);
//            }
//            else
//            {
//                TooltipParticles.Add(particle);
//            }
//        }
//    }
//    public abstract class Particle : ModType
//    {
//        public Vector2 Position;
//        public int TimeInWorld;
//        public bool Active;
//        protected override void Register()
//        {
//            ModTypeLookup<Particle>.Register(this);
//        }
//        public virtual void Update()
//        {
//        }
//        public virtual void OnSpawn()
//        {
//        }
//        public virtual void Draw(SpriteBatch spriteBatch)
//        {
//        }
//        public virtual void DrawInUI(SpriteBatch spriteBatch, Vector2 linePos)
//        {
//        }
//    }
//}
