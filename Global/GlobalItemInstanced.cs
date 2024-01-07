using Microsoft.Xna.Framework;
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
        public byte Spell = 0;
        public override bool InstancePerEntity => true;
        public override void SetDefaults(Item entity)
        {
            Spell = 0;

            if (entity.DamageType == DamageClass.Summon && entity.type <= 5455)
            {
                entity.useTime = entity.useAnimation = 16;
                entity.mana = 0;
            }
        }
    }
}
