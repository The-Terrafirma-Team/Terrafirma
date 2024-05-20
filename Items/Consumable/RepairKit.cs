using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Consumable
{
    internal class RepairKit : ModItem
    {
        public override void SetDefaults()
        {
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.width = 11;
            Item.height = 16;
            Item.UseSound = SoundID.Item4;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 0, 10, 0);
            Item.maxStack = 9999;
            Item.consumable = true;
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 99;
        }
    }
}
