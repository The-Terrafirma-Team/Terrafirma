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
using Terraria.ModLoader;

namespace Terrafirma.Projectiles.Hostile
{
    public class TerragrimSpiritBlade : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(8);
            Projectile.hostile = true;
            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
            Projectile.alpha = 255;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver4;
            Projectile.ai[0]++;

            if(Projectile.alpha > 0)
            {
                Projectile.alpha = (int)(Projectile.alpha * 0.95f);
            }

            if (Projectile.ai[0] is > 130 and < 180)
            {
                Projectile.velocity *= 1.1f;
            }
            if (Projectile.ai[0] > 130)
            {
                Projectile.velocity = Projectile.velocity.RotatedBy(Projectile.ai[1] * 0.02f);
            }
            if (Projectile.ai[0] > 210)
            {
                Projectile.alpha += 20;
            }
            if (Projectile.ai[0] > 100 && Projectile.alpha > 200)
                Projectile.Kill();
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Asset<Texture2D> tex = TextureAssets.Projectile[Type];
            Main.EntitySpriteDraw(tex.Value, Projectile.Center - Projectile.velocity - Main.screenPosition, null, new Color(0.6f, 1f, 0.8f, 0f) * 0.5f * Projectile.Opacity, Projectile.rotation, tex.Size() / 2, 1 + MathF.Sin(Projectile.timeLeft * 0.1f) * 0.2f, SpriteEffects.None);
            Main.EntitySpriteDraw(tex.Value,Projectile.Center - Main.screenPosition,null,new Color(0.6f,1f,0.8f,0.2f) * Projectile.Opacity,Projectile.rotation,tex.Size() / 2,1,SpriteEffects.None);
            return false;
        }
    }
}
