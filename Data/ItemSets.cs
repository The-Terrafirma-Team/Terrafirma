using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;

namespace Terrafirma.Data
{
    public class ItemSets
    {
        public static bool[] ThrowerWeapon = ItemID.Sets.Factory.CreateBoolSet();
        public static bool[] AltFireDoesNotConsumeFeralCharge = ItemID.Sets.Factory.CreateBoolSet();
    }
}
