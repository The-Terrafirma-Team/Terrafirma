using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Projectiles.Melee;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terrafirma.Systems.Elements;

namespace Terrafirma.Items.Weapons.Melee.Spears
{
    internal class CarmineHook: ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 70;
            Item.useTime = 26;
            Item.useAnimation = 26;
            Item.knockBack = 6;

            Item.useStyle = ItemUseStyleID.Shoot;
            Item.DamageType = DamageClass.Melee;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(gold: 4, silver: 95);

            Item.shoot = ModContent.ProjectileType<CarmineHookProjectile>();
            Item.shootSpeed = 10;

            Item.GetElementItem().elementData.Dark = true;
            Item.GetElementItem().elementData.Water = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.DarkLance)
                .AddIngredient(ItemID.TheRottedFork)
                .AddIngredient(ItemID.Ectoplasm, 10)
                .AddIngredient(ItemID.Ichor, 10)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
