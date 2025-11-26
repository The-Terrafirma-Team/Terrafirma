using System.Collections.Generic;
using Terrafirma.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Terrafirma.Content.Items.Equipment.Accessories.Vanilla
{
    public class AnkhCharmShield : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type is ItemID.AnkhCharm or ItemID.AnkhShield;
        }
        public override void UpdateEquip(Item item, Player player)
        {
            player.PlayerStats().DebuffTimeMultiplier *= 0.5f;
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Insert(tooltips.FindAppropriateLineForTooltip(), new TooltipLine(Mod, "Tooltip", Language.GetTextValue("Mods.Terrafirma.VanillaItemTooltips.AnkhCharm")));
        }
    }
}
