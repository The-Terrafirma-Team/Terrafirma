using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Equipment.Magic
{
    public class LuckyDice : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToAccessory();
            Item.value = ContentSamples.ItemsByType[ItemID.ManaFlower].value;
            Item.rare = ItemRarityID.Blue;
        }
    }
}
