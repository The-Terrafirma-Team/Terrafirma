using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Content.Items.Weapons.Melee
{
    public class Lance : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.JoustingLance);
            Item.damage = 15;
            Item.shoot = ModContent.ProjectileType<LanceProj>();
            Item.shootSpeed = 2.3f;
        }
    }
    public class LanceProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.DismountsPlayersOnHit[Type] = true;
            ProjectileID.Sets.NoMeleeSpeedVelocityScaling[Type] = true;
        }
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.JoustingLance);
            AIType = ProjectileID.JoustingLance;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.Knockback *= Main.player[Projectile.owner].velocity.Length() / 15f;
            modifiers.SourceDamage *= 0.1f + Main.player[Projectile.owner].velocity.Length() / 7f * 0.9f;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float rotationFactor = Projectile.rotation + (float)Math.PI / 4f;
            float scaleFactor = 95f; 
            float widthMultiplier = 23f; 
            float collisionPoint = 0f; 
            Rectangle lanceHitboxBounds = new Rectangle(0, 0, 300, 300);

            lanceHitboxBounds.X = (int)Projectile.position.X - lanceHitboxBounds.Width / 2;
            lanceHitboxBounds.Y = (int)Projectile.position.Y - lanceHitboxBounds.Height / 2;

            Vector2 hitLineEnd = Projectile.Center + rotationFactor.ToRotationVector2() * scaleFactor;
            if (lanceHitboxBounds.Intersects(targetHitbox)
                && Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, hitLineEnd, widthMultiplier * Projectile.scale, ref collisionPoint))
            {
                return true;
            }
            return false;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Vector2 origin = Vector2.Zero;
            float rotation = Projectile.rotation;
            if (Projectile.direction > 0)
            {
                origin.X += texture.Width;
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            Vector2 position = new(Projectile.Center.X, Projectile.Center.Y - Main.player[Projectile.owner].gfxOffY);
            Main.EntitySpriteDraw(texture, position - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), null, lightColor, rotation, origin, Projectile.scale, spriteEffects, 0);
            return false;
        }
    }
}
