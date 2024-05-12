using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace Terrafirma.Common.Templates
{
    public abstract class HeldProjectile : ModProjectile
    {
        public Player player;
        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(16);
            Projectile.tileCollide = false;
        }
        public override bool ShouldUpdatePosition()
        {
            return false;
        }
        public override bool PreAI()
        {
            return base.PreAI();
        }
        public void commonHeldLogic(int dummyTime = 2)
        {
            player = Main.player[Projectile.owner];
            player.heldProj = Projectile.whoAmI;
            if (player.channel && !stoppedChanneling)
            {
                Projectile.timeLeft = dummyTime;
                player.SetDummyItemTime(dummyTime);
            }
        }
        public int faceDirection = 0;
        public bool stoppedChanneling = false;
        float rotation = 0;
        public void positionSelf(Vector2 holdoutOffset, float rotationOffset = MathHelper.PiOver4, bool moveWhenNotChanneling = false)
        {
            if ((player.channel || moveWhenNotChanneling) && !stoppedChanneling)
            {
                rotation = (player.MountedCenter - player.velocity + new Vector2(0, player.gfxOffY)).DirectionTo(Main.MouseWorld).ToRotation();
                Projectile.rotation = rotation + rotationOffset;
                faceDirection = Main.MouseWorld.X - player.Center.X < 0 ? -1 : 1;
            }
            else
            {
                stoppedChanneling = true;
            }
            Projectile.Center = player.MountedCenter.ToPoint().ToVector2() + holdoutOffset.RotatedBy(rotation) + new Vector2(0, player.gfxOffY);
            player.direction = faceDirection;
        }

        //Drawing

        public void commonDiagonalItemDraw(Color color, Asset<Texture2D> tex, float scale = 1, float rotationOffset = 0f)
        {
            Main.EntitySpriteDraw(tex.Value,Projectile.Center - Main.screenPosition, new Rectangle(0, tex.Height() / Main.projFrames[Type] * Projectile.frame, tex.Width(),tex.Height() / Main.projFrames[Type]), color, Projectile.rotation + rotationOffset - (Projectile.spriteDirection == 1 ? 0 : MathHelper.PiOver2), new Vector2(0,tex.Height() / Main.projFrames[Type] - (Projectile.spriteDirection == 1 ? 0 : tex.Height() / Main.projFrames[Type])), scale, Projectile.spriteDirection == 1? SpriteEffects.None : SpriteEffects.FlipVertically);
        }
        public void commonDiagonalItemDrawManualRotation(Color color, Asset<Texture2D> tex, float scale = 1, float rotation = 0f)
        {
            Main.EntitySpriteDraw(tex.Value, Projectile.Center - Main.screenPosition, new Rectangle(0, tex.Height() / Main.projFrames[Type] * Projectile.frame, tex.Width(), tex.Height() / Main.projFrames[Type]), color, rotation, new Vector2(0, tex.Height() / Main.projFrames[Type]), scale, Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipVertically);
        }
        public void commonDiagonalItemDraw(Color color, Asset<Texture2D> tex, Vector2 scale, float rotationOffset = 0f)
        {
            Main.EntitySpriteDraw(tex.Value, Projectile.Center - Main.screenPosition, new Rectangle(0, tex.Height() / Main.projFrames[Type] * Projectile.frame, tex.Width(), tex.Height() / Main.projFrames[Type]), color, Projectile.rotation + rotationOffset, new Vector2(0, tex.Height() / Main.projFrames[Type]), scale, Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipVertically);
        }
    }
}
