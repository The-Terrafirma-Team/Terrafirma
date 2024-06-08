//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
using System.IO;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Terrafirma.Common
{
    public class NetSendIDs
    {
        public const byte syncWrench = 0;
        public const byte syncCursor = 1;
    }
    public class Network : ModSystem
    {
        public static void HandlePacket(BinaryReader reader, int fromWho)
        {
            ModPacket packet = ModContent.GetInstance<Terrafirma>().GetPacket();

            if (Main.netMode == NetmodeID.Server)
            {
                byte packetID = reader.ReadByte();
                switch (packetID)
                {
                    case NetSendIDs.syncWrench:
                        break;
                    case NetSendIDs.syncCursor:
                        int packetPlayer = reader.ReadInt32();
                        Vector2 PacketVector = reader.ReadVector2();
                        packet.Write((byte)packetID);
                        packet.Write((int)packetPlayer);
                        packet.WriteVector2(PacketVector);
                        packet.Send(-1, -1);
                        break;
                }
            }

            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                byte packetID = reader.ReadByte();
                switch (packetID)
                {
                    case NetSendIDs.syncWrench:
                        break;
                    case NetSendIDs.syncCursor:
                        int PlayerInt = reader.ReadInt32();
                        Vector2 PosVector = reader.ReadVector2();
                        Main.player[PlayerInt].PlayerStats().MouseWorld = PosVector;
                        break;
                }
            }
        }
    }
}
