using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Common.Players;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using ReLogic.Content;
using Microsoft.Xna.Framework.Graphics;
using Terrafirma.Buffs.Debuffs;
using Terrafirma.Particles;
using Terrafirma.Common.Interfaces;
using Terrafirma.Data;

namespace Terrafirma.Common.Templates.Melee
{
    public abstract class PaladinHammer : HeldProjectile, IUsesStoredMeleeCharge
    {
        public virtual float ChargeIncrement => 0.01f;
        public virtual int ThrownProjectile => 1;
        public override void SetStaticDefaults()
        {
            ProjectileSets.AutomaticallyGivenMeleeScaling[Type] = true;
        }
        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(2);
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }
        public override bool? CanDamage()
        {
            return false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
        public virtual void Throw(bool fullCharge)
        {
            SoundEngine.PlaySound(SoundID.Item1, player.position);
            if (Main.myPlayer == player.whoAmI)
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), player.getFrontArmPosition() + player.Center, player.Center.DirectionTo(Main.MouseWorld) * player.HeldItem.shootSpeed * player.GetWeaponAttackSpeed(player.HeldItem), ThrownProjectile, (Projectile.damage / 2) + (int)(Projectile.damage * Easing.SineOut(Projectile.ai[0]) * 2.5f), Projectile.knockBack + (int)(Projectile.knockBack * Projectile.ai[0]), player.whoAmI, MathHelper.Clamp(Projectile.ai[0],0.1f,1f));
        }
        public override void AI()
        {
            commonHeldLogic(12);

            player.direction = MathF.Sign(player.PlayerStats().MouseWorld.X - player.Center.X);
            Projectile.ai[0] += ChargeIncrement * player.GetAdjustedWeaponSpeedPercent(player.HeldItem);

            if (Projectile.ai[0] > 1f)
            {
                Projectile.ai[1]++;
                Projectile.ai[0] = 1f;
            }
            if (Projectile.ai[1] == 1 && player.whoAmI == Main.myPlayer)
            {
                BigSparkle bigsparkle = new BigSparkle();
                bigsparkle.fadeInTime = 4;
                bigsparkle.Rotation = Main.rand.NextFloat(-0.4f, 0.4f);
                bigsparkle.Scale = 3f * Projectile.scale;
                bigsparkle.secondaryColor = Color.Transparent;
                ParticleSystem.AddParticle(bigsparkle, Projectile.Center + (TextureAssets.Projectile[Type].Size() * new Vector2(0.65f,-0.65f) * Projectile.scale).RotatedBy(Projectile.rotation), Vector2.Zero, new Color(0.3f, 0.3f, 0.6f, 0f));
                SoundEngine.PlaySound(SoundID.MaxMana, player.position);
            }
            // Animate the player
            int handMovement = 0;

            if (Projectile.ai[0] < 0.2f)
                handMovement = PlayerAnimation.PointDownRight;
            else if (Projectile.ai[0] < 0.4f)
                handMovement = PlayerAnimation.PointRight;
            else if (Projectile.ai[0] < 0.7f)
                handMovement = PlayerAnimation.PointUpRight;
            else
                handMovement = PlayerAnimation.PointUp;

            if (player.channel)
                player.bodyFrame.Y = player.bodyFrame.Height * handMovement;
            Projectile.position = player.getFrontArmPosition() + player.Center;
            Projectile.position = Projectile.position.ToPoint().ToVector2();
            Projectile.spriteDirection = player.direction;

            Projectile.rotation = MathHelper.Lerp(1.5f * player.direction, -1.5f * player.direction, Easing.SineOut(Projectile.ai[0]));
            if (player.direction == -1)
                Projectile.rotation -= MathHelper.PiOver2;

            if (!player.channel)
            {
                Throw(Projectile.ai[1] > 0);
                Projectile.Kill();
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            commonDiagonalItemDraw(lightColor, TextureAssets.Projectile[Type], Projectile.scale);
            return false;
        }

        public void ApplyStoredCharge(Player player, Projectile projectile)
        {
            projectile.ai[0] = player.PlayerStats().StoredMeleeCharge;
        }
    }

    public abstract class PaladinHammerThrown : ModProjectile
    {
        public bool Returning = false;
        public virtual int FlightDuration => 60;
        public virtual float ReturnSpeed => 14f;
        public override void SetDefaults()
        {
            Projectile.QuickDefaults(false,32);
            Projectile.penetrate = -1;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];
            player.GiveTension((int)(Projectile.ai[0] * 10));
            Projectile.damage = (int)(Projectile.damage * 0.8f);
            if (Projectile.ai[1] < FlightDuration * Projectile.ai[0])
            {
                if (Projectile.ai[0] == 1f)
                {
                    target.AddBuff(ModContent.BuffType<Stunned>(), 60 * 3);
                }
                Projectile.ai[1] = FlightDuration * Projectile.ai[0];
                Projectile.velocity = Projectile.Center.DirectionTo(player.Center) * ReturnSpeed;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            Projectile.CommonBounceLogic(oldVelocity);
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            return false;
        }
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 6;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }

        public void drawHammer(Color color, Asset<Texture2D> tex, Vector2 position, float scale, float rotation)
        {
            Main.EntitySpriteDraw(tex.Value, position - Main.screenPosition, new Rectangle(0, tex.Height() / Main.projFrames[Type] * Projectile.frame, tex.Width(), tex.Height() / Main.projFrames[Type]), color, rotation - (Projectile.spriteDirection == 1 ? 0 : MathHelper.PiOver2), new Vector2(tex.Width() / 2f, tex.Height() / 2f / Main.projFrames[Type]), scale, Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipVertically);
        }
        public void drawHammer(Color color, Asset<Texture2D> tex, Vector2 position, float scale, float rotation, Vector2 origin)
        {
            Main.EntitySpriteDraw(tex.Value, position - Main.screenPosition, new Rectangle(0, tex.Height() / Main.projFrames[Type] * Projectile.frame, tex.Width(), tex.Height() / Main.projFrames[Type]), color, rotation - (Projectile.spriteDirection == 1 ? 0 : MathHelper.PiOver2), origin, scale, Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipVertically);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Asset<Texture2D> tex = TextureAssets.Projectile[Type];
            int trailLength = ProjectileID.Sets.TrailCacheLength[Type];
            if (Projectile.ai[0] == 1)
            {
                for (int i = 0; i < trailLength; i++)
                {
                    drawHammer(lightColor * ((trailLength - i) / (float)trailLength) * 0.4f, tex, Projectile.oldPos[i] + Projectile.Size / 2, Projectile.scale, Projectile.oldRot[i]);
                }
            }
            drawHammer(lightColor, tex, Projectile.Center, Projectile.scale, Projectile.rotation);
            return false;
        }
        public virtual void Return()
        {

        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (Projectile.ai[1] == 0)
            {
                Projectile.scale = player.GetAdjustedItemScale(player.HeldItem);
                Projectile.Resize((int)(Projectile.width * Projectile.scale), (int)(Projectile.width * Projectile.scale));
                Projectile.spriteDirection = player.direction;
            }
            Projectile.rotation += Projectile.spriteDirection * 0.6f;
            Projectile.ai[1]++;

            if (Projectile.ai[1] % 10 == 0)
                SoundEngine.PlaySound(SoundID.Item7, Projectile.position);

            if (Projectile.ai[1] > FlightDuration * Projectile.ai[0])
            {
                if (Projectile.localAI[0] == 0)
                {

                    Returning = true;
                    Return();
                    Projectile.localAI[0] = 1;
                }
                Projectile.tileCollide = false;
                //Projectile.velocity += Projectile.Center.DirectionTo(player.Center) * 0.7f;
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, Projectile.Center.DirectionTo(player.Center) * ReturnSpeed, 0.1f);

                if (Projectile.Hitbox.Intersects(player.Hitbox))
                {
                    Projectile.Kill();
                }
            }
        }
    }
}
