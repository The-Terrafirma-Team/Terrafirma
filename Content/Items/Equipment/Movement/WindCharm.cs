using System.Collections.Generic;
using Terrafirma.Common;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Terrafirma.Content.Items.Equipment.Movement
{
    public class WindCharm : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToAccessory();
        }
        public override void UpdateEquip(Player player)
        {
            PlayerStats stats = player.PlayerStats(); 
            stats.AirResistenceMultiplier *= 0.2f;
        }
    }
}
