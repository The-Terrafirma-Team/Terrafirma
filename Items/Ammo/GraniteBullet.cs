using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terrafirma.Items.Materials;

namespace Terrafirma.Items.Ammo
{
    internal class GraniteBullet : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 6;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 18;
            Item.height = 30;
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.knockBack = 2f;
            Item.value = Item.sellPrice(0, 0, 0, 20);
            Item.rare = ItemRarityID.Green;
            Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.Bullets.GraniteBullet>();
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
            .AddIngredient(ItemID.Granite, 15)
            .AddIngredient(ItemID.MusketBall, 70)
            .AddIngredient(ModContent.ItemType<EnchantedStone>())
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}
