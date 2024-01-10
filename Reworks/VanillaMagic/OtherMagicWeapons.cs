using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerrafirmaRedux.Global;
using static TerrafirmaRedux.Reworks.VanillaMagic.GemStaves;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using TerrafirmaRedux.Particles;

namespace TerrafirmaRedux.Reworks.VanillaMagic
{
    internal class OtherMagicWeapons : GlobalItemInstanced
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ItemID.RainbowGun;
        }

        public override void SetDefaults(Item entity)
        {
            if (entity.type == ItemID.RainbowGun)
            {
                entity.shoot = ProjectileID.WoodenArrowFriendly;
            }
        }

        //Use Time Multiplier
        public override float UseTimeMultiplier(Item item, Player player)
        {
            switch (item.GetGlobalItem<GlobalItemInstanced>().Spell)
            {
                case 1: return 0.15f;
            }
            return base.UseTimeMultiplier(item, player);
        }

        //Use Animation Multiplier
        public override float UseAnimationMultiplier(Item item, Player player)
        {
            switch (item.GetGlobalItem<GlobalItemInstanced>().Spell)
            {
                case 1: return 0.15f;
            }
            return base.UseAnimationMultiplier(item, player);
        }

        public override bool? CanAutoReuseItem(Item item, Player player)
        {
            return true;
        }

        public override void ModifyManaCost(Item item, Player player, ref float reduce, ref float mult)
        {
            switch (item.GetGlobalItem<GlobalItemInstanced>().Spell)
            {
                case 1: mult = 4/20f; break;
            }
            base.ModifyManaCost(item, player, ref reduce, ref mult);
        }

        //Modify Shoot Stats
        public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            switch (item.GetGlobalItem<GlobalItemInstanced>().Spell)
            {
                case 0:
                    type = ProjectileID.RainbowFront;
                    if (player.ownedProjectileCounts[ProjectileID.RainbowBack] > 0)
                    {
                        for (int j = 0; j < Main.projectile.Length; j++)
                        {
                            if (Main.projectile[j].active && Main.projectile[j].owner == player.whoAmI && (Main.projectile[j].type == ProjectileID.RainbowFront || Main.projectile[j].type == ProjectileID.RainbowBack))
                            {
                                Main.projectile[j].Kill();
                            }
                        }
                    }
                   
                    break;
                case 1:
                    type = ModContent.ProjectileType<ColoredPrism>();
                    damage = (int)(damage * 1.1f);
                    velocity = velocity.RotatedByRandom(0.1f);
                    break;
            }

        }

        //Shoot
        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            switch (item.GetGlobalItem<GlobalItemInstanced>().Spell)
            {
                
            }
            return base.Shoot(item, player, source, position, velocity, type, damage, knockback);
        }

        //Can Use Item
        public override bool CanUseItem(Item item, Player player)
        {
            switch (item.GetGlobalItem<GlobalItemInstanced>().Spell)
            {
            }
            return base.CanUseItem(item, player);
        }
    }

    #region ColoredPrism
    public class ColoredPrism : ModProjectile
    {
        Color ShotColor = new Color(Main.DiscoColor.R, Main.DiscoColor.G, Main.DiscoColor.B, 0);
        public override string Texture => $"TerrafirmaRedux/Reworks/VanillaMagic/RainbowShot";

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Projectile.type] = 4;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;
        }
        public override void SetDefaults()
        {
            Projectile.penetrate = 3;
            Projectile.tileCollide = true;
            Projectile.friendly = true;

            Projectile.timeLeft = 300;
            Projectile.Opacity = 0f;

            Projectile.Size = new Vector2(10);
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D RainbowShot = ModContent.Request<Texture2D>("TerrafirmaRedux/Reworks/VanillaMagic/RainbowShot").Value;

            for (int i = 0; i < 5; i++)
            {
                Main.EntitySpriteDraw(RainbowShot, Projectile.oldPos[i] + Projectile.Size/2 - Main.screenPosition, new Rectangle(0, 0, RainbowShot.Width, RainbowShot.Height), ShotColor * (Projectile.Opacity - (i * 0.2f) - 0.3f), Projectile.oldRot[i], RainbowShot.Size() / 2, 1, SpriteEffects.None, 0);
            }
            Main.EntitySpriteDraw(RainbowShot, Projectile.Center - Main.screenPosition, new Rectangle(0, 0, RainbowShot.Width, RainbowShot.Height), ShotColor * Projectile.Opacity, Projectile.rotation, RainbowShot.Size() / 2, 1, SpriteEffects.None, 0);
            Main.EntitySpriteDraw(RainbowShot, Projectile.Center - Main.screenPosition, new Rectangle(0, 0, RainbowShot.Width, RainbowShot.Height), new Color(1f, 1f, 1f, 0f) * 0.4f * Projectile.Opacity, Projectile.rotation, RainbowShot.Size() / 2, 1, SpriteEffects.None, 0);
            return false;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Projectile.Opacity += 0.075f;

            if (Projectile.timeLeft == 300)
            {
                ParticleSystem.AddParticle(new BigSparkle(), Projectile.Center + Vector2.Normalize(Projectile.velocity) * 46f, Vector2.Zero, ShotColor, 0, 10, 1, 1f, Main.rand.NextFloat(-0.1f, 0.1f));
            }

            if (Main.rand.NextBool(10))
            {
                ParticleSystem.AddParticle(new BigSparkle(), Projectile.Center, Vector2.Zero, ShotColor * 0.3f, 0, 8, Main.rand.NextFloat(0.3f, 0.8f), 1f, Main.rand.NextFloat(-MathHelper.PiOver2, MathHelper.PiOver2));
            }
        }

        public override void OnKill(int timeLeft)
        {
            ParticleSystem.AddParticle(new BigSparkle(), Projectile.Center, Vector2.Zero, ShotColor, 0, 10, 1, 1f, Main.rand.NextFloat(-MathHelper.PiOver2, MathHelper.PiOver2));
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            ParticleSystem.AddParticle(new BigSparkle(), Projectile.Center, Vector2.Zero, ShotColor, 0, 10, 1, 1f, Main.rand.NextFloat(-MathHelper.PiOver2, MathHelper.PiOver2));
        }
    } 
    #endregion
}
