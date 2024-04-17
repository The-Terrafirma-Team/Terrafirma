using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.VanillaMagic.Spells.Hardmode
{
    internal class SkySpears : Spell
    {
        public override int UseAnimation => 33;
        public override int UseTime => 11;
        public override int ManaCost => 21;
        public override int ReuseDelay => 0;
        public override string TexurePath => "Terrafirma/Systems/MageClass/SpellIcons/Hardmode/SkyFracture";
        public override int[] SpellItem => new int[] { ItemID.SkyFracture };

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            type = ModContent.ProjectileType<SkySpear>();
            damage = (int)(damage * 1.1f);
            position = Main.MouseWorld - new Vector2(Main.rand.Next(-100, 100), 500);
            velocity = position.DirectionTo(Main.MouseWorld) * 8f;

            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0, Main.MouseWorld.Y, 0);
            return false;
        }
    }

    public class SkySpear : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 8;
        }
        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(20);
            Projectile.friendly = true;
            Projectile.alpha = 0;
            Projectile.tileCollide = false;

            DrawOffsetX = 0;
            DrawOriginOffsetY = 0;

            Projectile.extraUpdates = 3;
        }
        public override void OnSpawn(IEntitySource source)
        {
            for (int i = 0; i < 20; i++)
            {
                Vector2 rotatedvector = new Vector2(4, 0).RotatedBy( ((Math.PI * 2) / 20f) * i);
                Dust dust = Dust.NewDustPerfect(Projectile.position, DustID.DungeonSpirit, new Vector2(rotatedvector.X, rotatedvector.Y * 0.5f), 0, Color.White, 1.5f);
                dust.noGravity = true;
            }

        }
        public override void AI()
        {
            if (Projectile.ai[0] == 0)
            {
                Projectile.frame = Main.rand.Next(8);
                Projectile.Opacity = 0f;
            }
            if (Projectile.position.Y > Projectile.ai[1]) Projectile.tileCollide = true;
            Projectile.rotation = Projectile.velocity.ToRotation() - MathHelper.PiOver2;
            Projectile.Opacity += 0.1f;
            Projectile.ai[0]++;

        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Type].Value;
            for (int i = 0; i < 5; i++)
            {
                Main.EntitySpriteDraw(tex, 
                    Projectile.position - Main.screenPosition - Projectile.velocity * (i * 4) + new Vector2(6,0), 
                    new Rectangle(0, Projectile.frame * 48, tex.Width, tex.Height / 8), 
                    new Color(0.8f * (0.9f - 0.15f * i), 1f * (1f - 0.1f * i), 1f, 0) * (0.9f - 0.15f * i) * Projectile.Opacity, 
                    Projectile.rotation, 
                    new Vector2(tex.Width, tex.Height / 8) / 2, 
                    Projectile.scale + (i * 0.3f), 
                    SpriteEffects.None, 
                    0f);
            }
            return false;
        }
        public override void OnKill(int timeLeft)
        {
            Collision.TileCollision(Projectile.position, Projectile.velocity, 4, 4, true);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);

            for (int i = 0; i < 3; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position + Projectile.velocity * 3, 2, 2, DustID.DungeonSpirit, -Projectile.velocity.X / 2f, -Projectile.velocity.Y / 2f, 0, Color.White, 2f);
                dust.noGravity = true;
            }
            for (int i = 0; i < 3; i++)
            {
                Dust dust = Dust.NewDustPerfect(Projectile.position + Projectile.velocity * 3, DustID.DungeonSpirit, Main.rand.NextVector2Circular(5, 5), 0, Color.White, 2f);
                dust.noGravity = true;
            }
        }
    }
}
