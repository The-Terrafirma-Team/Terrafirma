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
            if(entity.type == ProjectileID.InfernoFriendlyBlast)
            {
                entity.usesLocalNPCImmunity = true;
                entity.localNPCHitCooldown = 10;
                entity.timeLeft = 60;
            }

            if (
                entity.type == ProjectileID.EnchantedBoomerang ||
                entity.type == ProjectileID.FruitcakeChakram ||
                entity.type == ProjectileID.IceBoomerang ||
                entity.type == ProjectileID.Shroomerang ||
                entity.type == ProjectileID.WoodenBoomerang ||
                entity.type == ProjectileID.BloodyMachete ||
                entity.type == ProjectileID.CombatWrench ||
                entity.type == ProjectileID.Flamarang ||
                entity.type == ProjectileID.ThornChakram ||
                entity.type == ProjectileID.Trimarang ||
                entity.type == ProjectileID.Bananarang ||
                entity.type == ProjectileID.LightDisc ||
                entity.type == ProjectileID.FlyingKnife ||
                entity.type == ProjectileID.PossessedHatchet ||
                entity.type == ProjectileID.PaladinsHammerFriendly 
            )
            {
                entity.DamageType = DamageClass.Ranged;
            }
                
        }
    }
}
