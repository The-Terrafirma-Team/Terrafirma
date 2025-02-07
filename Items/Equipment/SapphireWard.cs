using Terrafirma.Common;
using Terrafirma.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Equipment
{
    public class SapphireWard : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToAccessory();
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(silver: 75);
            Item.defense = 3;

        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<TerrafirmaModPlayer>().SapphireWard = true;
        }
    }
}
