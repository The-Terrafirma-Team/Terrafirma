using Microsoft.Xna.Framework;
using Terrafirma.Common;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Weapons.Ranged.Guns.Hardmode
{
    internal class HandCannon : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 90;
            Item.crit = 20;
            Item.knockBack = 6f;

            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 32;
            Item.useTime = 32;
            Item.width = 56;
            Item.height = 34;
            Item.UseSound = new SoundStyle("Terrafirma/Sounds/Pistol") {PitchVariance = 0.3f, Pitch = -0.3f, MaxInstances = 3 };
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.noMelee = true;

            Item.rare = ItemRarityID.Pink;
            Item.value = Item.sellPrice(0, 1, 90, 0);

            Item.useAmmo = AmmoID.Bullet;
            Item.shoot = ProjectileID.Bullet;
            Item.shootSpeed = 16f;

            Item.scale = 0.8f;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-8, 1);
        }
        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            PlayerAnimation.gunStyle(player, 0.15f, 6, 2);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile P = Projectile.NewProjectileDirect(source,position, velocity, type, damage, knockback,player.whoAmI);
            P.scale *= 1.3f;
            P.extraUpdates += 2;
            P.Resize(P.width * 4, P.height * 4);

            player.velocity += velocity * -0.2f;
            return false;
        }
        public override bool? UseItem(Player player)
        {
            for (int i = 0; i < 5; i++)
            {
                Dust newdust = Dust.NewDustPerfect(player.MountedCenter + new Vector2(-Item.width + 20, -4 * -player.direction).RotatedBy((player.MountedCenter - Main.MouseWorld).ToRotation()), DustID.Smoke, -(Vector2.Normalize(player.MountedCenter - Main.MouseWorld) * Main.rand.NextFloat(1f, 3f)), 200, Color.White, Main.rand.NextFloat(1.2f, 2f));
                newdust.velocity += new Vector2(0, Main.rand.NextFloat(-0.4f, 0.4f)).RotatedBy((player.MountedCenter - Main.MouseWorld).ToRotation());
            }
            for (int i = 0; i < 5; i++)
            {
                Dust newdust = Dust.NewDustPerfect(player.MountedCenter + new Vector2(-Item.width + 20, -4 * -player.direction).RotatedBy((player.MountedCenter - Main.MouseWorld).ToRotation()), DustID.DesertTorch, -(Vector2.Normalize(player.MountedCenter - Main.MouseWorld) * Main.rand.NextFloat(1f, 3f)), 200, Color.White, Main.rand.NextFloat(1.2f, 2f));
                newdust.velocity += new Vector2(0, Main.rand.NextFloat(-0.4f, 0.4f)).RotatedBy((player.MountedCenter - Main.MouseWorld).ToRotation());
                newdust.noGravity = true;
                newdust.noLight = true;
            }
            return base.UseItem(player);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Handgun, 1)
            .AddIngredient(ItemID.HallowedBar, 5)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}
