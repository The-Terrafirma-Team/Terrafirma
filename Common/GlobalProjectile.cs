using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Buffs.Buffs;
using Terrafirma.Common.Players;
using Terrafirma.Projectiles.Ranged.Bullets;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Common
{
    public class GlobalProjectile : Terraria.ModLoader.GlobalProjectile
    {
        public override void SetDefaults(Projectile projectile)
        {
            switch (projectile.type)
            {
                case ProjectileID.ThrowingKnife:
                case ProjectileID.PoisonedKnife:
                    projectile.usesLocalNPCImmunity = true;
                    projectile.localNPCHitCooldown = -1;
                    break;
                case ProjectileID.InfernoFriendlyBlast:
                    projectile.usesLocalNPCImmunity = true;
                    projectile.localNPCHitCooldown = 10;
                    projectile.timeLeft = 60;
                    break;
                case ProjectileID.ChlorophyteBullet:
                    projectile.aiStyle = -1;
                    break;
            }

            //if (
            //    entity.type == ProjectileID.EnchantedBoomerang ||
            //    entity.type == ProjectileID.FruitcakeChakram ||
            //    entity.type == ProjectileID.IceBoomerang ||
            //    entity.type == ProjectileID.Shroomerang ||
            //    entity.type == ProjectileID.WoodenBoomerang ||
            //    entity.type == ProjectileID.BloodyMachete ||
            //    entity.type == ProjectileID.CombatWrench ||
            //    entity.type == ProjectileID.Flamarang ||
            //    entity.type == ProjectileID.ThornChakram ||
            //    entity.type == ProjectileID.Trimarang ||
            //    entity.type == ProjectileID.Bananarang ||
            //    entity.type == ProjectileID.LightDisc ||
            //    entity.type == ProjectileID.FlyingKnife ||
            //    entity.type == ProjectileID.PossessedHatchet ||
            //    entity.type == ProjectileID.PaladinsHammerFriendly 
            //)
            //{
            //    entity.DamageType = DamageClass.Ranged;
            //}
        }
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (Main.player[projectile.owner].GetModPlayer<PlayerStats>().ExtraWeaponPierceMultiplier > 0 && projectile.penetrate > 0)
            {
                projectile.maxPenetrate = (int)(projectile.maxPenetrate * Main.player[projectile.owner].GetModPlayer<PlayerStats>().ExtraWeaponPierceMultiplier);
                projectile.penetrate = (int)(projectile.penetrate * Main.player[projectile.owner].GetModPlayer<PlayerStats>().ExtraWeaponPierceMultiplier);
            }
        }
        public override void AI(Projectile projectile)
        {
            switch (projectile.type)
            {
                case ProjectileID.ChlorophyteBullet:
                    projectile.ai[0]++;
                    if (projectile.ai[0] > 30)
                        projectile.velocity.Y += 0.1f;
                    break;
            }
        }
        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            switch (projectile.type)
            {
                case ProjectileID.ChlorophyteBullet:
                    Projectile.NewProjectile(projectile.GetSource_FromThis(),projectile.Center,projectile.Center - target.Center,ModContent.ProjectileType<ChloroSprout>(),projectile.damage,projectile.knockBack,projectile.owner,target.whoAmI,target.rotation,Main.rand.NextFloat(-0.2f,0.2f));
                    break;
            }
        }
    }
}
