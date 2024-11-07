using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Common;
using Terrafirma.Common.Players;
using Terrafirma.Data;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace Terrafirma.Systems
{
    public class TensionBar : UIState
    {
        public static Asset<Texture2D> bar;
        public override void Draw(SpriteBatch spriteBatch)
        {
            PlayerStats stats = Main.LocalPlayer.PlayerStats();
            float multiply = stats.Tension / (stats.TensionMax + (float)stats.TensionMax2);
            Vector2 vector = new Vector2(Main.screenWidth - 350, 68);
            spriteBatch.Draw(bar.Value, vector, bar.Frame(2, 1, 0), Color.White, 0, new Vector2(0, bar.Height()), 1, SpriteEffects.None, 0);

            if(multiply != 0)
                spriteBatch.Draw(bar.Value, vector, new Rectangle(bar.Width() / 2, 0, bar.Height(), 8 + (int)((bar.Height() - 6) * multiply)), (Color.White * 0.7f) with { A = 255}, 0, new Vector2(0, bar.Height()), 1, SpriteEffects.None, 0); ;

            spriteBatch.Draw(bar.Value, vector, new Rectangle(bar.Width() / 2, 0, bar.Height(),6 + (int)((bar.Height() - 6) * multiply)), Color.White, 0, new Vector2(0, bar.Height()), 1, SpriteEffects.None, 0);;
        
        }
    }

    [Autoload(Side = ModSide.Client)]
    public class TensionBarSystem : ModSystem
    {
        private UserInterface tensionBarInterface;
        internal TensionBar tensionBar;
        public override void Load()
        {
            TensionBar.bar = Mod.Assets.Request<Texture2D>("Assets/TensionBar");
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
