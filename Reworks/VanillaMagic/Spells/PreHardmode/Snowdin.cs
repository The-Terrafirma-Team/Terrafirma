using Humanizer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Linq;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Graphics;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.VanillaMagic.Spells.PreHardmode
{
    internal class Snowdin : Spell
    {
        public override int UseAnimation => 42;
        public override int UseTime => 42;
        public override int ManaCost => 4;
        public override int ReuseDelay => -1;
        public override int[] SpellItem => new int[] { ItemID.WandofFrosting };

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<SnowdinBomb>(), damage, knockback, player.whoAmI);
            return false;
        }
    }

    public class SnowdinBomb : ModProjectile
    {
        bool rolling = false;
        public override void SetDefaults()
        {
            Projectile.tileCollide = true;
            Projectile.friendly = true;

            Projectile.timeLeft = 60 * 12;
            Projectile.Opacity = 1f;
            Projectile.penetrate = -1;
            Projectile.Size = new Vector2(16);
        }

        public override void AI()
        {
            Projectile.velocity.Y += 0.3f;
            if (Projectile.ai[0] > 40 && !rolling) Projectile.velocity.X *= 0.97f;
            Projectile.rotation += 0.1f * Projectile.velocity.X / Projectile.scale;
            if (Projectile.ai[0] % 4 == 0)
            {
                Dust d = Dust.NewDustDirect(Projectile.position + new Vector2(4), 8, 8, DustID.Snow, -Projectile.velocity.X / 5f, -Projectile.velocity.Y / 5f, Scale: 1.5f);
                d.noGravity = true;
            }
            if (Projectile.ai[0] > 180 * Projectile.scale)
            {
                TFUtils.Explode(Projectile, (int)(120 * Projectile.scale));
                Projectile.Kill();
            }
            Projectile.ai[0]++;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Point position = (Projectile.Center + new Vector2(0, Projectile.height)).ToTileCoordinates();
            Point slopedposition = position;

            for (int i = 0; i < 3; i++)
            {
                switch (i)
                {
                    case 0: position = (Projectile.Left + new Vector2(0, Projectile.height)).ToTileCoordinates(); break;
                    case 1: position = (Projectile.Center + new Vector2(0, Projectile.height)).ToTileCoordinates(); break;
                    case 2: position = (Projectile.Right + new Vector2(0, Projectile.height)).ToTileCoordinates(); break;
                }
                if (Main.tile[position].BlockType == BlockType.SlopeDownRight || Main.tile[position].BlockType == BlockType.SlopeDownLeft)
                {
                    slopedposition = position;
                }
            }

            if (Main.tile[position].TileType == TileID.SnowBlock)
            {
                int oldheight = Projectile.height;
                Projectile.scale += 0.003f * Math.Abs(Projectile.velocity.X);
                Projectile.Resize((int)(16 * Projectile.scale), (int)(16 * Projectile.scale));
                Projectile.position.Y -= Projectile.height - oldheight;
            }

            if (Main.tile[slopedposition].BlockType == BlockType.SlopeDownLeft)
            {
                rolling = true;
                Projectile.velocity.X += 0.2f;
                if (Projectile.velocity.X > 0) Projectile.position.Y += 2f * Projectile.velocity.X;
            }
            else if (Main.tile[slopedposition].BlockType == BlockType.SlopeDownRight)
            {
                rolling = true;
                Projectile.velocity.X -= 0.2f;
                if (Projectile.velocity.X > 0) Projectile.position.Y -= 2f * Projectile.velocity.X;
            }
            else rolling = false;
            return false;
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            return true;
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 15 * Projectile.scale; i++)
            {
                Dust d = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.Snow, Main.rand.NextFloat(-6f,6f) * 2f * Projectile.scale, Main.rand.NextFloat(-6f, 6f) * 2f * Projectile.scale, Scale: 2f);
                d.noGravity = true;
            }
            SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);
            SoundEngine.PlaySound(SoundID.Item51, Projectile.Center);
            base.OnKill(timeLeft);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Type].Value;
            Main.EntitySpriteDraw(tex,
                Projectile.Center - Main.screenPosition,
                null,
                lightColor,
                Projectile.rotation,
                tex.Size() / 2,
                Projectile.scale,
                SpriteEffects.None);
            return false;
        }
    }
}
