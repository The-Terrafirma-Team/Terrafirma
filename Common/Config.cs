using System;
using System.ComponentModel;
using Microsoft.Xna.Framework;
using Terraria.ModLoader.Config;

namespace Terrafirma.Common
{
    public class ClientConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Header("$Mods.Terrafirma.Configs.Headers.Accessibility")]
        [DefaultValue(true)]
        public bool EnableScreenshake;

        [Header("$Mods.Terrafirma.Configs.Headers.Preferences")]
        [DefaultValue(true)]
        public bool EnableMusicReplacements;
        [DefaultValue(true)]
        public bool EnableAmbientParticles;
    }
    public class ServerConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Header("$Mods.Terrafirma.Configs.Headers.Modules")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool CombatReworkEnabled;
    }
}
