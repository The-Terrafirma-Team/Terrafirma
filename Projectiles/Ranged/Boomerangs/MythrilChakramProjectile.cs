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
using Microsoft.Xna.Framework;

namespace Terrafirma.Projectiles.Ranged.Boomerangs
{
    public class MythrilChakramProjectile : ChakramTemplate
    {
        public override string Texture => "Terrafirma/Items/Weapons/Ranged/Boomerangs/Chakram/MythrilChakram";
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
            DrawOffsetX = -5;
            DrawOriginOffsetY = -5;

        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, hit, damageDone);
            if (Main.myPlayer == Projectile.owner)
            {
                for (int i = 0; i < 2; i++)
                {
                    Projectile newproj = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(i == 0 ? -5 : 5), ModContent.ProjectileType<MythrilChakramSplitProjectile>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0, 0, target.whoAmI); ;
                    if (i == 0) newproj.rotation = (float)Math.PI;
                }


            }
            Projectile.Kill();

        }
    }
}
