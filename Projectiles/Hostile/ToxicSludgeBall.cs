using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Particles;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Projectiles.Hostile
{
    public class ToxicSludgeBall : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(16);
            Projectile.hostile = true;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 60 * 10;
            Projectile.alpha = ContentSamples.NpcsByNetId[NPCID.ToxicSludge].alpha;
        }
        public override void AI()
        {
            Projectile.velocity.Y += 0.2f;
            Projectile.rotation += Projectile.direction * 0.1f;
            if (Projectile.ai[1] > 9)
            {
                Projectile.Kill();
            }
            if (Projectile.timeLeft == 60 * 10)
            {
                for (int i = 0; i < 20; i++)
                {
                    Dust d = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(8, 4), DustID.t_Slime, Main.rand.NextVector2Circular(2, 2) + Projectile.velocity / 3, Projectile.alpha, new Color(210, 230, 140));
                    d.noGravity = !Main.rand.NextBool(5);
                }
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if(oldVelocity != Projectile.velocity)
            {
                Projectile.velocity.Y = Projectile.oldVelocity.Y * -1f;
                SoundEngine.PlaySound(SoundID.NPCHit1 with { Volume = 0.5f}, Projectile.position);
                if(Projectile.velocity.Y != oldVelocity.Y)
                {
                    Projectile.velocity.Y = Projectile.oldVelocity.Y * -0.9f;
                }
                if (Projectile.velocity.X != oldVelocity.X)
                {
                    Projectile.velocity.X = Projectile.oldVelocity.X * -0.9f;
                }
                for (int i = 0; i < 20; i++)
                {
                    Dust d = Dust.NewDustPerfect(Projectile.Center + Vector2.Normalize(Projectile.oldVelocity) * 8,DustID.t_Slime, Vector2.Zero, Projectile.alpha, new Color(210, 230, 140));
                    d.velocity = -new Vector2(Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(0, 3)).RotatedBy(Projectile.velocity.ToRotation());
                    d.noGravity = !Main.rand.NextBool(3);
                }
                Projectile.ai[1]++;
                if (Projectile.velocity.Length() < 2)
                {
                    Projectile.Kill();
                }
            }
            return false;
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.t_Slime, Main.rand.NextVector2Circular(6,6), Projectile.alpha, new Color(210, 230, 140));
                d.noGravity = !Main.rand.NextBool(3);
            }
        }
    }
}
