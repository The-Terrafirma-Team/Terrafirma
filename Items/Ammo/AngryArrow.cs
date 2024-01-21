using Microsoft.Xna.Framework.Graphics;
using TerrafirmaRedux.Projectiles.Ranged.Arrows;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Items.Ammo
{
    internal class AngryArrow : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 17;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 14;
            Item.height = 32;
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true; 
            Item.knockBack = 5.25f;
            Item.value = Item.sellPrice(0, 0, 0, 22);
            Item.rare = ItemRarityID.LightPurple;
            Item.shoot = ModContent.ProjectileType<AngryArrowProjectile>();
            Item.shootSpeed = 2f;
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
            .AddIngredient(ItemID.Deathweed)
            .AddIngredient(ItemID.Ectoplasm)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }


}
