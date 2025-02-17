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
using Terrafirma.Common.Templates;
using Terrafirma.Common.Projectiles;

namespace Terrafirma.Common
{
    public class NetSendIDs
    {
        public const byte syncSentryBuff = 0;
        public const byte syncCursor = 1;
    }
    public class Network : ModSystem
    {
        //Quick Explanation for whoever needs to add more Network related stuff
        //
        // Packets have to be sent from Client > Server > Other Clients
        // ┌───┐    ┌───┐    ┌───┐ 
        // │   │    │   │    │   │ 
        // └─┬─┘ ─► └─┬─┘ ─► └─┬─┘ 
        //  ─┴─      ─┴─      ─┴─  
        // Client   Server   Other
        //                  Clients
        //
        // It's important to specify the NetMode (If the code reading the network data is a client or the server)
        // Usually there's a method that sends the packet you're trying to share to the server.
        // that packet is then read in this class on server-side and finally sent to the other clients where the contents are unpacked and used (Also in this Class)
        //
        // Each packet contains contains a specific set of information that has to be read in the same order
        // If you send a float and a Vector2, then the packet needs to read it a float first and a Vector2 second (Order has to be kept the same)
        // Therefore it's important for the first piece of data sent and read to be a universal value that tells what type of packet you're trying to work with (PacketID)
        //
        // PacketID is essential, without it the code can't tell what packet you're trying to work with
        // => (Ex: You send a Mouse position packet but the code starts reading it as a Wrench Buff packet and obviosuly breaks)
        //
        // TRY TO KEEP THE AMOUNT OF DATA SENT AS LOW AS POSSIBLE TO NOT CAUSE LAG, BANDWIDTH IS LIMITED

        public static void HandlePacket(BinaryReader reader, int fromWho)
        {
            ModPacket packet = ModContent.GetInstance<Terrafirma>().GetPacket();

            if (Main.netMode == NetmodeID.Server)
            {
                byte packetID = reader.ReadByte(); //First piece of data that is read (PacketID)
                switch (packetID)
                {
                    case NetSendIDs.syncSentryBuff:
                        int projidentity = reader.ReadInt32();
                        int buffid = reader.ReadInt32();
                        int buffduration = reader.ReadInt32();
                        packet.Write((byte)packetID);
                        packet.Write(projidentity);
                        packet.Write(buffid);
                        packet.Write(buffduration);
                        packet.Send(-1, -1);
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
                    case NetSendIDs.syncSentryBuff:
                        int projidentity = reader.ReadInt32();
                        int buffid = reader.ReadInt32();
                        int buffduration = reader.ReadInt32();
                        WrenchItem.ApplySentryBuff(Main.projectile[projidentity], SentryBuffID.sentrybuffs[buffid], buffduration);
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
