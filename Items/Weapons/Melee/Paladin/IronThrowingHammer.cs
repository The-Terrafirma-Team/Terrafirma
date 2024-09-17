using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace Terrafirma.Items.Weapons.Melee.Paladin
{
    internal class IronThrowingHammer : ModItem
    {
        public override bool MeleePrefix()
        {
            return true;
        }
        public override void SetDefaults()
        {
            Item.damage = 20;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.knockBack = 4;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.DamageType = DamageClass.Melee;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.rare = ContentSamples.ItemsByType[ItemID.IronHammer].rare;
            Item.value = ContentSamples.ItemsByType[ItemID.IronHammer].value;

            Item.shoot = ModContent.ProjectileType<Projectiles.Melee.Paladin.IronThrowingHammer>();
            Item.shootSpeed = 7;
            Item.channel = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddTile(TileID.Anvils).AddIngredient(ItemID.IronBar, 8).Register();
        }
    }
}
