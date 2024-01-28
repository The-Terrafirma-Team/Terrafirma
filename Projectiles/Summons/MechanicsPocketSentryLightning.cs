using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using TerrafirmaRedux.Projectiles.Ranged.Boomerangs;
using System.Collections.Generic;
using Terraria.ID;

namespace TerrafirmaRedux.Projectiles.Summons
{
    internal class MechanicsPocketSentryLightning : ModProjectile
    {

        public override string Texture => "TerrafirmaRedux/Projectiles/Summons/MechanicsPocketSentry";

        NPC targetnpc = null;
        Vector2 origpos = Vector2.Zero;
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.height = 4;
            Projectile.width = 4;
            Projectile.DamageType = DamageClass.Summon;

            Projectile.tileCollide = true;
            Projectile.timeLeft = 3000;
            Projectile.extraUpdates = 100;
            Projectile.penetrate = 1;
            Projectile.Opacity = 0f;
        }
        public override void AI()
        {
            targetnpc = Main.npc[(int)Projectile.ai[1]];
            if (Projectile.ai[0] % 30 == 0 && targetnpc != null) 
            {
                float minimalise = 50f * Math.Clamp(Projectile.ai[0] / 100f, 2f, 10f);
                Projectile.velocity = Projectile.Center.DirectionTo(targetnpc.Center).RotatedByRandom(Projectile.Center.Distance(targetnpc.Center) / minimalise); 
            }

            if (Projectile.ai[0] % 3 == 0 && !Projectile.Hitbox.Intersects(targetnpc.Hitbox))
            {
                Dust newdust = Dust.NewDustPerfect(Projectile.Center, DustID.TreasureSparkle, Vector2.Zero, 0, Color.White, 1f);
                newdust.noGravity = true;
                Lighting.AddLight(Projectile.Center, new Vector3(0.8f, 0.85f, 0.4f));
            }
            if (!targetnpc.active)
                Projectile.Kill();
            Projectile.ai[0]++;

        }
    }
}
