using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Buffs.Debuffs;
using Terraria;
using Terraria.DataStructures;
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

        public override void OnSpawn(IEntitySource source)
        {
            if (Projectile.ai[0] == -1)
            {
                Projectile.rotation = Main.rand.NextFloat(0f, MathHelper.PiOver2);
            }
        }
        public override void OnKill(int timeLeft)
        {

            for(int i = 0; i < 5; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Chlorophyte, Main.rand.NextVector2Circular(4, 4), 128);
                d.noGravity = true;
                d.fadeIn = 2;
            }

            if (Projectile.localAI[0] == 1)
                return;
            if (Projectile.ai[0] > -1)
            {
                NPC target = Main.npc[(int)Projectile.ai[0]];
                target.SimpleStrikeNPC(Projectile.damage / 4, 0, false, 0, DamageClass.Ranged, true, Main.player[Projectile.owner].luck);
            }
            else
            {
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    NPC target = Main.npc[i];
                    if (!target.friendly && target.Center.Distance(Projectile.Center) < 40f)
                    {
                        target.SimpleStrikeNPC(Projectile.damage / 4, 0, false, 0, DamageClass.Ranged, true, Main.player[Projectile.owner].luck);
                    }
                }
            }
        }
        public override void AI()
        {
            NPC target;
            if (Projectile.ai[0] > -1)
            {
                target = Main.npc[(int)Projectile.ai[0]];
                Projectile.rotation = Projectile.ai[2] + target.rotation;

                if (!target.active)
                {
                    Projectile.localAI[0] = 1;
                    Projectile.Kill();
                }

                Projectile.Center = target.Center + Projectile.velocity.RotatedBy(target.rotation - Projectile.ai[1]);
            }
            Projectile.frameCounter++;

            

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
