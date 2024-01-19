using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Items.Weapons.Summoner
{
    public class MetalWrench : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToWrench(8, 25);
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            player.WrenchHitSentry(hitbox, 0, 60 * 3);
        }
    }
}
