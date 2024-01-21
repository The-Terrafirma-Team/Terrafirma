using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using TerrafirmaRedux.Projectiles.Ranged.Bullets;

namespace TerrafirmaRedux.Items.Ammo
{
    internal class BouncyBuckshot : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 9;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 18;
            Item.height = 30;
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.knockBack = 3f;
            Item.value = Item.sellPrice(0, 0, 0, 12);
            Item.rare = ItemRarityID.LightRed;
            Item.shoot = ModContent.ProjectileType<BouncyBuckshotProjectile>();
            Item.shootSpeed = 2f;
            Item.ammo = AmmoID.Bullet;
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 99;
        }

        public override void AddRecipes()
        {
            CreateRecipe(50)
            .AddIngredient(ModContent.ItemType<Buckshot>(), 50)
            .AddIngredient(ItemID.PinkGel, 1)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }

    }
}
