using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace Terrafirma.Common
{
    public class MeteorMusic : ModSceneEffect
    {
        public override bool IsSceneEffectActive(Player player)
        {
            return player.ZoneMeteor;
        }
        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeMedium;
        public override int Music => MusicLoader.GetMusicSlot("Terrafirma/Music/EnnwayMeteor");
    }
}
