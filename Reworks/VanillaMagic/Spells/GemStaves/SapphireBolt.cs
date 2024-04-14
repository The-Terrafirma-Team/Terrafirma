using Microsoft.Xna.Framework;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terrafirma.Reworks.VanillaMagic.Spells.GemStaves
{
    internal class SapphireBolt : Spell
    {
        public override int UseAnimation => 34;
        public override int UseTime => 34;
        public override int ManaCost => 6;
        public override int[] SpellItem => new int[] { ItemID.SapphireStaff };

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            type = ProjectileID.SapphireBolt;

            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0, 0, 0);
            return false;
        }
    }
}
