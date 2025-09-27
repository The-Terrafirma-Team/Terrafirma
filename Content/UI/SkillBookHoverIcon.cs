using Microsoft.CodeAnalysis.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Common.Mechanics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace Terrafirma.Content.UI
{
    internal class SkillBookHoverIcon : UIElement
    {

        public Skill skill;
        public Vector2 position = Vector2.Zero;
        Vector2 velocity = Vector2.Zero;
        float scale = 1.0f;
        float opacity = 1.0f;
        float rot = 0.0f;

        bool gone = false;
        Vector2? slotPos = null;
        Vector2? swapSlotPos = null;
        Vector2? swapSlotTarget = null;
        Skill swapSkill = null;

        public SkillBookHoverIcon(Skill iconSkill)
        {
            skill = iconSkill;
            Initialize();
        }

        public override void OnInitialize()
        {
            position = Main.MouseScreen;
            base.OnInitialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (!Main.mouseLeft)
            {
                gone = true;
                for(int i = 0; i < SkillsBar.hoveredSlots.Length; i++)
                {
                    if (SkillsBar.hoveredSlots[i])
                    {
                        int? swapSlot = null;
                        for (int k = 0; k < Main.LocalPlayer.GetModPlayer<SkillsPlayer>().MaxSkills; k++)
                        {
                            Skill checkSkill = Main.LocalPlayer.GetModPlayer<SkillsPlayer>().EquippedSkills[k];
                            if (checkSkill != null && k != i && checkSkill.GetType() == skill.GetType())
                            {
                                swapSlot = k;
                            }
                        }


                        if (swapSlot != null)
                        {
                            Main.LocalPlayer.GetModPlayer<SkillsPlayer>().EquippedSkills[swapSlot.Value] = Main.LocalPlayer.GetModPlayer<SkillsPlayer>().EquippedSkills[i];
                            swapSlotPos = SkillsBar.skillSlots[i].Center();
                            swapSlotTarget = SkillsBar.skillSlots[swapSlot.Value].Center() + new Vector2(-4, 1);
                            swapSkill = Main.LocalPlayer.GetModPlayer<SkillsPlayer>().EquippedSkills[i];
                        }
                        Main.LocalPlayer.GetModPlayer<SkillsPlayer>().EquippedSkills[i] = (Skill)Activator.CreateInstance(skill.GetType());
                        slotPos = SkillsBar.skillSlots[i].Center.ToVector2() + new Vector2(-4,1);
                        SoundEngine.PlaySound(SoundID.MenuOpen);
                    }
                    SkillsBar.hoveredSlots[i] = false;
                }
            }

            if (gone)
            {
                if (slotPos == null)
                {
                    opacity -= 0.1f;
                    scale += 0.02f;
                    position += velocity * 0.1f;
                    velocity *= 0.95f;
                    rot *= 0.95f;
                    if (opacity <= 0.0f)
                    {
                        Remove();
                        Deactivate();
                    }
                }
                else
                {
                    position = Vector2.Lerp(position, slotPos.Value, 0.3f);
                    scale = MathHelper.Lerp(scale, 1.0f, 0.2f);
                    rot = MathHelper.Lerp(rot, 0.0f, 0.4f);
                    if (scale <= 1.01f)
                    {
                        Remove();
                        Deactivate();
                    }
                }

                if (swapSlotTarget != null)
                {
                    swapSlotPos = Vector2.Lerp(swapSlotPos.Value, swapSlotTarget.Value, 0.3f);
                }
            }
            else
            {
                scale = MathHelper.Lerp(scale, 1.2f, 0.2f);
                position = Vector2.Lerp(position,Main.MouseScreen, 0.3f);
                velocity = Vector2.Lerp(velocity, Main.MouseScreen - position, 0.4f);
                rot = 0.07f * (float)Math.Sin(Main.timeForVisualEffects / 20.0f) + (velocity.X / 60f);
            }


            for (int i = 0; i < SkillsBar.skillSlots.Length; i++)
            {
                if (!gone) SkillsBar.hoveredSlots[i] = SkillsBar.skillSlots[i].Contains(Main.MouseScreen.ToPoint());
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (swapSlotTarget != null && swapSkill != null)
            {
                spriteBatch.Draw(
                    SkillsSystem.SkillTextures[swapSkill.ID].Value,
                    swapSlotPos.Value,
                    new Rectangle(0, 0, SkillsSystem.SkillTextures[swapSkill.ID].Width(), SkillsSystem.SkillTextures[swapSkill.ID].Width()),
                    Color.White * opacity,
                    0.0f,
                    new Vector2(SkillsSystem.SkillTextures[swapSkill.ID].Width(), SkillsSystem.SkillTextures[swapSkill.ID].Height() / 2) / 2,
                    1.0f,
                    SpriteEffects.None,
                    0f);
            }

            spriteBatch.Draw(
                SkillsSystem.SkillTextures[skill.ID].Value,
                position,
                new Rectangle(0, 0, SkillsSystem.SkillTextures[skill.ID].Width(), SkillsSystem.SkillTextures[skill.ID].Width()),
                Color.White * opacity,
                rot,
                new Vector2(SkillsSystem.SkillTextures[skill.ID].Width(), SkillsSystem.SkillTextures[skill.ID].Height()/2) / 2,
                scale,
                SpriteEffects.None,
                0f);

            spriteBatch.Draw(
                SkillBook.skillBorderTex.Value,
                position - new Vector2(0, 1),
                new Rectangle(0, 0, SkillBook.skillBorderTex.Width(), SkillBook.skillBorderTex.Height()),
                Color.White * opacity * ((opacity - 1f) * 5f),
                rot,
                SkillBook.skillBorderTex.Size() / 2,
                scale,
                SpriteEffects.None,
                0f);

        }
    }
}
