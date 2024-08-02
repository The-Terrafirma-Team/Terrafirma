using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terrafirma.Projectiles.Melee.Knight;

namespace Terrafirma.Items.Weapons.Melee.Knight
{
    internal class CarmineHook : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return false;
        }
        public override void SetDefaults()
        {
            Item.damage = 70;
            Item.useTime = 26;
            Item.useAnimation = 26;
            Item.knockBack = 6;

            Item.useStyle = ItemUseStyleID.Shoot;
            Item.DamageType = DamageClass.Melee;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(gold: 4, silver: 95);

            Item.shoot = ModContent.ProjectileType<CarmineHookProjectile>();
            Item.shootSpeed = 10;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.DarkLance)
                .AddIngredient(ItemID.TheRottedFork)
                .AddIngredient(ItemID.Ectoplasm, 10)
                .AddIngredient(ItemID.Ichor, 10)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
