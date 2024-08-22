using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terrafirma.Buffs.Debuffs;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Projectiles.Ranged.Arrows
{
    internal class CactusArrowProjectile : ModProjectile
    {
        Vector2 offset =  Vector2.Zero;
        float stickrotation = 0f;
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            DrawOffsetX = -Projectile.width / 2 - 5;
            Projectile.penetrate = 5;
            Projectile.timeLeft = 600;
            Projectile.friendly = true;
            Projectile.arrow = true;
            Projectile.hide = true;
            Projectile.DamageType = DamageClass.Ranged;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }

        private readonly Point[] stickingProjectiles = new Point[8];
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.ai[0] == 0)
            {
                offset = (Projectile.Center + Projectile.velocity) - target.Center;
                stickrotation = Projectile.rotation;
                Projectile.timeLeft = 180;
                Projectile.velocity = Vector2.Zero;
                Projectile.ai[0] = 1;
                Projectile.ai[1] = target.whoAmI;
                Projectile.netUpdate = true;
                Projectile.tileCollide = false;
                Projectile.KillOldestJavelin(Projectile.whoAmI, Type, target.whoAmI, stickingProjectiles);
            }
        }

        public override void AI()
        {

            if (Projectile.ai[0] == 1)
            {
                Projectile.Center = Main.npc[(int)Projectile.ai[1]].Center + offset;
                Projectile.ai[2]++;
                Projectile.rotation = stickrotation;

                if (Projectile.ai[2] % 60 == 0)
                {
                    Main.npc[(int)Projectile.ai[1]].SimpleStrikeNPC(Projectile.damage, 0, false, 0, DamageClass.Ranged, true);
                }
                if (!Main.npc[(int)Projectile.ai[1]].active) Projectile.Kill();
            }
            else
            {
                Projectile.velocity.Y += 0.1f;
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            }
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (targetHitbox.Width > 8 && targetHitbox.Height > 8)
            {
                targetHitbox.Inflate(-targetHitbox.Width / 12, -targetHitbox.Height / 12);
            }
            return projHitbox.Intersects(targetHitbox);
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                Dust.NewDust(Projectile.Center - Projectile.velocity, 4, 4, DustID.t_Cactus, Main.rand.NextFloat(-0.4f,0.4f), Main.rand.NextFloat(-0.4f, 0.4f), Scale: 1.2f);
            }
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        }

        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            if (Projectile.ai[0] == 1)
            {
                behindNPCsAndTiles.Add((int)Projectile.ai[1]);
            }
            behindNPCsAndTiles.Add(index);
            base.DrawBehind(index, behindNPCsAndTiles, behindNPCs, behindProjectiles, overPlayers, overWiresUI);
        }
    }
}
