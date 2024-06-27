using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terrafirma.Systems.MageClass;
using Terrafirma.Systems.Primitives;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.VanillaMagic.Spells.Evil
{
    internal class CrimsonWorm : Spell
    {
        public override int UseAnimation => 24;
        public override int UseTime => 24;
        public override int ManaCost => 5;
        public override int[] SpellItem => [ItemID.CrimsonRod];

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source,position, velocity, ModContent.ProjectileType<CrimsonRodWorm>(), damage, knockback,player.whoAmI);
            return false;
        }
    }
    public class CrimsonRodWorm : ModProjectile
    {
        private static Asset<Texture2D> trailTex;
        Trail trail;
        public override void Load()
        {
            trailTex = ModContent.Request<Texture2D>(Texture);
        }
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 6;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }
        public override void SetDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 13;
            trail = new Trail(Projectile.oldPos, TrailWidth.FlatWidth, 14);
            trail.trailtexture = trailTex.Value;
            trail.color = f => Lighting.GetSubLight(Projectile.oldPos[(int)((1 - f) * (ProjectileID.Sets.TrailCacheLength[Type] - 1))]).ToColor();

            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.Size = new Vector2(16);
            Projectile.penetrate = 5;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
            Projectile.timeLeft = 60 * 10;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            NPC target = TFUtils.FindClosestNPC(500, Projectile.Center);
            if (!Collision.SolidTiles(Projectile.position, 16, 16))
            {
                Projectile.velocity.Y += 0.5f;
                if(target != null)
                    Projectile.velocity.X += Projectile.Center.DirectionTo(target.Center).X * 1f;
            }
            else if (target != null)
            {
                if (Projectile.timeLeft % 10 == 0)
                {
                    SoundEngine.PlaySound(SoundID.WormDigQuiet, Projectile.position);
                    Collision.HitTiles(Projectile.position, Projectile.velocity, 16, 16);
                }
                Projectile.velocity += Projectile.Center.DirectionTo(target.Center) * 0.8f;
            }
            if(target == null || !target.active)
            {
                Projectile.velocity.Y += 1;
            }
            Projectile.velocity = Projectile.velocity.LengthClamp(16);
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (projHitbox.Intersects(targetHitbox))
                return true;
            Vector2 pos = Projectile.position;
            for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[Type] - 1; i++)
            {
                //pos += (Projectile.oldRot[i]).ToRotationVector2() * -18;
                pos += Projectile.oldPos[i].DirectionFrom(Projectile.oldPos[i + 1]) * -MathHelper.Clamp(Projectile.oldPos[i].Distance(Projectile.oldPos[i + 1]) * 3, 0, 16);
                if (targetHitbox.Intersects(new Rectangle((int)pos.X, (int)pos.Y, 16, 16)))
                    return true;
            }
            return false;
        }
        public override void OnKill(int timeLeft)
        {
            Vector2 pos = Projectile.position;
            for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[Type] - 1; i++)
            {
                //pos += (Projectile.oldRot[i]).ToRotationVector2() * -18;
                pos += Projectile.oldPos[i].DirectionFrom(Projectile.oldPos[i + 1]) * -MathHelper.Clamp(Projectile.oldPos[i].Distance(Projectile.oldPos[i + 1]) * 3, 0, 16);
                SoundEngine.PlaySound(SoundID.NPCDeath1, Projectile.position);
                for (int x = 0; x < 8; x++)
                {
                    Dust.NewDustPerfect(pos, DustID.Blood);
                }
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            trail.Draw(Projectile.Center);
            //Asset<Texture2D> tex = TextureAssets.Projectile[Type];

            //Vector2 pos = Projectile.position;

            //for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[Type] - 1; i++)
            //{
            //    //pos += (Projectile.oldRot[i]).ToRotationVector2() * -18;
            //    pos += Projectile.oldPos[i].DirectionFrom(Projectile.oldPos[i+1]) * -MathHelper.Clamp(Projectile.oldPos[i].Distance(Projectile.oldPos[i+1]) * 3,0,16);
            //    float rotation = Projectile.oldRot[i];
            //    Main.EntitySpriteDraw(tex.Value, pos - Main.screenPosition + Projectile.Size / 2, new Rectangle(20 * i, 0, 20, 14), lightColor, rotation, new Vector2(10, 7), 1, SpriteEffects.FlipHorizontally, 0);
            //}

            return false;
        }
    }
}
