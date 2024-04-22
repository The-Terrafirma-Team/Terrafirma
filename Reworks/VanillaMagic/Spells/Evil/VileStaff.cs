using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.VanillaMagic.Spells.Evil
{
    public class VileStaff : GlobalItem
    {
        public override bool InstancePerEntity => base.InstancePerEntity;
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.Vilethorn;
        }
        public override void SetDefaults(Item item)
        {
            item.shootSpeed = 15;
            item.damage = 25;
            item.channel = true;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.Shoot;
        }
    }
}
