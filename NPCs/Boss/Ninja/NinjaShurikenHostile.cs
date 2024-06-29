using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.NPCs.Boss.Ninja
{
    public class NinjaShurikenHostile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 10;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }
        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(26);
            Projectile.hostile = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 240;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            base.OnHitPlayer(target, info);
        }

        public override void AI()
        {
            Projectile.rotation += 0.25f * Projectile.direction;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Type].Value;

            for (int i = 0; i < Projectile.oldPos.Length/2; i++)
            {
                Main.EntitySpriteDraw(
                    tex,
                    Projectile.oldPos[i * 2] + (tex.Size()/2) - new Vector2(3) - Main.screenPosition,
                    tex.Bounds,
                    lightColor * 0.5f * ((Projectile.oldPos.Length / 2 - i) / (float)(Projectile.oldPos.Length / 2)),
                    Projectile.oldRot[i * 2],
                    tex.Size() / 2,
                    Projectile.scale,
                    SpriteEffects.None
                    );
            }
            Main.EntitySpriteDraw(
                tex,
                Projectile.Center - Main.screenPosition,
                tex.Bounds,
                lightColor,
                Projectile.rotation,
                tex.Size() / 2,
                Projectile.scale,
                SpriteEffects.None
                );

            return false;
        }
    }
}
