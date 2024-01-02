using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using TerrafirmaRedux.Buffs.Buffs;

namespace TerrafirmaRedux.Items.Consumable
{
    internal class SlapJuice: ModItem
    { 
        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 30;

            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item3;

            Item.maxStack = 30;
            Item.consumable = true;

            Item.rare = ItemRarityID.Orange;
            Item.value = Item.buyPrice(silver: 2);

            Item.buffType = ModContent.BuffType<SlapBuff>();
            Item.buffTime = 120; 
        }

        public override bool CanUseItem(Player player)
        {
            return base.CanUseItem(player) && !player.HasBuff(ModContent.BuffType<SlapBuff>());
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 20;

            ItemID.Sets.DrinkParticleColors[Type] = new Color[3] {
                new Color(243, 98, 0),
                new Color(255, 187, 71),
                new Color(255, 219, 65)
            };
        }
    }
}
