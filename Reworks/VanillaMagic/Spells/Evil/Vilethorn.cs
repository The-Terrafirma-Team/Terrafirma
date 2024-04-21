using Microsoft.Xna.Framework;
using System;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.VanillaMagic.Spells.Evil
{
    public class Vilethorn : Spell
    {
        public override int UseAnimation => 28;
        public override int UseTime => 28;
        public override int ManaCost => 10;
        public override int[] SpellItem => new int[] { ItemID.Vilethorn };
    }
}
