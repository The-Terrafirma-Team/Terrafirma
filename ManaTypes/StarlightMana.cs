using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Buffs.Mana;
using Terrafirma.Systems.MageClass.ManaTypes;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Terrafirma.ManaTypes
{
    public class StarlightMana : ManaType
    {
        public override void TickEffect(Player player)
        {
            player.AddBuff(ModContent.BuffType<StarlightManaBuff>(), 2);         
            if (Main.timeForVisualEffects % 4 == 0)
            {
                Dust d = Dust.NewDustDirect(player.position - new Vector2(10), player.width + 20, player.height + 20, DustID.PurpleTorch, -player.velocity.X * 0.5f, -player.velocity.Y * 0.5f, Scale: 2f);
                d.noGravity = true;
            }
        }
    }
}
