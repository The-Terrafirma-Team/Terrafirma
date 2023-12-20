using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Projectiles
{
    internal class CrystalChainBladesProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.damage = 16;
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 300;
            Projectile.friendly = true;
        }

        public override void AI()
        {
            Projectile.rotation = (Projectile.position - Main.player[Projectile.owner].position).ToRotation();
            Projectile.ai[0] += 1;

            if (Projectile.ai[0] > 35)
            {
                Projectile.ai[1] = 1;
            }
            else if (Projectile.ai[0] > 10 && Projectile.ai[1] == 0)
            {
                Projectile.velocity *= 0.85f;
            }

            if (Projectile.ai[1] == 1)
            {
                Projectile.velocity = -Vector2.Normalize(Projectile.position - Main.player[Projectile.owner].position) * 25f;
                Projectile.tileCollide = false;
                if (Projectile.position.Distance(Main.player[Projectile.owner].position) < 20f)
                {
                    Projectile.Kill();
                }
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.ai[1] = 1;
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            return false;
        }
        public override void OnSpawn(IEntitySource source)
        {
            Projectile.rotation = (Projectile.position - Main.player[Projectile.owner].position).ToRotation();
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Asset<Texture2D> ChainSprite = ModContent.Request<Texture2D>("TerrafirmaRedux/Projectiles/CrystalChainBladesChain");
            Asset<Texture2D> BladeSprite = ModContent.Request<Texture2D>("TerrafirmaRedux/Projectiles/CrystalChainBladesProjectile");

            for (int i = 0; i < (int)(Projectile.Center.Distance(Main.player[Projectile.owner].Center) / ChainSprite.Height()); i ++)
            {
                Main.EntitySpriteDraw(ChainSprite.Value, Main.player[Projectile.owner].Center + ( Vector2.Normalize(Projectile.Center - Main.player[Projectile.owner].Center) * i * ChainSprite.Height() ) - Main.screenPosition , null, lightColor, (Projectile.Center - Main.player[Projectile.owner].Center).ToRotation() + 90 * (float)(Math.PI/180), ChainSprite.Size() / 2, 1f, SpriteEffects.None, 0);
            }

            if ((Projectile.position - Main.player[Projectile.owner].position).X < 0)
            {
                if (Projectile.ai[2] == 1)
                {
                    Main.EntitySpriteDraw(BladeSprite.Value, Main.player[Projectile.owner].Center + (Projectile.Center - Main.player[Projectile.owner].Center) - Main.screenPosition, null, lightColor, (Projectile.Center - Main.player[Projectile.owner].Center).ToRotation() + 90 * (float)(Math.PI / 180), BladeSprite.Size() / 2 - new Vector2(4, 0), 1f, SpriteEffects.FlipHorizontally, 0);
                }
                else
                {
                    Main.EntitySpriteDraw(BladeSprite.Value, Main.player[Projectile.owner].Center + (Projectile.Center - Main.player[Projectile.owner].Center) - Main.screenPosition, null, lightColor, (Projectile.Center - Main.player[Projectile.owner].Center).ToRotation() + 90 * (float)(Math.PI / 180), BladeSprite.Size() / 2 - new Vector2(-4, 0), 1f, SpriteEffects.None, 0);
                }
                
            }
            else
            {
                if (Projectile.ai[2] == 1)
                {
                    Main.EntitySpriteDraw(BladeSprite.Value, Main.player[Projectile.owner].Center + (Projectile.Center - Main.player[Projectile.owner].Center) - Main.screenPosition, null, lightColor, (Projectile.Center - Main.player[Projectile.owner].Center).ToRotation() + 90 * (float)(Math.PI / 180), BladeSprite.Size() / 2 - new Vector2(-4, 0), 1f, SpriteEffects.None, 0);
                }
                else
                {
                    Main.EntitySpriteDraw(BladeSprite.Value, Main.player[Projectile.owner].Center + (Projectile.Center - Main.player[Projectile.owner].Center) - Main.screenPosition, null, lightColor, (Projectile.Center - Main.player[Projectile.owner].Center).ToRotation() + 90 * (float)(Math.PI / 180), BladeSprite.Size() / 2 - new Vector2(4, 0), 1f, SpriteEffects.FlipHorizontally, 0);
                }
            }

            return false;
        }
    }
}
