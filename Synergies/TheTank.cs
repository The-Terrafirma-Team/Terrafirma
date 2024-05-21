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
    public class TheTank : AccessorySynergy
    {
        public override List<int> SynergyAccessories => new List<int> { ModContent.ItemType<HeartContainer>() };
    }

    internal class TheTankItem : ModPlayer
    {
        public override void UpdateEquips()
        {
            if (Player.GetModPlayer<AccessorySynergyPlayer>().ActivatedSynergies.ContainsSynergy(new TheTank())) Player.statLifeMax2 += 150;

            base.UpdateEquips();
        }
    }
}
