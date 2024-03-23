using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terrafirma.Global.Structs;
using Terraria.GameContent.UI.Elements;
using Terrafirma.Global;
using System.Collections.Generic;
using System;
using System.Linq;
using Terraria.DataStructures;
using Microsoft.CodeAnalysis.Text;

namespace Terrafirma.Systems.Cooking
{
    [Autoload(Side = ModSide.Client)]
    internal class CookingPotUI : UIState
    {
        Item[] PotSlots = new Item[] { new Item(0), new Item(0), new Item(0) };
        UIItemSlot Itemslot = new UIItemSlot(new Item[] { }, 0, 3);
        UIItemSlot[] ItemslotArray = new UIItemSlot[] {};
        Vector2 tileposition = Vector2.Zero;
        
        public void Flush()
        {
            for (int i = 0; i < PotSlots.Length; i++)
            {
                if (PotSlots[i] != new Item(0))
                {
                    Main.LocalPlayer.QuickSpawnItemDirect(Main.LocalPlayer.GetSource_FromThis(), PotSlots[i], PotSlots[i].stack);
                    PotSlots[i] = new Item(0);
                }
            }

            RemoveAllChildren();
        }

        /// <summary>
        /// Creates Spell UI
        /// </summary>
        public void Create(Vector2 tilepos)
        {

            Flush();
            ItemslotArray = new UIItemSlot[] { };

            tileposition = tilepos - new Vector2(40, 60) * Main.UIScale;

            for (int i = 0; i < 3; i++)
            {
                Itemslot = new UIItemSlot(PotSlots, i, 3);
                Itemslot.Width.Pixels = 37;
                Itemslot.Height.Pixels = 37;
                Itemslot.Top.Pixels = (tileposition.Y - Main.screenPosition.Y) / Main.UIScale;
                Itemslot.Left.Pixels = (tileposition.X + Itemslot.Width.Pixels * i - Main.screenPosition.X) / Main.UIScale;

                Append(Itemslot);
                ItemslotArray = ItemslotArray.Append(Itemslot).ToArray();
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (tileposition != Vector2.Zero)
            {
                for (int i = 0; i < ItemslotArray.Length; i++)
                {
                    ItemslotArray[i].Top.Pixels = (tileposition.Y - Main.screenPosition.Y) / Main.UIScale;
                    ItemslotArray[i].Left.Pixels = (tileposition.X + ItemslotArray[i].Width.Pixels * i - Main.screenPosition.X) / Main.UIScale;
                }
            }                
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

    }
}
