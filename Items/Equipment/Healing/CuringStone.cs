using Microsoft.Xna.Framework;
using Terrafirma.Common;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Equipment.Healing
{
    class CuringStone : ModItem
    {
		public override void SetDefaults()
		{
			Item.DefaultToAccessory();
		}

		public override void UpdateEquip(Player player)
		{
			player.PlayerStats().DebuffTimeMultiplier -= 0.2f;
			player.PlayerStats().HealingMultiplier += 0.1f;
		}
	}
}
