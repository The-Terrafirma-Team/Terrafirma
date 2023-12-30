using Microsoft.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Projectiles.Ranged
{
    internal class BirdshotProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 8; 
            Projectile.height = 8; 

            Projectile.friendly = true; 
            Projectile.hostile = false; 
            Projectile.DamageType = DamageClass.Ranged; 
            Projectile.penetrate = 1; 

            Projectile.timeLeft = 400;

            Projectile.ignoreWater = true; 
            Projectile.tileCollide = true; 
            Projectile.extraUpdates = 1;

            Projectile.aiStyle = 1;
            AIType = ProjectileID.Bullet;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            return false;
        }
        public override void AI()
        {
            if (Projectile.ai[2] == 0)
            {
                for (int i = 0; i < 7; i++)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, Projectile.velocity.RotatedBy(Main.rand.NextFloat(-20, 20) * (Math.PI / 180)) * Main.rand.NextFloat(0.9f, 1.1f), Projectile.type, Projectile.damage / 4, Projectile.knockBack, Projectile.owner, 0, 0, 1);

                }
                Projectile.Kill();
            }
            else
            {
                Lighting.AddLight(Projectile.position, new Vector3(0.4f, 0.4f, 0));
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

            Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
            }

            return true;
        }

        public override void OnKill(int timeLeft)
        {
            if (Projectile.ai[2] == 1)
            {
                Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
                SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            }

        }

    }
}
