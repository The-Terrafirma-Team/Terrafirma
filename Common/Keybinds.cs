using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Security.Cryptography.X509Certificates;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace Terrafirma.Common
{
    internal class Keybinds : ModPlayer
    {
        public bool Shifting;
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            

            if (triggersSet.SmartSelect )
            {
                Shifting = true;
            }
            else
            {
                Shifting = false;
            }
            
        }

    }
}
