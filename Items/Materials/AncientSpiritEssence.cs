using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using System;

namespace Terrafirma.Items.Materials
{
    public class AncientSpiritEssence : ModItem
    {
        public override void SetDefaults()
        {
            Item.Size = new Vector2(24);
            Item.rare = ItemRarityID.Green;
            Item.maxStack = Item.CommonMaxStack;
            Item.value = Item.sellPrice(0, 0, 2, 0);
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 255, 255, 0);
        }
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 99;
            ItemID.Sets.ItemIconPulse[Item.type] = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
        }
        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, new Vector3(0.1f, 0.4f + MathF.Sin((float)Main.timeForVisualEffects * 0.01f) * 0.1f, 0.3f + MathF.Sin((float)Main.timeForVisualEffects * 0.03f) * 0.1f));
        }
    }
}
