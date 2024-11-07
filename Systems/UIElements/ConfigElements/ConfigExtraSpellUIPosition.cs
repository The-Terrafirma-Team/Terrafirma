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

        public override void OnActivate()
        {
            snapPoints = new ConfigSnapPoint[]
            {
                new ConfigSnapPoint(new Vector2(0.8f,0f), new Vector2(0,0)),
                new ConfigSnapPoint(new Vector2(0.35f,0f), new Vector2(0,0)),
                new ConfigSnapPoint(new Vector2(0.5f,0.5f), new Vector2(0,10)),
                new ConfigSnapPoint(new Vector2(0.0f,1f), new Vector2(25,-10))
            };
            base.OnActivate();
        }
    }
}
