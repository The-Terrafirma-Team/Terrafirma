using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Common;
using Terraria.ModLoader;

namespace Terrafirma.Systems.UIElements.ConfigElements
{
    internal class ConfigExtraSpellUIPosition : ConfigVector2Select
    {
        public override ref Vector2 SetPos => ref ModContent.GetInstance<ClientConfig>().ExtraSpellUiPosition;
    }
}
