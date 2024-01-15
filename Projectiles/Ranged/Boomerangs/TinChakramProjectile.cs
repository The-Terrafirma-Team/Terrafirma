using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using TerrafirmaRedux.Global.Templates;

namespace TerrafirmaRedux.Projectiles.Ranged.Boomerangs
{
    public class TinChakramProjetile : ChakramTemplate
    {
        public override string Texture => "TerrafirmaRedux/Items/Weapons/Ranged/Boomerangs/Chakram/TinChakram";
        protected override int BounceAmount => 2;
        protected override int BounceMode => 1;
        protected override float ReturnSpeed => 14f;
        protected override float ReturnAcc => 0.015f;

        public override void SetDefaults()
        {
            AttackTime = 30;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 3600;
            Projectile.friendly = true;
            Projectile.damage = 16;
            Projectile.width = 20;
            Projectile.height = 20;
            DrawOffsetX = -4;
            DrawOriginOffsetY = -10;

        }
    }
}
