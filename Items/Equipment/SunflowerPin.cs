using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terrafirma.Buffs.Buffs;
using Terrafirma.Buffs.Debuffs;
using Terrafirma.Common;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Terrafirma.Items.Equipment
{

    [AutoloadEquip(EquipType.Head)]
    public class SunflowerPin : ModItem
    {
        public override void SetStaticDefaults()
        {
            ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true;
        }
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 24;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(silver: 10);
            Item.defense = 1;
        }
        public override void UpdateEquip(Player player)
        {
            player.AddBuff(BuffID.Sunflower, 1);
        }

        public override void AddRecipes()
        {
            Recipe.Create(Type).AddIngredient(ItemID.Sunflower, 1).AddRecipeGroup(RecipeGroupID.IronBar, 1).AddTile(TileID.WorkBenches).Register();
            Recipe.Create(Type).AddIngredient(ItemID.Sunflower, 1).AddRecipeGroup("Terrafirma:CopperBar", 1).AddTile(TileID.WorkBenches).Register();
            base.AddRecipes();
        }
    }
}
