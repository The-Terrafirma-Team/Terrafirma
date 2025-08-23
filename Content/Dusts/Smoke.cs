using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace Terrafirma.Content.Dusts
{
    public class Smoke : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.frame = new Rectangle(40, 38 * Main.rand.Next(3), 40, 38);
        }
        public override bool Update(Dust dust)
        {
            dust.alpha+= Main.rand.Next(10);
            if (dust.alpha > 255)
                dust.active = false;

            dust.fadeIn = MathHelper.Clamp(dust.fadeIn + Main.rand.NextFloat(0.15f), 0, 1);

            dust.position += dust.velocity;
            dust.rotation += dust.velocity.X * 0.1f;
            dust.velocity.X *= 0.99f;
            dust.velocity.Y -= 0.03f;
            return false;
        }
        public override bool PreDraw(Dust dust)
        {
            Color light = new Color(Lighting.GetSubLight(dust.position));

            Color backDrawColor = Color.Transparent;
            if (dust.customData is Color color)
            {
                backDrawColor.R = (byte)Math.Clamp(color.R - (255 - light.R), 0, 255);
                backDrawColor.G = (byte)Math.Clamp(color.G - (255 - light.G), 0, 255);
                backDrawColor.B = (byte)Math.Clamp(color.B - (255 - light.B), 0, 255);
                backDrawColor.A = (byte)Math.Clamp(color.A - (255 - light.A), 0, 255);
            }

            Main.spriteBatch.Draw(Texture2D.Value, dust.position - Main.screenPosition, dust.frame with { X = 0}, backDrawColor * (1f - dust.alpha / 255f) * dust.fadeIn, dust.rotation, dust.frame.Size() / 2, dust.scale, SpriteEffects.None, 0);
            Main.spriteBatch.Draw(Texture2D.Value, dust.position - Main.screenPosition, dust.frame, dust.GetColor(light) * (1f - dust.alpha / 255f) * dust.fadeIn, dust.rotation, dust.frame.Size() / 2, dust.scale, SpriteEffects.None, 0);
            return false;
        }
    }
}
