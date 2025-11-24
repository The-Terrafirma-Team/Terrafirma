using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Content.Dusts
{
    public class SimpleColorableGlowyDust : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.frame = new Rectangle(0, Main.rand.Next(2) * 10, 10, 10);
            dust.rotation = Main.rand.NextFloat(MathHelper.TwoPi);
        }

        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            return dust.color;
        }
        public override bool Update(Dust dust)
        {
            if (!dust.noLight)
            Lighting.AddLight(dust.position, dust.color.ToVector3() * (1f - dust.alpha / 255f) * dust.scale * 0.5f);

            if(dust.customData == null || dust.customData is not int)
                return true;
            if ((int)dust.customData == 0)
            {
                if (Collision.SolidCollision(dust.position - Vector2.One * 5f, 10, 10) && dust.fadeIn == 0f)
                {
                    dust.scale *= 0.9f;
                    dust.velocity *= 0.25f;
                }
            }
            else if ((int)dust.customData == 1)
            {
                dust.scale *= 0.98f;
                dust.velocity.Y *= 0.98f;
                if (Collision.SolidCollision(dust.position - Vector2.One * 5f, 10, 10) && dust.fadeIn == 0f)
                {
                    dust.scale *= 0.9f;
                    dust.velocity *= 0.25f;
                }
            }
            else if ((int)dust.customData == 2)
            {
                dust.velocity.Y += 0.2f;
                if (Collision.SolidCollision(dust.position - Vector2.One * 5f, 10, 10) && dust.fadeIn == 0f)
                {
                    dust.scale *= 0.9f;
                    dust.velocity *= 0.25f;
                }
            }
            return true;
        }
    }
    public class SimpleColorableGlowierDust : SimpleColorableGlowyDust
    {
        public override string Texture => "Terrafirma/Content/Dusts/SimpleColorableGlowyDust";
        public override bool PreDraw(Dust dust)
        {
            Main.EntitySpriteDraw(Texture2D.Value, dust.position - Main.screenPosition, dust.frame, dust.color, dust.rotation, dust.frame.Size() / 2, dust.scale, SpriteEffects.None);

            Main.EntitySpriteDraw(TextureAssets.Npc[NPCID.Shimmerfly].Value, dust.position - Main.screenPosition, new Rectangle(44,0,22,22), dust.color * 0.5f * dust.scale, dust.rotation, new Vector2(11), dust.scale * dust.scale * 1.5f, SpriteEffects.None);
            //Main.EntitySpriteDraw(TextureAssets.Npc[NPCID.Shimmerfly].Value, dust.position - Main.screenPosition, new Rectangle(44, 0, 22, 22), new Color(0.4f, 0.4f, 0.4f, 0f) * dust.scale, dust.rotation, new Vector2(11), dust.scale * 0.5f, SpriteEffects.None);
            byte max = Math.Max(Math.Max(dust.color.A, dust.color.G),dust.color.B);
            Main.EntitySpriteDraw(TextureAssets.Npc[NPCID.Shimmerfly].Value, dust.position - Main.screenPosition, new Rectangle(44, 0, 22, 22), new Color(max,max,max,0) * dust.scale, dust.rotation, new Vector2(11), dust.scale * 0.5f, SpriteEffects.None);
            return false;
        }
    }
}
