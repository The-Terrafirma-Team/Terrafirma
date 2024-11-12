using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Consumable.Summon
{
    public class SlimyUmbrella : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.SlimeCrown);
        }
        public override bool? UseItem(Player player)
        {
            Main.StartSlimeRain();
            SoundEngine.PlaySound(SoundID.Roar);
            return base.UseItem(player);
        }
    }
}
