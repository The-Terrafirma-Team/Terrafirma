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
    internal class IceBlockSpell : Spell
    {
        public override int UseAnimation => 9;
        public override int UseTime => 9;
        public override int ManaCost => 6;
        public override int[] SpellItem => new int[] { ItemID.IceRod };

        public override void Update(Item item, Player player)
        {
            item.useStyle = ItemUseStyleID.Swing;
            base.Update(item, player);
        }

    }
}