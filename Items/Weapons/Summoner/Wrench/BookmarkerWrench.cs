using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Global.Items;
using Terrafirma.Global.Players;
using Terrafirma.Particles.LegacyParticles;
using Terrafirma.Projectiles.Summon.Sentry.PreHardmode;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Weapons.Summoner.Wrench
{
    public class BookmarkerWrench : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToWrench(14, 25);
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.buyPrice(0, 4, 0);
        }   
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                if (Main.projectile[i].type != ModContent.ProjectileType<CrimsonHeartSentry>() && hitbox.Intersects(Main.projectile[i].Hitbox))
                {
                    player.WrenchHitSentry(hitbox, SentryBuffID.SentryPriority, 30);
                }
            }
        }
        public override bool MeleePrefix()
        {
            return true;
        }
    }
}
