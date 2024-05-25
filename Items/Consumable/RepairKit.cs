using Microsoft.Xna.Framework;
using Terrafirma.Common.Interfaces;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Consumable
{
    public class RepairKit : ModItem, IUseOnItemInInventoryItem
    {
        public override void SetDefaults()
        {
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.width = 11;
            Item.height = 16;
            Item.UseSound = SoundID.Item4;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 0, 10, 0);
            Item.maxStack = 9999;
            Item.consumable = true;
        }
        
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 99;
        }
        public bool canBeUsedOnThisItem(Player player, Item item, Item itemBeingUsedOn, int context)
        {
            return itemBeingUsedOn.CanHavePrefixes() && itemBeingUsedOn.rare < ContentSamples.ItemsByType[itemBeingUsedOn.type].rare;
        }
        public void useOnItem(Player player, Item item, Item itemBeingUsedOn, int context)
        {
            itemBeingUsedOn.SetDefaults(itemBeingUsedOn.type);
            itemBeingUsedOn.Prefix(-1);
            SoundEngine.PlaySound(SoundID.Item37);
            item.stack--;
            PopupText.NewText(new AdvancedPopupRequest() { Text = itemBeingUsedOn.HoverName, DurationInFrames = 60, Color = ItemRarity.GetColor(itemBeingUsedOn.rare), }, player.Center - new Vector2(0, 40));
        }
    }
}
