using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Items.Weapons.Melee.Shortswords;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Data
{
    public class ProjectileSets
    {
        public static bool[] TrueMeleeProjectiles = ProjectileID.Sets.Factory.CreateBoolSet();
        public static bool[] DontInheritElementFromSource = ProjectileID.Sets.Factory.CreateBoolSet();
    }
}
