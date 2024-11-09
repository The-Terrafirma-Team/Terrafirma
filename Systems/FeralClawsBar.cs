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
    public class FeralClawsBar : UIState
    {
        public static Asset<Texture2D> bar;
        public override void Draw(SpriteBatch spriteBatch)
        {
            PlayerStats stats = Main.LocalPlayer.PlayerStats();

            if (stats.FeralChargeMax == 0 || ModContent.GetInstance<ClientConfig>().MeleeDamageMultiplier != 0)
                return;

            int segments = (int)stats.FeralChargeMax;
            Vector2 pos = new Vector2((Main.screenWidth / 2f) + stats.FeralChargeMax, (Main.screenHeight / 2f) + 40);


            Rectangle midSegment = new Rectangle(8, 0, 6, 18);
            Rectangle fill = new Rectangle(24, 0, 6, 18);
            Rectangle leftSegment = new Rectangle(0, 0, 6, 18);
            Rectangle rightSegment = new Rectangle(16, 0, 6, 18);

            for (int i = -1; i <= segments; i++)
            {
                float xOffset = 0f;
                Rectangle drawRect = midSegment;
                if (i == -1)
                {
                    drawRect = leftSegment;
                }
                else if (i == segments)
                {
                    xOffset += -2;
                    drawRect = rightSegment;
                }
                spriteBatch.Draw(bar.Value, pos + new Vector2(xOffset + (midSegment.Width * (i - (segments / 2f)) * 0.94f), 0), drawRect, Color.White, 0, new Vector2(drawRect.Width / 2, 0), 1, SpriteEffects.None, 0);
                if (i > -1 && i < segments && (!Main.LocalPlayer.ItemAnimationActive || (ItemSets.AltFireDoesNotConsumeFeralCharge[Main.LocalPlayer.HeldItem.type] && Main.LocalPlayer.altFunctionUse == 2)))
                {
                    spriteBatch.Draw(bar.Value, pos + new Vector2(xOffset + (midSegment.Width * (i - (segments / 2f)) * 0.94f), 0), new Rectangle(fill.X, fill.Y, (int)MathHelper.Clamp((stats.FeralCharge - i) * fill.Width, 0, fill.Width), fill.Height), Color.White, 0, new Vector2(drawRect.Width / 2, 0), 1, SpriteEffects.None, 0);
                }
            }

            //Rectangle midSegment = new Rectangle(8, 0, 12, 24);
            //Rectangle fill = new Rectangle(30, 0, 12, 24);
            //Rectangle leftSegment = new Rectangle(0, 0, 6, 24);
            //Rectangle rightSegment = new Rectangle(22, 0, 6, 24);

            //for (int i = -1; i <= segments; i++)
            //{
            //    float xOffset = 0f;
            //    Rectangle drawRect = midSegment;
            //    if (i == -1)
            //    {
            //        xOffset += 3;
            //        drawRect = leftSegment;
            //    }
            //    else if (i == segments)
            //    {
            //        xOffset += -3;
            //        drawRect = rightSegment;
            //    }
            //    spriteBatch.Draw(bar.Value, pos + new Vector2(xOffset + (midSegment.Width * (i - (segments/2f)) * 0.97f),0), drawRect, Color.White, 0, new Vector2(drawRect.Width / 2, 0), 1, SpriteEffects.None, 0);
            //    if(i > -1 && i < segments && (!Main.LocalPlayer.ItemAnimationActive || (ItemSets.AltFireDoesNotConsumeFeralCharge[Main.LocalPlayer.HeldItem.type] && Main.LocalPlayer.altFunctionUse == 2)))
            //    {
            //        spriteBatch.Draw(bar.Value, pos + new Vector2(xOffset + (midSegment.Width * (i - (segments / 2f)) * 0.97f), 0), new Rectangle(fill.X,fill.Y,(int)MathHelper.Clamp((stats.FeralCharge - i) * fill.Width,0,fill.Width),fill.Height), Color.White, 0, new Vector2(drawRect.Width / 2, 0), 1, SpriteEffects.None, 0);
            //    }
            //}
        }
    }

    [Autoload(Side = ModSide.Client)]
    public class FeralClawsBarSystem : ModSystem
    {
        private UserInterface feralClawsBarInterface;
        internal FeralClawsBar feralClawsBar;
        public override void Load()
        {
            FeralClawsBar.bar = Mod.Assets.Request<Texture2D>("Assets/FeralBar");
            feralClawsBar = new();
            feralClawsBarInterface = new();
            feralClawsBarInterface.SetState(feralClawsBar);
        }
        //public override void UpdateUI(GameTime gameTime)
        //{
        //    feralClawsBarInterface?.Update(gameTime);
        //}
        //public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        //{
        //    int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
        //    if (mouseTextIndex != -1)
        //    {
        //        layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
        //            "Terrafirma: Feral Claws Bar",
        //            delegate
        //            {
        //                feralClawsBarInterface?.Draw(Main.spriteBatch, new GameTime());
        //                return true;
        //            },
        //            InterfaceScaleType.UI)
        //        );
        //    }
        //}
    }
}
