using Microsoft.Xna.Framework;
using Terrafirma.Common.Interfaces;
using Terrafirma.Common.Players;
using Terrafirma.ManaTypes;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Consumable.Mana
{
    public class TestManaTypeConsumable : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 30;

            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item3;

            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(copper: 20);
        }


        public override bool? UseItem(Player player)
        {
            if (player.ItemAnimationJustStarted)
            {
                player.AddManaType(new DawnMana(), 0.5f, 1f);
                player.AddManaType(new NatureMana(), 0f, 0.5f);
                //player.ResetManaTypes();
            }
            return base.UseItem(player);
        }

    }
}
