using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace Terrafirma.Items.Materials
{
    public class Mistcap : ModItem
    {
        public override void SetDefaults()
        {
            Item.Size = new Vector2(16);
            Item.rare = ItemRarityID.White;
            Item.maxStack = Item.CommonMaxStack;
            Item.value = Item.sellPrice(0, 0, 2, 0);
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 99;
        }
    }
}
