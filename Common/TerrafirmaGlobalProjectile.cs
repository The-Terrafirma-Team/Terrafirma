using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Buffs.Buffs;
using Terrafirma.Common.Interfaces;
using Terrafirma.Common.Players;
using Terrafirma.Data;
using Terrafirma.Projectiles.Ranged.Bullets;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Common
{
    public class TerrafirmaGlobalProjectile : GlobalProjectile
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
        }
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (Main.player[projectile.owner].GetModPlayer<PlayerStats>().ExtraWeaponPierceMultiplier > 0 && projectile.penetrate > 0)
            {
                projectile.maxPenetrate = (int)(projectile.maxPenetrate * Main.player[projectile.owner].GetModPlayer<PlayerStats>().ExtraWeaponPierceMultiplier);
                projectile.penetrate = (int)(projectile.penetrate * Main.player[projectile.owner].GetModPlayer<PlayerStats>().ExtraWeaponPierceMultiplier);
            }
            if(source is EntitySource_ItemUse_WithAmmo ammoSource)
            {
                switch (ammoSource.Item.prefix)
                {
                    case PrefixID.Frenzying: 
                        projectile.velocity = projectile.velocity.RotatedByRandom(0.12f); 
                        break;
                    case PrefixID.Furious: 
                        projectile.velocity = projectile.velocity.RotatedByRandom(0.12f); 
                        break;
                }

                if (ProjectileSets.AutomaticallyGivenMeleeScaling[projectile.type])
                {
                    float scale = ammoSource.Player.GetAdjustedItemScale(ammoSource.Item);
                    projectile.Resize((int)(projectile.width * scale), (int)(projectile.height * scale));
                    projectile.scale = scale;
                }
                if (projectile.ModProjectile is IUsesStoredMeleeCharge i && ammoSource.Player.PlayerStats().StoredMeleeCharge != 0)
                {
                    i.ApplyStoredCharge(ammoSource.Player,projectile);
                    ammoSource.Player.PlayerStats().StoredMeleeCharge = 0;
                }
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
                    Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, projectile.Center - target.Center, ModContent.ProjectileType<ChloroSprout>(), projectile.damage, projectile.knockBack, projectile.owner, target.whoAmI, target.rotation, Main.rand.NextFloat(-0.2f, 0.2f));
                    break;
            }
        }

        public override bool OnTileCollide(Projectile projectile, Vector2 oldVelocity)
        {
            switch (projectile.type)
            {
                case ProjectileID.ChlorophyteBullet:
                    Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center + oldVelocity, Vector2.Zero, ModContent.ProjectileType<ChloroSprout>(), projectile.damage, projectile.knockBack, projectile.owner, -1, 0, Main.rand.NextFloat(-0.2f, 0.2f));
                    break;
            }

            return base.OnTileCollide(projectile,oldVelocity);
        }
    }
}
