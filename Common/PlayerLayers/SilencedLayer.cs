using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Terrafirma.Common.PlayerLayers
{
    public class SilencedLayer : PlayerDrawLayer
    {
        private static Asset<Texture2D> SilenceTex;
        public override void Load()
        {
            SilenceTex = Mod.Assets.Request<Texture2D>("Assets/Silenced");
        }
        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            return drawInfo.drawPlayer.silence;
        }
        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            if (drawInfo.shadow > 0)
                return;
            float alpha = Lighting.Brightness((int)(drawInfo.drawPlayer.Center.X / 16), (int)((drawInfo.drawPlayer.Center.Y + drawInfo.drawPlayer.gfxOffY) / 16));
            drawInfo.DrawDataCache.Add(new DrawData(SilenceTex.Value, new Vector2((int)drawInfo.Center.X, (int)drawInfo.Center.Y) - Main.screenPosition + new Vector2(0, -30), null, Color.White * alpha, 0f, SilenceTex.Size() / 2, 1f + Main.masterColor * 0.1f, SpriteEffects.None, 0));
        }
        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.LastVanillaLayer);
    }
}
