using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terrafirma.Systems.Elements;
using Terrafirma.Projectiles.Summon.Sentry.PreHardmode;

namespace Terrafirma.Items.Weapons.Summoner.Sentry.PreHardmode
{
    internal class HellfireGargoyleStaff : ModItem
    {
        public override void SetDefaults()
        {
            Item.sentry = true;
            Item.damage = 22;
            Item.DamageType = DamageClass.Summon;
            Item.knockBack = 2f;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.UseSound = SoundID.Item46;

            Item.width = 40;
            Item.height = 40;

            Item.autoReuse = true;
            Item.noMelee = true;
            Item.mana = 20;

            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(0, 0, 54, 0);
            Item.shoot = ModContent.ProjectileType<HellfireGargoyleSentry>();

            Item.GetElementItem().elementData.Fire = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddIngredient(ItemID.HellstoneBar, 17).AddTile(TileID.Anvils).Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player == Main.LocalPlayer)
            {
                int WorldX;
                int WorldY;
                int PushUpY;
                Main.LocalPlayer.FindSentryRestingSpot(type, out WorldX, out WorldY, out PushUpY);

                Projectile.NewProjectile(source, new Vector2(WorldX, WorldY - PushUpY - 8), Vector2.Zero, type, damage, 0, player.whoAmI, 0, 0, player.direction);
                player.UpdateMaxTurrets();
            }
            return false;
        }
    }
}
