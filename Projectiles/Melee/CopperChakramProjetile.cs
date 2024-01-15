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

namespace TerrafirmaRedux.Projectiles.Melee
{
    public class CopperChakramProjetile : ChakramTemplate
    {
        public override string Texture => "TerrafirmaRedux/Items/Weapons/Melee/Boomerangs/Chakram/CopperChakram";
        protected override int BounceAmount => 0;
        protected override int AttackTime => 40;
        protected override int BounceMode => 0;
        protected override float ReturnSpeed => 14f;

        public override void SetDefaults()
        {
            Projectile.penetrate = -1;
            Projectile.timeLeft = 3600;
            Projectile.friendly = true;
            Projectile.damage = 16;
            Projectile.width = 20;
            Projectile.height = 20;
            DrawOffsetX = -5;
            DrawOriginOffsetY = -5;
            
        }
    }
}
