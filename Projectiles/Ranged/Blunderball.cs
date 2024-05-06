using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace Terrafirma.Projectiles.Ranged
{
    internal class Blunderball : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Type] = 2;
            ProjectileID.Sets.TrailCacheLength[Type] = 8;
        }
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
            Projectile.extraUpdates = 1;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if(oldVelocity.X != Projectile.velocity.X)
            {
                Projectile.velocity.X *= -1;
            }
            if (oldVelocity.Y != Projectile.velocity.Y)
            {
                Projectile.velocity.Y *= -0.3f;
            }
            Projectile.velocity.X *= 0.96f;
            Projectile.ai[1] = 1;
            Projectile.extraUpdates = 0;
            Projectile.knockBack = 2f;
            return false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (Projectile.velocity.Length() < 3 && Projectile.ai[1] == 1)
                return false;
            return base.CanHitNPC(target);
        }
        public override bool CanHitPvp(Player target)
        {
            if (Projectile.velocity.Length() < 3 && Projectile.ai[1] == 1)
                return false;
            return base.CanHitPvp(target);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.velocity = Vector2.Normalize(-Projectile.velocity)*6 + new Vector2(0,-3);
            Projectile.extraUpdates = 0;
            Projectile.knockBack = 2f;
        }

        public override void AI()
        {
            if (Projectile.ai[1] == 1)
            {
                Projectile.alpha++;
                if (Projectile.alpha > 254)
                    Projectile.Kill();
            }

            Projectile.rotation += 0.1f * Projectile.velocity.X;
            Projectile.ai[0]++;

            if (Projectile.ai[0] > 20)
                Projectile.velocity.Y += 0.5f;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            for(int i = 1; i < ProjectileID.Sets.TrailCacheLength[Type]; i++)
            {
                Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, Projectile.oldPos[i] + new Vector2(8,8) - Main.screenPosition, null, lightColor * (1 - (i/ (float)ProjectileID.Sets.TrailCacheLength[Type])) * 0.5f * Projectile.Opacity, Projectile.oldRot[i], new Vector2(8), 1f, SpriteEffects.None);
            }
            Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, Projectile.Center - Main.screenPosition, null, lightColor * Projectile.Opacity, Projectile.rotation, new Vector2(8), 1f, SpriteEffects.None);
            return false;
        }
    }
}
