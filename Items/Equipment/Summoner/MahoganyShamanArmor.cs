using Microsoft.Xna.Framework;
using System;
using Terrafirma.Common;
using Terrafirma.Items.Materials;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Terrafirma.Items.Equipment.Summoner
{
    [AutoloadEquip(EquipType.Head)]
    public class MahoganyShamanMask : ModItem
    {
        public override void SetStaticDefaults()
        {
            ArmorIDs.Head.Sets.IsTallHat[Item.headSlot] = true;
        }
        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(0, 0, 5, 0);
            Item.rare = ItemRarityID.Blue;
            Item.defense = 2;
        }
        public override void UpdateEquip(Player player)
        {
            player.statManaMax2 += 40;
            player.PlayerStats().SwarmSpeedMultiplier += 0.05f;
        }
        public override void UpdateArmorSet(Player player)
        {
            Lighting.AddLight(player.Center, new Vector3(0, 0.2f, 0));
            player.GetModPlayer<MahoganyShamanPlayer>().active = true;
            player.setBonus = Language.GetTextValue("Mods.Terrafirma.Items.MahoganyShamanMask.SetBonus");
        }
        public override void UpdateVanitySet(Player player)
        {
            player.GetModPlayer<PlayerDrawEffects>().SineDarken = true;
            for (int i = 0; i < 2; i++)
            {
                Dust d = Dust.NewDustPerfect(player.Center + new Vector2((2 + i*6) * player.direction, -10 + player.gfxOffY + (player.LegFrameIsOneThatRaisesTheBody() ? -2 : 0) * player.gravDir).RotatedBy(player.fullRotation), DustID.GemEmerald);
                d.scale = 0.6f;
                d.velocity = Vector2.Zero;
                d.alpha = 128;
                d.noGravity = true;
                d.noLightEmittence = true;
            }
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<MahoganyShamanBody>() && legs.type == ModContent.ItemType<MahoganyShamanLegs>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(Type)
                .AddTile(TileID.WorkBenches)
                .AddIngredient(ItemID.RichMahogany, 20)
                .AddIngredient(ModContent.ItemType<MahoganyLeaf>(), 10)
                .Register();
        }
    }
    [AutoloadEquip(EquipType.Body)]
    public class MahoganyShamanBody : ModItem
    {
        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(0, 0, 5, 0);
            Item.rare = ItemRarityID.Blue;
            Item.defense = 3;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Summon) += 0.1f;
            player.PlayerStats().SwarmSpeedMultiplier += 0.05f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(Type)
                .AddTile(TileID.WorkBenches)
                .AddIngredient(ItemID.RichMahogany, 30)
                .AddIngredient(ModContent.ItemType<MahoganyLeaf>(), 15)
                .Register();
        }
    }
    [AutoloadEquip(EquipType.Legs)]
    public class MahoganyShamanLegs : ModItem
    {
        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(0, 0, 5, 0);
            Item.rare = ItemRarityID.Blue;
            Item.defense = 2;
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.10f;
            player.PlayerStats().SwarmSpeedMultiplier += 0.05f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(Type)
                .AddTile(TileID.WorkBenches)
                .AddIngredient(ItemID.RichMahogany, 25)
                .AddIngredient(ModContent.ItemType<MahoganyLeaf>(), 12)
                .Register();
        }
    }
    public class MahoganyShamanPlayer : ModPlayer
    {
        public bool active = false;
        public override void ResetEffects()
        {
            active = false;
        }
    }
}