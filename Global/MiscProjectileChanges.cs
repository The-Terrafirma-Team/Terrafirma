using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Global
{
    public class MiscProjectileChanges : GlobalProjectile
    {
        public override void SetDefaults(Projectile entity)
        {
            if(entity.type == ProjectileID.ThrowingKnife || entity.type == ProjectileID.PoisonedKnife)
            {
                entity.usesLocalNPCImmunity = true;
                entity.localNPCHitCooldown = -1;
            }

        }
    }
}
