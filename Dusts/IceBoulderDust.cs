using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ReLogic.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Terrafirma.Dusts
{
    internal class IceBoulderDust : ModDust
    {
        Asset<Texture2D> Tex;
        public override void SetStaticDefaults()
        {
            Tex = ModContent.Request<Texture2D>("Terrafirma/Dusts/IceBoulderDust");
            base.SetStaticDefaults();
        }
        public override void OnSpawn(Dust dust)
        {
            dust.frame = new Rectangle(0, Main.rand.Next(3) * 20, 20, 20);
            base.OnSpawn(dust);
        }

        public override bool Update(Dust dust)
        {
            if (!dust.noGravity) dust.velocity.Y += 0.2f;
            dust.rotation = dust.velocity.ToRotation();
            dust.position += dust.velocity;
            dust.scale *= 0.98f;
            if (dust.fadeIn > 20) dust.color *= 0.98f;
            if (dust.scale <= 0.1f) dust.active = false;
            dust.fadeIn++;
            return false;
        }

        public override bool PreDraw(Dust dust)
        {
            Rectangle actualframe = dust.scale > 1.2f? new Rectangle(20, dust.frame.Y * 2, dust.frame.Width * 2, dust.frame.Height * 2) : dust.frame;
            float actualscale = actualframe.X == 0 ? 1 : 0.5f;
            Main.EntitySpriteDraw(Tex.Value,
                dust.position - Main.screenPosition,
                actualframe,
                Lighting.GetSubLight(dust.position).ToColor(),
                dust.rotation,
                actualframe.Size() / 2,
                dust.scale * actualscale,
                SpriteEffects.None);
            return false;
        }
    }
}
