//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Terraria.ModLoader;

//namespace Terrafirma.Common
//{
//    public class NetSendIDs
//    {
//        public const byte syncWrench = 0;
//        public const byte syncCursor = 1;
//    }
//    public class Network : ModSystem
//    {
//        public void syncCursor()
//        {
//            ModPacket packet = Mod.GetPacket();
//            packet.Write(NetSendIDs.syncCursor);
//        }
//        public override void NetReceive(BinaryReader reader)
//        {
//            byte msgType = reader.ReadByte();
//            switch (msgType)
//            {
//                case NetSendIDs.syncWrench:
//                    break;
//                case NetSendIDs.syncCursor:
//                    // Fred Code Real
//                    break;
//            }
//        }
//    }
//}
