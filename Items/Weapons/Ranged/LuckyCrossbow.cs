using Microsoft.Xna.Framework;
using TerrafirmaRedux.Rarities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Items.Weapons.Ranged
{
    internal class LuckyCrossbow : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 11;
            Item.knockBack = 2f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 8;
            Item.useTime = 8;
            Item.width = 64;
            Item.height = 34;
            Item.UseSound = SoundID.Item1;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.noMelee = true;

            Item.rare = ModContent.RarityType<LuckyRarity>();
            Item.value = Item.sellPrice(0, 40, 0, 0);

            Item.useAmmo = AmmoID.Arrow;
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.shootSpeed = 20f;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-7,-4);
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            if (Main.rand.Next(101) <= 33)
            {
                return false;
            }
            return true;
        }
    }
}
