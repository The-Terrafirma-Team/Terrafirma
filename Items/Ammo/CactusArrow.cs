using Microsoft.Xna.Framework.Graphics;
using Terrafirma.Projectiles.Ranged.Arrows;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Ammo
{
    internal class CactusArrow : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 3;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 18;
            Item.height = 34;
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true; 
            Item.knockBack = 1f;
            Item.value = Item.sellPrice(0, 0, 0, 2);
            Item.rare = ItemRarityID.White;
            Item.shoot = ModContent.ProjectileType<CactusArrowProjectile>();
            Item.shootSpeed = 4f;
            Item.ammo = AmmoID.Arrow; 
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 99;
        }

        public override void AddRecipes()
        {
            CreateRecipe(25)
            .AddIngredient(ItemID.WoodenArrow,25)
            .AddIngredient(ItemID.Cactus)
            .AddTile(TileID.WorkBenches)
            .Register();
        }
    }


}
