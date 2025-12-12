using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using Terrafirma.Common.Mechanics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace Terrafirma.Content.UI
{
    public class SkillsBar : UIState
    {
        internal static Asset<Texture2D> BarTex;
        internal static Asset<Texture2D> SelectTex;
        internal static Asset<Texture2D> menuIconTex;
        public static Rectangle[] skillSlots;
        public static bool[] hoveredSlots;

        TerrafirmaUIImage menuIcon;

        public override void OnInitialize()
        {
            hoveredSlots = new bool[4];

            menuIcon = new TerrafirmaUIImage(menuIconTex);
            menuIcon.frame = new Rectangle(0, 0, 30, 30);
            menuIcon.Left.Pixels = Main.screenWidth * 0.5f + 30;
            menuIcon.Top.Pixels = 10;
            Append(menuIcon);

            base.OnInitialize();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            SkillsPlayer player = Main.LocalPlayer.GetModPlayer<SkillsPlayer>();
            Vector2 position = new Vector2((Main.playerInventory ? 330 : 280) * Main.UIScale, 16);
            position.X -= 24 * (player.MaxSkills - 2);
            Rectangle midRect = new Rectangle(62, 0, 56, 86);

            skillSlots = new Rectangle[player.MaxSkills];

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


                if (i < 4 && player.EquippedSkills[i] != null)
                {
                    if (i == 0)
                    {
                        drawPos.X += 4;
                    }
                    int textOffset = 17;
                    if (player.EquippedSkills[i].ManaCost > 0)
                    {
                        for (int z = 0; z < 4; z++)
                        {
                            spriteBatch.DrawString(FontAssets.MouseText.Value, $"{player.EquippedSkills[i].ManaCost}", drawPos + new Vector2(midRect.Width * i - textOffset, 2) + new Vector2(1.6f, 0).RotatedBy(z * MathHelper.PiOver2), Color.Black, 0, Vector2.Zero, 0.8f, SpriteEffects.None, 0);
                        }
                        spriteBatch.DrawString(FontAssets.MouseText.Value, $"{player.EquippedSkills[i].ManaCost}", drawPos + new Vector2(midRect.Width * i - textOffset, 2), new Color(100, 100, 255), 0, Vector2.Zero, 0.8f, SpriteEffects.None, 0);

                    }
                    if (player.EquippedSkills[i].TensionCost > 0)
                    {
                        if (player.EquippedSkills[i].ManaCost > 0)
                            textOffset = 0;
                        for (int z = 0; z < 4; z++)
                        {
                            spriteBatch.DrawString(FontAssets.MouseText.Value, $"{player.EquippedSkills[i].TensionCost}", drawPos + new Vector2(midRect.Width * i - textOffset, 2) + new Vector2(1.6f, 0).RotatedBy(z * MathHelper.PiOver2), Color.Black, 0, Vector2.Zero, 0.8f, SpriteEffects.None, 0);
                        }
                        spriteBatch.DrawString(FontAssets.MouseText.Value, $"{player.EquippedSkills[i].TensionCost}", drawPos + new Vector2(midRect.Width * i - textOffset, 2), new Color(64, 255, 160), 0, Vector2.Zero, 0.8f, SpriteEffects.None, 0);

                    }
                    spriteBatch.Draw(SkillsSystem.SkillTextures[player.EquippedSkills[i].ID].Value, drawPos + new Vector2(midRect.Width * i, 0), new Rectangle(0, 0, 48, 48), player.EquippedSkills[i].Cooldown > 0 ? Color.DarkGray : Color.White, 0f, new Vector2(24, -25), 1.0f, SpriteEffects.None, 0);
                    int percent = (int)(48 * player.EquippedSkills[i].Cooldown);
                    percent /= 2;
                    percent *= 2;
                    // this makes it fill from the top instead
                    //spriteBatch.Draw(SkillsSystem.SkillTextures[player.Skills[i].ID()].Value, drawPos + new Vector2(midRect.Width * i, 48 - percent), new Rectangle(0, 50 + (48 - percent), 48, percent), Color.White, 0f, new Vector2(24, -25), 1.0f, SpriteEffects.None, 0);

                    if (player.EquippedSkills[i].Cooldown > 0)
                    {
                        spriteBatch.Draw(SkillsSystem.SkillTextures[player.EquippedSkills[i].ID].Value, drawPos + new Vector2(midRect.Width * i, 0), new Rectangle(0, 0, 48, percent + 2), Color.White with { A = 128 }, 0f, new Vector2(24, -25), 1.0f, SpriteEffects.None, 0);
                        spriteBatch.Draw(SkillsSystem.SkillTextures[player.EquippedSkills[i].ID].Value, drawPos + new Vector2(midRect.Width * i, 0), new Rectangle(0, 50, 48, percent), new Color(64, 64, 64), 0f, new Vector2(24, -25), 1.0f, SpriteEffects.None, 0);
                    }
                    else
                    {
                        spriteBatch.Draw(BarTex.Value, drawPos + new Vector2(midRect.Width * i, 0), new Rectangle(188, 0, 48, 48), player.EquippedSkills[i].RechargeFlashColor with { A = 0 } * (SkillsPlayer.CooldownFlashLight[i] / 255f) * 0.75f, 0f, new Vector2(24, -25), 1.0f, SpriteEffects.None, 0);
                    }
                }

                Vector2 slotPos = drawPos + new Vector2(midRect.Width * i, 0);
                if (i == 0) slotPos += new Vector2(4, 25);
                else slotPos += new Vector2(4, 25);

                skillSlots[i] = new Rectangle((int)slotPos.X - 24, (int)slotPos.Y, 48, 48);
                if (i < 4 && hoveredSlots[i]) spriteBatch.Draw(SelectTex.Value, slotPos, new Rectangle(0, 0, 48, 48), new Color(1f, 1f, 1f, 0f), 0f, new Vector2(28, 0), 1.01f, SpriteEffects.None, 0);

                menuIcon.Draw(spriteBatch);
            }
        }

        public override void Update(GameTime gameTime)
        {
            //Initialize();
            SkillsPlayer player = Main.LocalPlayer.GetModPlayer<SkillsPlayer>();
            Vector2 position = new Vector2((Main.playerInventory ? 330 : 280) * Main.UIScale, 16);
            menuIcon.Left.Pixels = position.X - 24 * Math.Clamp(player.MaxSkills - 2,1,1000) - 66;
            menuIcon.Top.Pixels = 46;
            menuIcon.Width.Pixels = 30;
            menuIcon.Height.Pixels = 30;

            if (menuIcon.IsMouseHovering)
            {
                Main.LocalPlayer.mouseInterface = true;
                menuIcon.frame = new Rectangle(30, 0, 30, 30);
            }
            else menuIcon.frame = new Rectangle(0, 0, 30, 30);

            base.Update(gameTime);
        }

        public override void LeftClick(UIMouseEvent evt)
        {
            if (menuIcon.IsMouseHovering)
            {
                SkillBookSystem.Flip();
                SoundEngine.PlaySound(SoundID.MenuTick);
            }
            base.LeftClick(evt);
        }
    }

    [Autoload(Side = ModSide.Client)]
    public class SkillsBarSystem : ModSystem
    {
        private UserInterface skillsBarInterface;
        internal SkillsBar skillsBar;
        public override void Load()
        {
            SkillsBar.BarTex = Mod.Assets.Request<Texture2D>("Assets/UI/SkillsBar");
            SkillsBar.SelectTex = Mod.Assets.Request<Texture2D>("Assets/UI/SkillsBarSelect");
            SkillsBar.menuIconTex = Mod.Assets.Request<Texture2D>("Assets/UI/SkillMenuIcon");
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
            if (Terrafirma.CombatReworkEnabled)
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
