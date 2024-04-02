using System.Linq;
using Terrafirma.Global;
using Terrafirma.Items.Equipment.Sacred;
using Terrafirma.Items.Equipment.Tempire.Monarch;
using Terrafirma.Items.Equipment.Wings;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Terrafirma.Items.Equipment.Elemental
{
    [AutoloadEquip(EquipType.Head)]
    public class GraniteHelmet : ModItem
    {
        public override void AddRecipes()
        {
            CreateRecipe().AddTile(TileID.Anvils).AddIngredient(ItemID.Granite, 30).AddIngredient(ItemID.Sapphire, 2).Register();
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<GraniteChestplate>() && legs.type == ModContent.ItemType<GraniteGreaves>();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = Language.GetTextValue("Mods.Terrafirma.Items.GraniteHelmet.SetBonus");
            player.pickSpeed += 0.1f;
            player.PlayerStats().EarthDamage += 0.15f;
        }
        public override void UpdateVanitySet(Player player)
        {
            player.GetModPlayer<PlayerDrawEffects>().SineDarken = true;
        }
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(silver: 75);
        }
        public override void UpdateEquip(Player player)
        {
            player.pickSpeed += 0.1f;
            player.PlayerStats().EarthDamage += 0.05f;
        }
    }

    [AutoloadEquip(EquipType.Body)]
    public class GraniteChestplate : ModItem
    {
        public override void AddRecipes()
        {
            CreateRecipe().AddTile(TileID.Anvils).AddIngredient(ItemID.Granite, 40).AddIngredient(ItemID.Sapphire, 5).Register();
        }
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(silver: 75);
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.05f;
            player.pickSpeed += 0.1f;
            player.PlayerStats().EarthDamage += 0.05f;
        }
    }

    [AutoloadEquip(EquipType.Legs)]
    public class GraniteGreaves : ModItem
    {
        public override void AddRecipes()
        {
            CreateRecipe().AddTile(TileID.Anvils).AddIngredient(ItemID.Granite, 25).AddIngredient(ItemID.Sapphire, 3).Register();
        }
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(silver: 75);
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.05f;
            player.PlayerStats().EarthDamage += 0.05f;
        }
    }
}
