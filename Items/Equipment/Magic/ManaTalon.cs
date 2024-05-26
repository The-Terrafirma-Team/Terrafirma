using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Equipment.Magic
{
    public class ManaTalon : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToAccessory();
            Item.value = ContentSamples.ItemsByType[ItemID.ManaFlower].value;
            Item.value = ItemRarityID.Blue;
        }
    }
}
