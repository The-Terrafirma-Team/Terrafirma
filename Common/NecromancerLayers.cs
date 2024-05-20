using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Common.Templates;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Terrafirma.Common
{
    public class NecromancerLayer : PlayerDrawLayer
    {
        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            return drawInfo.drawPlayer.HeldItem.ModItem is NecromancerScythe;
        }
        public void drawSoul(PlayerDrawSet drawInfo, bool front)
        {
            NecromancerScythe scythe = drawInfo.drawPlayer.HeldItem.ModItem as NecromancerScythe;

            Vector2 position = drawInfo.Center - Main.screenPosition;
            position = new Vector2((int)position.X, (int)position.Y);

            Asset<Texture2D> tex = Mod.Assets.Request<Texture2D>("Assets/Souls/" + scythe.SoulName);

            for (int i = 0; i < (scythe.DamageDealt / scythe.DamagePerSoul); i++)
            {
                double timer = Main.timeForVisualEffects + i * (MathHelper.TwoPi * 5.5f);
                float circle = (float)Math.Sin(timer * 0.03f);
                float halfCircle = (float)Math.Sin((timer * 0.03f) + 1.521229);
                if ((halfCircle < 0 && front) || (halfCircle > 0 && !front))
                    drawInfo.DrawDataCache.Add(new DrawData(tex.Value, position + new Vector2(circle * 28f, Math.Abs(halfCircle) * 8f), new Rectangle(tex.Height() * (i % 2 == 0 ? 0 : 1), 0, tex.Height(), tex.Height()), Color.Lerp(Color.White, Color.Gray, halfCircle) * 0.8f, 0, new Vector2(tex.Width() / 4, tex.Height() / 2), 0.65f - halfCircle * 0.15f, SpriteEffects.None));
            }
        }
        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            if (drawInfo.shadow > 0)
                return;
            drawSoul(drawInfo, true);
        }
        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.LastVanillaLayer);
    }
    public class NecromancerLayerBack : NecromancerLayer
    {
        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            if (drawInfo.shadow > 0)
                return;
            drawSoul(drawInfo, false);
        }
        public override Position GetDefaultPosition() => new BeforeParent(PlayerDrawLayers.FirstVanillaLayer);
    }
}
