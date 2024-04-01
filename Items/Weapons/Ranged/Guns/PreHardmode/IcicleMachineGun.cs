using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terrafirma.Global;
using Terrafirma.Projectiles.Ranged;
using Terrafirma.Systems;

namespace Terrafirma.Items.Weapons.Ranged.Guns.PreHardmode
{
    internal class IcicleMachineGun : ModItem
    {
        public override void SetStaticDefaults()
        {
            Elements.iceItem.Add(Type);
        }
        public override void SetDefaults()
        {
            Item.damage = 9;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 2f;
            Item.useAnimation = 10;
            Item.useTime = 10;
            Item.ArmorPenetration = 5;

            Item.width = 44;
            Item.height = 22;

            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item11;

            Item.autoReuse = true;
            Item.noMelee = true;

            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(0, 2, 15, 0);

            Item.useAmmo = ItemID.IceBlock;
            Item.shoot = ModContent.ProjectileType<IcicleProjectile>();
            Item.shootSpeed = 20f;

            Item.scale = 0.85f;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-12, 3);
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            Vector2 muzzleoff = new Vector2(26, -8 * player.direction).RotatedBy(velocity.ToRotation());
            position = player.MountedCenter + muzzleoff;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.IllegalGunParts, 1)
            .AddRecipeGroup(RecipeGroupID.IronBar, 10)
            .Register();
        }
    }
}
