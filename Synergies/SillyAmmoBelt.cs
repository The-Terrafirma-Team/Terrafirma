using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Common.Items;
using Terrafirma.Items.Equipment.Healing;
using Terrafirma.Systems.AccessorySynergy;
using Terraria;
using Terraria.ModLoader;

namespace Terrafirma.Synergies
{
    public class SillyAmmoBelt : AccessorySynergy
    {
        public override List<int> SynergyAccessories => new List<int> { ModContent.ItemType<HeartContainer>() };
    }

    internal class SillyAmmoBeltItem : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (player.GetModPlayer<AccessorySynergyPlayer>().ActivatedSynergies.ContainsSynergy(new SillyAmmoBelt()))
            {

            }

            base.ModifyShootStats(item, player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }
    }
}
