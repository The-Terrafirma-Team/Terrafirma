using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;

namespace Terrafirma.Particles
{
    public class SunflowerBeam : Particle
    {
        private static Asset<Texture2D> tex;
        float alpha = 0.01f;
        float scale = 1f;
        public override void Load()
        {
            tex = TextureAssets.Projectile[Terraria.ID.ProjectileID.MedusaHeadRay];
        }
        public override void OnSpawn()
        {
            scale = Main.rand.NextFloat(0.5f, 1.2f);
        }
        public override void Update()
        {
            position += velocity;
            velocity *= 0.99f;
            if (TimeInWorld < 120)
                alpha += 0.001f;
            else
                alpha -= 0.0001f;
            if (alpha < 0) Active = false;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            for(int i = 0; i < 2; i++)
                spriteBatch.Draw(tex.Value, position - Main.screenPosition, null, new Color(255,255,255,0) * alpha,0.3f + i * MathHelper.Pi,new Vector2(tex.Width() / 2, tex.Height()),new Vector2(1.3f,scale),SpriteEffects.None,0);
        }
    }
}
