using TerrafirmaRedux.Items.Weapons.Melee.Shortswords;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using TerrafirmaRedux.Projectiles.Summons;
using Terraria.DataStructures;

<<<<<<<< HEAD:Items/Weapons/Summoner/Sentry/Hardmode/IchorSentryStaff.cs
namespace TerrafirmaRedux.Items.Weapons.Summoner.Sentry.Hardmode
========
namespace TerrafirmaRedux.Items.Weapons.Summoner.Sentry
>>>>>>>> 94aca8f26a8d46abea18f75efe3a8ed1b0c597fd:Items/Weapons/Summoner/Sentry/IchorSentryStaff.cs
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
            Item.UseSound = SoundID.DD2_DefenseTowerSpawn;
            Item.mana = 20;
            Item.width = 38;
            Item.height = 46;

            Item.autoReuse = true;
            Item.noMelee = true;



            Item.rare = ItemRarityID.Pink;
            Item.value = Item.sellPrice(0, 4, 05, 0);
            Item.shoot = ModContent.ProjectileType<IchorSentry>();

        }

        public override void AddRecipes()
        {
            CreateRecipe(1)
            .AddIngredient(ItemID.SoulofFright, 20)
            .AddIngredient(ItemID.HallowedBar, 10)
            .AddIngredient(ItemID.Ichor, 10)
            .Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player == Main.LocalPlayer)
            {
                int WorldX;
                int WorldY;
                int PushUpY;
                Main.LocalPlayer.FindSentryRestingSpot(type, out WorldX, out WorldY, out PushUpY);

                Projectile.NewProjectile(default, new Vector2(WorldX, WorldY - PushUpY + 8), Vector2.Zero, type, damage, 0, player.whoAmI, 0, 0, 0);
                player.UpdateMaxTurrets();
            }
            return false;
        }
    }
}
