using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terrafirma.Projectiles.Ranged.Bullets;

namespace Terrafirma.Items.Ammo
{
    internal class IncendiaryBall : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 10;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 18;
            Item.height = 30;
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.knockBack = 3f;
            Item.value = Item.buyPrice(0, 0, 0, 2);
            Item.rare = ItemRarityID.Orange;
            Item.shoot = ModContent.ProjectileType<IncendiaryBallProjectile>();
            Item.shootSpeed = 3f;
            Item.ammo = AmmoID.Bullet;
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 99;
        }

        public override void AddRecipes()
        {
            CreateRecipe(70)
                .AddIngredient(ItemID.HellstoneBar, 1)
                .AddIngredient(ItemID.MusketBall, 70)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
