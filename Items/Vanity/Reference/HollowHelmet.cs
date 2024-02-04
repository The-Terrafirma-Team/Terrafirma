using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Vanity.Reference
{
    [AutoloadEquip(EquipType.Head)]
    public class HollowHelmet : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 30;
            Item.rare = ItemRarityID.Red; //ModContent.RarityType<KinRarity>();
            Item.value = Item.sellPrice(silver: 75);
            Item.vanity = true;
            
        }

        public override void SetStaticDefaults()
        {
            ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true;

        }

    }

    public class HollowHelmetDrawLayer : PlayerDrawLayer
    {
        private Texture2D HelmetTexture = ModContent.Request<Texture2D>("Terrafirma/Items/Vanity/Reference/HollowHelmet_Head1").Value;
        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.Head);
        public override bool IsHeadLayer => true;

        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            return true;
        }
        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Player player = drawInfo.drawPlayer;
            SpriteEffects spriteEffects;
            if (player.gravDir == 1f)
            {
                if (player.direction == 1)
                {
                    spriteEffects = SpriteEffects.None;
                }
                else
                {
                    spriteEffects = SpriteEffects.FlipHorizontally;
                }

                if (!player.dead)
                {
                    player.legPosition.Y = 0f;
                    player.headPosition.Y = 0f;
                    player.bodyPosition.Y = 0f;
                }
            }
            else
            {
                if (player.direction == 1)
                {
                    spriteEffects = SpriteEffects.FlipVertically;
                }
                else
                {
                    spriteEffects = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;
                }

                if (!player.dead)
                {
                    player.legPosition.Y = 6f;
                    player.headPosition.Y = 6f;
                    player.bodyPosition.Y = 6f;
                }
            }
            var headFrame = new Vector2(player.headFrame.Width * 0.5f, player.headFrame.Height * 0.4f);
            if (player.head == EquipLoader.GetEquipSlot(ModContent.GetInstance<Terrafirma>(), "HollowHelmet", EquipType.Head))
            {
                var value = new DrawData(
                    HelmetTexture,
                    new Vector2(Main.screenPosition.X + 20f, Main.screenPosition.Y + 20f),
                    player.bodyFrame,
                    drawInfo.colorArmorHead,
                    0,
                    headFrame,
                    1f,
                    SpriteEffects.None,
                    0);
                value.shader = drawInfo.cHead;

                drawInfo.DrawDataCache.Add(value);
            }
        } 

    } 

}


