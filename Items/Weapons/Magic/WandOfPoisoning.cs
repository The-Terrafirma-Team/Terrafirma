using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using TerrafirmaRedux.Projectiles;

namespace TerrafirmaRedux.Items.Weapons.Magic
{
    internal class WandOfPoisoning : ModItem 
    {

        public override void SetDefaults()
        {
            Item.damage = 12;
            Item.knockBack = 0f;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 26;
            Item.crit = 10;
            Item.useTime = 26;
            Item.width = 20;
            Item.height = 26;
            Item.UseSound = SoundID.Item8;
            Item.DamageType = DamageClass.Magic;
            Item.autoReuse = false;
            Item.noMelee = true;

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 0, 20, 0);

            Item.mana = 3;
            Item.shoot = ModContent.ProjectileType<Poison>();
            Item.shootSpeed = 6f;
        }
    }
}
