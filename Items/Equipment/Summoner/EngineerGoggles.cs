using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Equipment.Summoner
{
    [AutoloadEquip(EquipType.Face)]
    public class EngineerGoggles : ModItem
    {
        public override void SetStaticDefaults()
        {
            ArmorIDs.Face.Sets.DrawInFaceFlowerLayer[Item.faceSlot] = true;
        }
        public override void SetDefaults()
        {
            Item.DefaultToAccessory(32, 28);
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = ItemRarityID.Green;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<PlayerStats>().SentryRangeMultiplier += 0.2f;
        }
    }
}
