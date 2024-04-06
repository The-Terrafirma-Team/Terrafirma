using Microsoft.CodeAnalysis;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Systems.Elements.Beastiary;
using Terraria.GameContent;
using Terraria;
using Terraria.ModLoader;
using ReLogic.Content;
using Microsoft.Xna.Framework;

namespace Terrafirma.Particles
{
    public class HeartWaveParticle : Particle
    {
        public int timeleft = 60;
        public float Scale = 1f;
        private static Asset<Texture2D> HeartWave; 
        public override void OnSpawn()
        {
            
            base.OnSpawn();
        }
        public override void SetStaticDefaults()
        {
            HeartWave = ModContent.Request<Texture2D>("Terrafirma/Particles/LegacyParticles/CrimsonHeartSentryWaveEffect");
        }
        public override void Update()
        {
            Scale = 1f + (timeleft - 60) * 0.015f;
            if (TimeInWorld > timeleft) Active = false;
            base.Update();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            HeartWave = ModContent.Request<Texture2D>("Terrafirma/Particles/LegacyParticles/CrimsonHeartSentryWaveEffect");
            spriteBatch.Draw(HeartWave.Value, 
                Position - Main.screenPosition, 
                HeartWave.Value.Bounds, 
                new Color(200, 10, 0, 0) * ((60 - TimeInWorld) / 60f),
                TimeInWorld * 0.06f, 
                HeartWave.Size() / 2,
                (float)Math.Sin(TimeInWorld * 0.08f) * 0.6f * Scale, 
                SpriteEffects.None, 
                0);
        }
    }
}
