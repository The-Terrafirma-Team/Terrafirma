using SteelSeries.GameSense;
using System;
using System.ComponentModel;
using Microsoft.Xna.Framework;
using Terrafirma.Systems;
using Terraria.ModLoader.Config;
using Terrafirma.Systems.UIElements.ConfigElements;

namespace Terrafirma.Common
{
    public class ClientConfig : ModConfig
    {
        public const int MaxSpellBorders = 9;
        public override ConfigScope Mode => ConfigScope.ClientSide;


        [Header("$Mods.Terrafirma.Configs.ClientConfig.Headers.Accessibility")]

        [DrawTicks]
        [Range(0, MaxSpellBorders - 1)]
        [DefaultValue(true)]
        public bool EnableScreenshake;

        [Header("$Mods.Terrafirma.Configs.ClientConfig.Headers.UIPosition")]

        [DrawTicks]
        [Range(0, 1)]
        [DefaultValue(0)]
        public int MeleeDamageMultiplier;

        [DrawTicks]
        [CustomModConfigItem(typeof(ConfigExtraSpellUIPosition))]
        [DefaultValue(typeof(Vector2), "1400, 0")]
        public Vector2 ExtraSpellUiPosition;

        [DrawTicks]
        [DefaultValue(typeof(Color), "160, 255, 255, 200")]
        public Color SpellRingUiColor;

        [DrawTicks]
        [CustomModConfigItem(typeof(ConfigSpellBorderSelector))]
        [DefaultValue(0)]
        public int SpellBorder;

    }
}
