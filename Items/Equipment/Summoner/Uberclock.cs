﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerrafirmaRedux.Global;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Items.Equipment.Summoner
{
    public class Uberclock : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToAccessory();
            Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.rare = ItemRarityID.Lime;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<PlayerStats>().SentrySpeedMultiplier -= 0.2f;
            player.GetModPlayer<PlayerStats>().SentryRangeMultiplier += 0.2f;
        }
    }
}
