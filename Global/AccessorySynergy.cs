using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Global
{
    public class AccessorySynergyPlayer : ModPlayer {

        public int[] EquippedAccessories = new int[] {};
        string[] ActivatedSynergies = new string[] { };

        public override void ResetEffects()
        {
           EquippedAccessories = new int[] {};
           ActivatedSynergies = new string[] {};
        }

        public override void UpdateEquips()
        {
            //Check For Synergies 
            if (EquippedAccessories.Contains(ItemID.AngelWings) && EquippedAccessories.Contains(ItemID.HandOfCreation))
            {
                ActivatedSynergies = ActivatedSynergies.Append("CoolSynergy").ToArray();
            }

        }

    }

    public class GlobalAccessorySynergy : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override void UpdateEquip(Item item, Player player)
        {
            player.GetModPlayer<AccessorySynergyPlayer>().EquippedAccessories = player.GetModPlayer<AccessorySynergyPlayer>().EquippedAccessories.Append(item.type).ToArray();
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if ( player.GetModPlayer<AccessorySynergyPlayer>() )
        }

    }
}




