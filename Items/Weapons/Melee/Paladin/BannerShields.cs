using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terrafirma.Common.Templates.Melee;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.Localization;

namespace Terrafirma.Items.Weapons.Melee.Paladin
{
    public class RedTowerShield : TowerShield
    {
        public override bool MeleePrefix()
        {
            return true;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips[tooltips.FindLastIndex(tt => tt.Mod.Equals("Terraria") && tt.Name.Equals("Damage"))].Hide();
            base.ModifyTooltips(tooltips);
        }
        public override void SetDefaults()
        {
            Item.damage = 22;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.knockBack = 4;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.DamageType = DamageClass.Melee;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.rare = ContentSamples.ItemsByType[ItemID.LeadHammer].rare;
            Item.value = ContentSamples.ItemsByType[ItemID.LeadHammer].value;

            Item.shoot = ModContent.ProjectileType<Projectiles.Melee.Paladin.BannerTowerShield>();
            Item.shootSpeed = 7;
            Item.channel = true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source,position, velocity, type, damage, knockback,player.whoAmI,ai2: 0);
            return false;
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddTile(TileID.Anvils).AddRecipeGroup(RecipeGroupID.IronBar, 8).AddRecipeGroup(RecipeGroupID.Wood, 15).AddIngredient(ItemID.Silk, 8).Register();
        }
    }
    public class GreenTowerShield : RedTowerShield
    {
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, ai2: 1);
            return false;
        }
    }
    public class YellowTowerShield : RedTowerShield
    {
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, ai2: 2);
            return false;
        }
    }
    public class BlueTowerShield : RedTowerShield
    {
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, ai2: 3);
            return false;
        }
    }
}
