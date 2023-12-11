using TerrafirmaRedux.Items.Hallow;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Reworks.QueenSlime
{
    public class QueenSlime : GlobalNPC
    {
        public override bool InstancePerEntity => base.InstancePerEntity;
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == NPCID.QueenSlimeBoss;
        }
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<MajesticGel>(), 1, 32, 54));
        }

        public override void SetDefaults(NPC entity)
        {
            entity.lifeMax = 14000;
        }
    }
    public class QueenSlimeProjectileNerf : GlobalProjectile
    {
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (source is EntitySource_Parent parent && parent.Entity is NPC npc && npc.type is >= 658 and <= 660)
            {
                projectile.damage = (int)(projectile.damage * 0.7f);
            }
        }
    }
    public class QueenSlimeLootBag : GlobalItem
    {
        public override bool InstancePerEntity => base.InstancePerEntity;
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ItemID.QueenSlimeBossBag;
        }
        public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<MajesticGel>(), 1, 32, 54));
        }
    }
}
