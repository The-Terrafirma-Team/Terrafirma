using System.Collections.Generic;
using Terrafirma.Utilities;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Terrafirma.Content.Prefixes
{
    // Life
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
        public override PrefixCategory Category => PrefixCategory.Accessory;
        public override IEnumerable<TooltipLine> GetTooltipLines(Item item)
        {
            yield return new TooltipLine(Mod, "PrefixAccDefense", "+2" + Lang.tip[25].Value)
            {
                IsModifier = true
            };
            yield return new TooltipLine(Mod, "MaxLifePrefix", Language.GetText("Mods.Terrafirma.Misc.MaxLifePrefix").WithFormatArgs("+20").ToString())
            {
                IsModifier = true
            };
        }
        public override void ApplyAccessoryEffects(Player player)
        {
            player.statLifeMax2 += 20;
            player.statDefense += 2;
        }
    }

    // Tension
    public class Tense : ModPrefix
    {
        public override PrefixCategory Category => PrefixCategory.Accessory;
        public override IEnumerable<TooltipLine> GetTooltipLines(Item item)
        {
            yield return new TooltipLine(Mod, "PrefixAccDefense", "-1" + Lang.tip[25].Value)
            {
                IsModifier = false
            };
            yield return new TooltipLine(Mod, "MaxTensionPrefix", Language.GetText("Mods.Terrafirma.Misc.MaxTensionPrefix").WithFormatArgs("+10").ToString())
            {
                IsModifier = true
            };
        }
        public override void ApplyAccessoryEffects(Player player)
        {
            player.PlayerStats().TensionMax += 10;
            player.statDefense -= 1;
        }
    }
}
