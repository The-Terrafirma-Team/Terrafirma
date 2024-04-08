using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Common.Templates;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Projectiles.Magic
{
    public class DruidStaff : DruidProjectile
    {
        public override void SetDefaults()
        {
            Projectile.hide = true;
            Projectile.aiStyle = -1;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.Size = new Vector2(8);
            Projectile.extraUpdates = 2;
            Projectile.friendly = true;
        }
        public override void PostAI()
        {
            Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.RainbowTorch, Vector2.Zero);
            d.noGravity = true;
            d.color = allignmentColor;
            d.scale = 1;
            Projectile.ai[0]++;
        }
        public override void evilAI(float ratio)
        {
            if (Projectile.ai[0] == 1)
            {
                Projectile.velocity -= Projectile.velocity * ratio * 0.5f;
            }
        }
        public override void coldAI(float ratio)
        {
            base.coldAI(ratio);
        }
        public override void hotAI(float ratio)
        {
            base.hotAI(ratio);
        }
        public override void hallowAI(float ratio)
        {
            base.hallowAI(ratio);
        }
        public override void waterAI(float ratio)
        {
            base.waterAI(ratio);
        }
        public override void jungleAI(float ratio)
        {
            base.jungleAI(ratio);
        }
    }
}
