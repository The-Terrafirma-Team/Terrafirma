using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Terrafirma.Prefixes
{  
    public class OK : Healthy
    {
        public override int RegenIncrease => 3;
        public override int maxLifeIncrease => 5;
    }
    public class Healthy : ModPrefix
    {
        public virtual int maxLifeIncrease => 10;
        public virtual int RegenIncrease => 2;
        public override PrefixCategory Category => PrefixCategory.Accessory;
        public override void ApplyAccessoryEffects(Player player)
        {
            player.statLifeMax2 += maxLifeIncrease;
            player.lifeRegen += RegenIncrease;
        }
        public override IEnumerable<TooltipLine> GetTooltipLines(Item item)
        {
            yield return new TooltipLine(Mod, "MaxLifePrefix", Language.GetText("Mods.Terrafirma.Misc.MaxLifePrefix").WithFormatArgs("+" + maxLifeIncrease).ToString())
            {
                IsModifier = true
            };
            yield return new TooltipLine(Mod, "LifeRegenPrefix", Language.GetText("Mods.Terrafirma.Misc.LifeRegenPrefix").WithFormatArgs("+" + RegenIncrease).ToString())
            {
                IsModifier = true
            };
        }
    }
  
    public class Hearty : Healthy
    {
        public override int RegenIncrease => 1;
        public override int maxLifeIncrease => 15;
    }
    public class Stoic : ModPrefix
    {
        public override IEnumerable<TooltipLine> GetTooltipLines(Item item)
        {
            yield return new TooltipLine(Mod, "MaxLifePrefix", Language.GetText("Mods.Terrafirma.Misc.MaxLifePrefix").WithFormatArgs("+20").ToString())
            {
                IsModifier = true
            };
        }
        public override void ApplyAccessoryEffects(Player player)
        {
            player.statLifeMax2 += 20;
        }
    }
}
