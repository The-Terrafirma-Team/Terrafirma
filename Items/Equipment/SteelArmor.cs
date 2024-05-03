using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terrafirma.Common;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Terrafirma.Items.Equipment
{
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
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<SteelChestplate>() && legs.type == ModContent.ItemType<SteelGreaves>();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = Language.GetTextValue("Mods.Terrafirma.Items.SteelHelmet.SetBonus");
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
            Item.defense = 3;
        }
        public override void UpdateEquip(Player player)
        {
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
            Item.defense = 5;
        }
        public override void UpdateEquip(Player player)
        {
        }
    }

    [AutoloadEquip(EquipType.Legs)]
    public class SteelGreaves : ModItem
    {
        public override void SetDefaults()
        {
            Item.defense = 3;
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(silver: 75);
        }
        public override void UpdateEquip(Player player)
        {
        }
    }
}
