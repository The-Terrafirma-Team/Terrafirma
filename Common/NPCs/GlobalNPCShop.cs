using Terrafirma.Items.Ammo;
using Terrafirma.Items.Equipment.Ranged;
using Terrafirma.Items.Placeable;
using Terrafirma.Items.Tools;
using Terrafirma.Items.Weapons.Summoner.Sentry.Hardmode;
using Terrafirma.Items.Weapons.Summoner.Sentry.PreHardmode;
using Terrafirma.Items.Weapons.Summoner.Wrench;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Common
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
            if(shop.NpcType == NPCID.Dryad)
            {
                shop.InsertAfter(ItemID.Sunflower,ModContent.ItemType<BigSunflower>());
                shop.InsertAfter(ModContent.ItemType<BigSunflower>(), ModContent.ItemType<GiantSunflower>());
            }

            if (shop.NpcType == NPCID.Cyborg)
            {
                shop.InsertAfter(ItemID.DryRocket, ModContent.ItemType<ICBA>());

            }

            if (shop.NpcType == NPCID.Mechanic)
            {
                shop.InsertAfter(ItemID.EngineeringHelmet, ModContent.ItemType<MechanicsPocketDefenseSystem>());
                shop.InsertAfter(ModContent.ItemType<MechanicsPocketDefenseSystem>(), ModContent.ItemType<BarbedWireCanister>(), Condition.Hardmode);
                shop.InsertAfter(ModContent.ItemType<BarbedWireCanister>(), ModContent.ItemType<DemolitionHammer>(), Condition.Hardmode);

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
