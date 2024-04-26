using Microsoft.Xna.Framework;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.VanillaMagic.Spells.Hardmode
{
    internal class RainbowGunRainbow : Spell
    {
        public override int UseAnimation => 40;
        public override int UseTime => 40;
        public override int ManaCost => 20;
        public override int[] SpellItem => new int[] { ItemID.RainbowGun };

        public override void SetDefaults(Item entity)
        {
            if (entity.type == ItemID.RainbowGun) entity.shoot = ModContent.ProjectileType<ColoredPrism>();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return true;
        }
    }
}
