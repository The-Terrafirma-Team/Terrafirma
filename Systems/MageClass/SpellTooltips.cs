using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerrafirmaRedux.Global;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace TerrafirmaRedux.Systems.MageClass
{
    internal class SpellTooltips : GlobalItem
    {
        public override bool InstancePerEntity => true;
        internal Player getplayer;
        internal float manacost;

        public override void UpdateInventory(Item item, Player player)
        {
            getplayer = player;
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (ModContent.GetInstance<SpellIndex>().ItemCatalogue.ContainsKey(item.type) )
            {

                var manaremove = tooltips.Where(tooltip => tooltip.Name == "UseMana").FirstOrDefault();
                tooltips.Remove(manaremove);

                tooltips.Add(new TooltipLine(TerrafirmaRedux.Mod, "SpellAmount", ModContent.GetInstance<SpellIndex>().ItemCatalogue[item.type].Length + " Spells (Hold " + "Shift" + " For more info)"));
                if (getplayer != null && getplayer.GetModPlayer<Keybinds>().Shifting)
                {
                    for (int i = 0; i < ModContent.GetInstance<SpellIndex>().ItemCatalogue[item.type].Length; i++)
                    {
                        tooltips.Add(new TooltipLine(TerrafirmaRedux.Mod, "SpellDescription",
                        "[c/999999:" +
                        ModContent.GetInstance<SpellIndex>().SpellCatalogue[ModContent.GetInstance<SpellIndex>().ItemCatalogue[item.type][i]].Item3 +
                        ": " +
                        ModContent.GetInstance<SpellIndex>().SpellCatalogue[ModContent.GetInstance<SpellIndex>().ItemCatalogue[item.type][i]].Item4 +
                        ". Uses " +
                        (int)(ModContent.GetInstance<SpellIndex>().SpellCatalogue[ModContent.GetInstance<SpellIndex>().ItemCatalogue[item.type][i]].Item5 * getplayer.manaCost) +
                        " Mana ]"
                        ));
                    }

                }
            }
        }
    }
}
