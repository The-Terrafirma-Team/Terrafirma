using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Data;
using Terrafirma.Items.Equipment.Summoner;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Common.Items
{
    public class SwarmGlobal : GlobalProjectile
    {
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (source is EntitySource_ItemUse parent && parent.Entity is Player player && player.GetModPlayer<MahoganyShamanPlayer>().active && ItemSets.isSwarmSummonItem[player.HeldItem.type])
            {
                if (Main.rand.NextBool(3))
                {
                    projectile.scale += 0.1f;
                    projectile.damage += 4;
                    projectile.knockBack += 0.2f;

                    for (int i = 0; i < 12; i++)
                    {
                        Dust d = Dust.NewDustPerfect(projectile.Center, DustID.GemEmerald, Main.rand.NextVector2Circular(6, 6));
                        d.noGravity = true;
                        d.frame.Y = 10;
                        d.fadeIn = 1.4f;
                    }
                }
            }
        }
    }
}
