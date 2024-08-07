﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Security.Cryptography.X509Certificates;
using Terrafirma.Common.Items;
using Terrafirma.Systems.MageClass;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace Terrafirma.Common
{
    internal class Keybinds : ModPlayer
    {
        public static ModKeybind tertiaryAttack { get; set; }
        public static ModKeybind previousSpell { get; set; }
        public static ModKeybind nextSpell { get; set; }
        public override void Load()
        {
            tertiaryAttack = KeybindLoader.RegisterKeybind(Mod, "tertiaryAttack", Keys.C);
            previousSpell = KeybindLoader.RegisterKeybind(Mod, "previousSpell", Keys.Z);
            nextSpell = KeybindLoader.RegisterKeybind(Mod, "nextSpell", Keys.X);
        }
        public override void Unload()
        {
            tertiaryAttack = null;
            previousSpell = null;
            nextSpell = null;
        }

        public bool Shifting;
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            
            if (triggersSet.SmartSelect )
            {
                Shifting = true;
            }
            else
            {
                Shifting = false;
            }


            if (previousSpell != null && previousSpell.JustPressed && SpellIndex.ItemCatalogue.ContainsKey(Player.HeldItem.type) && !Player.ItemAnimationActive)
            {
                if (SpellIndex.GetWeaponSpellIndexWithAccessory(Player.HeldItem.GetGlobalItem<GlobalItemInstanced>().Spell, Player.HeldItem.type) > 0)
                {
                    Player.HeldItem.GetGlobalItem<GlobalItemInstanced>().Spell = SpellIndex.GetWeaponSpellFromIndex(SpellIndex.GetWeaponSpellIndexWithAccessory(Player.HeldItem.GetGlobalItem<GlobalItemInstanced>().Spell, Player.HeldItem.type) - 1, Player.HeldItem.type);
                }
                else
                {
                    Player.HeldItem.GetGlobalItem<GlobalItemInstanced>().Spell = SpellIndex.GetWeaponSpellFromIndex(SpellIndex.GetMaxSpellsforWeaponwithAccessory(Player.HeldItem.type) - 1, Player.HeldItem.type);
                }
            }

            if (nextSpell != null && nextSpell.JustPressed && SpellIndex.ItemCatalogue.ContainsKey(Player.HeldItem.type) && !Player.ItemAnimationActive)
            {
                if (SpellIndex.GetWeaponSpellIndexWithAccessory(Player.HeldItem.GetGlobalItem<GlobalItemInstanced>().Spell, Player.HeldItem.type) < SpellIndex.GetMaxSpellsforWeaponwithAccessory(Player.HeldItem.type) - 1)
                {
                    Player.HeldItem.GetGlobalItem<GlobalItemInstanced>().Spell = SpellIndex.GetWeaponSpellFromIndex(SpellIndex.GetWeaponSpellIndexWithAccessory(Player.HeldItem.GetGlobalItem<GlobalItemInstanced>().Spell, Player.HeldItem.type) + 1, Player.HeldItem.type);
                }
                else
                {
                    Player.HeldItem.GetGlobalItem<GlobalItemInstanced>().Spell = SpellIndex.GetWeaponSpellFromIndex(0, Player.HeldItem.type);
                }
                //Player.HeldItem.GetGlobalItem<GlobalItemInstanced>().Spell = SpellIndex.GetWeaponSpellIndex(Player.HeldItem.GetGlobalItem<GlobalItemInstanced>().Spell, Player.HeldItem.type) < SpellIndex.ItemCatalogue[Player.HeldItem.type].Length - 1 ? SpellIndex.ItemCatalogue[Player.HeldItem.type][SpellIndex.GetWeaponSpellIndex(Player.HeldItem.GetGlobalItem<GlobalItemInstanced>().Spell, Player.HeldItem.type) + 1] : SpellIndex.ItemCatalogue[Player.HeldItem.type][0];
            }

        }

    }
}
