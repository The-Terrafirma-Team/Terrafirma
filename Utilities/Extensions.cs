using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Terrafirma.Utilities
{
    public static class Extensions
    {
        public static Vector2 LengthClamp(this Vector2 vector, float max, float min = 0)
        {
            if (vector.Length() > max) return Vector2.Normalize(vector) * max;
            else if (vector.Length() < min) return Vector2.Normalize(vector) * min;
            else return vector;
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
