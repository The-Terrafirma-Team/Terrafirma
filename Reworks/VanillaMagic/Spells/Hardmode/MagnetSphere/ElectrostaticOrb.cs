using Microsoft.Xna.Framework;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terrafirma.Reworks.VanillaMagic.Spells.Hardmode.MagnetSphere
{
    internal class ElectrostaticOrb : Spell
    {
        public override int[] SpellItem => new int[] { ItemID.MagnetSphere };
    }
}
