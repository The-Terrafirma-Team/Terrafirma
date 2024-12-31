using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Common.Structs;
using Terrafirma.Systems.MageClass.ManaTypes;
using Terraria;
using Terraria.ID;

namespace Terrafirma.ManaTypes
{
    public class TestManaType2 : ManaType
    {

        public override void UseEffect(Player player, NumberRange range)
        {
            player.AddBuff(BuffID.Confused, 1);
        }
    }
}
