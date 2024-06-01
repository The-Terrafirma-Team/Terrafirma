using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.UI;

namespace Terrafirma.Systems.UIElements
{
    public class UIItemSlot_Terrafirma : UIElement
    {
        private Item[] _itemArray;
        private int _itemIndex;
        private int _itemSlotContext;
        public Color color = Color.White;
        public bool LeftMouse = true;
        public bool RightMouse = true;

        public UIItemSlot_Terrafirma(Item[] itemArray, int itemIndex, int itemSlotContext, Color SlotColor)
        {
            _itemArray = itemArray;
            _itemIndex = itemIndex;
            _itemSlotContext = itemSlotContext;
            color = SlotColor;
            Width.Pixels = 48;
            Height.Pixels = 48;
        }

        private void HandleItemSlotLogic()
        {
            if (base.IsMouseHovering)
            {
                Main.LocalPlayer.mouseInterface = true;
                Item inv = _itemArray[_itemIndex];
                Item fakeitem = new Item(ItemID.None);

                ItemSlot.OverrideHover(ref inv, _itemSlotContext);
                ItemSlot.MouseHover(ref inv, _itemSlotContext);

                if (LeftMouse) ItemSlot.LeftClick(ref inv, _itemSlotContext);
                else ItemSlot.LeftClick(ref fakeitem, _itemSlotContext);
                if (RightMouse) ItemSlot.RightClick(ref inv, _itemSlotContext);
                else ItemSlot.RightClick(ref fakeitem, _itemSlotContext);

                _itemArray[_itemIndex] = inv;
            }
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            HandleItemSlotLogic();
            Item inv = _itemArray[_itemIndex];
            Vector2 position = GetDimensions().Center() + new Vector2(52f, 52f) * -0.5f * Main.inventoryScale;
            ItemSlot.Draw(spriteBatch, ref inv, _itemSlotContext, position, color);
        }
    }
}
