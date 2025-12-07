using System;
using Terrafirma.Common.Mechanics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Content.Items.DebugTools
{
    public class SkillResetter : ModItem
    {
        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = Item.useAnimation = 20;
            Item.width = Item.height = 16;
            Item.UseSound = SoundID.Item4;
            Item.rare = ItemRarityID.Expert;
        }
        public override bool? UseItem(Player player)
        {
            if (player.ItemAnimationJustStarted)
            {
                player.GetModPlayer<SkillsPlayer>().EquippedSkills = [null, null, null, null];
            }
            return base.UseItem(player);
        }
    }
    public class SkillUnlearner : ModItem
    {
        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = Item.useAnimation = 20;
            Item.width = Item.height = 16;
            Item.UseSound = SoundID.Item4;
            Item.rare = ItemRarityID.Expert;
        }
        public override bool? UseItem(Player player)
        {
            player.GetModPlayer<SkillsPlayer>().EquippedSkills = [null, null, null, null];
            if (player.ItemAnimationJustStarted && Main.myPlayer == player.whoAmI)
            {
                Array.Fill(SkillsPlayer.RememberedSkillIDs, false);
            }
            return base.UseItem(player);
        }
    }
    public class SkillLearner : ModItem
    {
        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = Item.useAnimation = 20;
            Item.width = Item.height = 16;
            Item.UseSound = SoundID.Item4;
            Item.rare = ItemRarityID.Expert;
        }
        public override bool? UseItem(Player player)
        {
            if (player.ItemAnimationJustStarted && Main.myPlayer == player.whoAmI)
            {
                Array.Fill(SkillsPlayer.RememberedSkillIDs, true);
            }
            return base.UseItem(player);
        }
    }
    public class SkillSlotChanger : ModItem
    {
        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = Item.useAnimation = 20;
            Item.width = Item.height = 16;
            Item.UseSound = SoundID.Item4;
            Item.rare = ItemRarityID.Expert;
        }
        public override bool? UseItem(Player player)
        {
            if (player.ItemAnimationJustStarted)
            {
                SkillsPlayer p = player.GetModPlayer<SkillsPlayer>();
                p.MaxSkills++;
                if(p.MaxSkills > 4)
                {
                    p.MaxSkills = 2;
                }
            }
            return base.UseItem(player);
        }
    }
}
