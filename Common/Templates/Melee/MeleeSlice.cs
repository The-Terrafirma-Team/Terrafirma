using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
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
using static Terraria.Player;

namespace Terrafirma.Common.Templates.Melee
{
    public abstract class MeleeSlice : HeldProjectile
    {
        public static Asset<Texture2D> slashTex;
        public virtual int Length => 106;
        public virtual float slashSize => 106;
        public virtual Color slashColor1 => Color.White;
        public virtual Color slashColor2 => Color.Black;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 6;
            ProjectileID.Sets.TrailingMode[Type] = 2;
            slashTex = Mod.Assets.Request<Texture2D>("Assets/SwordSlash");
        }
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.penetrate = 3;
            Projectile.stopsDealingDamageAfterPenetrateHits = true;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
        }
        public override void OnSpawn(IEntitySource source)
        {
            player = Main.player[Projectile.owner];
            Projectile.timeLeft = player.itemAnimationMax;
            Projectile.ai[1] = player.itemAnimationMax;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            return targetHitbox.IntersectsConeSlowMoreAccurate(player.MountedCenter, Length * Projectile.scale, Projectile.rotation - MathHelper.PiOver4, 0.1f);
        }
        // x = (Projectile.timeLeft / Projectile.ai[1])
        float extend { get => (float)Math.Pow(Projectile.ai[2] / Projectile.ai[1],2);  }
        public override void AI()
        {
            Projectile.scale = player.GetAdjustedItemScale(player.HeldItem);
            Projectile.timeLeft -= (int)Projectile.ai[0];
            Projectile.ai[2] ++;

            Projectile.rotation = MathHelper.SmoothStep(-2.3f, 1f, extend);

            player.heldProj = Projectile.whoAmI;
            player.SetDummyItemTime(Projectile.timeLeft);
            //PlayerAnimation.ArmPointToDirection(Projectile.rotation - MathHelper.PiOver4, player);

            player.SetCompositeArmFront(true, CompositeArmStretchAmount.Full, (Projectile.rotation - MathHelper.PiOver2 - MathHelper.PiOver4) * player.direction);
            if (Projectile.timeLeft / Projectile.ai[1] > 0.6f)
                player.bodyFrame.Y = 56;
            if (Projectile.timeLeft / Projectile.ai[1] < 0.2f)
                player.bodyFrame.Y = 0;

            Projectile.Center = player.GetFrontHandPosition(CompositeArmStretchAmount.Full, (Projectile.rotation - MathHelper.PiOver2 - MathHelper.PiOver4) * player.direction);
            if (player.direction == -1)
                Projectile.rotation = MathHelper.Pi - Projectile.rotation + MathHelper.PiOver2;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            float extend2 = MathF.Sin((Projectile.timeLeft / Projectile.ai[1]) * MathHelper.Pi);

            Vector2 scaleVector = Vector2.SmoothStep(player.direction == 1 ? new Vector2(1.2f, 0.6f) : new Vector2(0.6f, 1.2f), new Vector2(1.1f), extend2);

            int slashLength = (int)(slashTex.Height() / 8 * (extend * 1.4f)); 

            for (int i = 0; i < 4; i++)
            {
                //Main.EntitySpriteDraw(tex.Value, Projectile.Center - Main.screenPosition, new Rectangle(0, tex.Height() / 4 * i, tex.Width(), (int)((tex.Height() / 4) * extend)), Color.White * extend2 * extend, (Projectile.rotation + (i * 0.1f * extend) - MathHelper.PiOver4 * 1.1f), new Vector2(tex.Width() / 2, tex.Height() / 8 ), Projectile.scale * 1.1f, player.direction == 1 ? SpriteEffects.None: SpriteEffects.FlipVertically);
                
                Main.EntitySpriteDraw(
                    slashTex.Value, 
                    Projectile.Center - Main.screenPosition, 
                    new Rectangle(0, slashTex.Height() / 4 * i, slashTex.Width(),slashLength), 
                    Color.Lerp(slashColor1, slashColor2, i / 4f) * extend2, 
                    Projectile.rotation + (player.direction * extend * 0.1f) -MathHelper.PiOver4 - (player.direction == 1 ? 0 : MathHelper.TwoPi), 
                    new Vector2(slashTex.Width() / 2 + (slashLength * 0.1f), player.direction == 1 ? slashLength : 0), 
                    (Projectile.scale + (extend2 * 0.2f)) * slashSize, 
                    player.direction == 1 ? SpriteEffects.None : SpriteEffects.FlipVertically);;
            }
            commonDiagonalItemDraw(lightColor, TextureAssets.Projectile[Type], scaleVector * Projectile.scale);
            return false;
        }
    }
}
