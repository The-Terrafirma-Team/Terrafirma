using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TerrafirmaRedux.Global;

namespace TerrafirmaRedux.Items.Weapons.Ranged
{
    internal class KnifeShooter : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            Item.damage = 11;
            Item.knockBack = 2f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 8;
            Item.useTime = 8;
            Item.width = 44;
            Item.height = 22;
            Item.UseSound = SoundID.Item11;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.noMelee = true;

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 0, 20, 0);

            Item.useAmmo = ItemID.ThrowingKnife;
            Item.shoot = ProjectileID.ThrowingKnife;
            Item.shootSpeed = 12f;
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return !Main.rand.NextBool(3);
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0,0);
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            Vector2 muzzleoff = new Vector2(Item.width - 5, 4 * player.direction).RotatedBy(Math.Atan2(velocity.Y,velocity.X));
            position = player.Center + muzzleoff;
            velocity = velocity.RotatedByRandom(0.01f);
        }
    }
}
