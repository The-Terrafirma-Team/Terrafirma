using System;
using System.ComponentModel;
using Terrafirma.Systems;
using Terraria.ModLoader.Config;

namespace Terrafirma.Common
{
    public class ClientConfig : ModConfig
    {
        public const int MaxSpellBorders = 7;
        public override ConfigScope Mode => ConfigScope.ClientSide;
        [Header("$Mods.Terrafirma.Configs.ClientConfig.SpellVisuals")]

        [Range(0, MaxSpellBorders - 1)]
        [DefaultValue(0)]
        public int SpellBorder;

        [Range(12, 255)]
        [DefaultValue(47)]
        public byte SpellR;

        [Range(12, 255)]
        [DefaultValue(215)]
        public byte SpellG;

        [Range(12, 255)]
        [DefaultValue(237)]
        public byte SpellB;

        [Range(0, 1)]
        [DefaultValue(0)]
        public int MeleeDamageMultiplier;
    }
}
