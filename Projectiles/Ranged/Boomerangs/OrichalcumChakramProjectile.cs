using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terrafirma.Global.Templates;

namespace Terrafirma.Projectiles.Ranged.Boomerangs
{
    public class OrichalcumChakramProjectile : ChakramTemplate
    {
        protected override int BounceAmount => 0;
        protected override int BounceMode => 0;
        protected override float ReturnSpeed => 14f;

        public override void SetDefaults()
        {

            AttackTime = 40;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 3600;
            Projectile.friendly = true;
            Projectile.damage = 16;
            Projectile.width = 20;
            Projectile.height = 20;
            DrawOffsetX = -1;
            DrawOriginOffsetY = -1;

        }
    }
}
