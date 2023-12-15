using Microsoft.Xna.Framework;
using System;
using TerrafirmaRedux.Projectiles;
using TerrafirmaRedux.Rarities;
using Terraria;
using Terraria.DataStructures;
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
            Item.UseSound = SoundID.Item5;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.noMelee = true;

            Item.rare = ModContent.RarityType<LuckyRarity>();
            Item.value = Item.sellPrice(0, 40, 0, 0);

            Item.useAmmo = AmmoID.Arrow;
            Item.shoot = ModContent.ProjectileType<LuckyArrow>();
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

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            type = ModContent.ProjectileType<LuckyArrow>();
            Vector2 muzzleoff = new Vector2(Item.width - 10, -6 * player.direction).RotatedBy(Math.Atan2(velocity.Y, velocity.X));
            position = player.Center + muzzleoff;
        }
    }
}
