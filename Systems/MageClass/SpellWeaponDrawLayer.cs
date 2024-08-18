using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria;

namespace Terrafirma.Systems.MageClass
{
    internal class SpellWeaponDrawLayer : PlayerDrawLayer
    {
        Asset<Texture2D> weaponTex = TextureAssets.Item[Main.LocalPlayer.HeldItem.type];

        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.HeldItem);
        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            return;
            if (!drawInfo.drawPlayer.ItemAnimationActive) return;

            drawInfo.drawPlayer.HeldItem.noUseGraphic = false;
            weaponTex = TextureAssets.Item[drawInfo.drawPlayer.HeldItem.type];

            Vector2 WeaponOffset = -(drawInfo.drawPlayer.HeldItem.ModItem != null ? drawInfo.drawPlayer.HeldItem.ModItem.HoldoutOffset() ?? Vector2.Zero : Vector2.Zero);

            Vector2 basePosition = drawInfo.drawPlayer.itemLocation - Main.screenPosition;
            basePosition = new Vector2((int)basePosition.X, (int)basePosition.Y) + (drawInfo.drawPlayer.RotatedRelativePoint(drawInfo.drawPlayer.MountedCenter) - drawInfo.drawPlayer.Center);

            DrawData value;
            switch (drawInfo.drawPlayer.HeldItem.useStyle)
            {
                case ItemUseStyleID.Swing:
                    value = new DrawData(weaponTex.Value,
                    basePosition,
                    weaponTex.Value.Bounds,
                    Color.White,
                    drawInfo.drawPlayer.itemRotation,
                    new Vector2(drawInfo.drawPlayer.direction == -1 ? weaponTex.Width() : 0,
                    drawInfo.drawPlayer.gravDir == 1 ? weaponTex.Height() : 0) + WeaponOffset,
                    drawInfo.drawPlayer.GetAdjustedItemScale(drawInfo.drawPlayer.HeldItem),
                    drawInfo.itemEffect);

                    drawInfo.DrawDataCache.Add(value);
                    break;

                case ItemUseStyleID.Shoot:

                    Vector2 WeaponOff = Main.DrawPlayerItemPos(drawInfo.drawPlayer.gravDir, drawInfo.drawPlayer.HeldItem.type);

                    value = new DrawData(weaponTex.Value,
                    basePosition + new Vector2(WeaponOff.X * drawInfo.drawPlayer.direction, WeaponOff.Y),
                    weaponTex.Value.Bounds,
                    Color.White,
                    drawInfo.drawPlayer.itemRotation,
                    new Vector2(drawInfo.drawPlayer.direction == 1 ? 0 : weaponTex.Width(), weaponTex.Height() / 2),
                    drawInfo.drawPlayer.GetAdjustedItemScale(drawInfo.drawPlayer.HeldItem),
                    drawInfo.itemEffect);

                    drawInfo.DrawDataCache.Add(value);
                    break;

            }
        }
    }
}
