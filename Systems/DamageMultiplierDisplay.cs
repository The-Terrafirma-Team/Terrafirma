using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Common;
using Terrafirma.Common.Players;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace Terrafirma.Systems
{
    public class DamageMultiplierDisplay : UIState
    {
        public static Asset<Texture2D> display;
        float TextScale = 1f;
        public override void Draw(SpriteBatch spriteBatch)
        {
            PlayerStats stats = Main.LocalPlayer.PlayerStats();

            if (stats.FeralChargeMax == 0 || ModContent.GetInstance<ClientConfig>().MeleeDamageMultiplier != 1)
                return;

            spriteBatch.Draw(display.Value, new Vector2(Main.screenWidth / 2 + 40, Main.screenHeight / 2 + 40), null, Color.White, 0f, display.Size() / 2, 1f, SpriteEffects.None, 0f);

            //Text
            string StatNumber = Math.Round(stats.FeralCharge+1, 1) == (int)Math.Round(stats.FeralCharge + 1, 1) ? Math.Round(stats.FeralCharge + 1, 1).ToString() + ".0x" : Math.Round(stats.FeralCharge + 1, 1).ToString() + "x";
            TextScale = MathHelper.Lerp(TextScale, 1f, 0.1f) ;
            if (Math.Round(stats.FeralCharge + 1, 1) == (int)Math.Round(stats.FeralCharge + 1, 1) && (int)Math.Round(stats.FeralCharge + 1, 1) != 0) TextScale = 1.2f;
            //Border
            int BorderWidth = 2;
            //Position
            Vector2 TextPos = new Vector2(Main.screenWidth / 2 + 25, Main.screenHeight / 2 + 40);
            Vector2 TextOrigin = new Vector2(-2, 10);

            Color[] MultiplierColors = new Color[] { Color.White, Color.Yellow, Color.Orange, Color.IndianRed, Color.Red, Color.Magenta, Color.Violet, Color.Indigo, Color.Cyan, Color.LimeGreen };
            Color MultiplierColor;
            MultiplierColor = Color.Lerp(MultiplierColors[ Math.Clamp((int)stats.FeralCharge, 0, MultiplierColors.Length - 1)], MultiplierColors[ Math.Clamp((int)stats.FeralCharge + 1,0, MultiplierColors.Length - 1)], stats.FeralCharge - (int)stats.FeralCharge);

            if (Main.LocalPlayer.ItemAnimationActive)
            {
                StatNumber = "1.0x";
                MultiplierColor = MultiplierColors[0];
                TextScale = 1f;
            }

            spriteBatch.DrawString(FontAssets.MouseText.Value, StatNumber, new Vector2(TextPos.X - BorderWidth, TextPos.Y), Color.Black, 0f, TextOrigin, TextScale, SpriteEffects.None, 0f);
            spriteBatch.DrawString(FontAssets.MouseText.Value, StatNumber, new Vector2(TextPos.X + BorderWidth, TextPos.Y), Color.Black, 0f, TextOrigin, TextScale, SpriteEffects.None, 0f);
            spriteBatch.DrawString(FontAssets.MouseText.Value, StatNumber, new Vector2(TextPos.X, TextPos.Y - BorderWidth), Color.Black, 0f, TextOrigin, TextScale, SpriteEffects.None, 0f);
            spriteBatch.DrawString(FontAssets.MouseText.Value, StatNumber, new Vector2(TextPos.X, TextPos.Y + BorderWidth), Color.Black, 0f, TextOrigin, TextScale, SpriteEffects.None, 0f);

            spriteBatch.DrawString(FontAssets.MouseText.Value, StatNumber, TextPos, MultiplierColor, 0f, TextOrigin, TextScale, SpriteEffects.None, 0f);
        }
    }

    [Autoload(Side = ModSide.Client)]
    public class DamageMultiplierDisplaySystem : ModSystem
    {
        private UserInterface multiplierinterface;
        internal DamageMultiplierDisplay multiplierdisplay;
        public override void Load()
        {
            DamageMultiplierDisplay.display = Mod.Assets.Request<Texture2D>("Assets/MultiplierBG");
            multiplierdisplay = new();
            multiplierinterface = new();
            multiplierinterface.SetState(multiplierdisplay);
        }
        public override void UpdateUI(GameTime gameTime)
        {
            multiplierinterface?.Update(gameTime);
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "Terrafirma: Damage Multiplier Display",
                    delegate
                    {
                        multiplierinterface?.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
