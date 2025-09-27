using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Common.Mechanics;
using Terrafirma.Common.Systems;
using Terrafirma.Content.Particles;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Terrafirma.Common.Templates
{
    public abstract class SkillScroll : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return Terrafirma.CombatReworkEnabled;
        }
        public virtual Skill SkillToLearn => null;
        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = Item.useAnimation = 20;
            Item.Size = new Vector2(16);
            Item.UseSound = SoundID.Item4;
            Item.rare = ItemRarityID.Blue;
        }
        public override bool? UseItem(Player player)
        {
            if (player.ItemAnimationJustStarted)
            {
                SkillToLearn.ID = SkillToLearn.GetID();
                SkillsPlayer p = player.GetModPlayer<SkillsPlayer>();
                for(int i = 0; i < p.MaxSkills; i++)
                {
                    if (p.EquippedSkills[i] == null)
                    {
                        for (int x = 0; x < 10; x++)
                        {
                            ParticleSystem.NewParticle(new StarSparkle(Main.rand.Next(14, 42), Main.rand.NextFloat(-0.3f, 0.3f), Main.rand.NextFloat(0.4f, 1f), SkillToLearn.RechargeFlashColor with { A = 0 } * Main.rand.NextFloat(0.2f,1f), Main.rand.NextVector2Circular(5, 5) + new Vector2(0, -3)), player.Bottom);
                        }
                        p.EquippedSkills[i] = SkillToLearn;
                        break;
                    }
                    else if (p.EquippedSkills[i].ID == SkillToLearn.ID)
                    {
                        break;
                    }
                }
                SkillsPlayer.RememberedSkillIDs[SkillToLearn.ID] = true;
            }
            return base.UseItem(player);
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Skill s = SkillsSystem.Skills[SkillToLearn.ID];
            int i = tooltips.FindAppropriateLineForTooltip();
            PlayerStats stats = Main.LocalPlayer.PlayerStats();

            tooltips.Insert(i, new TooltipLine(Mod, "SkillScroll", Language.GetText("Mods.Terrafirma.Skills.Tooltips.SkillScroll").WithFormatArgs(s.DisplayName).Value)); i++;

            int cd = (int)Math.Ceiling((s.CooldownMax / 60f * stats.SkillCooldown));
            tooltips.Insert(i, new TooltipLine(Mod, "Cooldown", Language.GetText("Mods.Terrafirma.Skills.Tooltips.Cooldown").WithFormatArgs(cd).Value)); i++;

            if (s.CastTimeMax > 0)
            {
                int ct = (int)Math.Ceiling((s.CastTimeMax / 60f * stats.SkillCastTime));
                tooltips.Insert(i, new TooltipLine(Mod, "CastTime", Language.GetText("Mods.Terrafirma.Skills.Tooltips.CastTime").WithFormatArgs(ct).Value)); i++;
            }

            if (s.ManaCost > 0)
            {
                tooltips.Insert(i, new TooltipLine(Mod, "SkillManaCost", Language.GetText("Mods.Terrafirma.Skills.Tooltips.ManaCost").WithFormatArgs(s.TotalManaCost(Main.LocalPlayer)).Value)); i++;
            }
            if (s.TensionCost > 0)
            {
                tooltips.Insert(i, new TooltipLine(Mod, "SkillTensionCost", Language.GetText("Mods.Terrafirma.Skills.Tooltips.TensionCost").WithFormatArgs(s.TotalTensionCost(Main.LocalPlayer)).Value)); i++;
            }

            tooltips.Insert(i, new TooltipLine(Mod,"SkillTooltip", s.Tooltip.WithFormatArgs(s.TooltipFormatting()).Value));
        }
    }
}
