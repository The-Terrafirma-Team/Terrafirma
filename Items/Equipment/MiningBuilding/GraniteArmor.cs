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
            if (power > 0 && set)
            {
                SoundEngine.PlaySound(SoundID.NPCDeath45,Player.position);
                //for (int x = 0; x < 8; x++)
                //{
                //    PixelCircle p = new PixelCircle();
                //    p.outlineColor = new Color(183, 247, 255);
                //    p.scale = Main.rand.NextFloat(2, 4);
                //    p.gravity = 0.2f;
                //    ParticleSystem.AddParticle(p, Player.Center, Main.rand.NextVector2Circular(6, 6) + new Vector2(0, -3), new Color(0, 192, 255));
                //}
                for(int i = 0; i < 16; i++)
                {
                    ParticleSystem.AddParticle(new ImpactSparkle() { Scale = 0.3f, LifeTime = 60, secondaryColor = new Color(1f,1f,1f,0f) * power }, Player.Center, Main.rand.NextVector2Circular(8,8), new Color(0.4f, 1f, 1f, 0f) * power);
                }
                ParticleSystem.AddParticle(new Shockwave() {Scale = new Vector2(0.5f,0.5f) * power }, Player.Center, Vector2.Zero, new Color(0.4f, 1f, 1f, 0f) * power);
                modifiers.IncomingDamageMultiplier *= (0.5f + (1 - power) * 0.5f);
                power = 0;
            }
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
                    //PixelCircle p = new PixelCircle();
                    //p.outlineColor = new Color(183,247,255);
                    //p.scale = Main.rand.NextFloat(2,4);
                    //p.gravity = 0.2f;
                    //p.tileCollide = true;
                    //ParticleSystem.AddParticle(p, new Vector2((i * 16) + 8, (j * 16) + 8), Main.rand.NextVector2Circular(4, 4) + new Vector2(0,-3), new Color(0, 192, 255));
                    //Dust d = Dust.NewDustPerfect(new Vector2((i * 16) + 8, (j * 16) + 8),DustID.GemSapphire,Main.rand.NextVector2Circular(4,4));
                    //d.noGravity = true;
                    Vector2 rand = Main.rand.NextVector2Circular(1, 1);
                    ParticleSystem.AddParticle(new ImpactSparkle() { Scale = 0.6f, LifeTime = 30}, Main.LocalPlayer.Center + rand * 80, -rand * 6, new Color(0.4f,1f,1f,0f) * 0.8f);
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
            if (drawInfo.shadow != 0)
                return;

            GraniteSetBonus setPlayer = drawInfo.drawPlayer.GetModPlayer<GraniteSetBonus>();

            Vector2 position = drawInfo.Center - Main.screenPosition;
            position = new Vector2((int)position.X, (int)position.Y);
            float glow = (float)Math.Sin(Main.timeForVisualEffects * 0.01f) * 0.1f;
            drawInfo.DrawDataCache.Add(new DrawData(tex.Value,position,null, new Color(1f,1f,1f,0f) * ((setPlayer.power * 0.8f) + glow * 2f),0,tex.Value.Size() / 2f, (setPlayer.power * 1.1f) + glow * setPlayer.power, SpriteEffects.None));
        }
        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.LastVanillaLayer);
    }

    [AutoloadEquip(EquipType.Head)]
    public class GraniteHelmet : ModItem
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
            Item.defense = 2;
        }
        public override void UpdateEquip(Player player)
        {
            player.pickSpeed -= 0.1f;
        }
    }

    [AutoloadEquip(EquipType.Body)]
    public class GraniteChestplate : ModItem
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
    public class GraniteGreaves : ModItem
    {
        public override void AddRecipes()
        {
            CreateRecipe().AddTile(TileID.Anvils).AddIngredient(ItemID.Granite, 25).AddIngredient(ItemID.Sapphire, 3).AddIngredient(ModContent.ItemType<EnchantedStone>()).Register();
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
