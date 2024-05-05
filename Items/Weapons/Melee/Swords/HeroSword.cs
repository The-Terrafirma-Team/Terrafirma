using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terrafirma.Particles;
using Terrafirma.Projectiles.Melee;
using Terrafirma.Rarities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Weapons.Melee.Swords
{
    public class HeroSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 100;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.knockBack = 6;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.DamageType = DamageClass.Melee;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<HeroSwordProjectile>();

            Item.rare = ModContent.RarityType<FinalQuestRarity>();
            Item.value = Item.sellPrice(gold: 20, silver: 00);
            Item.shootSpeed = 20;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[Item.shoot] < 1) return base.CanUseItem(player);
            return false;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            if(player.ownedProjectileCounts[Item.shoot] > 0)
            { 
                player.itemLocation = Vector2.Zero;
            }
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                player.itemLocation = Vector2.Zero;
                return base.Shoot(player, source, position, velocity, type, damage, knockback);
            }
            return false;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D SwordTexture = ModContent.Request<Texture2D>("Terrafirma/Items/Weapons/Melee/Swords/HeroSword").Value;
            spriteBatch.Draw(SwordTexture,
                Item.position - Main.screenPosition - new Vector2(13, 13),
                new Rectangle(0, 0, 56, 56),
                new Color(Main.DiscoColor.R, Main.DiscoColor.G, Main.DiscoColor.B, 0) * 0.1f,
                rotation,
                new Vector2(16, 42),
                scale: scale + 1f + (float)Math.Sin(Main.timeForVisualEffects / 15f) / 10f,
                SpriteEffects.None,
                0);
            spriteBatch.Draw(SwordTexture,
                Item.position - Main.screenPosition - new Vector2(13, 13),
                new Rectangle(0, 0, 56, 56),
                new Color(Main.DiscoColor.R, Main.DiscoColor.G, Main.DiscoColor.B, 0),
                rotation,
                new Vector2(16, 42),
                scale: scale + 0.25f + (float)Math.Sin(Main.timeForVisualEffects / 20f) / 10f,
                SpriteEffects.None,
                0);
            spriteBatch.Draw(SwordTexture,
                Item.position - Main.screenPosition,
                new Rectangle(0, 0, 56, 56),
                Color.White,
                rotation,
                new Vector2(28, 56),
                scale: scale,
                SpriteEffects.None,
                0);
            spriteBatch.Draw(SwordTexture,
                Item.position - Main.screenPosition - new Vector2(13, 13),
                new Rectangle(0, 0, 56, 56),
                new Color(Main.DiscoColor.R, Main.DiscoColor.G, Main.DiscoColor.B, 0) * 0.5f,
                rotation,
                new Vector2(16, 42),
                scale: scale + (float)Math.Sin(Main.timeForVisualEffects / 20f) / 10f,
                SpriteEffects.None,
                0);

            if (Main.timeForVisualEffects % 40 == 0)
            {
                BigSparkle bigsparkle = new BigSparkle();
                bigsparkle.fadeInTime = 10;
                bigsparkle.Rotation = Main.rand.NextFloat(-MathHelper.PiOver2, MathHelper.PiOver2);
                bigsparkle.Scale = 1f;
                ParticleSystem.AddParticle(bigsparkle, Item.position - new Vector2(13, 13) + Main.rand.NextVector2Circular(20, 40).RotatedBy(MathHelper.PiOver4) - new Vector2(0, 20).RotatedBy(MathHelper.PiOver4), Vector2.Zero, new Color(Main.DiscoColor.R, Main.DiscoColor.G, Main.DiscoColor.B, 0) * 0.5f);
                //LegacyParticleSystem.AddParticle(new BigSparkle(), Item.position - new Vector2(13, 13) + Main.rand.NextVector2Circular(20,40).RotatedBy(MathHelper.PiOver4) - new Vector2(0,20).RotatedBy(MathHelper.PiOver4), Vector2.Zero, new Color(Main.DiscoColor.R, Main.DiscoColor.G, Main.DiscoColor.B, 0) * 0.5f, 0, 10, 1, 1f, Main.rand.NextFloat(-MathHelper.PiOver2, MathHelper.PiOver2));
            }

            return false;
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D SwordTexture = ModContent.Request<Texture2D>("Terrafirma/Items/Weapons/Melee/Swords/HeroSword").Value;

            spriteBatch.Draw(SwordTexture,
                position + new Vector2(-6, 6),
                new Rectangle(0, 0, 56, 56),
                new Color(Main.DiscoColor.R, Main.DiscoColor.G, Main.DiscoColor.B, 0) * 0.1f,
                0,
                new Vector2(16, 42),
                scale: scale + 0.3f + (float)Math.Sin(Main.timeForVisualEffects / 15f) / 20f,
                SpriteEffects.None,
                0);
            spriteBatch.Draw(SwordTexture,
                position + new Vector2(-6, 6),
                new Rectangle(0, 0, 56, 56),
                new Color(Main.DiscoColor.R, Main.DiscoColor.G, Main.DiscoColor.B, 0),
                0,
                new Vector2(16, 42),
                scale: scale + 0.1f + (float)Math.Sin(Main.timeForVisualEffects / 20f) / 20f,
                SpriteEffects.None,
                0);
            spriteBatch.Draw(SwordTexture,
                position + new Vector2(-6, 6),
                new Rectangle(0, 0, 56, 56),
                Color.White,
                0,
                new Vector2(16, 42),
                scale: scale,
                SpriteEffects.None,
                0);
            spriteBatch.Draw(SwordTexture,
                position + new Vector2(-6, 6),
                new Rectangle(0, 0, 56, 56),
                new Color(Main.DiscoColor.R, Main.DiscoColor.G, Main.DiscoColor.B, 0) * 0.5f,
                0,
                new Vector2(16, 42),
                scale: scale + (float)Math.Sin(Main.timeForVisualEffects / 20f) / 10f,
                SpriteEffects.None,
                0);
            return false;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            BigSparkle bigsparkle = new BigSparkle();
            bigsparkle.fadeInTime = 10;
            bigsparkle.smallestSize = 0.1f;
            bigsparkle.Rotation = Main.rand.NextFloat(-MathHelper.PiOver2, MathHelper.PiOver2);
            bigsparkle.Scale = 1f;
            ParticleSystem.AddParticle(bigsparkle, target.Center, Vector2.Zero, new Color(Main.DiscoColor.R, Main.DiscoColor.G, Main.DiscoColor.B, 0));
            //LegacyParticleSystem.AddParticle(new BigSparkle(), target.Center, Vector2.Zero, new Color(Main.DiscoColor.R, Main.DiscoColor.G, Main.DiscoColor.B, 0), 0, 10, 1, 1f, Main.rand.NextFloat(-MathHelper.PiOver2, MathHelper.PiOver2));
            base.OnHitNPC(player, target, hit, damageDone);
        }
    }
}
