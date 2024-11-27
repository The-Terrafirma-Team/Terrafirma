using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Audio;

namespace Terrafirma.Projectiles.Summon.Sentry.PreHardmode
{
    internal class ShockPotionLightning : ModProjectile
    {

        public override string Texture => "Terrafirma/Projectiles/Summon/Sentry/PreHardmode/MechanicsPocketSentry";

        NPC targetnpc = null;
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.height = 4;
            Projectile.width = 4;
            Projectile.DamageType = DamageClass.Magic;

            Projectile.tileCollide = true;
            Projectile.timeLeft = 600;
            Projectile.extraUpdates = 60;
            Projectile.penetrate = -1;
            Projectile.Opacity = 0f;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (target.whoAmI == Projectile.ai[1])
                return false;
            return base.CanHitNPC(target);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            targetnpc = target;
            Projectile.ai[1] = target.whoAmI;
        }
        public override void AI()
        {
            int t = Find();
            if (t == -1)
                return;

            if (Projectile.ai[0] == 0)
            {
                SoundEngine.PlaySound(SoundID.DD2_LightningAuraZap, Projectile.Center);
            }

            targetnpc = Main.npc[t];
            if (!targetnpc.active)
                Projectile.Kill();

            if (Projectile.ai[0] % 30 == 0)
            {
                float minimalise = 50f * Math.Clamp(Projectile.ai[0] / 100f, 2f, 10f);
                Projectile.velocity = Projectile.Center.DirectionTo(targetnpc.Center).RotatedByRandom(Projectile.Center.Distance(targetnpc.Center) / minimalise);
            }

            if (Projectile.ai[0] % 3 == 0)
            {
                Dust newdust = Dust.NewDustPerfect(Projectile.Center, DustID.Frost, Vector2.Zero, 0, Color.White, 1f);
                newdust.noGravity = true;
                newdust.scale = Projectile.timeLeft / 450f;
            }
            Projectile.ai[0]++;

        }
        private int Find(float maxRange = 800f)
        {
            float num = maxRange;
            int result = -1;
            for (int i = 0; i < 200; i++)
            {
                NPC nPC = Main.npc[i];
                bool flag = nPC.CanBeChasedBy(this);
                if (Projectile.localNPCImmunity[i] != 0 || i == Projectile.ai[1])
                    flag = false;
                if (flag)
                {
                    float num2 = Projectile.Distance(Main.npc[i].Center);
                    if (num2 < num && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, nPC.position, nPC.width, nPC.height))
                    {
                        num = num2;
                        result = i;
                    }
                }
            }

            return result;
        }
    }
}
