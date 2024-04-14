
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terrafirma.Systems.Elements;
using Terrafirma.Spells.Tempire;

namespace Terrafirma.Items.Weapons.Magic.Tempire
{
    public class Majesty : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.staff[Item.type] = true;
        }
        public override void SetDefaults()
        {
            Item.DefaultToMagicWeapon(ModContent.ProjectileType<FantasticalDoubleHelixProj>(), 22, 8, true);
            Item.mana = 5;
            Item.damage = 100;
            Item.value = Item.sellPrice(0, 0, 20, 0);
            Item.UseSound = SoundID.Item8;
            Item.rare = ItemRarityID.Lime;
            Item.GetElementItem().elementData.Arcane = true;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }
    }
}
