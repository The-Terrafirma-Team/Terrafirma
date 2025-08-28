using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Common.Mechanics;
using Terraria;

namespace Terrafirma
{
    public partial class Terrafirma
    {
        internal enum TFNetMessage : byte
        {
            Blocking,
            SkillActivate
        }
        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            TFNetMessage type = (TFNetMessage)reader.ReadByte();
            switch (type)
            {
                case TFNetMessage.Blocking:
                    BlockingPlayer.RecieveBlockMessage(reader, whoAmI);
                    break;
                case TFNetMessage.SkillActivate:
                    SkillsSystem.RecieveSkillUse(reader, whoAmI);
                    break;
                default:
                    Main.NewText($"SOMEONE SEND AN INVALID NET MESSAGE TYPE {type}. DESTROY THEM. KTHX BYEEEEE :3", Color.Red);
                    break;
            }
        }
    }
}
