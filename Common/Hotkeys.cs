using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace Terrafirma.Common
{
    public class Hotkeys : ModSystem
    {
        public ModKeybind tertiaryAttack { get; set; }

        public override void Load()
        {
            tertiaryAttack = KeybindLoader.RegisterKeybind(Mod, "tertiaryAttack", Keys.C);
        }
        public override void Unload()
        {
            tertiaryAttack = null;
        }
    }
}
