using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Mono.Cecil;
using Terrafirma.Projectiles.Melee.Other;

namespace Terrafirma.Items.Weapons.Melee.OtherMelee
{
    internal class OrichalcumChakram : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 35;
            Item.useTime = 14;
            Item.useAnimation = 14;
            Item.knockBack = 6;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.DamageType = DamageClass.Melee;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.sellPrice(gold: 2, silver: 60);

            Item.shoot = ModContent.ProjectileType<OrichalcumChakramProjectile>();
            Item.shootSpeed = 10;
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 9;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.OrichalcumBar, 10)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
