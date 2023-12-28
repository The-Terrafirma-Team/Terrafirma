using Microsoft.Xna.Framework;
using TerrafirmaRedux.Global;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Items.Equipment
{
    public class DrumMag : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToAccessory(8, 16);
            Item.rare = ItemRarityID.Green;
            Item.value = Item.buyPrice(0, 5, 0, 0);
        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<TerrafirmaGlobalPlayer>().DrumMag = true;
        }
    }
}
