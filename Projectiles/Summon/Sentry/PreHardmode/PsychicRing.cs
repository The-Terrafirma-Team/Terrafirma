using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ID;
using Terraria.GameContent;

namespace Terrafirma.Projectiles.Summon.Sentry.PreHardmode
{
    public class PsychicRing : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.height = 20;
            Projectile.width = 20;
            Projectile.DamageType = DamageClass.Summon;

            Projectile.tileCollide = false;
            Projectile.timeLeft = 3600;
            Projectile.usesLocalNPCImmunity = true;
        }
        public override void AI()
        {
            if (Main.npc[(int)Projectile.ai[0]].active) Projectile.velocity = Vector2.Lerp(Projectile.velocity, Projectile.DirectionTo(Main.npc[(int)Projectile.ai[0]].Center) * 14f, 0.03f);
            else if (TFUtils.FindClosestNPC(1000f, Projectile.Center) != null)
            {
                NPC ClosestNPC = TFUtils.FindClosestNPC(1000f, Projectile.Center);
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, Projectile.DirectionTo(ClosestNPC.Center) * 14f, 0.05f);
            }
            else Projectile.Kill();
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 4; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.Center, 16, 16, DustID.GemAmethyst, Main.rand.NextFloat(-0.5f, 0.5f), Main.rand.NextFloat(-0.5f, 0.5f), 0, new Color(240, 200, 255, 0));
                dust.noGravity = true;
            }
            base.OnKill(timeLeft);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Asset<Texture2D> tex = TextureAssets.Projectile[Type];
            Main.EntitySpriteDraw(tex.Value,Projectile.Center - Main.screenPosition,tex.Value.Bounds,new Color(180, 20, 255, 0) * 0.6f,0,tex.Value.Size() / 2,1f,SpriteEffects.None);
            Main.EntitySpriteDraw(tex.Value, Projectile.Center - Projectile.velocity - Main.screenPosition, tex.Value.Bounds, new Color(180, 20, 255, 0) * 0.4f, 0, tex.Value.Size() / 2, 1.1f, SpriteEffects.None);

            return false;
        }
    }
}
