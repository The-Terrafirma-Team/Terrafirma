using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terrafirma.Projectiles.Ranged.Boomerangs;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.DataStructures;
using Terrafirma.Particles;

namespace Terrafirma.Projectiles.Summon.Sentry.PreHardmode
{
    internal class CrimsonHeartSentryInvisProj : ModProjectile
    {
        public override string Texture => "Terrafirma/Projectiles/Summon/Sentry/PreHardmode/CrimsonHeartSentryHeart";
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.height = 6;
            Projectile.width = 6;
            Projectile.DamageType = DamageClass.Summon;

            Projectile.tileCollide = false;
            Projectile.timeLeft = Projectile.SentryLifeTime;
            Projectile.penetrate = 0;
            Projectile.hide = true;
        }

    }
}
