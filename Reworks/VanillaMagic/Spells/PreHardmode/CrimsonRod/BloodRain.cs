using Microsoft.Xna.Framework;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terrafirma.Reworks.VanillaMagic.Spells.PreHardmode.CrimsonRod
{
    internal class BloodRain : Spell
    {
        public override int UseAnimation => 24;
        public override int UseTime => 24;
        public override int ManaCost => 30;
        public override int[] SpellItem => [ItemID.CrimsonRod];
    }
}
