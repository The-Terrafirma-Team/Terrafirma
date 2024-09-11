using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terrafirma.Buffs.Buffs;
using Terrafirma.Buffs.Debuffs;
using Terrafirma.Common.Players;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Terrafirma.Items.Equipment
{
    public class SteelSetBonus : ModPlayer
    {
        public bool active;
        public bool ShutUp;
        public override void ResetEffects()
        {
            active = false;
            ShutUp = false;
        }
        public override void PostUpdateEquips()
        {
            if (active && !ShutUp && Player.PlayerDoublePressedSetBonusActivateKey())
            {
                Player.AddBuff(ModContent.BuffType<ShutUp>(),60 * 60);
                float rot = 60;
                for(int i = 0; i < rot; i++)
                {
                    Dust d = Dust.NewDustPerfect(Player.MountedCenter, DustID.GemTopaz, new Vector2(50, 0).RotatedBy(i / rot * MathHelper.TwoPi));
                    d.noGravity = true;
                    d.scale = 3;

                    Dust d2 = Dust.NewDustPerfect(Player.MountedCenter, DustID.GemTopaz, new Vector2(25, 0).RotatedBy(i / rot * MathHelper.TwoPi));
                    d2.noGravity = true;
                    d2.scale = 2;
                    d2.alpha = 64;
                }

                for(int i = 0; i < Main.player.Length; i++)
                {
                    Player p = Main.player[i];
                    if(p.Center.Distance(Player.Center) < 550)
                    {
                        p.AddBuff(ModContent.BuffType<Confidence>(), 60 * 5);
                    }
                }
            }
        }
    }
    public class SteelHelmetPlume : PlayerDrawLayer
    {
        Asset<Texture2D> tex;
        public override bool IsHeadLayer => true;
        public override void SetStaticDefaults()
        {
            tex = Mod.Assets.Request<Texture2D>("Items/Equipment/SteelHelmet_Plume");
        }
        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            return drawInfo.drawPlayer.head == EquipLoader.GetEquipSlot(Mod, "SteelHelmet", EquipType.Head);
        }
        public override Position GetDefaultPosition() => new BeforeParent(PlayerDrawLayers.Head);
        protected override void Draw(ref PlayerDrawSet drawInfo)
        {

            Vector2 position = drawInfo.Center - Main.screenPosition;
            position = new Vector2((int)position.X, (int)position.Y);

            position -= new Vector2(7 * drawInfo.drawPlayer.direction, 21 + (drawInfo.drawPlayer.LegFrameIsOneThatRaisesTheBody()? 2 : 0));

            int frame = 0;

            //if (Math.Abs(drawInfo.drawPlayer.velocity.Y) < 2)
            //{
            //    float offset = drawInfo.drawPlayer.legFrame.Y - 56 * 3;
            //    if (drawInfo.drawPlayer.LegFrameIsOneThatRaisesTheBody())
            //        frame = 2;
            //    else if ((offset >= 392 && offset < 560) || (offset >= 784 && offset < 952))
            //        frame = 1;
            //}
            //else if (drawInfo.drawPlayer.velocity.Y < -2)
            //    frame = 2;
            //else
            //    frame = 1;

            if (drawInfo.drawPlayer.velocity.Y < -2)
                frame = 2;
            else if (drawInfo.drawPlayer.velocity.Y > 2)
                frame = 1;

            SpriteEffects effect = drawInfo.drawPlayer.direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            drawInfo.DrawDataCache.Add(new DrawData(tex.Value,position,new Rectangle(0, frame * tex.Height() / 3, tex.Width(),tex.Height() / 3),drawInfo.colorArmorHead,0,new Vector2(tex.Width() / 2, tex.Height() / 6),1, effect) {shader = drawInfo.cHead });
        }
    }

    [AutoloadEquip(EquipType.Head)]
    public class SteelHelmet : ModItem
    {
        public static LocalizedText SetBonusText { get; private set; }
        public override void SetStaticDefaults()
        {
            SetBonusText = this.GetLocalization("SetBonus").WithFormatArgs(Terrafirma.SetBonusKey);
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<SteelChestplate>() && legs.type == ModContent.ItemType<SteelGreaves>();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = SetBonusText.Value;
            player.GetModPlayer<SteelSetBonus>().active = true;
        }
        public override void UpdateVanitySet(Player player)
        {
            player.armorEffectDrawShadowLokis = true;
        }
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(silver: 75);
            Item.defense = 2;
        }
        public override void UpdateEquip(Player player)
        {
            player.PlayerStats().AmmoSaveChance += 0.1f;
            player.whipRangeMultiplier += 0.3f;
        }
    }

    [AutoloadEquip(EquipType.Body,EquipType.Back)]
    public class SteelChestplate : ModItem
    {
        public override void SetStaticDefaults()
        {
            ArmorIDs.Body.Sets.IncludedCapeBack[Item.bodySlot] = Item.backSlot;
        }
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(silver: 75);
            Item.defense = 4;
        }
        public override void UpdateEquip(Player player)
        {
            player.PlayerStats().MeleeWeaponScale += 0.2f;
            player.longInvince = true;
        }
    }

    [AutoloadEquip(EquipType.Legs)]
    public class SteelGreaves : ModItem
    {
        public override void SetDefaults()
        {
            Item.defense = 2;
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(silver: 75);
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.1f;
            player.jumpSpeedBoost += 1;
        }
    }
}
