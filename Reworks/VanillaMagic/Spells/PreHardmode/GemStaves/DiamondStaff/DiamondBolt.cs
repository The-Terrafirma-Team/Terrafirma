using Microsoft.Xna.Framework;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terrafirma.Reworks.VanillaMagic.Spells.PreHardmode.GemStaves.DiamondStaff
{
    internal class DiamondBolt : Spell
    {
        public override int UseAnimation => 26;
        public override int UseTime => 26;
        public override int ManaCost => 8;
        public override int[] SpellItem => new int[] { ItemID.DiamondStaff };

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            type = ProjectileID.DiamondBolt;

            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0, 0, 0);
            return false;
        }
    }
}
