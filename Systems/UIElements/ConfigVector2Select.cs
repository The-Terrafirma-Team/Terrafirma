using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.Config.UI;

namespace Terrafirma.Systems.UIElements
{
    // This custom config UI element uses vanilla config elements paired with custom drawing.
    internal class ConfigVector2Select : ConfigElement
    {

        public override void Draw(SpriteBatch spriteBatch)
        {

            Texture2D tex = TextureAssets.MagicPixel.Value;
            spriteBatch.Draw(tex, new Vector2(200, 200), new Rectangle(0,0,250,250), Color.White);
        }
    }
}
