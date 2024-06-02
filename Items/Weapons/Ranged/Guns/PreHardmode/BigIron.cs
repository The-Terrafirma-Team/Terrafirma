using Microsoft.Xna.Framework;
using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Projectiles.Ranged;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Weapons.Ranged.Guns.PreHardmode
{
    internal class BigIron : ModItem
    {
        private int ShootTimer = 0;
        private int AnimTimer = 0;
        float ShootRot = 0f;
        public override void SetDefaults()
        {
            Item.damage = 27;
            Item.knockBack = 5f;

            Item.width = 40;
            Item.height = 26;

            Item.autoReuse = true;
            Item.useTime = 28;
            Item.useAnimation = 28;

            Item.shoot = ProjectileID.Bullet;
            Item.useAmmo = AmmoID.Bullet;
            Item.shootSpeed = 15;
            Item.UseSound = SoundID.Item11;
            Item.value = Item.sellPrice(gold: 2);

            Item.rare = ItemRarityID.Blue;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.DamageType = DamageClass.Ranged;
            Item.scale = 0.9f;

            Item.noMelee = true;

        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, -2);
        }

        public override float UseAnimationMultiplier(Player player)
        {
            if (player.altFunctionUse == 2) return 2.5f;
            else return base.UseAnimationMultiplier(player);
        }

        public override float UseTimeMultiplier(Player player)
        {
            if (player.altFunctionUse == 2) return 2.5f;
            else return base.UseTimeMultiplier(player);
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            if (player.itemAnimation % 3 == 0 && player.itemAnimation > Item.useTime * 2.5f - 14)
            {
                float direction = Math.Abs(player.Center.DirectionTo(Main.MouseWorld).ToRotation()) >= MathHelper.PiOver2? 1 : 0;
                player.itemRotation = player.Center.DirectionTo(Main.MouseWorld).RotatedByRandom(0.45).ToRotation() + direction * MathHelper.Pi;
            }
            base.UseStyle(player, heldItemFrame);
        }
        public override void UpdateInventory(Player player)
        {
            if (ShootTimer >= 1) ShootTimer++;
            if ((ShootTimer + 1) % 3 == 0)
            {
                Vector2 velocity = player.DirectionTo(Main.MouseWorld).RotatedByRandom(0.45) * Item.shootSpeed;
                ShootRot = velocity.ToRotation();
                SoundEngine.PlaySound(SoundID.Item11, player.Center);
                Projectile.NewProjectile(Item.GetSource_FromThis(), player.Center + new Vector2(16,10 * -player.direction).RotatedBy(player.Center.DirectionTo(Main.MouseWorld).ToRotation()), velocity, Item.shoot, Item.damage, Item.knockBack, player.whoAmI);
            }
            if (ShootTimer > 14) ShootTimer = 0;
            base.UpdateInventory(player);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                AnimTimer = 1;
                ShootTimer = 1;
                return false;
            }
            else
            {
                return base.Shoot(player, source, position, velocity, type, damage, knockback);
            }
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {

            Vector2 muzzleoff = new Vector2(Item.width - 20, -5 * player.direction).RotatedBy(Math.Atan2(velocity.Y, velocity.X));
            position = player.Center + muzzleoff;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
    }
}
