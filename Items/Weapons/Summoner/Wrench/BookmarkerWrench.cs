using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Global.Items;
using Terraria;
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
            player.WrenchHitSentry(hitbox, SentryBuffID.SentryPriority, 30);
        }

        public override bool MeleePrefix()
        {
            return true;
        }
    }
}
