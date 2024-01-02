using TerrafirmaRedux.Projectiles.Tools;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Items.Tools
{
    internal class Uldrax: ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 37;
            Item.DamageType = DamageClass.MeleeNoSpeed;

            Item.knockBack = 5f;
            Item.autoReuse = true;
            Item.shootSpeed = 50f;
            Item.noMelee = true;

            Item.pick = 200;
            Item.axe = 110;
            Item.tileBoost = -1;
            Item.shoot = ModContent.ProjectileType<UldraxProjectile>();

            Item.width = 76;
            Item.height = 24;

            Item.useTime = 3;
            Item.useAnimation = 12;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item23;
            Item.noUseGraphic = true;
            Item.channel = true;

            Item.value = Item.sellPrice(gold: 6);
            Item.rare = ItemRarityID.Pink;
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.HallowedBar, 20)
                .AddIngredient(ItemID.SoulofFright, 5)
                .AddIngredient(ItemID.SoulofMight, 5)
                .AddIngredient(ItemID.SoulofSight, 5)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
