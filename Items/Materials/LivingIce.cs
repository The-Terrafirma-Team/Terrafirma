using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using System;

namespace Terrafirma.Items.Materials
{
    public class LivingIce : ModItem
    {
        public override void SetDefaults()
        {
            Item.Size = new Vector2(24);
            Item.rare = ItemRarityID.Blue;
            Item.maxStack = Item.CommonMaxStack;
            Item.value = Item.sellPrice(0, 0, 1, 0);
        }
        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, new Vector3(0.1f,0.3f,0.3f) * 0.3f);

            if (Main.rand.NextBool(17))
            {
                Dust d = Dust.NewDustDirect(Item.position, 24, 24, DustID.Snow);
                d.velocity *= 0.1f;
                d.noGravity = true;
            }
        }
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 99;
        }
    }
}
