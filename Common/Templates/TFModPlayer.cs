using System.Reflection;
using Terrafirma.Common.Attributes;
using Terraria.ModLoader;

namespace Terrafirma.Common.Templates
{
    public abstract class TFModPlayer : ModPlayer
    {
        public override void ResetEffects()
        {
            FieldInfo[] info = GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);
            for (int i = 0; i < info.Length; i++)
            {
                ResetDefaultsAttribute a = info[i].GetCustomAttribute<ResetDefaultsAttribute>();
                if(a != null)
                info[i].SetValue(this, a.Default);
            }
        }
    }
}
