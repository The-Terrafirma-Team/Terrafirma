using Microsoft.Xna.Framework.Graphics;
using TerrafirmaRedux.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Items.Ammo
{
    internal class PhantasmalArrow : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 16;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 18;
            Item.height = 30;
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true; 
            Item.knockBack = 3f;
            Item.value = Item.sellPrice(0, 0, 0, 20);
            Item.rare = ItemRarityID.Orange;
            Item.shoot = ModContent.ProjectileType<PhantasmalArrowProjectile>();
            Item.shootSpeed = 2f;
            Item.ammo = AmmoID.Arrow; 
        }
    }


}
