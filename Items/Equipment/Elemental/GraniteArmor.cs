using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terrafirma.Global;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace Terrafirma.Items.Equipment.Elemental
{
    public class GraniteSetBonus : ModPlayer
    {
        public bool set = false;
        public float power = 0f;
        public int powerGoDownTimer = 0;
        public override void ResetEffects()
        {
            set = false;
            powerGoDownTimer--;
            if (powerGoDownTimer < 0)
                power -= 0.01f;
            if(power < 0) power = 0;
        }
        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            modifiers.FinalDamage -= (45 * power);
            power = 0;
        }
    }

    public class GraniteSetGlobalTile : GlobalTile
    {
        public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            GraniteSetBonus setPlayer = Main.LocalPlayer.GetModPlayer<GraniteSetBonus>();

            if (!fail && !effectOnly && !noItem && setPlayer.set && TileID.Sets.Ore[Main.tile[i, j].TileType])
            {
                for(int x = 0; x < 4; x++)
                {
                    Dust d = Dust.NewDustPerfect(new Vector2((i * 16) + 8, (j * 16) + 8),DustID.GemSapphire,Main.rand.NextVector2Circular(4,4));
                    d.noGravity = true;
                }

                setPlayer.powerGoDownTimer = 60 * 30;
                setPlayer.power += 0.1f;
                if (setPlayer.power > 1) setPlayer.power = 1;
            }
        }
    }
    public class GranitePlayerLayer : PlayerDrawLayer
    {
        Asset<Texture2D> tex;
        public override void SetStaticDefaults()
        {
            tex = ModContent.Request<Texture2D>(Terrafirma.AssetPath + "GraniteShield");
        }
        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            return drawInfo.drawPlayer.GetModPlayer<GraniteSetBonus>().set;
        }
        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            GraniteSetBonus setPlayer = drawInfo.drawPlayer.GetModPlayer<GraniteSetBonus>();

            Vector2 position = drawInfo.Center - Main.screenPosition;
            position = new Vector2((int)position.X, (int)position.Y);
            float glow = (float)Math.Sin(Main.timeForVisualEffects * 0.01f) * 0.1f;
            drawInfo.DrawDataCache.Add(new DrawData(tex.Value,position,null, new Color(1f,1f,1f,0f) * ((setPlayer.power * 0.8f) + glow * 2f),0,tex.Value.Size() / 2f, (setPlayer.power * 1.1f) + glow, SpriteEffects.None));
        }
        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.LastVanillaLayer);
    }

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
            player.GetModPlayer<GraniteSetBonus>().set = true;
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
            player.pickSpeed -= 0.1f;
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
            player.pickSpeed -= 0.1f;
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
