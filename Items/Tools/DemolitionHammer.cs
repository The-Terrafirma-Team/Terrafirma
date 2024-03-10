using Microsoft.Xna.Framework;
using Terrafirma.Projectiles.Tools;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Tools
{
    internal class DemolitionHammer: ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 15;
            Item.DamageType = DamageClass.MeleeNoSpeed;

            Item.knockBack = 5f;
            Item.autoReuse = true;
            Item.shootSpeed = 50f;
            Item.noMelee = true;

            Item.pick = 0;
            Item.axe = 0;
            Item.shoot = ModContent.ProjectileType<DemolitionHammerProjectile>();

            Item.width = 52;
            Item.height = 22;

            Item.useTime = 10;
            Item.useAnimation = 12;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item23;
            Item.noUseGraphic = true;
            Item.channel = true;

            Item.value = Item.buyPrice(gold: 4);
            Item.rare = ItemRarityID.LightRed;
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }
    }
}
