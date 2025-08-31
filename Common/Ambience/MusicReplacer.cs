using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using System.Threading;
using Terraria.ID;
using Terrafirma.Common;

namespace TerrafirmaSceneries.Common.Ambience
{
    internal class MusicSystem : ModSystem // mfw the setstaticdefaults in the ModSceneEffects didn't work: ):
    {
        public override void SetStaticDefaults()
        {
            MusicID.Sets.SkipsVolumeRemap[MusicLoader.GetMusicSlot("Terrafirma/Assets/Music/Meteor")] = true;
            MusicID.Sets.SkipsVolumeRemap[MusicLoader.GetMusicSlot("Terrafirma/Assets/Music/Hell")] = true;
        }
    }
    internal class MeteorMusic : ModSceneEffect
    {
        public override bool IsSceneEffectActive(Player player)
        {
            return player.ZoneMeteor && ModContent.GetInstance<ClientConfig>().EnableMusicReplacements;
        }
        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeMedium;
        public override int Music => MusicLoader.GetMusicSlot("Terrafirma/Assets/Music/Meteor");
    }
    internal class HellAlternate : ModSceneEffect
    {
        public static bool AltHellTheme = false;
        public override bool IsSceneEffectActive(Player player)
        {
            if(!Main.audioSystem.IsTrackPlaying(MusicID.Hell) && !Main.audioSystem.IsTrackPlaying(Music))
            {
                AltHellTheme = Main.rand.NextBool();
            }
            return player.ZoneUnderworldHeight && AltHellTheme && ModContent.GetInstance<ClientConfig>().EnableMusicReplacements;
        }
        public override SceneEffectPriority Priority => SceneEffectPriority.Environment;
        public override int Music => MusicLoader.GetMusicSlot("Terrafirma/Assets/Music/Hell");
    }
}
