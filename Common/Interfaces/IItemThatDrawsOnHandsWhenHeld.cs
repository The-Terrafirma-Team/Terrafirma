using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.DataStructures;

namespace Terrafirma.Common.Interfaces
{
    internal interface IItemThatDrawsOnHandsWhenHeld
    {
        void DrawFrontHand(ref PlayerDrawSet drawInfo);
        void DrawOffHand(ref PlayerDrawSet drawInfo);
    }
}
