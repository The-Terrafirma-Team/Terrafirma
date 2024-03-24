using Terrafirma.Projectiles.Melee;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Weapons.Melee.Swords
{
    public class Fireaxe : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToSword(30, 35, 4);
            Item.useTime = 60;
            Item.shoot = ModContent.ProjectileType<FireaxeSlash>();
            Item.shootSpeed = 10;
            Item.rare = ItemRarityID.Yellow;
            Item.scale = 1.5f;
        }
    }
}
