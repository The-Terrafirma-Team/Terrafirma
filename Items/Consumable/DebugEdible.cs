using Microsoft.Xna.Framework;
using Terrafirma.Common.Interfaces;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Consumable
{
    public class DebugEdible : ModItem
    {
        public override void SetDefaults()
        {
            Item.consumable = true;
            Item.rare = ItemRarityID.Expert;
            Item.maxStack = Item.CommonMaxStack;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.Item29;
            Item.value = Item.sellPrice(0,2);
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.Size = new Vector2(16);
        }
        public override bool CanUseItem(Player player)
        {
            return true;
        }
        public override bool? UseItem(Player player)
        {
            player.PlayerStats().SpiritCrystals = 0;
            player.PlayerStats().TensionMax = 50;
            player.PlayerStats().Tension = 0;
            player.ConsumedManaCrystals = 4;
            return true;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Main.DiscoColor;
        }
    }
}
