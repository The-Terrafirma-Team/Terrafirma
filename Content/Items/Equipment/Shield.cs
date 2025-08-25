﻿using System.Collections.Generic;
using Terrafirma.Common;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Terrafirma.Content.Items.Equipment
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
            stats.ParryDamage += 3;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (!Terrafirma.CombatReworkEnabled)
                return;
            tooltips.Insert(tooltips.FindAppropriateLineForTooltip(), new TooltipLine(Mod,"tooltipCombatRework", Language.GetText("Mods.Terrafirma.Items.Shield.TooltipCombatRework").Value));
        }
    }
}
