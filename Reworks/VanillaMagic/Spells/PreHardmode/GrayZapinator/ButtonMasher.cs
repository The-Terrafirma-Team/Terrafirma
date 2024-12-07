using Microsoft.Xna.Framework;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terrafirma.Reworks.VanillaMagic.Spells.PreHardmode.GrayZapinator
{
    internal class ButtonMasher : Spell
    {
        public override int UseAnimation => -1;
        public override int UseTime => -1;
        public override int ManaCost => -1;
        public override int[] SpellItem => new int[] { ItemID.ZapinatorGray };

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return true;
        }
    }

}
