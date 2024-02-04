using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using Terraria.Audio;
using Terraria.ID;
using Terraria.GameContent.UI.Elements;
using Terrafirma.Global;
using Terraria.UI.Chat;
using Terraria.GameContent;
using ReLogic.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace Terrafirma.Systems.MageClass
{
    [Autoload(Side = ModSide.Client)]
    internal class NPCQuestButtonUIState : UIState
    {
        public bool UIOpen = false;
        internal bool justHovered = false;
        internal float ChatHeight = 0f;

        NPC selectednpc = null;

        UIText Title = null;
        Vector2 TitlePos = Vector2.Zero;

        public void Create(NPC npc)
        {
            selectednpc = npc;

            Title = new UIText("", 1f, false);
            Title.SetText("Quests");
            Title.HAlign = 0.5f;
            Title.MarginLeft = 390f;
            Title.MarginTop = 152f + ChatHeight;
            Title.Top.Set(10, 0);

            TitlePos = Main.ScreenSize.ToVector2() * new Vector2(0.5f, 0f) + new Vector2(190f, 170f + ChatHeight);

            Append(Title);

            UIOpen = true;
        }

        public void Flush() { RemoveAllChildren(); UIOpen = false; }

        public override void LeftClick(UIMouseEvent evt)
        {
            if (Title.IsMouseHovering && selectednpc != null)
            {
                ModContent.GetInstance<NPCQuestButtonSystem>().OpenSelectorUI(selectednpc);
            }

         }

        public override void Draw(SpriteBatch spriteBatch)
        {

            TitlePos = Main.ScreenSize.ToVector2() * new Vector2(0.5f, 0f) + new Vector2(190f, 170f + ChatHeight );
            float mousecolorfloat = (float)(int)Main.mouseTextColor / 255f;
            if (Title.IsMouseHovering)
            {
                ChatManager.DrawColorCodedStringWithShadow(spriteBatch,
                    FontAssets.MouseText.Value,
                    "Quests",
                    TitlePos,
                    new Color((byte)(224f * mousecolorfloat), (byte)(201f * mousecolorfloat), (byte)(92f * mousecolorfloat), Main.mouseTextColor) * 1.1f,
                    Color.Brown,
                    0,
                    new Vector2(25f, 10f),
                    new Vector2(1.15f));
                if (!justHovered)
                {
                    justHovered = true;
                    SoundEngine.PlaySound(SoundID.MenuTick);
                }
            }
            else
            {
                ChatManager.DrawColorCodedStringWithShadow(spriteBatch, 
                    FontAssets.MouseText.Value, 
                    "Quests", 
                    TitlePos,
                    new Color((byte)(224f * mousecolorfloat), (byte)(201f * mousecolorfloat), (byte)(92f * mousecolorfloat), Main.mouseTextColor),
                    Color.Black,
                    0, 
                    new Vector2(25f, 10f), 
                    Vector2.One);
                if (justHovered) 
                { 
                    justHovered = false;
                    SoundEngine.PlaySound(SoundID.MenuTick);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            ChatHeight = (30 * (int)(Main.npcChatText.Length / 54));
            Title.MarginTop = 152f + ChatHeight;
            base.Update(gameTime);
        }

    }
}
