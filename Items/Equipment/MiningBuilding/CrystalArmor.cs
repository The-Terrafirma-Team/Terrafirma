using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terrafirma.Common;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Terrafirma.Particles;
using Terraria.Audio;
using Terrafirma.Items.Materials;

namespace Terrafirma.Items.Equipment.MiningBuilding
{
    public class CrystalSetbonus : ModPlayer
    {
        public bool set = false;
        public override void ResetEffects()
        {
            set = false;
        }
    }

    [AutoloadEquip(EquipType.Head)]
    public class CrystalHelmet : ModItem
    {
        public override void AddRecipes()
        {
            CreateRecipe().AddTile(TileID.Anvils).AddIngredient(ItemID.Granite, 30).AddIngredient(ItemID.Sapphire, 2).AddIngredient(ModContent.ItemType<EnchantedStone>()).Register();
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<GraniteChestplate>() && legs.type == ModContent.ItemType<GraniteGreaves>();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = Language.GetTextValue("Mods.Terrafirma.Items.GraniteHelmet.SetBonus");
            player.GetModPlayer<CrystalSetbonus>().set = true;
        }
        public override void UpdateVanitySet(Player player)
        {
            player.armorEffectDrawShadow = true;
        }
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(silver: 75);
            Item.defense = 2;
        }
        public override void UpdateEquip(Player player)
        {
            player.pickSpeed -= 0.1f;
        }
    }

    [AutoloadEquip(EquipType.Body)]
    public class CrystalChestplate : ModItem
    {
        public override void AddRecipes()
        {
            CreateRecipe().AddTile(TileID.Anvils).AddIngredient(ItemID.Granite, 40).AddIngredient(ItemID.Sapphire, 5).AddIngredient(ModContent.ItemType<EnchantedStone>(),2).Register();
        }
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(silver: 75);
            Item.defense = 3;
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.05f;
            player.pickSpeed -= 0.1f;
            player.blockRange += 1;
        }
    }

    [AutoloadEquip(EquipType.Legs)]
    public class CrystalGreaves : ModItem
    {
        public override void AddRecipes()
        {
            CreateRecipe().AddTile(TileID.Anvils).AddIngredient(ItemID.Granite, 15).AddIngredient(ItemID.Sapphire, 6).AddIngredient(ModContent.ItemType<EnchantedStone>()).Register();
        }
        public override void SetDefaults()
        {
            Item.defense = 1;
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(silver: 75);
        }
        public override void UpdateEquip(Player player)
        {
            player.blockRange += 1;
            player.moveSpeed += 0.05f;
        }
    }
}
