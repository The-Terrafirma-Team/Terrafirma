using Microsoft.Xna.Framework;
using Terrafirma.Common;
using Terraria.ModLoader;

namespace Terrafirma
{
    public partial class Terrafirma : Mod
    {
        public static bool CombatReworkEnabled => ModContent.GetInstance<ServerConfig>().CombatReworkEnabled;
        public static Color TensionGainColor = new Color(64, 222, 170);
        public static Color UnparryableAttackColor = Color.Red;
    }
}
