﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerrafirmaRedux.Projectiles.Melee;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Mono.Cecil;

namespace TerrafirmaRedux.Items.Weapons.Melee.Boomerangs.Chakram
{
    internal class CopperChakram : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 9;
            Item.useTime = 26;
            Item.useAnimation = 26;
            Item.knockBack = 6;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.DamageType = DamageClass.Melee;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.rare = ItemRarityID.White;
            Item.value = Item.sellPrice(silver: 7, copper: 50);

            Item.shoot = ModContent.ProjectileType<CopperChakramProjetile>();
            Item.shootSpeed = 10;
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.CopperBar, 10)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
