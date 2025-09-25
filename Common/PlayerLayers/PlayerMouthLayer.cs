using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace Terrafirma.Common.PlayerLayers
{
    public enum MouthFrame : byte
    {
        Closed,
        OO,
        AA,
        EE,
        Talking
    }
    public class PlayerMouthLayer : ModPlayer
    {
        public MouthFrame MouthFrame = MouthFrame.Closed;
        int TalkFrame = 0;
        public override void ResetEffects()
        {
            MouthFrame = MouthFrame.Closed;
            if(Main.timeForVisualEffects % 6 == 0)
            {
                TalkFrame = Main.rand.Next(3);

            }
        }

        private static Asset<Texture2D> tex;
        public override void Load()
        {
            On_PlayerDrawLayers.DrawPlayer_21_Head_TheFace += On_PlayerDrawLayers_DrawPlayer_21_Head_TheFace;
            tex = Mod.Assets.Request<Texture2D>("Assets/PlayerMouth");
        }
        private void On_PlayerDrawLayers_DrawPlayer_21_Head_TheFace(On_PlayerDrawLayers.orig_DrawPlayer_21_Head_TheFace orig, ref PlayerDrawSet drawinfo)
        {
            orig.Invoke(ref drawinfo);
            PlayerMouthLayer p = drawinfo.drawPlayer.GetModPlayer<PlayerMouthLayer>();
            if (p.MouthFrame != MouthFrame.Closed)
            {
                Rectangle frame = new Rectangle(0,0,4,2);
                switch (p.MouthFrame)
                {
                    case MouthFrame.AA:
                        frame.Y = 4;
                        break;
                    case MouthFrame.EE:
                        frame.Y = 8;
                        break;
                    case MouthFrame.Talking:
                        frame.Y = 4 * p.TalkFrame;
                        break;
                }

                int num = 0;
                num += drawinfo.drawPlayer.bodyFrame.Y / 56;
                if (num >= Main.OffsetsPlayerHeadgear.Length)
                    num = 0;

                Vector2 vector = Main.OffsetsPlayerHeadgear[num];

                DrawData item = new DrawData(tex.Value, new Vector2((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2)), (int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, frame, drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect);

                item.position += new Vector2(drawinfo.drawPlayer.direction == 1 ? 22 : 14, 24) + vector;

                drawinfo.DrawDataCache.Add(item);
            }
        }
    }
}
