using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Buffs.Debuffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Projectiles.Ranged.Bullets
{
    public class ChloroSprout : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 3;
        }
        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(28);
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.timeLeft = 60 * 1;
            Projectile.DamageType = DamageClass.Ranged;
        }
        public override bool? CanDamage()
        {
            return false;
        }
        public override bool ShouldUpdatePosition()
        {
            return false;
        }
        public override void Kill(int timeLeft)
        {
            NPC target = Main.npc[(int)Projectile.ai[0]];

            for(int i = 0; i < 5; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Chlorophyte, Main.rand.NextVector2Circular(4, 4), 128);
                d.noGravity = true;
                d.fadeIn = 2;
            }

            if (Projectile.localAI[0] == 1)
                return;
            target.SimpleStrikeNPC(Projectile.damage / 4, 0, false, 0, DamageClass.Ranged, true, Main.player[Projectile.owner].luck);
            if (Main.rand.NextBool(5))
                Main.NewText("Figure out what these actually do plz");
        }
        public override void AI()
        {
            NPC target = Main.npc[(int)Projectile.ai[0]];
            Projectile.rotation = Projectile.ai[2] + target.rotation;
            Projectile.frameCounter++;

            if (!target.active)
            {
                Projectile.localAI[0] = 1;
                Projectile.Kill();
            }

            Projectile.Center = target.Center + Projectile.velocity.RotatedBy(target.rotation - Projectile.ai[1]);

            if (Projectile.frame < 2 && Projectile.frameCounter > 5)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
            }
            //if(Projectile.frame == 2)
            //{
            //    for(int i = 0; i < Main.projectile.Length; i++)
            //    {
            //        Projectile proj = Main.projectile[i];
            //        if (proj.friendly && proj.active && proj.type != Type && proj.Colliding(proj.Hitbox, new Rectangle(Projectile.Hitbox.X - 20, Projectile.Hitbox.Y - 20, Projectile.width + 20, Projectile.height + 20)))
            //        {
            //            Projectile.Kill();
            //        }
            //    }
            //}
        }
    }
}
