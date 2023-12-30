using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TerrafirmaRedux.Global;
using TerrafirmaRedux.Projectiles.Ranged;

namespace TerrafirmaRedux.Items.Weapons.Ranged
{
    internal class IcicleMachineGun : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
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
            return new Vector2(-12,3);
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            Vector2 muzzleoff = new Vector2(26, -5 * player.direction).RotatedBy(velocity.ToRotation()) + new Vector2(13,0);
            position = player.MountedCenter + muzzleoff;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i < 8; i++)
            {
                Dust.NewDust(position + new Vector2(-13,0), 2, 2, DustID.Ice, velocity.X * Main.rand.NextFloat(0.2f, 0.3f), velocity.Y * Main.rand.NextFloat(0.2f, 0.3f), 0, default, Main.rand.NextFloat(0.8f, 1f));
            }
            return true;
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
