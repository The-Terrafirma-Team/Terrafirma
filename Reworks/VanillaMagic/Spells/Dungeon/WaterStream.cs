using Microsoft.Xna.Framework;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terrafirma.Reworks.VanillaMagic.Spells.Dungeon
{
    internal class WaterStream : Spell
    {
        public override int UseAnimation => 16;
        public override int UseTime => 8;
        public override int ManaCost => 7;
        public override int[] SpellItem => new int[] { ItemID.AquaScepter };

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return true;
        }
    }
 
}
