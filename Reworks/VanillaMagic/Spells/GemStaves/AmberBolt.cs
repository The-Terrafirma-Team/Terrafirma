using Microsoft.Xna.Framework;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terrafirma.Reworks.VanillaMagic.Spells.GemStaves
{
    internal class AmberBolt : Spell
    {
        public override int UseAnimation => 28;
        public override int UseTime => 28;
        public override int ManaCost => 7;
        public override string TexurePath => "Terrafirma/Systems/MageClass/SpellIcons/PreHardmode/GemStaff/AmberBolt";
        public override int[] SpellItem => new int[] { ItemID.AmberStaff };

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            type = ProjectileID.AmberBolt;

            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0, 0, 0);
            return false;
        }
    }
}
