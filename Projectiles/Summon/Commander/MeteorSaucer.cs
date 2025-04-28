using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Buffs.Armies;
using Terrafirma.Common.Templates;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Projectiles.Summon.Commander
{
    public class MeteorSaucer : CommanderSummonTemplate
    {
        public override int AssociatedBuff => ModContent.BuffType<MeteorSaucerArmy>();
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 12;
        }
        public override void SetDefaults()
        {
            Projectile.QuickDefaults(false, 36);
            Projectile.DamageType = DamageClass.Summon;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
        }
        public override void AI()
        {
            base.AI();
            Projectile.frameCounter++;
            Projectile.frame = ((Projectile.frameCounter / 4) % 4) + mode * 4;
            Projectile.rotation = Projectile.velocity.X * 0.1f;
        }
        public override void SwitchMode(byte oldMode)
        {
            Projectile.ai[0] = Main.rand.NextFloat(0, MathHelper.TwoPi / 0.1f);
            Projectile.ai[1] = 0;
            Projectile.ai[2] = Main.rand.Next(30);
        }
        public override void PrimaryAI(bool Focused)
        {
            Projectile.ai[0]++;

            if(Projectile.Center.Distance(owner.Center) > Projectile.ai[1] && Projectile.ai[1] > 0)
            {
                Projectile.ai[1] = 0;
            }
            else if(Projectile.Center.Distance(owner.Center) < 60 && Projectile.ai[1] == 0)
            {
                Projectile.ai[1] = Math.Min(owner.Center.Distance(owner.PlayerStats().MouseWorld),300);
            }

            Projectile.velocity += Projectile.Center.DirectionTo(owner.Center + new Vector2(Projectile.ai[1] * 2).RotatedBy(owner.Center.DirectionTo(owner.PlayerStats().MouseWorld).ToRotation() - MathHelper.PiOver4));
            Projectile.velocity += new Vector2(0, 0.1f).RotatedBy(Projectile.ai[0] * 0.1f);
            Projectile.velocity = Projectile.velocity.LengthClamp(9);
            //Projectile.velocity = Vector2.Lerp(Projectile.Center.DirectionTo(Vector2.Lerp(owner.Center, owner.PlayerStats().MouseWorld, Math.Abs(MathF.Sin(Projectile.ai[0] * MathHelper.TwoPi / 120)))) * 12f,Projectile.velocity,0.9f);
        }
        public override void SecondaryAI(bool Focused)
        {
            Projectile.ai[0]++;
            Projectile.ai[1] += 0.01f;
            Projectile.Center = Vector2.Lerp(Projectile.Center, owner.Center + new Vector2(0, 50).RotatedBy(Projectile.ai[0] * 0.1f), Math.Min(Projectile.ai[1],1));
            Projectile.velocity = Projectile.position.DirectionFrom(Projectile.oldPosition) * Math.Min(Projectile.ai[1], 1) * Projectile.position.Distance(Projectile.oldPosition);
        }
        public override void TertriaryAI(bool Focused)
        {
            Projectile.ai[0]++;
            Projectile.ai[1] += 0.001f;
            Projectile.ai[2]++;
            Projectile.Center = Vector2.Lerp(Projectile.Center, owner.Center + new Vector2(0,32) + new Vector2(1).RotatedBy(Projectile.ai[0] * 0.07f) * new Vector2(64,16), Math.Min(Projectile.ai[1], 0.3f));
            Projectile.velocity = Projectile.position.DirectionFrom(Projectile.oldPosition) * Math.Min(Projectile.ai[1], 1) * Projectile.position.Distance(Projectile.oldPosition);
            if (Projectile.ai[2] > 30)
            {
                Projectile.ai[2] = 0;
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Top, new Vector2(0, -12),ProjectileID.LaserMachinegunLaser,Projectile.damage,Projectile.knockBack,Projectile.owner);
            }
        }
    }
}
