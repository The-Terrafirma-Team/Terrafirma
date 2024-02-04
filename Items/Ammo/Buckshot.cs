using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terrafirma.Projectiles.Ranged.Bullets;

namespace Terrafirma.Items.Ammo
{
    internal class Buckshot : ModItem
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
            Item.value = Item.buyPrice(0, 0, 0, 12);
            Item.rare = ItemRarityID.Green;
            Item.shoot = ModContent.ProjectileType<BuckshotProjectile>();
            Item.shootSpeed = 2f;
            Item.ammo = AmmoID.Bullet;
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 99;
        }

    }
}
