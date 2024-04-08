using Microsoft.Xna.Framework;
using Terrafirma.Common.Players;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Equipment.Ranged
{
    public class DrumMag : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToAccessory(8, 16);
            Item.rare = ItemRarityID.Green;
            Item.value = Item.buyPrice(0, 5, 0, 0);
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<TerrafirmaModPlayer>().DrumMag = true;
            player.GetDamage(DamageClass.Ranged) *= 1.05f;
        }
    }
}
