using TerrafirmaRedux.Projectiles.Ranged;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace TerrafirmaRedux.Items.Ammo
{
    internal class BoneBullet : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 7;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 18;
            Item.height = 30;
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.knockBack = 3f;
            Item.value = Item.sellPrice(0, 0, 0, 20);
            Item.rare = ItemRarityID.Green;
            Item.shoot = ModContent.ProjectileType<BoneBulletProjectile>();
            Item.shootSpeed = 2f;
            Item.ammo = AmmoID.Bullet;
        }

        public override void AddRecipes()
        {
            CreateRecipe(20)
            .AddIngredient(ItemID.Bone, 1)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}
