﻿using Microsoft.Xna.Framework;
using Terrafirma.Common;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Equipment.Healing
{
    class HeartContainer : ModItem
    {
		public override void SetDefaults()
		{
			Item.DefaultToAccessory(30, 30);
			Item.rare = ItemRarityID.LightRed;
			Item.value = Item.buyPrice(0, 5, 0, 0);
		}

		public override void UpdateEquip(Player player)
		{
			player.statLifeMax2 += 50;
		}
	}
}
