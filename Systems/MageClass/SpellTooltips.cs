using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerrafirmaRedux.Global;
using Terraria;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Systems.MageClass
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
            if (ModContent.GetInstance<SpellIndex>().ItemCatalogue.ContainsKey(item.type))
            {
                tooltips.Add(new TooltipLine(TerrafirmaRedux.Mod, "SpellAmount", ModContent.GetInstance<SpellIndex>().ItemCatalogue[item.type].Length + " Spells (Hold " + "Shift" + " For more info)"));
                if (getplayer != null && getplayer.GetModPlayer<Keybinds>().Shifting)
                {
                    for (int i = 0; i < ModContent.GetInstance<SpellIndex>().ItemCatalogue[item.type].Length; i++)
                    {
                        tooltips.Add(new TooltipLine(TerrafirmaRedux.Mod, "SpellDescription", 
                        "[c/AAAAAA:" + 
                        ModContent.GetInstance<SpellIndex>().SpellDescription[ModContent.GetInstance<SpellIndex>().ItemCatalogue[item.type][i]].Item1 + 
                        ": " + 
                        ModContent.GetInstance<SpellIndex>().SpellDescription[ModContent.GetInstance<SpellIndex>().ItemCatalogue[item.type][i]].Item2 + 
                        "]"
                        ));
                    }
                    
                }
            }
        }
    }
}
