using Terrafirma.Common;
using Terrafirma.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Content.Items.Equipment.Accessories.Movement.WindCharm
{
    public class WindCharm : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToAccessory();
            Item.value = Item.sellPrice(0, 1, 50);
            Item.rare = ItemRarityID.Blue;
        }
        public override void UpdateEquip(Player player)
        {
            PlayerStats stats = player.PlayerStats(); 
            stats.AirResistenceMultiplier *= 0.2f;
        }
    }
}
