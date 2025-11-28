using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace Terrafirma.Common.Systems
{
    //Only god knows why we have this instead of particle orchestrator
    public class ParticleSystem : ModSystem
    {
        private static List<Particle> Particles = new();
        public override void Load()
        {
            On_Main.DrawDust += On_Main_DrawDust;
        }
        public override void Unload()
        {
            Particles.Clear();
        }
        private void On_Main_DrawDust(On_Main.orig_DrawDust orig, Main self)
        {
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer, null, Main.Transform);
            for (int i = 0; i < Particles.Count; i++)
            {
                Particles[i].Draw(Main.spriteBatch, Main.screenPosition);
            }
            Main.spriteBatch.End();
            orig.Invoke(self);
        }
        public override void PostUpdateDusts()
        {
            for (int i = 0; i < Particles.Count; i++)
            {
                Particles[i].Update();
                Particles[i].TimeInWorld++;
                if (Particles[i].Active == false)
                {
                    Particles.RemoveAt(i);
                    i--;
                }
            }
        }
        public static void NewParticle(Particle particle, Vector2 position)
        {
            particle.Position = position;
            Particles.Add(particle);
            particle.OnSpawn();
        }
    }
    public abstract class Particle : ILoadable
    {
        public Vector2 Position;
        public int TimeInWorld;
        public bool Active = true;
        public virtual void Update()
        {
        }
        public virtual void OnSpawn()
        {
        }
        public virtual void Draw(SpriteBatch spriteBatch, Vector2 ScreenPos)
        {
        }
        public void Load(Mod mod)
        {
        }
        public void Unload()
        {
        }
    }
}
