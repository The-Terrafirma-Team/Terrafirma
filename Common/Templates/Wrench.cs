using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace Terrafirma.Common.Templates
{
    public abstract class Wrench : ModItem
    {
        public virtual int BuffID => 0;
        public virtual int BuffDuration => 0;
    }
}
