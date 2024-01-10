using TerrafirmaRedux.Items.Weapons.Melee.Shortswords;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using TerrafirmaRedux.Projectiles.Summons;
using Terraria.DataStructures;

namespace TerrafirmaRedux.Items.Weapons.Summoner
{
    internal class IchorSentryStaff : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 30;
            Item.knockBack = 1f;
            Item.DamageType = DamageClass.Summon;
            Item.sentry = true;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.UseSound = SoundID.Item8;

            Item.width = 38;
            Item.height = 46;

            Item.autoReuse = true;
            Item.noMelee = true;

            Item.rare = ItemRarityID.Pink;
            Item.value = Item.sellPrice(0, 0, 30, 0);
            Item.shoot = ModContent.ProjectileType<IchorSentry>();

        }
        public override bool? UseItem(Player player)
        {
            Projectile.NewProjectile(default, Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<IchorSentry>(), 14, 0, -1, 0, 0, 0);
            player.UpdateMaxTurrets();
            return true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return false;
        }
    }
}
