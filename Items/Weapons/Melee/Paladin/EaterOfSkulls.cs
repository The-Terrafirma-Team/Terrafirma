using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terrafirma.Projectiles.Melee.Knight;
using Terrafirma.Projectiles.Melee.Paladin;
using Terrafirma.Data;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terrafirma.Common;
using Terraria.GameInput;
using Terraria.Localization;
using System.Linq;

namespace Terrafirma.Items.Weapons.Melee.Paladin
{
    internal class EaterOfSkulls : ModItem
    {
        public override bool MeleePrefix()
        {
            return true;
        }
        public override void SetDefaults()
        {
            Item.damage = 15;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.knockBack = 6;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.DamageType = DamageClass.Melee;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(silver: 36);

            Item.shoot = ModContent.ProjectileType<Projectiles.Melee.Paladin.EaterOfSkulls>();
            Item.shootSpeed = 10;
            Item.channel = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddTile(TileID.Anvils).AddIngredient(ItemID.DemoniteBar, 8).AddIngredient(ItemID.ShadowScale, 3).Register();
        }
    }
}
