using Microsoft.Xna.Framework;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terrafirma.Reworks.VanillaMagic.Spells.PreHardmode.GemStaves.EmeraldStaff
{
    internal class EmeraldBolt : Spell
    {
        public override int UseAnimation => 32;
        public override int UseTime => 32;
        public override int ManaCost => 6;
        public override int[] SpellItem => new int[] { ItemID.EmeraldStaff };

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            type = ProjectileID.EmeraldBolt;

            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0, 0, 0);
            return false;
        }

        public override void Update(Item item, Player player)
        {
            item.UseSound = SoundID.Item8;
            item.channel = false;
            base.Update(item, player);
        }
    }
}
