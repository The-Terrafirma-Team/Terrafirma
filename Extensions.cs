using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terrafirma.Common;
using Terrafirma.Common.Interfaces;
using Terrafirma.Content.NPCs.Vanilla;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Terrafirma
{
    public static class Extensions
    {
        public static void DrawConfusedQuestionMark(this NPC rCurrentNPC, SpriteBatch spritebatch, Vector2 screenPos, float heightOffset = 0f)
        {
            float num36 = Main.NPCAddHeight(rCurrentNPC);
            if (rCurrentNPC.confused)
            {
                Vector2 halfSize = rCurrentNPC.frame.Size() / 2;
                spritebatch.Draw(TextureAssets.Confuse.Value, new Vector2(rCurrentNPC.position.X - screenPos.X + (float)(rCurrentNPC.width / 2) - (float)TextureAssets.Npc[rCurrentNPC.type].Width() * rCurrentNPC.scale / 2f + halfSize.X * rCurrentNPC.scale, rCurrentNPC.position.Y - screenPos.Y + (float)rCurrentNPC.height - (float)TextureAssets.Npc[rCurrentNPC.type].Height() * rCurrentNPC.scale / (float)Main.npcFrameCount[rCurrentNPC.type] + 4f + halfSize.Y * rCurrentNPC.scale + num36 + heightOffset - (float)TextureAssets.Confuse.Height() - 20f), new Microsoft.Xna.Framework.Rectangle(0, 0, TextureAssets.Confuse.Width(), TextureAssets.Confuse.Height()), rCurrentNPC.GetShimmerColor(new Microsoft.Xna.Framework.Color(250, 250, 250, 70)), rCurrentNPC.velocity.X * -0.05f, new Vector2(TextureAssets.Confuse.Width() / 2, TextureAssets.Confuse.Height() / 2), Main.essScale + 0.2f, SpriteEffects.None, 0f);
            }
        }
        public static PlayerStats PlayerStats(this Player player)
        {
            return player.GetModPlayer<PlayerStats>();
        }
        public static NPCStats NPCStats(this NPC npc)
        {
            return npc.GetGlobalNPC<NPCStats>();
        }
        public static void ParryStrike(this Player p, NPC n)
        {
            float power = p.PlayerStats().ParryPower;
            p.StrikeNPCDirect(n, n.CalculateHitInfo(p.PlayerStats().ParryDamage, n.Center.X < p.Center.X ? -1 : 1, false, 4, DamageClass.Melee));
            if (n.ModNPC is ICustomBlockBehavior behavior)
                behavior.OnBlocked(p, power);
            else
            {
                for(int i = 0; i < n.EntityGlobals.Length; i++)
                {
                    if (n.EntityGlobals[i] is ICustomBlockBehavior behaviorGlobal)
                    {
                        behaviorGlobal.OnBlocked(p, power, n);
                    }
                }
                //switch (n.type)
                //{
                //    case NPCID.BlueSlime:
                //        n.GetGlobalNPC<Slime>().OnBlocked(n, p, power);
                //        break;
                //    case NPCID.DemonEye:
                //    case NPCID.CataractEye:
                //    case NPCID.PurpleEye:
                //    case NPCID.GreenEye:
                //    case NPCID.DemonEyeOwl:
                //    case NPCID.DemonEyeSpaceship:
                //    case NPCID.DialatedEye:
                //        n.GetGlobalNPC<DemonEye>().OnBlocked(n, p, power);
                //        break;
                //}
            }
        }
        public static bool CheckTension(this Player player, int Tension, bool Consume = true)
        {
            PlayerStats pStats = player.PlayerStats();
            if (pStats.Tension >= Tension)
            {
                if (Consume)
                {
                    pStats.Tension -= player.ApplyTensionBonusScaling(Tension, true);
                }
                return true;
            }
            return false;
        }
        public static void GiveTension(this Player player, int Tension, bool Numbers = true)
        {
            PlayerStats pStats = player.PlayerStats();
            int gain = (int)(Tension * pStats.TensionGainMultiplier);
            pStats.Tension += gain;
            if (pStats.Tension > pStats.TensionMax2)
                pStats.Tension = pStats.TensionMax2;
            if (Numbers)
                CombatText.NewText(player.Hitbox, new Color(64,222,170), gain);
        }
        public static int ApplyTensionBonusScaling(this Player player, int number, bool drain = false)
        {
            PlayerStats stats = player.PlayerStats();
            if (!drain)
            {
                return (int)(number * stats.TensionGainMultiplier) + stats.FlatTensionGain;
            }
            else
            {
                return (int)(number * stats.TensionCostMultiplier) + stats.FlatTensionCost;
            }
        }
        public static Vector2 LengthClamp(this Vector2 vector, float max, float min = 0)
        {
            if (vector.Length() > max) return Vector2.Normalize(vector) * max;
            else if (vector.Length() < min) return Vector2.Normalize(vector) * min;
            else return vector;
        }
        public static void QuickDefaults(this Projectile proj, bool hostile = false, int size = 8, int aiStyle = -1)
        {
            proj.aiStyle = aiStyle;
            proj.hostile = hostile;
            proj.friendly = !hostile;
            proj.width = size;
            proj.height = size;
        }
        public static string NicenUpKeybindNameIfApplicable(string name)
        {
            switch (name)
            {
                case "Mouse1":
                    return Language.GetText("Mods.Terrafirma.KeybindReplacements.Mouse1").Value;
                case "Mouse2":
                    return Language.GetText("Mods.Terrafirma.KeybindReplacements.Mouse2").Value;
                case "Mouse3":
                    return Language.GetText("Mods.Terrafirma.KeybindReplacements.Mouse3").Value;
                case "OemOpenBrackets":
                    return "[";
                case "OemCloseBrackets":
                    return "]";
                case "OemSemiColon":
                    return ";";
                case "OemColon":
                    return ":";
                case "OemQuotes":
                    return "'";
                case "OemComma":
                    return ",";
                case "OemPeriod":
                    return ".";
                case "OemQuestion":
                    return "?";
                case "OemPipe":
                    return "/";
                case "OemPlus":
                    return "+";
                case "OemMinus":
                    return "-";
                default:
                    break;
            }
            return name;
        }
        /// <summary>
        /// Will likely need to be updated as cases arise. Fearful.
        /// Gives you the index to insert a tooltip at so that it doesn't go after the modifiers stats things.
        /// </summary>
        public static int FindAppropriateLineForTooltip(this List<TooltipLine> tooltips)
        {
            int index = tooltips.Count;
            for (int i = 0; i < tooltips.Count; i++)
            {
                if (tooltips[i].Mod.Equals("Terraria"))
                {
                    if (tooltips[i].Name.Equals("Defense") || tooltips[i].Name.Equals("Material") || tooltips[i].Name.Equals("Tooltip0") || tooltips[i].Name.Equals("Tooltip1"))
                        index = i + 1;
                }
            }
            //new TooltipLine();
            return index;
        }
    }
}
