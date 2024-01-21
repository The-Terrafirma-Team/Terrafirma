using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerrafirmaRedux.Projectiles.Ranged;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Items.Weapons.Ranged.Guns.PreHardmode
{
    internal class GunSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 30;
            Item.knockBack = 5f;

            Item.width = 20;
            Item.height = 20;

            Item.autoReuse = true;
            Item.useTime = 24;
            Item.useAnimation = 24;

            Item.shoot = ProjectileID.Bullet;
            Item.useAmmo = AmmoID.Bullet;
            Item.shootSpeed = 15;
            Item.UseSound = SoundID.Item11;
            Item.value = Item.sellPrice(gold: 1);

            Item.rare = ItemRarityID.Orange;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.DamageType = DamageClass.Ranged;
            Item.scale = 0.9f;


        }

        public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
        {
            hitbox.Width = 20;
            hitbox.Height = 20;
            hitbox.X = (int)player.MountedCenter.X + (int)new Vector2(60, 0).RotatedBy(player.itemRotation).X * player.direction - 10;
            hitbox.Y = (int)player.MountedCenter.Y + (int)new Vector2(60, 0).RotatedBy(player.itemRotation).Y * player.direction - 10;
            base.UseItemHitbox(player, ref hitbox, ref noHitbox);
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-12, -1);
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            Vector2 muzzleoff = new Vector2(Item.width - 20, -5 * player.direction).RotatedBy(Math.Atan2(velocity.Y, velocity.X));
            position = player.Center + muzzleoff;
        }
    }
}
