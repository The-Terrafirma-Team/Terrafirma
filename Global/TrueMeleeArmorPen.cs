using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terrafirma.Items.Weapons.Melee.Shortswords;

namespace Terrafirma.Global
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
        public override void ModifyHitNPC(Projectile projectile, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (projectile.IsTrueMeleeProjectile())
            {
                modifiers.ArmorPenetration += 15;
            }
        }
    }
}
