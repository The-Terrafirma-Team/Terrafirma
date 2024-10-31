using Microsoft.Xna.Framework;
using Terrafirma.Systems.MageClass;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria;

namespace Terrafirma.Reworks.VanillaMagic.Spells.PreHardmode.WeatherPain
{
    internal class NimbleFeet : Spell
    {
        public override int UseAnimation => 12;
        public override int UseTime => 12;
        public override int ManaCost => 4;
        public override int[] SpellItem => new int[] { ItemID.WeatherPain };

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return false;
        }

        public override void Update(Item item, Player player)
        {
            if (player.ItemAnimationActive && player.HeldItem == item)
            {
                player.moveSpeed *= 2f;
            }

            item.UseSound = null;
        }
    }
}
