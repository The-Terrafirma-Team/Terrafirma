﻿using Humanizer;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Terrafirma.Common.Items
{
    internal class PrefixChanges : GlobalItem
    {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            int accuracy = 0;
            switch(item.prefix)
            {
                case PrefixID.Frenzying:
                case PrefixID.Furious:
                    accuracy -= 4;
                    break;
            }

            if (accuracy != 0)
            {
                string tip = "";
                if(accuracy > 0)
                {
                    tip = "+";
                }
                tip = tip + accuracy + Language.GetText("Mods.Terrafirma.Misc.AccuracyPrefix").ToString();
                tooltips.Add(new TooltipLine(Mod, "accuracyModifier", tip) { IsModifier = true, IsModifierBad = accuracy < 0});
            }
        }
        public override void Load()
        {
            On_Item.TryGetPrefixStatMultipliersForItem += On_Item_TryGetPrefixStatMultipliersForItem;
        }
        private bool On_Item_TryGetPrefixStatMultipliersForItem(On_Item.orig_TryGetPrefixStatMultipliersForItem orig, Item self, int rolledPrefix, out float dmg, out float kb, out float spd, out float size, out float shtspd, out float mcst, out int crt)
        {
            orig(self,rolledPrefix, out dmg, out kb, out spd, out size, out shtspd, out mcst, out crt);

            switch (rolledPrefix)
            {
                // universal
                case PrefixID.Hurtful:
                    dmg = 1.2f;
                    crt = -4;
                    break;
                case PrefixID.Forceful:
                    kb = 1.45f;
                    break;
                case PrefixID.Demonic:
                    kb = 0.5f;
                    dmg = 1.25f;
                    break;
                case PrefixID.Keen:
                    crt = 6;
                    break;
                case PrefixID.Zealous:
                    crt = 25;
                    dmg = 0.94f;
                    break;

                // Melee
                case PrefixID.Massive:
                    size = 1.5f;
                    spd = 1.1f;
                    kb = 1.1f;
                    break;
                case PrefixID.Large:
                    size = 1.2f;
                    break;
                case PrefixID.Heavy:
                    kb = 1.75f;
                    dmg = 1.1f;
                    break;
                case PrefixID.Savage:
                    dmg = 1.25f;
                    spd = 1.1f;
                    size = 1.23f;
                    crt = -8;
                    break;
                case PrefixID.Bulky:
                    dmg = 1.4f;
                    spd = 1.4f;
                    size = 1.24f;
                    kb = 1.1f;
                    break;
                case PrefixID.Dangerous:
                    dmg = 1.1f;
                    crt = 15;
                    size = 0.9f;
                    break;
                case PrefixID.Light:
                    spd = 0.8f;
                    size = 0.85f;
                    kb = 0.9f;
                    break;
                case PrefixID.Small:
                    spd = 0.65f;
                    size = 0.7f;
                    kb = 0.5f;
                    break;

                // ranged
                case PrefixID.Frenzying:
                    spd = 0.63f;
                    dmg = 0.7f;
                    // this one decreases accuracy, see GlobalProjectile.cs
                    break;
                case PrefixID.Hasty:
                    shtspd = 1.8f;
                    spd = 0.8f;
                    dmg = 0.9f;
                    break;
                case PrefixID.Intimidating:
                    kb = 1.3f;
                    crt = 6;
                    shtspd = 0.8f;
                    break;
                case PrefixID.Powerful:
                    dmg = 1.26f;
                    crt = 8;
                    spd = 1.24f;
                    break;
                case PrefixID.Rapid:
                    shtspd = 1.6f;
                    spd = 0.75f;
                    dmg = 0.8f;
                    break;

                // Magic
                case PrefixID.Furious:
                    spd = 0.85f;
                    dmg = 1.45f;
                    kb = 1;
                    mcst = 1.5f;
                    break;
                case PrefixID.Intense:
                    mcst = 1.2f;
                    dmg = 1.2f;
                    break;
            }

            return true;
        }
    }
}
