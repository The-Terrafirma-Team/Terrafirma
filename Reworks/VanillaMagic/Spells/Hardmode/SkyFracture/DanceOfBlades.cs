using Microsoft.Xna.Framework;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.VanillaMagic.Spells.Hardmode
{
    internal class DanceOfBlades : Spell
    {
        public override int UseAnimation => 12;
        public override int UseTime => 4;
        public override int ManaCost => 17;
        public override int[] SpellItem => new int[] { ItemID.SkyFracture };

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return true;
        }
    }
}
