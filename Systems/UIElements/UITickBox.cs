using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace Terrafirma.Systems.UIElements
{

    enum TickBoxState : byte
    {
        Unchecked = 0,
        Checked = 1,
        FalseChecked = 2
    }

    [Autoload(Side = ModSide.Client)]
    internal class UITickBox : UIElement
    {
        Texture2D Tex = ModContent.Request<Texture2D>("Terrafirma/Systems/UIElements/TickBoxUIElement", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
        public TickBoxState State = TickBoxState.Unchecked;
        public bool Sound = true;
        public override void OnInitialize()
        {
            Width.Pixels = 32;
            Height.Pixels = 32;
            
            base.OnInitialize();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            byte FrameX = (byte)State;
            byte FrameY = (byte)(IsMouseHovering ? 1 : 0);
            spriteBatch.Draw(Tex,
                new Vector2(Main.ScreenSize.X * HAlign + Left.Pixels, Main.ScreenSize.Y * VAlign + Top.Pixels),
                new Rectangle(34 * FrameX, 34 * FrameY, 32, 32),
                Color.White,
                0,
                new Vector2(16, 16),
                1f,
                SpriteEffects.None,
                0f);

            base.Draw(spriteBatch);
        }

        public override void LeftClick(UIMouseEvent evt)
        {
            if (Sound) SoundEngine.PlaySound(SoundID.MenuTick);

            base.LeftClick(evt);
        }
    }
}
