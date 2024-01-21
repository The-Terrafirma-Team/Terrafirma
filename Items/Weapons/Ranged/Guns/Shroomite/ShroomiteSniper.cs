using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerrafirmaRedux.Projectiles.Ranged;
using TerrafirmaRedux.Projectiles.Ranged.Bullets;
using TerrafirmaRedux.Projectiles.Tools;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Items.Weapons.Ranged.Guns.Shroomite
{
    internal class ShroomiteSniper : ModItem
    {

        public override void SetDefaults()
        {
            Item.damage = 195;
            Item.knockBack = 8f;
            Item.crit = 26;

            Item.width = 52;
            Item.height = 28;

            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.autoReuse = true;

            Item.UseSound = SoundID.Item40;

            Item.shoot = ProjectileID.Bullet;
            Item.useAmmo = AmmoID.Bullet;
            Item.shootSpeed = 15;
            Item.value = Item.sellPrice(gold: 10);

            Item.rare = ItemRarityID.Yellow;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.DamageType = DamageClass.Ranged;
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (type == ProjectileID.Bullet || type == ModContent.ProjectileType<ShroomiteBulletProjectile>())
            {
                type = ModContent.ProjectileType<HighVelocityShroomiteBullet>();
            }
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-12, 0);
        }

        public override void HoldItem(Player player)
        {
            player.scope = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.ShroomiteBar, 20)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
