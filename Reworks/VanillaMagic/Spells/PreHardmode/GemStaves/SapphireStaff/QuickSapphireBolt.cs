using Microsoft.Xna.Framework;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terrafirma.Reworks.VanillaMagic.Spells.PreHardmode.GemStaves
{
    internal class QuickSapphireBolt : Spell
    {
        public override int UseAnimation => 34;
        public override int UseTime => 34;
        public override int ManaCost => 7;
        public override int[] SpellItem => new int[] { ItemID.SapphireStaff };

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            type = ProjectileID.SapphireBolt;
            velocity *= 2f;
            damage = (int)(damage * 1.2f);

            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0, 0, 0);
            return false;
        }
    }
}
