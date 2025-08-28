using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Common;
using Terraria.ModLoader;

namespace Terrafirma
{
	// Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
	public partial class Terrafirma : Mod
	{
		public static bool CombatReworkEnabled => ModContent.GetInstance<ServerConfig>().CombatReworkEnabled;
	}
}
