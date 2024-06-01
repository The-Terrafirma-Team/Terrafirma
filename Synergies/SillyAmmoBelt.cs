using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Items.Equipment.Healing;
using Terrafirma.Items.Equipment.Ranged;
using Terrafirma.Systems.AccessorySynergy;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Synergies
{
    public class SillyAmmoBelt : AccessorySynergy
    {
        public override List<int> SynergyAccessories => new List<int> { ModContent.ItemType<AmmoCan>(), ModContent.ItemType<DrumMag>()};
    }

    internal class SillyAmmoBeltItem : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.useAmmo == AmmoID.Bullet;
        public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (player.GetModPlayer<AccessorySynergyPlayer>().ActivatedSynergies.ContainsSynergy(new SillyAmmoBelt()))
            {
                //No Chaos Chaos :( - Not Jevil         
                if (Main.rand.NextBool(3))
                {
                    type = new Item(Terrafirma.BulletArray[Main.rand.Next(Terrafirma.BulletArray.Length)]).shoot;
                    damage = Math.Max((int)(item.damage * 0.5f), damage);
                }
            }

            base.ModifyShootStats(item, player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }
    }
}
