﻿using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terrafirma.Projectiles.Melee.Knight;

namespace Terrafirma.Items.Weapons.Melee.Knight
{
    internal class Eruption : ModItem
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
            Item.value = Item.sellPrice(gold: 5);

            Item.shoot = ModContent.ProjectileType<EruptionProjectile>();
            Item.shootSpeed = 10;
            Item.channel = true;
        }
    }
}
