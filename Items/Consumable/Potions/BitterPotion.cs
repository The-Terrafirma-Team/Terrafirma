using Microsoft.Xna.Framework;
using Terrafirma.Buffs.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Consumable.Potions
{
    internal class BitterPotion : ModItem
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

            Item.buffType = ModContent.BuffType<Bitter>();
            Item.buffTime = 3600 * 5;
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 20;

            ItemID.Sets.DrinkParticleColors[Type] = new Color[3] {
                new Color(200,60,70),
                new Color(160,40,80),
                new Color(240,130,120)
            };
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.BottledWater, 1)
            .AddIngredient(ItemID.Vertebrae, 1)
            .AddIngredient(ItemID.ViciousMushroom, 1)
            .AddIngredient(ItemID.Deathweed, 1)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
}
