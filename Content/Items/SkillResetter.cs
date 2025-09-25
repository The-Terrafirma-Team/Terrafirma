using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Common.Mechanics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Content.Items
{
    public class SkillResetter : ModItem
    {
        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = Item.useAnimation = 20;
            Item.width = Item.height = 16;
            Item.UseSound = SoundID.Item4;
            Item.rare = ItemRarityID.Blue;
        }
        public override bool? UseItem(Player player)
        {
            if (player.ItemAnimationJustStarted)
            {
                player.GetModPlayer<SkillsPlayer>().EquippedSkills = new Skill[] { null, null, null, null };
            }
            return base.UseItem(player);
        }
    }
}
