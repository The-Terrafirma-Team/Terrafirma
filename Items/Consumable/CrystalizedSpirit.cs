using Microsoft.Xna.Framework;
using Terrafirma.Common.Interfaces;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Consumable
{
    public class CrystalizedSpirit : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.LifeCrystal);
        }
        
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 99;
        }
        public override bool ConsumeItem(Player player)
        {
            return player.PlayerStats().SpiritCrystals < 5;
        }
        public override bool? UseItem(Player player)
        {
            if(player.PlayerStats().SpiritCrystals < 5 && player.ItemAnimationJustStarted)
            {
                player.PlayerStats().SpiritCrystals++;
                player.PlayerStats().TensionMax += 20;
                player.PlayerStats().Tension += 20;
            }
            return base.UseItem(player);
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(1f, 1f, 1f, 0.5f);
        }
    }
}
