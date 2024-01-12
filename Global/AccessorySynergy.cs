using Microsoft.Xna.Framework;
using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using TerrafirmaRedux.Global.Structs;
using TerrafirmaRedux.Items.Ammo;
using TerrafirmaRedux.Items.Equipment.Healing;
using TerrafirmaRedux.Items.Equipment.Movement;
using TerrafirmaRedux.Items.Equipment.Ranged;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Global
{
    public class AccessorySynergyPlayer : ModPlayer
    {

        public int[] EquippedAccessories = new int[] { };
        public SynergyData[] ActivatedSynergies = new SynergyData[] { };

        public override void ResetEffects()
        {
            EquippedAccessories = new int[] { };
            ActivatedSynergies = new SynergyData[] { };

        }

        public override void UpdateEquips()
        {
            //Check For Synergies 

            //Silly Ammo Belt
            if (EquippedAccessories.Contains(ModContent.ItemType<DrumMag>()) && EquippedAccessories.Contains(ModContent.ItemType<AmmoCan>()))
            {
                ActivatedSynergies = ActivatedSynergies.Append(new SynergyData("Silly Ammo Belt", "33% chance for guns to shoot a random bullet", new int[] { ModContent.ItemType<DrumMag>(), ModContent.ItemType<AmmoCan>() })).ToArray();
            }
            //The Tank
            if (EquippedAccessories.Contains(ModContent.ItemType<HeartContainer>()))
            {
                ActivatedSynergies = ActivatedSynergies.Append(new SynergyData("The Tank", "Double all synergising Accessories's health increase", new int[] { ModContent.ItemType<HeartContainer>() })).ToArray();
                Player.statLifeMax2 += 150;
            }

        }

        public string[] ActivatedSynergyNames()
        {
            string[] synergies = new string[] { };

            for (int i = 0; i < ActivatedSynergies.Length; i++)
            {
                synergies = synergies.Append(ActivatedSynergies[i].Name).ToArray();
            }

            return synergies;
        }
    }

    public class GlobalAccessorySynergy : GlobalItem
    {
        Player itemplayer;
        string[] ActivatedSynergies = new string[] { };
        public override bool InstancePerEntity => true;

        public override void UpdateEquip(Item item, Player player)
        {
            player.GetModPlayer<AccessorySynergyPlayer>().EquippedAccessories = player.GetModPlayer<AccessorySynergyPlayer>().EquippedAccessories.Append(item.type).ToArray();
            itemplayer = player;
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            SynergyData pickedSynergy = new SynergyData("", "", new int[] { });
            if (itemplayer != null)
            {
                for (int i = 0; i < itemplayer.GetModPlayer<AccessorySynergyPlayer>().ActivatedSynergies.Length; i++)
                {
                    if (itemplayer.GetModPlayer<AccessorySynergyPlayer>().ActivatedSynergies[i].Accessories.Contains(item.type))
                    {
                        pickedSynergy = itemplayer.GetModPlayer<AccessorySynergyPlayer>().ActivatedSynergies[i];
                        break;
                    }
                }
            }


            if (pickedSynergy.Name != "")
            {
                string SynergyAccessories = "";
                for (int i = 0; i < pickedSynergy.Accessories.Length; i++)
                {
                    Item accessory = new Item();
                    accessory.SetDefaults(pickedSynergy.Accessories[i]);

                    SynergyAccessories += accessory.Name;
                    if (i < pickedSynergy.Accessories.Length - 2)
                    {
                        SynergyAccessories += ", ";
                    }
                    else if (i == pickedSynergy.Accessories.Length - 2)
                    {
                        SynergyAccessories += " and ";
                    }
                }


                tooltips.Add(new TooltipLine(TerrafirmaRedux.Mod, "SynergyName", "[c/2BE5FF:" + pickedSynergy.Name + "]"));

                tooltips.Add(new TooltipLine(TerrafirmaRedux.Mod, "SynergyItems", "[c/2BE5FF:" + "Synergy with " + SynergyAccessories + "]"));
                tooltips.Add(new TooltipLine(TerrafirmaRedux.Mod, "SynergyDescription", "[c/2BE5FF:" + pickedSynergy.Description + "]"));



            }
        }
    }
}




