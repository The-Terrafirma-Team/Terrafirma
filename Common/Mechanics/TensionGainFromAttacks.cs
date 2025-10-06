﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Common.Mechanics
{
    public class TensionGainFromAttacks : ModPlayer
    {
        public float AccumulatedTension = 0;
        private void AccumulatedTensionDistanceScaler(float damage, NPC target)
        {
            int maxDistance = 35;
            int giveTension = 0;
            AccumulatedTension += Math.Max(((16 * maxDistance) - Player.Center.Distance(target.Hitbox.ClosestPointInRect(Player.Center))) / 50 * damage, 0);
            while (AccumulatedTension > 75)
            {
                AccumulatedTension -= 75;
                giveTension++;
            }
            if (giveTension > 0)
            {
                Player.GiveTension(giveTension, true);
            }
        }
        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (target.friendly || target.lifeMax <= 5)
                return;
            AccumulatedTensionDistanceScaler(ContentSamples.ItemsByType[item.type].useAnimation * (hit.Crit? 2f : 1) * DataSets.ItemTensionGainMultiplier[item.type], target);
        }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (target.friendly || target.lifeMax <= 5)
                return;
            var global = proj.GetGlobalProjectile<TensionGainFromAttacksGlobalProjectile>();
            float accumulate = global.AccumulateTension;
            AccumulatedTensionDistanceScaler(accumulate * (hit.Crit ? 2f : 1), target);

            if (Player.heldProj != proj.whoAmI)
            global.AccumulateTension *= 0.8f;
        }
    }
    public class TensionGainFromAttacksGlobalProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public float AccumulateTension = 0;
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if(source is EntitySource_ItemUse_WithAmmo s)
            {
                AccumulateTension = ContentSamples.ItemsByType[s.Item.type].useTime * DataSets.ItemTensionGainMultiplier[s.Item.type];
            }
            else if (source is EntitySource_Parent s2 && s2.Entity is Projectile p)
            {
                AccumulateTension = p.GetGlobalProjectile<TensionGainFromAttacksGlobalProjectile>().AccumulateTension;
            }
            AccumulateTension *= DataSets.ProjectileTensionGainMultiplier[projectile.type];
        }
    }
}
