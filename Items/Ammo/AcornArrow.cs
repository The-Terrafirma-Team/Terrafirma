using Microsoft.Xna.Framework.Graphics;
using Terrafirma.Projectiles.Ranged.Arrows;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Ammo
{
    internal class AcornArrow : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 2;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 14;
            Item.height = 36;
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true; 
            Item.knockBack = 2f;
            Item.value = Item.sellPrice(0, 0, 0, 2);
            Item.rare = ItemRarityID.White;
            Item.shoot = ModContent.ProjectileType<AcornArrowProjectile>();
            Item.shootSpeed = 5f;
            Item.ammo = AmmoID.Arrow; 
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 99;
        }

        public override void AddRecipes()
        {
            CreateRecipe(1)
            .AddIngredient(ItemID.WoodenArrow,1)
            .AddIngredient(ItemID.Acorn)
            .Register();
        }
    }


}
