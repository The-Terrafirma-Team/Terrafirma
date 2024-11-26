using Microsoft.Xna.Framework;
using Terrafirma.Buffs.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Consumable.Potions
{
    internal class TastyPotion : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 30;

            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item3;

            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(silver: 2);

            Item.buffType = ModContent.BuffType<Tasty>();
            Item.buffTime = 3600 * 5;
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 20;

            ItemID.Sets.DrinkParticleColors[Type] = new Color[3] {
                new Color(80,80,230),
                new Color(190,120,240),
                new Color(70,40,130)
            };
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.BottledWater, 1)
            .AddIngredient(ItemID.RottenChunk, 1)
            .AddIngredient(ItemID.VileMushroom, 1)
            .AddIngredient(ItemID.Deathweed, 1)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
}
