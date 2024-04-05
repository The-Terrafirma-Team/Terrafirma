using Microsoft.Xna.Framework;
using System;
using Terrafirma.Items.Materials;
using Terrafirma.Projectiles.Ranged;
using Terrafirma.Projectiles.Ranged.Arrows;
using Terrafirma.Projectiles.Ranged.Tempire;
using Terrafirma.Rarities;
using Terrafirma.Systems.Elements;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Weapons.Ranged.Tempire
{
    internal class BottleOfPoison : ModItem
    {
        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Generic;
            Item.damage = 40;
            Item.knockBack = 2f;
            Item.useAnimation = 12;
            Item.useTime = 12;

            Item.width = 16;
            Item.height = 28;
            Item.UseSound = SoundID.Item1;
            Item.useStyle = ItemUseStyleID.Swing;

            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.consumable = true;

            Item.maxStack = Item.CommonMaxStack;

            Item.rare = ItemRarityID.Pink;
            Item.value = Item.sellPrice(0, 40, 0, 0);
            Item.shoot = ModContent.ProjectileType<BottleOfPoisonProjectile>();
            Item.shootSpeed = 8f;
        }
        public override void SetStaticDefaults()
        {
            AddElementsToVanillaContent.poisonItem.Add(Type);
        }
        public override void AddRecipes()
        {
            Recipe.Create(Type, 10)
                .AddIngredient(ItemID.BottledWater, 10)
                .AddIngredient(ModContent.ItemType<Mistcap>())
                .Register();

           
        }
    }
}
