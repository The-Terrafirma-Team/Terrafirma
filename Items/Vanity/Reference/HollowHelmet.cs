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
            Item.rare = ItemRarityID.Red; 
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
            Player p = drawInfo.drawPlayer;
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (p.gravDir == 1f)
            {
                if (p.direction == 1)
                {
                    spriteEffects = SpriteEffects.None;
                }
                else
                {
                    spriteEffects = SpriteEffects.FlipHorizontally;
                }

                if (!p.dead)
                {
                    p.legPosition.Y = 0f;
                    p.headPosition.Y = 0f;
                    p.bodyPosition.Y = 0f;
                }
            }
            else
            {
                if (p.direction == 1)
                {
                    spriteEffects = SpriteEffects.FlipVertically;
                }
                else
                {
                    spriteEffects = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;
                }

                if (!p.dead)
                {
                    p.legPosition.Y = 6f;
                    p.headPosition.Y = 6f;
                    p.bodyPosition.Y = 6f;
                }
            }
            var vector3 = new Vector2(p.legFrame.Width * 0.5f, p.legFrame.Height * 0.4f);
            if (p.head == EquipLoader.GetEquipSlot(ModContent.GetInstance<Terrafirma>(), "HollowHelmet", EquipType.Head))
            {
                var value = new DrawData(ModContent.Request<Texture2D>("Terrafirma/Items/Vanity/Reference/HollowHelmet_Head1").Value,
                    new Vector2(
                        (int)(drawInfo.Position.X - Main.screenPosition.X - (p.bodyFrame.Width / 2) + (p.width / 2)),
                        (int)(drawInfo.Position.Y - Main.screenPosition.Y + p.height - p.bodyFrame.Height - 4f)) +
                    p.headPosition + vector3, p.bodyFrame, drawInfo.colorHead * 2f, p.headRotation, vector3, 1f, spriteEffects, 0);
                value.shader = drawInfo.cHead;
                drawInfo.DrawDataCache.Add(value);
            }
        }

    } 

}


