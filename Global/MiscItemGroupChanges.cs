using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Global
{
    public class MiscItemGroupChanges : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type <= 5455;
        }
        public override void SetDefaults(Item entity)
        {
            //if (ItemID.Sets.SortingPriorityBossSpawns[entity.type] > -1)
            //{
            //    entity.consumable = false;
            //    entity.maxStack = 1;
            //}
            if(entity.DamageType == DamageClass.Summon)
            {
                entity.useTime = entity.useAnimation = 16;
                entity.mana = 0;
            }
        }
    }
}
