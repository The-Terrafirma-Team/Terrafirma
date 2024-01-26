using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace TerrafirmaRedux.Particles
{
    internal class Bookmark : Particle
    {
        public override void OnSpawn()
        {
            Scale = 1f;
        }
        public override void Update()
        {
            TimeInWorld = 1;
            if (ai1 > 0) ai1--;
            else Active = false;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D bookmark = ModContent.Request<Texture2D>("TerrafirmaRedux/Particles/Bookmark").Value;
            spriteBatch.Draw(bookmark, Position + new Vector2(0,bookmark.Height / 2) - Main.screenPosition, new Rectangle(0, 0, bookmark.Width, bookmark.Height), Color, Rotation, bookmark.Size() / 2, 1f * Scale, SpriteEffects.None, 0);
        }
    }
}
