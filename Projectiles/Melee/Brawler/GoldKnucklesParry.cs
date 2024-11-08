using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Common.Templates.Melee;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace Terrafirma.Projectiles.Melee.Brawler
{
    public class GoldKnucklesParry : MeleeParry
    {
        public override void AI()
        {
            base.AI();
            player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Quarter, MathHelper.PiOver4 * -3 * player.direction);
            player.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Quarter, MathHelper.PiOver4 * -3 * player.direction);
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.hide = true;
            Projectile.Opacity = 0;
        }
        public override void OnParryProjectile(Projectile p, Player player)
        {
            base.OnParryProjectile(p, player);
            player.AddBuff(ModContent.BuffType<Buffs.Buffs.Parry.GoldKnucklesParry>(),(int)(60 * 4 * player.PlayerStats().ParryBuffDurationMultiplier));
        }
        public override void OnParryNPC(NPC n, Player player)
        {
            base.OnParryNPC(n, player);
            player.AddBuff(ModContent.BuffType<Buffs.Buffs.Parry.GoldKnucklesParry>(), (int)(60 * 4 * player.PlayerStats().ParryBuffDurationMultiplier));
        }
    }
}
