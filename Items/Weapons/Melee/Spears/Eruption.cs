using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerrafirmaRedux.Projectiles.Melee;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Items.Weapons.Melee.Spears
{
    internal class Eruption: ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 60;
            Item.useTime = 26;
            Item.useAnimation = 26;
            Item.knockBack = 6;

            Item.useStyle = ItemUseStyleID.Shoot;
            Item.DamageType = DamageClass.Melee;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.sellPrice(gold: 3);

            Item.shoot = ModContent.ProjectileType<EruptionProjectile>();
            Item.shootSpeed = 10;
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }
    }
}
