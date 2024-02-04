using Microsoft.Xna.Framework;
using Terrafirma.Buffs.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Consumable.Potions
{
    public class KnockbackResistPotion : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 20;

            ItemID.Sets.DrinkParticleColors[Type] = new Color[3] {
                new Color(77, 147, 251),
                new Color(60, 113, 189),
                new Color(151, 193, 255)
            };
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.IronskinPotion);
            Item.buffType = ModContent.BuffType<KnockbackResist>();
        }

        public override void AddRecipes()
        {
            Recipe.Create(Type).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.ShadowScale).AddIngredient(ItemID.Granite, 6).AddTile(TileID.Bottles).Register();
        }
    }
}
