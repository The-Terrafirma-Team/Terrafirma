using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terrafirma.Projectiles.Ranged.Bullets;

namespace Terrafirma.Items.Ammo
{
    internal class MushroomBullet : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 7;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 14;
            Item.height = 18;
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.knockBack = 1f;
            Item.value = Item.buyPrice(0, 0, 0, 3);
            Item.rare = ItemRarityID.Blue;
            Item.shoot = ModContent.ProjectileType<MushroomBulletProjectile>();
            Item.shootSpeed = 3f;
            Item.ammo = AmmoID.Bullet;
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 99;
        }

        public override void AddRecipes()
        {
            CreateRecipe(10)
            .AddIngredient(ItemID.MusketBall, 10)
            .AddIngredient(ItemID.GlowingMushroom, 1)
            .AddTile(TileID.WorkBenches)
            .Register();
        }

    }
}
