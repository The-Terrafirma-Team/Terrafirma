using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Common.Mechanics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace Terrafirma.Common.PlayerLayers
{
    public class SpecialBlockAurasLayer : PlayerDrawLayer
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<ServerConfig>().CombatReworkEnabled;
        }

        private static Asset<Texture2D> BlockTex;
        public override void Load()
        {
            BlockTex = Mod.Assets.Request<Texture2D>("Assets/Block");
        }
        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            return false;
            return drawInfo.drawPlayer.GetModPlayer<BlockingPlayer>().blockAmount > 0;
        }
        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            if (drawInfo.shadow > 0)
                return;
            float alpha = Lighting.Brightness((int)(drawInfo.drawPlayer.Center.X / 16), (int)((drawInfo.drawPlayer.Center.Y + drawInfo.drawPlayer.gfxOffY) / 16));
            float opacity = drawInfo.drawPlayer.GetModPlayer<BlockingPlayer>().blockAmount;
            drawInfo.DrawDataCache.Add(new DrawData(BlockTex.Value, new Vector2((int)drawInfo.Center.X, (int)drawInfo.Center.Y) - Main.screenPosition - Vector2.UnitY, BlockTex.Frame(2,1,0,0), Color.Cyan with { A = 0 } * alpha * opacity, 0f, new Vector2(BlockTex.Height() / 2), 1f * opacity, SpriteEffects.None, 0));
            drawInfo.DrawDataCache.Add(new DrawData(BlockTex.Value, new Vector2((int)drawInfo.Center.X, (int)drawInfo.Center.Y) - Main.screenPosition - Vector2.UnitY, BlockTex.Frame(2, 1, 1, 0), Color.White with { A = 0 } * alpha * opacity, 0f, new Vector2(BlockTex.Height() / 2), 1f * opacity, SpriteEffects.None, 0));
        }
        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.LastVanillaLayer);
    }
    public class BlockingShieldLayer : PlayerDrawLayer
    {
        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.Shield);
        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            return drawInfo.drawPlayer.GetModPlayer<BlockingPlayer>().blockAmount > 0 && drawInfo.drawPlayer.GetModPlayer<BlockingPlayer>().ShieldToHoldWhileBlocking != -1;
        }
        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            int shield = drawInfo.drawPlayer.GetModPlayer<BlockingPlayer>().ShieldToHoldWhileBlocking;
            Vector2 zero = Vector2.Zero;
            if (true)
            {
                zero.Y -= 4f * drawInfo.drawPlayer.gravDir;
            }
            Rectangle bodyFrame = drawInfo.drawPlayer.bodyFrame;
            Vector2 zero2 = Vector2.Zero;
            Vector2 bodyVect = drawInfo.bodyVect;
            if (bodyFrame.Width != TextureAssets.AccShield[shield].Value.Width)
            {
                bodyFrame.Width = TextureAssets.AccShield[shield].Value.Width;
                bodyVect.X += bodyFrame.Width - TextureAssets.AccShield[shield].Value.Width;
                if (drawInfo.playerEffect.HasFlag(SpriteEffects.FlipHorizontally))
                {
                    bodyVect.X = (float)bodyFrame.Width - bodyVect.X;
                }
            }
            DrawData item;
            //if (true)
            //{
                //float num = (float)Math.Sin(Main.GlobalTimeWrappedHourly * ((float)Math.PI * 2f));
                //float x = 2.5f + 1.5f * num;
                //Color colorArmorBody = drawInfo.colorArmorBody;
                //colorArmorBody.A = 0;
                //colorArmorBody *= 0.45f - num * 0.15f;
                //for (float num2 = 0f; num2 < 4f; num2 += 1f)
                //{
                //    item = new DrawData(TextureAssets.AccShield[shield].Value, zero2 + new Vector2((int)(drawInfo.Position.X - Main.screenPosition.X - (float)(bodyFrame.Width / 2) + (float)(drawInfo.drawPlayer.width / 2)), (int)(drawInfo.Position.Y - Main.screenPosition.Y + (float)drawInfo.drawPlayer.height - (float)drawInfo.drawPlayer.bodyFrame.Height + 4f)) + drawInfo.drawPlayer.bodyPosition + new Vector2(bodyFrame.Width / 2, drawInfo.drawPlayer.bodyFrame.Height / 2) + zero + new Vector2(x, 0f).RotatedBy(num2 / 4f * ((float)Math.PI * 2f)), bodyFrame, colorArmorBody, drawInfo.drawPlayer.bodyRotation, bodyVect, 1f, drawInfo.playerEffect);
                //    item.shader = drawInfo.cShield;
                //    drawInfo.DrawDataCache.Add(item);
                //}
            //}
            item = new DrawData(TextureAssets.AccShield[shield].Value, zero2 + new Vector2((int)(drawInfo.Position.X - Main.screenPosition.X - (float)(bodyFrame.Width / 2) + (float)(drawInfo.drawPlayer.width / 2)), (int)(drawInfo.Position.Y - Main.screenPosition.Y + (float)drawInfo.drawPlayer.height - (float)drawInfo.drawPlayer.bodyFrame.Height + 4f)) + drawInfo.drawPlayer.bodyPosition + new Vector2(bodyFrame.Width / 2, drawInfo.drawPlayer.bodyFrame.Height / 2) + zero, bodyFrame, drawInfo.colorArmorBody, drawInfo.drawPlayer.bodyRotation, bodyVect, 1f, drawInfo.playerEffect);
            item.shader = drawInfo.cShield;
            drawInfo.DrawDataCache.Add(item);
            //if (true)
            //{
                Color colorArmorBody2 = drawInfo.colorArmorBody;
                float num3 = (float)Math.Sin(Main.GlobalTimeWrappedHourly * (float)Math.PI);
                colorArmorBody2.A = (byte)((float)(int)colorArmorBody2.A * (0.5f + 0.5f * num3));
                colorArmorBody2 *= 0.5f + 0.5f * num3;
                item = new DrawData(TextureAssets.AccShield[shield].Value, zero2 + new Vector2((int)(drawInfo.Position.X - Main.screenPosition.X - (float)(bodyFrame.Width / 2) + (float)(drawInfo.drawPlayer.width / 2)), (int)(drawInfo.Position.Y - Main.screenPosition.Y + (float)drawInfo.drawPlayer.height - (float)drawInfo.drawPlayer.bodyFrame.Height + 4f)) + drawInfo.drawPlayer.bodyPosition + new Vector2(bodyFrame.Width / 2, drawInfo.drawPlayer.bodyFrame.Height / 2) + zero, bodyFrame, colorArmorBody2, drawInfo.drawPlayer.bodyRotation, bodyVect, 1f, drawInfo.playerEffect);
                item.shader = drawInfo.cShield;
            //}
        }
    }
}
