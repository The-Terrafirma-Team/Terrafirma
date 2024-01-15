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
using TerrafirmaRedux.Projectiles.Ranged.Boomerangs;

namespace TerrafirmaRedux.Items.Weapons.Ranged.Boomerangs.Chakram
{
    internal class CobaltChakram : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 45;
            Item.useTime = 26;
            Item.useAnimation = 26;
            Item.knockBack = 6;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.DamageType = DamageClass.Ranged;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.sellPrice(gold: 1, silver: 05);

            Item.shoot = ModContent.ProjectileType<CobaltChakramProjectile>();
            Item.shootSpeed = 12;
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 3;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.CobaltBar, 10)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
