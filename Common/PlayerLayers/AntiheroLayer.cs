using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Items.Weapons.Melee.Knight;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Terrafirma.Common.PlayerLayers
{
    public class AntiheroLayer : PlayerDrawLayer
    {
        private static Asset<Texture2D> auraTex;
        public override void Load()
        {
            auraTex = Mod.Assets.Request<Texture2D>("Items/Weapons/Melee/Knight/AntiheroBorder");
        }
        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            return drawInfo.drawPlayer.HeldItem.type == ModContent.ItemType<Antihero>();
        }
        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            if (drawInfo.drawPlayer.HeldItem.ModItem is Antihero item && drawInfo.shadow == 0f)
            {
                float auraFloat = item.auraPresence;
                float auraRadius = item.auraRadius;

                if (auraRadius == -1) return;

                for (int i = 0; i < 40; i++)
                {
                    Rectangle frame = new Rectangle(0, 0, 0, 0);
                    if (i % 2 == 0) frame = new Rectangle(2, 12, 10, 10);
                    else frame = new Rectangle(16,2,18,22);

                    float rotation = (((MathHelper.TwoPi / 40f) * i) + ((float)Main.timeForVisualEffects / 100f));
                    drawInfo.DrawDataCache.Add(new DrawData(
                        auraTex.Value,
                        (drawInfo.drawPlayer.Center + new Vector2(auraRadius, 0).RotatedBy(rotation) - Main.screenPosition),
                        frame,
                        new Color(1f,0f,0f,1f) * auraFloat,
                        rotation - MathHelper.PiOver2,
                        frame.Size() / 2,
                        new Vector2(1f,1f),
                        SpriteEffects.None));
                }
            }
        }
        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.Shield);
    }
}
