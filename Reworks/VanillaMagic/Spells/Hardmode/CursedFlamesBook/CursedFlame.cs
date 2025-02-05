using Microsoft.Xna.Framework;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terrafirma.Reworks.VanillaMagic.Spells.Hardmode.CursedFlamesBook
{
    internal class CursedFlame : Spell
    {
        public override int UseAnimation => 15;
        public override int UseTime => 15;
        public override int ManaCost => 9;
        public override int[] SpellItem => new int[] { ItemID.CursedFlames };
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return true;
        }
    }
}
