using TerrafirmaRedux.Projectiles.Ranged;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Items.Ammo
{
    internal class ShroomiteBullet: ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 7;
            Item.knockBack = 3f;

            Item.DamageType = DamageClass.Ranged;

            Item.width = 18;
            Item.height = 30;
            
            Item.maxStack = Item.CommonMaxStack;
            Item.value = Item.sellPrice(copper: 24);
            Item.consumable = true;

            Item.rare = ItemRarityID.Yellow;
            Item.shoot = ModContent.ProjectileType<ShroomiteBulletProjectile>();
            Item.shootSpeed = 2f;

            Item.ammo = AmmoID.Bullet;
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 99;
        }

        public override void AddRecipes()
        {
            CreateRecipe(100)
                .AddIngredient(ItemID.MusketBall, 100)
                .AddIngredient(ItemID.ShroomiteBar)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
