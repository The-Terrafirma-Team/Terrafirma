using Microsoft.Xna.Framework;
using Terrafirma.Common.Interfaces;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Consumable
{
    public class CrystalizedSpirit : ModItem
    {
        public override void SetDefaults()
        {
            Item.consumable = true;
            Item.rare = ItemRarityID.Green;
            Item.maxStack = Item.CommonMaxStack;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.Item29;
            Item.value = Item.sellPrice(0,2);
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.Size = new Vector2(16);
        }
        
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 99;
        }
        public override bool CanUseItem(Player player)
        {
            return player.PlayerStats().SpiritCrystals < 5;
        }
        public override bool? UseItem(Player player)
        {
            player.PlayerStats().SpiritCrystals++;
            player.PlayerStats().TensionMax += 20;
            player.PlayerStats().Tension += 20;
            return true;
        }
        //public override bool ConsumeItem(Player player)
        //{
        //    Main.NewText(player.PlayerStats().SpiritCrystals);
        //    return player.PlayerStats().SpiritCrystals < 5;
        //}
        //public override void OnConsumeItem(Player player)
        //{
        //    player.PlayerStats().SpiritCrystals++;
        //    player.PlayerStats().TensionMax += 20;
        //    player.PlayerStats().Tension += 20;
        //}
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(1f, 1f, 1f, 0.5f);
        }
    }
}
