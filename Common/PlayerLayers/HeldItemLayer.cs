using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Common.Interfaces;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Terrafirma.Common.PlayerLayers
{
    public class HeldItemFrontLayer : PlayerDrawLayer
    {
        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            return drawInfo.drawPlayer.HeldItem.ModItem is IItemThatDrawsOnHandsWhenHeld;
        }
        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.HandOnAcc);
        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            IItemThatDrawsOnHandsWhenHeld item = drawInfo.drawPlayer.HeldItem.ModItem as IItemThatDrawsOnHandsWhenHeld;
            item.DrawFrontHand(ref drawInfo);
        }
    }
    public class HeldItemBackLayer : PlayerDrawLayer
    {
        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            return drawInfo.drawPlayer.HeldItem.ModItem is IItemThatDrawsOnHandsWhenHeld;
        }
        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.OffhandAcc);
        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            IItemThatDrawsOnHandsWhenHeld item = drawInfo.drawPlayer.HeldItem.ModItem as IItemThatDrawsOnHandsWhenHeld;
            item.DrawOffHand(ref drawInfo);
        }
    }
}
