using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terrafirma.Common.Structs
{
    public struct SynergyData
    {
        public string Name;
        public string Description;
        public int[] Accessories;

        public SynergyData(string Name, string Description, int[] Accessories)
        {
            this.Name = Name;
            this.Description = Description;
            this.Accessories = Accessories;
        }

        public SynergyData(string Name)
        {
            this.Name = Name;
            this.Description = "";
            this.Accessories = new int[] {};
        }
    }
}
