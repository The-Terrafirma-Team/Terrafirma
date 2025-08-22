using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Common.Mechanics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Terrafirma.Common.PlayerLayers
{
    public class BlockLayer : PlayerDrawLayer
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<ServerConfig>().CombatReworkEnabled;
        }

        private static Asset<Texture2D> BlockTex;
        public override void Load()
        {
            BlockTex = Mod.Assets.Request<Texture2D>("Assets/Block");
        }
        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            return drawInfo.drawPlayer.GetModPlayer<BlockingPlayer>().blockAmount > 0;
        }
        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            if (drawInfo.shadow > 0)
                return;
            float alpha = Lighting.Brightness((int)(drawInfo.drawPlayer.Center.X / 16), (int)((drawInfo.drawPlayer.Center.Y + drawInfo.drawPlayer.gfxOffY) / 16));
            float opacity = drawInfo.drawPlayer.GetModPlayer<BlockingPlayer>().blockAmount;

            drawInfo.DrawDataCache.Add(new DrawData(BlockTex.Value, new Vector2((int)drawInfo.Center.X, (int)drawInfo.Center.Y) - Main.screenPosition - Vector2.UnitY, BlockTex.Frame(2,1,0,0), Color.Cyan with { A = 0 } * alpha * opacity, 0f, new Vector2(BlockTex.Height() / 2), 1f * opacity, SpriteEffects.None, 0));
            drawInfo.DrawDataCache.Add(new DrawData(BlockTex.Value, new Vector2((int)drawInfo.Center.X, (int)drawInfo.Center.Y) - Main.screenPosition - Vector2.UnitY, BlockTex.Frame(2, 1, 1, 0), Color.White with { A = 0 } * alpha * opacity, 0f, new Vector2(BlockTex.Height() / 2), 1f * opacity, SpriteEffects.None, 0));
        }
        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.LastVanillaLayer);
    }
}
