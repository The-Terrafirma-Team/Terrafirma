using Microsoft.Xna.Framework.Graphics;
using Terrafirma.Projectiles.Ranged.Arrows;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Ammo
{
    internal class KebabArrow : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 5;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 18;
            Item.height = 44;
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true; 
            Item.knockBack = 3f;
            Item.value = Item.buyPrice(0, 0, 10, 0);
            Item.rare = ItemRarityID.Blue;
            Item.shoot = ModContent.ProjectileType<KebabArrowProjectile>();
            Item.shootSpeed = 2f;
            Item.ammo = AmmoID.Arrow; 
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 99;
        }

    }
}
