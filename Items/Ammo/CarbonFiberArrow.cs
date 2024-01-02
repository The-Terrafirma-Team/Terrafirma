using Microsoft.Xna.Framework.Graphics;
using TerrafirmaRedux.Projectiles.Ranged;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Items.Ammo
{
    internal class CarbonFiberArrow : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 19;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 14;
            Item.height = 34;
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true; 
            Item.knockBack = 3f;
            Item.value = Item.buyPrice(0, 0, 0, 60);
            Item.rare = ItemRarityID.LightRed;
            Item.shoot = ModContent.ProjectileType<CarbonFiberArrowProjectile>();
            Item.shootSpeed = 5f;
            Item.ammo = AmmoID.Arrow; 
        }
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 99;
        }

    }
}
