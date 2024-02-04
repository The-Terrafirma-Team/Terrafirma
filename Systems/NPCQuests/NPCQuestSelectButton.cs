using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using Terraria.Audio;
using Terraria.ID;

namespace Terrafirma.Systems.MageClass
{
    [Autoload(Side = ModSide.Client)]
    internal class NPCQuestSelectbutton : UIElement
    {
        public int PanelTextureBorder = 6;
        public Vector2 Size = new Vector2(30, 30);
        public Vector2 Position = Vector2.Zero;
        
        public override void OnActivate()
        {
            
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
                  
        }



    }
}
