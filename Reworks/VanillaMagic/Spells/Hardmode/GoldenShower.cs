using Microsoft.Xna.Framework;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terrafirma.Reworks.VanillaMagic.Spells.Hardmode
{
    internal class GoldenShower : Spell
    {
        public override int UseAnimation => 18;
        public override int UseTime => 6;
        public override int ManaCost => 7;
        public override int[] SpellItem => new int[] { ItemID.GoldenShower };
        public override void SetDefaults(Item entity)
        {
            entity.UseSound = null;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            type = ProjectileID.GoldenShowerFriendly;

            if (player.ItemAnimationJustStarted) SoundEngine.PlaySound(SoundID.Item13, player.position);

            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0, 0, 0);
            return false;
        }
    }
}
