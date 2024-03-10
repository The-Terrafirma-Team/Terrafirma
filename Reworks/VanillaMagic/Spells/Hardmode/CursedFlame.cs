using Microsoft.Xna.Framework;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terrafirma.Reworks.VanillaMagic.Spells.Hardmode
{
    internal class CursedFlame : Spell
    {
        public override int UseAnimation => 15;
        public override int UseTime => 15;
        public override int ManaCost => 9;
        public override string TexurePath => "Terrafirma/Systems/MageClass/SpellIcons/Hardmode/CursedFlame";
        public override int[] SpellItem => new int[] { ItemID.CursedFlames };

        public override void SetDefaults(Item entity)
        {
            entity.UseSound = null;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return true;
        }
    }
}
