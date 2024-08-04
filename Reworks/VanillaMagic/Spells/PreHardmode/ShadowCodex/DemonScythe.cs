using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Systems.MageClass;
using Terraria.ID;

namespace Terrafirma.Reworks.VanillaMagic.Spells.PreHardmode.ShadowCodex
{
    internal class DemonScythe : Spell
    {
        public override int UseAnimation => -1;
        public override int UseTime => -1;
        public override int ManaCost => -1;
        public override int ReuseDelay => -1;
        public override int[] SpellItem => [ItemID.DemonScythe];
    }
}
