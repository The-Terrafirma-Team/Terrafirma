using TerrafirmaRedux.Items.Ammo;
using TerrafirmaRedux.Items.Equipment.Ranged;
using TerrafirmaRedux.Items.Weapons.Summoner.Sentry.Hardmode;
using TerrafirmaRedux.Items.Weapons.Summoner.Sentry.PreHardmode;
using TerrafirmaRedux.Items.Weapons.Summoner.Wrench;
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
                shop.InsertAfter(ItemID.UnholyArrow, ModContent.ItemType<CarbonFiberArrow>(), Condition.Hardmode);
                shop.InsertAfter(ItemID.SilverBullet, ModContent.ItemType<Buckshot>(), Condition.DownedSkeletron);
                shop.InsertAfter(ModContent.ItemType<Buckshot>(), ModContent.ItemType<Birdshot>(), Condition.DownedSkeletron, Condition.BloodMoon);

            }

            if (shop.NpcType == NPCID.Cyborg)
            {
                shop.InsertAfter(ItemID.DryRocket, ModContent.ItemType<ICBA>());

            }

            if (shop.NpcType == NPCID.Mechanic)
            {
                shop.InsertAfter(ItemID.EngineeringHelmet, ModContent.ItemType<MechanicsPocketDefenseSystem>());

            }

            if (shop.NpcType == NPCID.Steampunker)
            {
                shop.InsertAfter(ItemID.Clentaminator, ModContent.ItemType<ClockworkTurretStaff>());
                

            }

            if (shop.NpcType == NPCID.GoblinTinkerer)
            {
                shop.InsertAfter(ItemID.RocketBoots, ModContent.ItemType<BookmarkerWrench>(), Condition.Hardmode);
            }
        } 
    }
}
