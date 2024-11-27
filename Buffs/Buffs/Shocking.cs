using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terrafirma.Common;
using Microsoft.Xna.Framework;
using Terrafirma.Projectiles.Summon.Sentry.PreHardmode;

namespace Terrafirma.Buffs.Buffs
{
    public class Shocking : ModBuff
    { 
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<ShockingPlayer>().active = true;
            player.GetModPlayer<ShockingPlayer>().index = buffIndex;
        }
    }
    public class ShockingPlayer : ModPlayer
    {
        public bool active;
        public int index = 0;
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if(hit.Crit)
                Projectile.NewProjectile(Player.GetSource_Buff(index), target.Center, Vector2.Zero,ModContent.ProjectileType<ShockPotionLightning>(),damageDone / 3,0,Player.whoAmI,0,target.whoAmI);
        }
    }
}

