using Terrafirma.Global.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Equipment.Summoner
{
    public class Uberclock : ModItem
    {
        public override void SetDefaults()
        { 
            Item.DefaultToAccessory();
            Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.rare = ItemRarityID.Lime;
        }
        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            return incomingItem.type != ModContent.ItemType<Overclock>();
        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<PlayerStats>().SentrySpeedMultiplier -= 0.2f;
            player.GetModPlayer<PlayerStats>().SentryRangeMultiplier += 0.2f;
        }
    }
}
