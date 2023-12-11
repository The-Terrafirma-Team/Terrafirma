using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using TerrafirmaRedux.Items.Melee.Shortswords;

namespace TerrafirmaRedux.Global
{
    public class TrueMeleeArmorPenetrationGlobalItem : GlobalItem
    {
        public override void ModifyHitNPC(Item item, Player player, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (item.DamageType == DamageClass.Melee)
            {
                modifiers.ArmorPenetration += 15;
            }
        }
    }
    public class TrueMeleeArmorPenetrationGlobalProjectile : GlobalProjectile
    {
        public static readonly bool[] TrueMeleeProjectiles = ProjectileID.Sets.Factory.CreateBoolSet(
        ModContent.ProjectileType<RapierProjectile>()
        );
        public override void ModifyHitNPC(Projectile projectile, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (projectile.IsTrueMeleeProjectile())
            {
                modifiers.ArmorPenetration += 15;
            }
        }
    }
}
