using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.DataStructures;
using System.IO;
using Terrafirma.Common;

namespace Terrafirma.Projectiles.Summon.Sentry.PreHardmode
{
    internal class CrimsonHeartSentry : ModProjectile
    {
        public override string Texture => "Terrafirma/Projectiles/Summon/Sentry/PreHardmode/CrimsonHeartSentryBase";
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.height = 42;
            Projectile.width = 22;
            Projectile.DamageType = DamageClass.Summon;

            Projectile.tileCollide = true;
            Projectile.timeLeft = Projectile.SentryLifeTime;
            Projectile.penetrate = -1;
            Projectile.sentry = true;
            Projectile.hide = true;
           
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            behindProjectiles.Add(index);
            base.DrawBehind(index, behindNPCsAndTiles, behindNPCs, behindProjectiles, overPlayers, overWiresUI);
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            fallThrough = false;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
        }
        public override void OnSpawn(IEntitySource source)
        {
            base.OnSpawn(source);
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }

        
        public override void AI()
        {
            Projectile.velocity.Y += 0.5f;
            if (Projectile.ai[0] > 3)
            {
                bool HeartExists = false;
                for (int i = 0; i < Main.projectile.Length; i++)
                {
                    if (Main.projectile[i].owner == Projectile.owner && Main.projectile[i].type == ModContent.ProjectileType<CrimsonHeartSentryHeart>() && Main.projectile[i].active)
                    {
                        HeartExists = true;
                    }
                }
                if (!HeartExists) Projectile.Kill();
            }

            Projectile.ai[0]++;

            foreach(Projectile projectile in Main.ActiveProjectiles)
            {
                if(projectile.owner == Projectile.owner && projectile.type == ModContent.ProjectileType<CrimsonHeartSentryHeart>())
                {
                    projectile.GetGlobalProjectile<SentryStats>().BuffType = Projectile.GetGlobalProjectile<SentryStats>().BuffType;
                    projectile.GetGlobalProjectile<SentryStats>().BuffTime = Projectile.GetGlobalProjectile<SentryStats>().BuffTime;
                }
            }
            //if (Main.netMode == NetmodeID.SinglePlayer)
            //{
            //    for (int i = 0; i < Projectile.GetGlobalProjectile<SentryStats>().BuffTime.Length; i++)
            //    {
            //        if (Projectile.GetGlobalProjectile<SentryStats>().BuffTime[i] > 0)
            //        {
            //            for (int k = 0; k < Main.projectile.Length; k++)
            //            {
            //                if (Main.projectile[k].owner == Projectile.owner &&
            //                    Main.projectile[k].type == ModContent.ProjectileType<CrimsonHeartSentryHeart>() &&
            //                    Main.projectile[k].active &&
            //                    Projectile.GetGlobalProjectile<SentryStats>().BuffTime[i] > Main.projectile[k].GetGlobalProjectile<SentryStats>().BuffTime[i]
            //                    )
            //                {
            //                    Main.projectile[k].GetGlobalProjectile<SentryStats>().BuffTime[i] = Projectile.GetGlobalProjectile<SentryStats>().BuffTime[i];
            //                }
            //            }
            //        }
            //    }
            //}

        }

        public override void SendExtraAI(BinaryWriter writer)
        {

            base.SendExtraAI(writer);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Asset<Texture2D> SentryBase = ModContent.Request<Texture2D>("Terrafirma/Projectiles/Summon/Sentry/PreHardmode/CrimsonHeartSentryBase");

            Main.EntitySpriteDraw(SentryBase.Value, Projectile.Center - Main.screenPosition, new Rectangle(0, 0, 22, 44), lightColor, 0, new Vector2(22, 44) / 2, 1, SpriteEffects.None, 0);      

            return false;
        }
    }
}
