using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace Terrafirma.Items.Materials
{
    public class MajesticGel : ModItem
    {
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(200, 200, 200, 170);
        }
        public override void SetDefaults()
        {
            Item.Size = new Vector2(16);
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.sellPrice(0, 0, 15, 0);
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 99;
        }
    }
}
