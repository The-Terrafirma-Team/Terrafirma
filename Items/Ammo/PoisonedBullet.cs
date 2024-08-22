using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terrafirma.Projectiles.Ranged.Bullets;

namespace Terrafirma.Items.Ammo
{
    internal class PoisonedBullet : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 5;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 10;
            Item.height = 14;
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.knockBack = 1.5f;
            Item.value = Item.sellPrice(0, 0, 0, 2);
            Item.rare = ItemRarityID.Green;
            Item.shoot = ModContent.ProjectileType<PosionBulletProjectile>();
            Item.shootSpeed = 4f;
            Item.ammo = AmmoID.Bullet;
        }
        
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 99;
        }

        public override void AddRecipes()
        {
            CreateRecipe(50)
            .AddIngredient(ItemID.MusketBall, 50)
            .AddIngredient(ItemID.VilePowder, 1)
            .AddTile(TileID.WorkBenches)
            .Register();
            CreateRecipe(50)
            .AddIngredient(ItemID.MusketBall, 50)
            .AddIngredient(ItemID.ViciousPowder, 1)
            .AddTile(TileID.WorkBenches)
            .Register();
        }
    }
}
