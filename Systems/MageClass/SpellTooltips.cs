using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Common;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Terrafirma.Systems.MageClass
{
    internal class SpellTooltips : GlobalItem
    {
        public override bool InstancePerEntity => true;
        internal Player getplayer;

        public override void UpdateInventory(Item item, Player player)
        {
            getplayer = player;
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (SpellID.itemcatalogue.ContainsKey(item.type))
            {

                var manaremove = tooltips.Where(tooltip => tooltip.Name == "UseMana").FirstOrDefault();
                tooltips.Remove(manaremove);

                //if (getplayer != null && !getplayer.GetModPlayer<Keybinds>().Shifting)
                //{
                //    if (SpellID.itemcatalogue[item.type].Length > 1)
                //    {
                //        tooltips.Add(new TooltipLine(Terrafirma.Mod, "SpellAmount", SpellID.itemcatalogue[item.type].Length + " Spells (Hold " + "Shift" + " for more info)"));
                //    }
                //    else
                //    {
                //        tooltips.Add(new TooltipLine(Terrafirma.Mod, "SpellAmount", SpellID.itemcatalogue[item.type].Length + " Spell (Hold " + "Shift" + " for more info)"));
                //    }
                //}


                //if (getplayer != null && getplayer.GetModPlayer<Keybinds>().Shifting)
                //{
                if (item.Spell() == null)
                    return;

                for (int i = 0; i < SpellID.itemcatalogue[item.type].Length; i++)
                {
                    int spellmanacost = SpellID.itemcatalogue[item.type][i].ManaCost == -1 ? item.mana : SpellID.itemcatalogue[item.type][i].ManaCost;

                    tooltips.Add(new TooltipLine(Terrafirma.Mod, "SpellDescription",
                    "[c/00ffff:" +
                    SpellID.itemcatalogue[item.type][i].GetSpellName() +
                    ":] [c/999999:" +
                    SpellID.itemcatalogue[item.type][i].GetSpellDesc() +
                    ". Uses " +
                    (int)(spellmanacost * getplayer.manaCost) +
                    " Mana ]"
                    ));
                }

                //}
            }
        }
    }
}
