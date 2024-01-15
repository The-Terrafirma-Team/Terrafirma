using Microsoft.Xna.Framework;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Global
{
    public class GlobalItemInstanced : GlobalItem
    {
        public byte Spell = 255;
        public override bool InstancePerEntity => true;
        public override void NetSend(Item item, BinaryWriter writer)
        {
            writer.Write(Spell);
        }
        public override void NetReceive(Item item, BinaryReader reader)
        {
            Spell = reader.ReadByte();
        }
        public override void SetDefaults(Item entity)
        {
            Spell = 0;

            if (entity.DamageType == DamageClass.Summon && entity.type <= 5455 && !entity.sentry)
            {
                entity.useTime = entity.useAnimation = 16;
                entity.mana = 0;
            }
        }
    }
}
