using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.UI;
using Terrafirma.Common;
using Terrafirma.Utilities;

namespace Terrafirma.Content.UI
{
    public class TensionBar : UIState
    {
        internal static Asset<Texture2D> BarTex;
        public override void Draw(SpriteBatch spriteBatch)
        {
            PlayerStats stats = Main.LocalPlayer.PlayerStats();
            float multiply = stats.Tension / (float)stats.TensionMax;
            Vector2 vector = new(Main.screenWidth - 350, 68);
            spriteBatch.Draw(BarTex.Value, vector, BarTex.Frame(2, 1, 0), Color.White, 0, new Vector2(0, BarTex.Height()), 1, SpriteEffects.None, 0);

            if (multiply != 0)
                spriteBatch.Draw(BarTex.Value, vector, new Rectangle(BarTex.Width() / 2, 0, BarTex.Height(), 8 + (int)((BarTex.Height() - 6) * multiply)), (Color.White * 0.7f) with { A = 255 }, 0, new Vector2(0, BarTex.Height()), 1, SpriteEffects.None, 0); ;

            spriteBatch.Draw(BarTex.Value, vector, new Rectangle(BarTex.Width() / 2, 0, BarTex.Height(), 6 + (int)((BarTex.Height() - 6) * multiply)), Color.White, 0, new Vector2(0, BarTex.Height()), 1, SpriteEffects.None, 0); ;
            
            for(int i = 0; i < 4; i++)
            {
                spriteBatch.DrawString(FontAssets.MouseText.Value, $"{stats.Tension}", vector - new Vector2(-12, 32) + new Vector2(2,0).RotatedBy(i * MathHelper.PiOver2), Color.Black);
            }
            spriteBatch.DrawString(FontAssets.MouseText.Value, $"{stats.Tension}",vector - new Vector2(-12,32),Color.White);
        }
    }

    [Autoload(Side = ModSide.Client)]
    public class TensionBarSystem : ModSystem
    {
        private UserInterface tensionBarInterface;
        internal TensionBar tensionBar;
        public override void Load()
        {
            TensionBar.BarTex = Mod.Assets.Request<Texture2D>("Assets/UI/TensionBar");
            tensionBar = new();
            tensionBarInterface = new();
            tensionBarInterface.SetState(tensionBar);
        }
        public override void UpdateUI(GameTime gameTime)
        {
            tensionBarInterface?.Update(gameTime);
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "Terrafirma: Tension Bar",
                    delegate
                    {
                        tensionBarInterface?.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
