using Microsoft.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Projectiles.Ranged.Bullets
{
    internal class ExplosiveBuckshotProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;

            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;

            Projectile.timeLeft = 400;

            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 1;

            Projectile.aiStyle = 1;
            AIType = ProjectileID.Bullet;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.ai[2] = 0;

            Projectile.ArmorPenetration = 10;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            return false;
        }

        public override void AI()
        {
            if (Projectile.ai[2] == 0 && Main.myPlayer == Projectile.owner)
            {
                for (int i = 0; i < 3; i++)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, Projectile.velocity.RotatedBy(Main.rand.NextFloat(-10, 10) * (Math.PI / 180)) * Main.rand.NextFloat(0.9f, 1.1f), Projectile.type, Projectile.damage / 2, Projectile.knockBack, Projectile.owner, 0, 0, 1);

                }
                Projectile.Kill();
            }
            else
            {
                Lighting.AddLight(Projectile.position, new Vector3(0.4f, 0.4f, 0));
                Dust TorchDust = Dust.NewDustPerfect(Projectile.Center, DustID.Torch, new Vector2(Projectile.velocity.X * Main.rand.NextFloat(0.8f, 1.2f), Projectile.velocity.Y * Main.rand.NextFloat(0.8f, 1.2f)), 0, default, Main.rand.NextFloat(1.2f, 1.4f));
                TorchDust.noGravity = true;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {

        }

        public override bool PreDraw(ref Color lightColor)
        {
            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

            Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
            }

            return true;
        }

        public override void OnKill(int timeLeft)
        {
            if (Projectile.ai[2] == 1)
            {
                for (int i = 0; i < 5; i++)
                {
                    Dust TorchDust = Dust.NewDustPerfect(Projectile.position, DustID.Torch, new Vector2(Main.rand.NextFloat(-1.6f, 1.6f) * 2, Main.rand.NextFloat(-1.6f, 1.6f) * 2), 0, default, Main.rand.NextFloat(1.5f, 1.8f));
                    TorchDust.noGravity = true;
                }
                for (int i = 0; i < 12; i++)
                {
                    Dust SmokeDust = Dust.NewDustPerfect(Projectile.position, DustID.Smoke, new Vector2(Main.rand.NextFloat(-1.4f, 1.4f) * 2, Main.rand.NextFloat(-1.4f, 1.4f) * 2), 128, default, Main.rand.NextFloat(1.2f, 1.7f));
                    SmokeDust.noGravity = true;
                }
                Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
                SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
                Projectile.Explode(100);
            }


        }

    }
}
