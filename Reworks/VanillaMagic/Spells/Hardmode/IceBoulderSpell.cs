using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terrafirma.Particles;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.VanillaMagic.Spells.Hardmode
{
    internal class IceBoulderSpell : Spell
    {
        public override int UseAnimation => 24;
        public override int UseTime => 24;
        public override int ManaCost => 16;
        public override int[] SpellItem => new int[] { ItemID.IceRod };

    }
}