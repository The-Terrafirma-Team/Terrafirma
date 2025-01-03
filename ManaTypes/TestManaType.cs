﻿using System;
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
    public class TestManaType : ManaType
    {
        public override void UseEffect(Player player, NumberRange range)
        {
            player.AddBuff(BuffID.OnFire, 1);
        }

        public override void NotInUseEffect(Player player, NumberRange range)
        {
            player.AddBuff(BuffID.Frostburn, 1);
        }
    }
}
