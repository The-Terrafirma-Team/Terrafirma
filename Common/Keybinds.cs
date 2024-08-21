using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Terrafirma.Common.Items;
using Terrafirma.Systems.AccessorySynergy;
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


            if (previousSpell != null && previousSpell.JustPressed && SpellID.itemcatalogue.ContainsKey(Player.HeldItem.type) && !Player.ItemAnimationActive)
            {
                if (SpellID.GetWeaponSpellIndexWithAccessory(Player.HeldItem.Spell(), Player.HeldItem.type) > 0)
                {
                    Player.HeldItem.GetGlobalItem<GlobalItemInstanced>().Spell = SpellID.GetWeaponSpellFromIndex(SpellID.GetWeaponSpellIndexWithAccessory(Player.HeldItem.Spell(), Player.HeldItem.type) - 1, Player.HeldItem.type);
                }
                else
                {
                    Player.HeldItem.GetGlobalItem<GlobalItemInstanced>().Spell = SpellID.GetWeaponSpellFromIndex(SpellID.GetMaxSpellsforWeaponwithAccessory(Player.HeldItem.type) - 1, Player.HeldItem.type);
                }
            }

            if (nextSpell != null && nextSpell.JustPressed && SpellID.itemcatalogue.ContainsKey(Player.HeldItem.type) && !Player.ItemAnimationActive)
            {
                if (SpellID.GetWeaponSpellIndexWithAccessory(Player.HeldItem.Spell(), Player.HeldItem.type) < SpellID.GetMaxSpellsforWeaponwithAccessory(Player.HeldItem.type) - 1)
                {
                    Player.HeldItem.GetGlobalItem<GlobalItemInstanced>().Spell = SpellID.GetWeaponSpellFromIndex(SpellID.GetWeaponSpellIndexWithAccessory(Player.HeldItem.Spell(), Player.HeldItem.type) + 1, Player.HeldItem.type);
                }
                else
                {
                    Player.HeldItem.GetGlobalItem<GlobalItemInstanced>().Spell = SpellID.GetWeaponSpellFromIndex(0, Player.HeldItem.type);
                }
                //Player.HeldItem.GetGlobalItem<GlobalItemInstanced>().Spell = SpellIndex.GetWeaponSpellIndex(Player.HeldItem.GetGlobalItem<GlobalItemInstanced>().Spell, Player.HeldItem.type) < SpellIndex.ItemCatalogue[Player.HeldItem.type].Length - 1 ? SpellIndex.ItemCatalogue[Player.HeldItem.type][SpellIndex.GetWeaponSpellIndex(Player.HeldItem.GetGlobalItem<GlobalItemInstanced>().Spell, Player.HeldItem.type) + 1] : SpellIndex.ItemCatalogue[Player.HeldItem.type][0];
            }

            //If held item has spells and its selected spell is not of this item (ex: Accessory spell)
            if(SpellID.itemcatalogue.ContainsKey(Player.HeldItem.type) &&
                Player.HeldItem.Spell() != null &&
               !SpellID.itemcatalogue[Player.HeldItem.type].ContainsSpell(Player.HeldItem.Spell()))
            {
                bool accessoriescontainspell = false;
                for (int i = 0; i < Player.HeldItem.Spell().SpellItem.Length; i++)
                {
                    if (Player.GetModPlayer<AccessorySynergyPlayer>().EquippedAccessories.Contains(Player.HeldItem.Spell().SpellItem[i]))
                    {
                        accessoriescontainspell = true;
                        break;
                    }
                }
                if (!accessoriescontainspell) Player.HeldItem.GetGlobalItem<GlobalItemInstanced>().Spell = SpellID.itemcatalogue[Player.HeldItem.type][0];
            }

        }

    }
}
