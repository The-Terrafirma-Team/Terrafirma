using TerrafirmaRedux.Items.Ammo;
using TerrafirmaRedux.Items.Equipment.Ranged;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Global
{
    public class GlobalNPCShop : GlobalNPC
    {
        public override void ModifyShop(NPCShop shop)
        {
            if (shop.NpcType == NPCID.ArmsDealer)
            {
                shop.Add(ModContent.ItemType<DrumMag>());
                shop.Add(ModContent.ItemType<Foregrip>(), Condition.Hardmode);
                shop.Add(ModContent.ItemType<CarbonFiberArrow>(), Condition.Hardmode);
            }
        }
    }
}
