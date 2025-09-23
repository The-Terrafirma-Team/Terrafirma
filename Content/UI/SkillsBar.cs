using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Graphics;
using System.Collections.Generic;
using Terrafirma.Common;
using Terrafirma.Common.Mechanics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.UI;

namespace Terrafirma.Content.UI
{
    public class SkillsBar : UIState
    {
        internal static Asset<Texture2D> BarTex;
        public override void Draw(SpriteBatch spriteBatch)
        {
            SkillsPlayer player = Main.LocalPlayer.GetModPlayer<SkillsPlayer>();
            Vector2 position = new Vector2(Main.screenWidth, 32) * 0.5f;
            position.X -= 24 * (player.MaxSkills - 2);
            Rectangle midRect = new Rectangle(62, 0, 56, 86);
            for (int i = 0; i < player.MaxSkills; i++)
            {
                Vector2 drawPos = position;
                Rectangle rect = new Rectangle(62, 0, 56, 86);
                if (i == 0)
                {
                    drawPos.X -= 4;
                    rect = new Rectangle(0, 0, 60, 86);
                }
                else if (i == player.MaxSkills - 1)
                    rect = new Rectangle(120, 0, 66, 86);
                spriteBatch.Draw(BarTex.Value, drawPos + new Vector2(midRect.Width * i, 0), rect, Color.White, 0f, new Vector2(28, 0), 1.01f, SpriteEffects.None, 0);
                if (player.Skills[i] != null)
                {
                    if (i == 0)
                    {
                        drawPos.X += 4;
                    }
                    int textOffset = 17;
                    if (player.Skills[i].ManaCost > 0)
                    {
                        for (int z = 0; z < 4; z++)
                        {
                            spriteBatch.DrawString(FontAssets.MouseText.Value, $"{player.Skills[i].ManaCost}", drawPos + new Vector2(midRect.Width * i - textOffset, 2) + new Vector2(1.6f, 0).RotatedBy(z * MathHelper.PiOver2), Color.Black, 0, Vector2.Zero, 0.8f, SpriteEffects.None, 0);
                        }
                        spriteBatch.DrawString(FontAssets.MouseText.Value, $"{player.Skills[i].ManaCost}", drawPos + new Vector2(midRect.Width * i - textOffset, 2), new Color(100, 100, 255), 0, Vector2.Zero, 0.8f, SpriteEffects.None, 0);

                    }
                    if (player.Skills[i].TensionCost > 0)
                    {
                        if (player.Skills[i].ManaCost > 0)
                            textOffset = 0;
                        for (int z = 0; z < 4; z++)
                        {
                            spriteBatch.DrawString(FontAssets.MouseText.Value, $"{player.Skills[i].TensionCost}", drawPos + new Vector2(midRect.Width * i - textOffset, 2) + new Vector2(1.6f, 0).RotatedBy(z * MathHelper.PiOver2), Color.Black, 0, Vector2.Zero, 0.8f, SpriteEffects.None, 0);
                        }
                        spriteBatch.DrawString(FontAssets.MouseText.Value, $"{player.Skills[i].TensionCost}", drawPos + new Vector2(midRect.Width * i - textOffset, 2), new Color(64, 255, 160), 0, Vector2.Zero, 0.8f, SpriteEffects.None, 0);

                    }
                    spriteBatch.Draw(SkillsSystem.SkillTextures[player.Skills[i].ID].Value, drawPos + new Vector2(midRect.Width * i, 0), new Rectangle(0, 0, 48, 48), player.Skills[i].Cooldown > 0 ? Color.DarkGray : Color.White, 0f, new Vector2(24, -25), 1.0f, SpriteEffects.None, 0);
                    int percent = (int)(48 * player.Skills[i].Cooldown);
                    percent /= 2;
                    percent *= 2;
                    // this makes it fill from the top instead
                    //spriteBatch.Draw(SkillsSystem.SkillTextures[player.Skills[i].ID()].Value, drawPos + new Vector2(midRect.Width * i, 48 - percent), new Rectangle(0, 50 + (48 - percent), 48, percent), Color.White, 0f, new Vector2(24, -25), 1.0f, SpriteEffects.None, 0);

                    if (player.Skills[i].Cooldown > 0)
                    {
                        spriteBatch.Draw(SkillsSystem.SkillTextures[player.Skills[i].ID].Value, drawPos + new Vector2(midRect.Width * i, 0), new Rectangle(0, 0, 48, percent + 2), Color.White with { A = 128 }, 0f, new Vector2(24, -25), 1.0f, SpriteEffects.None, 0);
                        spriteBatch.Draw(SkillsSystem.SkillTextures[player.Skills[i].ID].Value, drawPos + new Vector2(midRect.Width * i, 0), new Rectangle(0, 50, 48, percent), new Color(64, 64, 64), 0f, new Vector2(24, -25), 1.0f, SpriteEffects.None, 0);
                    }
                    else
                    {
                        spriteBatch.Draw(BarTex.Value, drawPos + new Vector2(midRect.Width * i, 0), new Rectangle(188, 0, 48, 48), player.Skills[i].RechargeFlashColor with { A = 0 } * (SkillsPlayer.CooldownFlashLight[i] / 255f) * 0.75f, 0f, new Vector2(24, -25), 1.0f, SpriteEffects.None, 0);
                    }
                }
            }
        }
    }

    [Autoload(Side = ModSide.Client)]
    public class SkillsBarystem : ModSystem
    {
        private UserInterface skillsBarInterface;
        internal SkillsBar skillsBar;
        public override void Load()
        {
            SkillsBar.BarTex = Mod.Assets.Request<Texture2D>("Assets/UI/SkillsBar");
            skillsBar = new();
            skillsBarInterface = new();
            skillsBarInterface.SetState(skillsBar);
        }
        public override void UpdateUI(GameTime gameTime)
        {
            skillsBarInterface?.Update(gameTime);
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            if (ModContent.GetInstance<ServerConfig>().CombatReworkEnabled)
            {
                int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
                if (mouseTextIndex != -1)
                {
                    layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                        "Terrafirma: Skills Bar",
                        delegate
                        {
                            skillsBarInterface?.Draw(Main.spriteBatch, new GameTime());
                            return true;
                        },
                        InterfaceScaleType.UI)
                    );
                }
            }
        }
    }
}
