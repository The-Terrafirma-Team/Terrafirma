using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terrafirma.Projectiles.Melee.Paladin;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.GameInput;
using Terraria.Localization;

namespace Terrafirma.Items.Weapons.Melee.Paladin
{
    internal class LeadThrowingHammer : ModItem
    {
        public override bool MeleePrefix()
        {
            return true;
        }
        public override void SetDefaults()
        {
            Item.damage = 10;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.knockBack = 4;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.DamageType = DamageClass.Melee;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.rare = ContentSamples.ItemsByType[ItemID.LeadHammer].rare;
            Item.value = ContentSamples.ItemsByType[ItemID.LeadHammer].value;

            Item.shoot = ModContent.ProjectileType<Projectiles.Melee.Paladin.LeadThrowingHammer>();
            Item.shootSpeed = 7;
            Item.channel = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddTile(TileID.Anvils).AddIngredient(ItemID.LeadBar, 8).Register();
        }
    }
}
