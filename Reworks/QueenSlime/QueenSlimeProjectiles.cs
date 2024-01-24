using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;

namespace TerrafirmaRedux.Reworks.QueenSlime
{
    public class QueenSlimeBeam : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 32;
            Projectile.aiStyle = -1;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.alpha = 255;
            Projectile.extraUpdates = 2;
        }

        public override bool? CanHitNPC(NPC target)
        {
            //if (Projectile.ai[2] is < 130 or > 170)
            //    return false;
            return base.CanHitNPC(target);
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float collsionPoint = 0;
            if (Projectile.ai[2] > 130)
                return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, Projectile.Center + new Vector2(0, -Projectile.ai[0]).RotatedBy(Projectile.rotation),Projectile.height,ref collsionPoint);
            return false;
        }
        public override void AI()
        {
            Projectile.position += -Projectile.velocity;
            Projectile.ai[2]++;
            if (Projectile.ai[2] < 120)
                Projectile.Opacity = (float)Math.Sin(Projectile.ai[2] / MathHelper.Pi / 10) * 0.4f;
            else if (Projectile.ai[2] > 130 && Projectile.ai[2] < 130 + 120)
            {
                Projectile.Opacity = (float)Math.Sin(Projectile.ai[2] / MathHelper.Pi / 10) * 1f;
            }
            else if (Projectile.ai[2] > 170)
            {
                Projectile.Opacity -= 0.05f;
                if(Projectile.Opacity <= 0)
                {
                    Projectile.active = false;
                }
            }

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

            if (Projectile.ai[2] == 130)
            {
                Projectile.ai[2] += 70;
                SoundEngine.PlaySound(SoundID.Item33,Projectile.Center);
                for(int i = 0; i < 100; i++)
                {
                    Dust d = Dust.NewDustPerfect(Projectile.Center + new Vector2(Main.rand.Next(-10, 10), -Main.rand.Next(1000)).RotatedBy(Projectile.rotation),DustID.HallowedTorch,new Vector2(Main.rand.NextFloat(-2,2),Main.rand.NextFloat(-10)).RotatedBy(Projectile.rotation));
                    d.noGravity = true;
                    d.fadeIn = Main.rand.NextFloat(1.5f);
                }
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = ModContent.Request<Texture2D>(Texture).Value;
            if (Projectile.ai[2] < 140)
            {
                Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition, new Rectangle(0, 0, tex.Width, tex.Height), new Color(1f, 0.5f, 0.8f, 0) * Projectile.Opacity, Projectile.rotation, new Vector2(tex.Width / 2f, tex.Height), new Vector2((1 + Projectile.Opacity * 0.2f) * Projectile.ai[1], Projectile.ai[0]), SpriteEffects.None);
            }
            else
            {
                Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition, new Rectangle(0, 0, tex.Width, tex.Height), new Color(1f, 0.5f, 0.8f, 0) * Projectile.Opacity, Projectile.rotation, new Vector2(tex.Width / 2f, tex.Height), new Vector2((3 + Projectile.Opacity * 2f) * Projectile.ai[1], Projectile.ai[0]), SpriteEffects.None);
                Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition, new Rectangle(0, 0, tex.Width, tex.Height), new Color(1f, 1f, 1f, 0) * ((Projectile.Opacity * 0.8f) + Main.masterColor * 0.2f), Projectile.rotation, new Vector2(tex.Width / 2f, tex.Height), new Vector2((2 + Projectile.Opacity * 2f) * Projectile.ai[1], Projectile.ai[0]), SpriteEffects.None);
            }
            return false;
        }
    }

    public class MajesticGelShot : ModProjectile
    {
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 255, 255, 128) * 0.8f;
        }
        public override void SetDefaults()
        {
            Projectile.hostile = true;
            Projectile.aiStyle = -1;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 480;
            Projectile.Size = new Vector2(16);
            Projectile.scale = 1.4f;
            DrawOriginOffsetY = 8;
        }
        public override void AI()
        {
            base.AI();
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Projectile.velocity *= 0.999f;
            if(Projectile.timeLeft == 1)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient && TFUtils.TypeCountNPC(NPCID.QueenSlimeMinionPurple) < 4)
                {
                    int npc = NPC.NewNPC(Projectile.GetSource_FromThis(), (int)Projectile.position.X, (int)Projectile.position.Y, NPCID.QueenSlimeMinionPurple);
                    NetMessage.SendData(MessageID.SyncNPC, -1, -1, NetworkText.Empty, npc);
                }
            }
        }
    }
}
