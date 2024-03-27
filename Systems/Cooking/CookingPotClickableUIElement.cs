using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Terrafirma.Particles;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace Terrafirma.Systems.Cooking
{
    [Autoload(Side = ModSide.Client)]
    internal class CookingPotClickableUIElement : UIElement
    {
        Texture2D ClickableTex = ModContent.Request<Texture2D>("Terrafirma/Systems/Cooking/CookingPotMinigameClickables", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
        int Value = 1;
        int TexFrameX = 0;
        int TexFrameY = 2;
        int Timer = 0;
        bool Active = false;

        public bool Clicked = false;
        public override void OnInitialize()
        {
            TexFrameX = Main.rand.Next(ClickableTex.Width / 38);
            Width.Pixels = 38f;
            Height.Pixels = 38f;
            Active = true;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Active && Timer < 70)
            {
                spriteBatch.Draw(ClickableTex,
                new Vector2(Main.ScreenSize.X * HAlign + Left.Pixels, Main.ScreenSize.Y * VAlign + Top.Pixels),
                new Rectangle(38 * TexFrameX, 38 * TexFrameY, 38, 38),
                Color.White,
                0,
                new Vector2(16, 16),
                1f,
                SpriteEffects.None,
                0f);
            }
            base.Draw(spriteBatch);
        }

        public override void LeftClick(UIMouseEvent evt)
        {
            if (Timer < 50)
            {
                Timer = 50;
                Clicked = true;
                SoundEngine.PlaySound(SoundID.MenuTick);
                
            }

            base.LeftClick(evt);
        }

        public override void Update(GameTime gameTime)
        {
            if (Active)
            {
                Timer++;
                if (Timer % 10 == 0 && TexFrameY > 0 && Timer < 40)
                {
                    TexFrameY--;
                }
                if (Timer % 10 == 0 && Timer >= 40 && TexFrameY < 2)
                {
                    TexFrameY++;
                }
            }
            base.Update(gameTime);
        }

        public override void OnDeactivate()
        {
            Active = false;
            base.OnDeactivate();
        }
    }
}
