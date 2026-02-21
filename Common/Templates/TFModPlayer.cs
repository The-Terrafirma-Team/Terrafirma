using System.Reflection;
using Terrafirma.Common.Attributes;
using Terraria.ModLoader;

namespace Terrafirma.Common.Templates
{
    public abstract class TFModPlayer : ModPlayer
    {
        private FieldInfo[] _info;
        public TFModPlayer()
        {
            _info = GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);
        }
        public override void ResetEffects()
        {
            for (int i = 0; i < _info.Length; i++)
            {
                ResetDefaultsAttribute a = _info[i].GetCustomAttribute<ResetDefaultsAttribute>();
                if(a != null)
                _info[i].SetValue(this, a.Default);
            }
        }
    }
}
