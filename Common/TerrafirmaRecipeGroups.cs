using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Terrafirma.Common
{
    internal class TerrafirmaRecipeGroups : ModSystem
    {
        public override void AddRecipeGroups()
        {
            RecipeGroup GoldBars = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.GoldBar)}", ItemID.GoldBar, ItemID.PlatinumBar);
            RecipeGroup.RegisterGroup(nameof(ItemID.GoldBar), GoldBars);
        }
    }
}
