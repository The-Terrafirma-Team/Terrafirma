using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terrafirma.Systems.MageClass;

namespace Terrafirma.Reworks.VanillaMagic.Spells.PreHardmode.WeatherPain
{
    internal class WeatherPain : Spell
    {
        public override int UseAnimation => -1;
        public override int UseTime => -1;
        public override int ManaCost => -1;
        public override int[] SpellItem => new int[] { ItemID.WeatherPain };

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return true;
        }

        public override void Update(Item item, Player player)
        {
            item.UseSound = SoundID.Item66;
        }
    }
}
