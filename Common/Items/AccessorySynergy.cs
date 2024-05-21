using Microsoft.Xna.Framework;
using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using Terrafirma.Common.Structs;
using Terrafirma.Items.Ammo;
using Terrafirma.Items.Equipment.Healing;
using Terrafirma.Items.Equipment.Movement;
using Terrafirma.Items.Equipment.Ranged;
using Terrafirma.Systems.AccessorySynergy;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Terrafirma.Common.Items
{
    public class AccessorySynergyPlayer : ModPlayer
    {
        /// <summary>
        /// Array of ItemIDs of all Accessories that the player has equipped (Doesn't count vanity slots). Updates Automatically
        /// </summary>
        public int[] EquippedAccessories = new int[] { };
        public AccessorySynergy[] ActivatedSynergies = new AccessorySynergy[] { };

        public override void ResetEffects()
        {
            EquippedAccessories = new int[] { };
            ActivatedSynergies = new AccessorySynergy[] { };
            base.ResetEffects();
        }

        public override void UpdateEquips()
        {

            for (int i = 0; i < SynergyID.SynergyIDs.Count; i++)
            {
                List<int> RequiredAccessories = SynergyID.SynergyIDs[i].SynergyAccessories;
                for (int k = 0; k < SynergyID.SynergyIDs[i].SynergyAccessories.Count; k++)
                {
                    if (EquippedAccessories.Contains(SynergyID.SynergyIDs[i].SynergyAccessories[k])) RequiredAccessories.Remove(SynergyID.SynergyIDs[i].SynergyAccessories[k]);
                }

                if (RequiredAccessories.Count <= 0)
                {
                    ActivatedSynergies = ActivatedSynergies.Append(SynergyID.SynergyIDs[i]).ToArray();
                }
            }

        }
    }

    public class GlobalAccessorySynergy : GlobalItem
    {
        AccessorySynergy[] ActivatedSynergies = new AccessorySynergy[] { };
        public override bool InstancePerEntity => true;

        public override void UpdateEquip(Item item, Player player)
        {
            player.GetModPlayer<AccessorySynergyPlayer>().EquippedAccessories = player.GetModPlayer<AccessorySynergyPlayer>().EquippedAccessories.Append(item.type).ToArray();
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {

            for (int i = 0; i < Main.LocalPlayer.GetModPlayer<AccessorySynergyPlayer>().ActivatedSynergies.Length; i++)
            {
                if (Main.LocalPlayer.GetModPlayer<AccessorySynergyPlayer>().ActivatedSynergies[i].SynergyAccessories.Contains(item.type))
                {
                    AccessorySynergy PickedSynergy = Main.LocalPlayer.GetModPlayer<AccessorySynergyPlayer>().ActivatedSynergies[i];
                    string SynergyAccessoriesString = "Synergy with ";

                    for (int k = 0;  k < PickedSynergy.SynergyAccessories.Count; k++)
                    {
                        if (k < PickedSynergy.SynergyAccessories.Count - 1) SynergyAccessoriesString += new Item(PickedSynergy.SynergyAccessories[k]).Name + ", ";
                        else SynergyAccessoriesString += new Item(PickedSynergy.SynergyAccessories[k]).Name;
                    }

                    TooltipLine synergyname = new TooltipLine(Terrafirma.Mod, "SynergyName" + i, "");
                    synergyname.OverrideColor = new Color(43, 229, 255);
                    synergyname.Text = PickedSynergy.GetSynergyName();
                    tooltips.Add(synergyname);

                    TooltipLine synergyitems = new TooltipLine(Terrafirma.Mod, "SynergyItems" + i, "");
                    synergyitems.OverrideColor = new Color(43, 229, 255);
                    synergyitems.Text = SynergyAccessoriesString;
                    tooltips.Add(synergyitems);

                    TooltipLine synergydescription = new TooltipLine(Terrafirma.Mod, "SynergyDescription" + i, "");
                    synergydescription.OverrideColor = new Color(43, 229, 255);
                    synergydescription.Text = PickedSynergy.GetSynergyDesc();
                    tooltips.Add(synergydescription);
                }
            }

        }
    }
}




