using System.Security.Cryptography.X509Certificates;
using Terrafirma.Common.Attributes;
using Terrafirma.Common.Templates;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Terrafirma.Content.Items.Equipment.Armor.LeatherArmor
{
    [AutoloadEquip(EquipType.Legs)]
    public class LeatherPants : ModItem
    {
        public override void AddRecipes()
        {
            CreateRecipe().AddIngredient(ItemID.Leather, 4).AddRecipeGroup(RecipeGroupID.IronBar, 4).AddTile(TileID.WorkBenches).Register();
        }
        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(0,0,10,0);
            Item.rare = ItemRarityID.Blue;
            Item.defense = 2;
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.1f;
        }
    }
    [AutoloadEquip(EquipType.Body)]
    public class LeatherChestplate : ModItem
    {
        public override void AddRecipes()
        {
            CreateRecipe().AddIngredient(ItemID.Leather, 6).AddRecipeGroup(RecipeGroupID.IronBar, 6).AddTile(TileID.WorkBenches).Register();
        }
        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(0, 0, 10, 0);
            Item.rare = ItemRarityID.Blue;
            Item.defense = 3;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Melee) += 0.07f;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return legs.type == ModContent.ItemType<LeatherPants>();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = Language.GetTextValue("Mods.Terrafirma.Items.LeatherChestplate.SetBonus");
            player.GetModPlayer<LeatherArmorPlayer>().Active = true;
        }
    }
    public class LeatherArmorPlayer : TFModPlayer
    {
        [ResetDefaults(false)]
        public bool Active = false;
        public override void PostUpdateRunSpeeds()
        {
            if(Active && Player.sunflower)
            {
                Player.maxRunSpeed *= 1.2f;
                Player.accRunSpeed *= 1.2f;
                Player.moveSpeed *= 1.2f;
            }
        }
    }
    public class LeatherArmorGlobalBuff : GlobalBuff
    {
        public override void ModifyBuffText(int type, ref string buffName, ref string tip, ref int rare)
        {
            if (!Main.LocalPlayer.GetModPlayer<LeatherArmorPlayer>().Active)
                return;
            int affectedBuffColor = ItemRarityID.Orange;
            switch (type)
            {
                case BuffID.CatBast:
                    rare = affectedBuffColor;
                    tip = Language.GetTextValue("Mods.Terrafirma.Buffs.CatBastUpgrade");
                    break;
                case BuffID.Campfire:
                    rare = affectedBuffColor;
                    tip = Language.GetTextValue("Mods.Terrafirma.Buffs.CampfireUpgrade");
                    break;
                case BuffID.HeartLamp:
                    rare = affectedBuffColor;
                    tip = Language.GetTextValue("Mods.Terrafirma.Buffs.HeartLampUpgrade");
                    break;
                case BuffID.StarInBottle:
                    rare = affectedBuffColor;
                    tip = Language.GetTextValue("Mods.Terrafirma.Buffs.StarInBottleUpgrade");
                    break;
                case BuffID.Sunflower:
                    rare = affectedBuffColor;
                    tip = Language.GetTextValue("Mods.Terrafirma.Buffs.SunflowerUpgrade");
                    break;

            }
        }
        public override void Update(int type, Player player, ref int buffIndex)
        {
            if (!player.GetModPlayer<LeatherArmorPlayer>().Active)
                return;
            switch (type)
            {
                case BuffID.CatBast:
                    player.statDefense += 2;
                    break;
                case BuffID.Campfire:
                    player.lifeRegen++;
                    break;
                case BuffID.HeartLamp:
                    player.lifeRegen += 1;
                    break;
                case BuffID.StarInBottle:
                    player.manaRegenBonus += 5;
                    break;
            }
        }
    }
}
