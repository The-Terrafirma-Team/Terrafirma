﻿using Microsoft.Xna.Framework;
using System.Linq;
using Terrafirma.Common.Players;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace Terrafirma.Items.Equipment.Ranged
{
    internal class AmmoCan : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToAccessory(32, 28);
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.buyPrice(0, 5, 0, 0);
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<TerrafirmaModPlayer>().AmmoCan = true;
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

    }

}
