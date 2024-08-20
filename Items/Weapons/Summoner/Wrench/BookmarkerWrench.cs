using Microsoft.Xna.Framework;
using Terrafirma.Common;
using Terrafirma.Common.Templates;
using Terrafirma.Projectiles.Summon.Sentry.PreHardmode;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Weapons.Summoner.Wrench
{
    public class BookmarkerWrench : WrenchItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToWrench(14, 25);
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.buyPrice(0, 4, 0);
        }   
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            foreach(Projectile proj in Main.ActiveProjectiles)
            {
                if(hitbox.Intersects(proj.Hitbox) && proj.sentry)
                {
                    proj.GetGlobalProjectile<SentryStats>().Priority = true;
                    TFUtils.UpdateSentryPriority(proj);
                }
            }
        }
    }
}
