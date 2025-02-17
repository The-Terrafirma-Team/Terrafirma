using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
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
using Terrafirma.Common.Players;

namespace Terrafirma.Items.Equipment.MiningBuilding
{
    //public class CrystalSetbonus : ModPlayer
    //{
    //    public bool set = false;
    //    public override void ResetEffects()
    //    {
    //        set = false;
    //    }
    //}

    [AutoloadEquip(EquipType.Head)]
    public class CrystalHelmet : ModItem
    {
        public override void AddRecipes()
        {
            CreateRecipe().AddTile(TileID.MythrilAnvil).AddIngredient(ItemID.PearlstoneBlock, 35).AddIngredient(ItemID.CrystalShard, 15).AddIngredient(ItemID.SoulofLight, 5).AddIngredient(ItemID.UnicornHorn).Register();
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<CrystalChestplate>() && legs.type == ModContent.ItemType<CrystalGreaves>();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = Language.GetTextValue("Mods.Terrafirma.Items.CrystalHelmet.SetBonus");
            //player.GetModPlayer<CrystalSetbonus>().set = true;

            Lighting.AddLight(player.PlayerStats().MouseWorld, new Vector3(Math.Abs((float)Math.Sin(Main.timeForVisualEffects * 0.006f)) * 0.5f + 0.5f, 0.5f, 1f) * 0.5f);
            ParticleSystem.AddParticle(new ColorDot() { Size = Main.rand.NextFloat(0.2f, 1f), TimeInWorld = 40, Waviness = Main.rand.NextFloat(0.04f), gravity = -0.1f, secondaryColor = Color.Lerp(new Color(0.3f, 1f, 1f, 0f), new Color(0.8f, 0.3f, 1f, 0f), Main.rand.NextFloat()) }, player.PlayerStats().MouseWorld, Main.rand.NextVector2Circular(1,1), Color.Lerp(new Color(0.3f, 1f, 1f, 0f), new Color(0.8f, 0.3f, 1f, 0f), Main.rand.NextFloat()));
        }
        public override void UpdateVanitySet(Player player)
        {
            player.armorEffectDrawShadow = true;
            player.GetModPlayer<PlayerDrawEffects>().CrystalAfterimages = true;
        }
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.sellPrice(gold: 2);
            Item.defense = 8;
        }
        public override void UpdateEquip(Player player)
        {
            player.blockRange += 1;
            player.pickSpeed -= 0.1f;
            player.GetDamage(DamageClass.Generic) += 0.07f;
            player.GetCritChance(DamageClass.Generic) += 0.1f;
            Lighting.AddLight(player.Center, new Vector3(Math.Abs((float)Math.Sin(Main.timeForVisualEffects * 0.004f)) * 0.5f + 0.5f,0.5f,1f));
        }
    }

    [AutoloadEquip(EquipType.Body)]
    public class CrystalChestplate : ModItem
    {
        public override void AddRecipes()
        {
            CreateRecipe().AddTile(TileID.MythrilAnvil).AddIngredient(ItemID.PearlstoneBlock, 45).AddIngredient(ItemID.CrystalShard, 20).AddIngredient(ItemID.SoulofLight, 10).Register();
        }
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.sellPrice(gold: 2);
            Item.defense = 15;
        }
        public override void UpdateEquip(Player player)
        {
            player.blockRange += 1;
            player.endurance += 0.05f;
            player.pickSpeed -= 0.2f;
            player.GetDamage(DamageClass.Generic) += 0.07f;
        }
    }

    [AutoloadEquip(EquipType.Legs)]
    public class CrystalGreaves : ModItem
    {
        public override void AddRecipes()
        {
            CreateRecipe().AddTile(TileID.MythrilAnvil).AddIngredient(ItemID.PearlstoneBlock, 35).AddIngredient(ItemID.CrystalShard, 25).AddIngredient(ItemID.SoulofLight, 5).AddIngredient(ItemID.PixieDust, 10).Register();
        }
        public override void SetDefaults()
        {
            Item.defense = 10;
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.sellPrice(gold: 2);
        }
        public override void UpdateEquip(Player player)
        {
            player.blockRange += 1;
            player.moveSpeed += 0.5f;
            player.jumpSpeedBoost += 2.5f;
            player.pickSpeed -= 0.1f;
            player.GetDamage(DamageClass.Generic) += 0.07f;
        }
    }
}
