using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Common.Players;
using Terraria;
using Terraria.DataStructures;

namespace Terrafirma.Common.Interfaces
{
    internal interface IItemThatDrawsOnHandsWhenHeld
    {
        void DrawFrontHand(ref PlayerDrawSet drawInfo);
        void DrawOffHand(ref PlayerDrawSet drawInfo);

        public static void commonFrontHandDrawData(Asset<Texture2D> ArmTex, ref PlayerDrawSet drawInfo, Vector2 offset = default, Color? color = null)
        {
            if (color == null)
                color = drawInfo.colorArmorBody;

            if (!drawInfo.drawPlayer.compositeFrontArm.enabled)
            {
                DrawData item = new DrawData(ArmTex.Value, new Vector2((int)(drawInfo.Position.X - Main.screenPosition.X - (float)(drawInfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawInfo.drawPlayer.width / 2)), (int)(drawInfo.Position.Y - Main.screenPosition.Y + (float)drawInfo.drawPlayer.height - (float)drawInfo.drawPlayer.bodyFrame.Height + 4f)) + drawInfo.drawPlayer.bodyPosition + new Vector2(drawInfo.drawPlayer.bodyFrame.Width / 2, drawInfo.drawPlayer.bodyFrame.Height / 2), drawInfo.compFrontArmFrame, color.Value, drawInfo.drawPlayer.bodyRotation, drawInfo.bodyVect, 1f, drawInfo.playerEffect, 0);
                if (drawInfo.drawPlayer.CurrentLegFrameIsOneThatRaisesTheBody())
                {
                    item.position.Y -= 2 * drawInfo.drawPlayer.gravDir;
                }
                item.position += offset;
                drawInfo.DrawDataCache.Add(item);
            }
            else
            {
                Vector2 vector = new Vector2((int)(drawInfo.Position.X - Main.screenPosition.X - (float)(drawInfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawInfo.drawPlayer.width / 2)), (int)(drawInfo.Position.Y - Main.screenPosition.Y + (float)drawInfo.drawPlayer.height - (float)drawInfo.drawPlayer.bodyFrame.Height + 4f)) + drawInfo.drawPlayer.bodyPosition + new Vector2(drawInfo.drawPlayer.bodyFrame.Width / 2, drawInfo.drawPlayer.bodyFrame.Height / 2);
                Vector2 value = Main.OffsetsPlayerHeadgear[drawInfo.drawPlayer.bodyFrame.Y / drawInfo.drawPlayer.bodyFrame.Height];
                vector += value * -drawInfo.playerEffect.HasFlag(SpriteEffects.FlipVertically).ToDirectionInt();
                float rotation = drawInfo.drawPlayer.bodyRotation + drawInfo.compositeFrontArmRotation;
                Vector2 bodyVect = drawInfo.bodyVect;
                Vector2 compositeOffset_FrontArm = new Vector2(-5 * ((!drawInfo.playerEffect.HasFlag(SpriteEffects.FlipHorizontally)) ? 1 : (-1)), 0f);
                bodyVect += compositeOffset_FrontArm;
                vector += compositeOffset_FrontArm;
                DrawData drawData2 = new DrawData(ArmTex.Value, vector, drawInfo.compFrontArmFrame, color.Value, rotation, bodyVect, 1f, drawInfo.playerEffect, 0);
                drawData2.position += offset - new Vector2(-drawInfo.drawPlayer.direction,1);
                PlayerDrawLayers.DrawCompositeArmorPiece(ref drawInfo, CompositePlayerDrawContext.FrontArmAccessory, drawData2);
            }
        }
    }
}
