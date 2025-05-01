using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terrafirma.Projectiles.Ranged.Bullets;

namespace Terrafirma.Items.Ammo
{
    internal class Chainshot : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 6;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 26;
            Item.height = 24;
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.knockBack = 3f;
            Item.value = Item.sellPrice(0, 0, 0, 10);
            Item.rare = ItemRarityID.Lime;
            Item.shoot = ModContent.ProjectileType<ChainshotProjectile>();
            Item.shootSpeed = 2f;
            Item.ammo = AmmoID.Bullet;
        }
        
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 99;
        }

    }
}
