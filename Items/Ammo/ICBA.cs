﻿using Microsoft.Xna.Framework.Graphics;
using Terrafirma.Projectiles.Ranged.Arrows;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Ammo
{
    internal class ICBA : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 15;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 14;
            Item.height = 32;
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true; 
            Item.knockBack = 5f;
            Item.value = Item.buyPrice(0, 0, 0, 80);
            Item.rare = ItemRarityID.Yellow;
            Item.shoot = ModContent.ProjectileType<ICBAProjectile>();
            Item.shootSpeed = 2f;
            Item.ammo = AmmoID.Arrow; 
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 99;
        }

    }
}
