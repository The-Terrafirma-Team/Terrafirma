using System.Collections.Generic;
using Terrafirma.Common;
using Terrafirma.Utilities;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Terrafirma.Content.Items.Equipment.Accessories.Defense.Shield
{
    [AutoloadEquip(EquipType.Shield)]
    public class Shield : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToAccessory();
            Item.defense = 2;
        }
        public override void UpdateEquip(Player player)
        {
            PlayerStats stats = player.PlayerStats(); 
            stats.KnockbackResist -= 0.35f;
            stats.ParryDamage += 6;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (!Terrafirma.CombatReworkEnabled)
                return;
            tooltips.Insert(tooltips.FindAppropriateLineForTooltip(), new TooltipLine(Mod,"tooltipCombatRework", Language.GetText("Mods.Terrafirma.Items.Shield.TooltipCombatRework").Value));
        }
    }
}
