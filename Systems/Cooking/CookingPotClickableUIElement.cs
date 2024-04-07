using Microsoft.Xna.Framework;
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
        bool Clicked2 = false;
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
            if (Clicked2 && Timer == 52)
            {
                for (int i = 0; i < 6; i++)
                {
                    BigSparkle p = new BigSparkle();
                    p.Scale = 2;
                    p.fadeInTime = 8;
                    p.Rotation = Main.rand.NextFloat(-MathHelper.PiOver2, MathHelper.PiOver2);
                    p.smallestSize = 0.05f;
                    ParticleSystem.AddParticle(p, new Vector2(Main.ScreenSize.X * HAlign + Left.Pixels, Main.ScreenSize.Y * VAlign + Top.Pixels), new Vector2(3f, 0).RotatedBy(MathHelper.Pi * 0.33f * i), new Color(213, 123, 11, 0) * 0.2f, true);
                }    
            }
            ParticleSystem.DrawUIParticle(Vector2.Zero);
            base.Draw(spriteBatch);
        }

        public override void LeftClick(UIMouseEvent evt)
        {
            if (Timer < 50)
            {
                Timer = 50;
                Clicked = true;
                Clicked2 = true;
                SoundEngine.PlaySound(SoundID.Item29 with { Volume = 0.1f });
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
