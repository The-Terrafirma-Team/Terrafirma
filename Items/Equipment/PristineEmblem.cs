using Terrafirma.Common;
using Terrafirma.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Equipment
{
    public class PristineEmblem : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToAccessory();
        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<TerrafirmaModPlayer>().PristineEmblem = true;
        }
    }
}
