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
    public class MushroomCap : ModItem
    { 
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 24;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(silver: 20);
            Item.defense = 1;
        }
        public override void UpdateEquip(Player player)
        {
            player.statLifeMax2 += 25;
        }

        public override void AddRecipes()
        {
            Recipe.Create(Type).AddIngredient(ItemID.Mushroom, 15).AddTile(TileID.WorkBenches).Register();
            base.AddRecipes();
        }
    }
}
