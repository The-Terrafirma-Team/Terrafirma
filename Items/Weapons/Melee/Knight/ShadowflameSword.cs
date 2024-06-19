using Terrafirma.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Weapons.Melee.Knight
{
    public class ShadowflameSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToSword(90, 25, 8);
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Melee.Knight.ShadowflameSword>();
            Item.UseSound = SoundID.DD2_FlameburstTowerShot;
            Item.shootSpeed = 8;
            Item.crit = 11;
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.sellPrice(0, 2, 0, 0);
        }
        public override bool MeleePrefix()
        {
            return true;
        }
    }
}
