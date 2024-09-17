using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terrafirma.Projectiles.Melee.Knight;
using Terrafirma.Projectiles.Melee.Paladin;

namespace Terrafirma.Items.Weapons.Melee.Paladin
{
    internal class EaterOfSkulls : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 60;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.knockBack = 6;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.DamageType = DamageClass.Melee;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(silver: 36);

            Item.shoot = ModContent.ProjectileType<Projectiles.Melee.Paladin.EaterOfSkulls>();
            Item.shootSpeed = 10;
            Item.channel = true;
        }
    }
}
