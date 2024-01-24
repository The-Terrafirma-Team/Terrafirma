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

        public override string Texture => "C:TerrafirmaRedux/Projectiles/Summons/MechanicsPocketSentry";

        NPC targetnpc = null;
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.height = 4;
            Projectile.width = 4;
            Projectile.DamageType = DamageClass.Summon;

            Projectile.tileCollide = true;
            Projectile.timeLeft = 1000;
            Projectile.extraUpdates = 10;
            Projectile.penetrate = -1;
            Projectile.hide = true;
        }
        public override void AI()
        {

            if (Projectile.ai[0] == 0) targetnpc = Utils.FindClosestNPC(400f, Projectile.Center);
            if (Projectile.ai[0] % 10 == 0) Projectile.velocity = Main.rand.NextVector2Circular(5, 5);

            Projectile.ai[0]++;

        }
    }
}
