using Microsoft.Xna.Framework;
using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace Terrafirma.Common
{
    public class ClientConfig : ModConfig
    {
        public override void OnChanged()
        {
            Terrafirma.UnparryableAttackColor = UnparryableColor with { A = 255 };
        }
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Header("$Mods.Terrafirma.Configs.Headers.Accessibility")]
        [DefaultValue(true)]
        public bool EnableScreenshake;
        [ColorNoAlpha]
        [DefaultValue(typeof(Color), "255, 0, 0, 255")]
        public Color UnparryableColor;

        [Header("$Mods.Terrafirma.Configs.Headers.Preferences")]
        [DefaultValue(true)]
        public bool EnableMusicReplacements;
        [DefaultValue(true)]
        public bool EnableAmbientParticles;

    }
    public class ServerConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        public CombatModule CombatPage = new CombatModule();

        [SeparatePage]
        public class CombatModule
        {
            [Header("$Mods.Terrafirma.Configs.Headers.CombatModule")]
            [DefaultValue(true)]
            [ReloadRequired]
            public bool CombatReworkEnabled;
            [DefaultValue(true)]
            public bool NPCPrefixes;
        }
    }
}
