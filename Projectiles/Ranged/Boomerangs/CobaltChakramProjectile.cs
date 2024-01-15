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
    public class CobaltChakramProjectile : ChakramTemplate
    {
        public override string Texture => "TerrafirmaRedux/Items/Weapons/Ranged/Boomerangs/Chakram/CobaltChakram";
        protected override int BounceAmount => 12;
        protected override int BounceMode => 1;
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
            DrawOffsetX = -9;
            DrawOriginOffsetY = -9;

        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, hit, damageDone);
            if (Projectile.penetrate > -BounceAmount)
            {
                AttackTime += 10;
            }
        }
    }
}
