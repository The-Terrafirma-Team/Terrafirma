using Terrafirma.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Terrafirma.Common
{
    public class ProjectileStats : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        /// <summary>
        /// This is only used on hostile projectiles
        /// </summary>
        public float DamageMultiplier = 1f;
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if(source is EntitySource_Parent p && p.Entity is NPC n)
            {
                DamageMultiplier = n.NPCStats().AttackDamage;
            }
        }
        public override void ModifyHitPlayer(Projectile projectile, Player target, ref Player.HurtModifiers modifiers)
        {
            modifiers.SourceDamage *= DamageMultiplier;
        }
    }
}
