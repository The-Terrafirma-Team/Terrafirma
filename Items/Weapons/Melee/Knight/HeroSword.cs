using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terrafirma.Particles;
using Terrafirma.Projectiles.Melee.Knight;
using Terrafirma.Rarities;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Weapons.Melee.Knight
{
    public class HeroSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 100;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.knockBack = 6;

            Item.useStyle = 15;
            Item.DamageType = DamageClass.Melee;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<Projectiles.Melee.Knight.HeroSword>();
            Item.rare = ModContent.RarityType<FinalQuestRarity>();
            Item.value = Item.sellPrice(gold: 20, silver: 00);
            Item.shootSpeed = 20;
            Item.noMelee = true;
            Item.noUseGraphic = true;
        }
        public override bool MeleePrefix()
        {
            return true;
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
            if (player.ownedProjectileCounts[Item.shoot] > 0)
            {
                player.itemLocation = Vector2.Zero;
            }
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, (player.PlayerStats().TimesHeldWeaponHasBeenSwung + 2) % 3);
            return false;
        }

        //public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        //{
        //    BigSparkle bigsparkle = new BigSparkle();
        //    bigsparkle.fadeInTime = 10;
        //    bigsparkle.smallestSize = 0.1f;
        //    bigsparkle.Rotation = Main.rand.NextFloat(-MathHelper.PiOver2, MathHelper.PiOver2);
        //    bigsparkle.Scale = 1f;
        //    ParticleSystem.AddParticle(bigsparkle, target.Center, Vector2.Zero, new Color(Main.DiscoColor.R, Main.DiscoColor.G, Main.DiscoColor.B, 0));
        //    //LegacyParticleSystem.AddParticle(new BigSparkle(), target.Center, Vector2.Zero, new Color(Main.DiscoColor.R, Main.DiscoColor.G, Main.DiscoColor.B, 0), 0, 10, 1, 1f, Main.rand.NextFloat(-MathHelper.PiOver2, MathHelper.PiOver2));
        //    base.OnHitNPC(player, target, hit, damageDone);
        //}
    }
}
