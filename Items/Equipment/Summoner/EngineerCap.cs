using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terrafirma.Buffs.Buffs;
using Terrafirma.Buffs.Debuffs;
using Terrafirma.Common;
using Terrafirma.Common.Players;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Terrafirma.Items.Equipment.Summoner
{

    [AutoloadEquip(EquipType.Head)]
    public class EngineerCap : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 12;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(silver: 10);
            Item.defense = 3;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<PlayerStats>().SentryDamageMultiplier += 0.1f;
        }

        public override void AddRecipes()
        {
            Recipe.Create(Type).AddIngredient(ItemID.Silk, 5).AddTile(TileID.Loom).Register();
            base.AddRecipes();
        }
    }
}
