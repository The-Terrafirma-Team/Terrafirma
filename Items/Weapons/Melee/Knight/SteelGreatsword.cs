﻿using Microsoft.Xna.Framework;
using Terrafirma.Items.Materials;
using Terrafirma.Projectiles.Melee.Knight;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Weapons.Melee.Knight
{
    public class SteelGreatsword : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToSword(45, 30, 7);
            Item.useStyle = 666;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Melee.Knight.SteelGreatsword>();
            Item.UseSound = new SoundStyle("Terrafirma/Sounds/SwordSound2") { PitchVariance = 0.3f, Pitch = -0.45f, MaxInstances = 10 };
            Item.shootSpeed = 8;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(0, 0, 45, 0);
        }
        int SlashCost = 30;
        public override bool AltFunctionUse(Player player)
        {
            return player.CheckTension(SlashCost,false);
        }
        public override bool MeleePrefix()
        {
            return true;
        }
        public override float UseSpeedMultiplier(Player player)
        {
            return 1f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                player.CheckTension(SlashCost, true);
                Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<SteelGreatswordSlash>(), damage * 3, knockback * 3, player.whoAmI);
                Projectile.NewProjectile(source, position, velocity, type, (int)(damage * 2f), knockback, player.whoAmI, 2);
            }
            else
            {
                int mhm = player.PlayerStats().TimesHeldWeaponHasBeenSwung % 2 == 0 ? 1 : 0;
                Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, mhm);
            }
            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe().AddTile(TileID.Anvils).AddIngredient(ModContent.ItemType<SteelBar>(), 8).AddRecipeGroup(RecipeGroupID.Wood, 4).Register();
        }
    }
}
