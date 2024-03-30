using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terrafirma.Buffs.Buffs;
using Terrafirma.Global.Templates;
using Terrafirma.Items.Materials;
using Terraria.DataStructures;
using Terrafirma.Systems.Cooking;

namespace Terrafirma.Items.Consumable.Food
{
    internal class LatiaoSticks : FoodTemplate
    {
        
        public override void SetDefaults()
        {
            Item.DefaultToFood(31, 16, BuffID.WellFed, 3600 * 15);

            Item.rare = ItemRarityID.Green;
            Item.value = Item.buyPrice(silver: 30);
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 20;
            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(int.MaxValue, 3));
            ItemID.Sets.FoodParticleColors[Item.type] = new Color[3] {
                new Color(174, 31, 12),
                new Color(200, 62, 16),
                new Color(234, 170, 106)
            };
            ItemID.Sets.IsFood[Type] = true;
        }
    }
}
