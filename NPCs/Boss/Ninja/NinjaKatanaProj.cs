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
    public class NinjaKatanaProj : ModProjectile
    {
        float truerotation { get => Projectile.spriteDirection == 1 ? Projectile.rotation + MathHelper.PiOver2 - 0.4f : Projectile.rotation + (float)Math.PI; }
        Vector2 bladetop { get => Projectile.Size.RotatedBy(Projectile.rotation); }
        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 120;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            base.OnHitPlayer(target, info);
        }

        public override void AI()
        {

            for (int i = 0; i < Main.player.Length; i++)
            {
                if (Collision.CheckAABBvLineCollision(Main.player[i].position, Main.player[i].Size, Projectile.BottomLeft, Projectile.BottomLeft + bladetop) && !Main.player[i].immune && Main.player[i].shieldParryTimeLeft <= 0)
                {
                    Player.HurtInfo info = new Player.HurtInfo();
                    info.Damage = Projectile.damage;
                    info.DamageSource = PlayerDeathReason.ByProjectile(i, Projectile.whoAmI);
                    info.Knockback = Projectile.knockBack;
                    info.PvP = false;
                    info.Dodgeable = true;
                    info.HitDirection = Projectile.spriteDirection;
                    Main.player[i].Hurt(info);
                }
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Type].Value;

            Main.EntitySpriteDraw(
                tex,
                Projectile.BottomLeft - Main.screenPosition,
                tex.Bounds,
                lightColor,
                truerotation,
                Projectile.spriteDirection == 1 ? new Vector2(0, tex.Height): new Vector2(tex.Width, tex.Height),
                Projectile.scale,
                Projectile.spriteDirection == 1? SpriteEffects.None : SpriteEffects.FlipHorizontally
                );

            //For drawing points at the start and end of the blade
            //Main.EntitySpriteDraw(
            //    TextureAssets.MagicPixel.Value,
            //    Projectile.BottomLeft - Main.screenPosition,
            //    tex.Bounds,
            //    lightColor,
            //    0f,
            //    Vector2.Zero,
            //    0.2f,
            //    SpriteEffects.None
            //    );
            //Main.EntitySpriteDraw(
            //TextureAssets.MagicPixel.Value,
            //    Projectile.BottomLeft + bladetop - Main.screenPosition,
            //    tex.Bounds,
            //    lightColor,
            //    0f,
            //    Vector2.Zero,
            //    0.2f,
            //    SpriteEffects.None
            //    );

            return false;
        }
    }
}
