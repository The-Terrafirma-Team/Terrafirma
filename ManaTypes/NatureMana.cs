using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Buffs.Mana;
using Terrafirma.Systems.MageClass.ManaTypes;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terrafirma.Common.Structs;

namespace Terrafirma.ManaTypes
{
    public class NatureMana : ManaType
    {
        public override int loopingBarTextureSegments => 1;
    }
}
