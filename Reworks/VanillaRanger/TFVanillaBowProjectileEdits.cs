using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.VanillaRanger
{
    internal class VanillaBowProjectileEdits : GlobalProjectile
    {
        public int sourceWeapon = 0;
        public float power = 0f;

        public override bool InstancePerEntity => true;

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            switch (sourceWeapon)
            {
                case ItemID.BorealWoodBow: if (Main.rand.NextFloat(0f, 1f) <= power) target.AddBuff(BuffID.Frostburn, 60 * 5); break;
            }
            base.OnHitNPC(projectile, target, hit, damageDone);
        }
    }
}
