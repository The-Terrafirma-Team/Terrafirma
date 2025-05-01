using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terrafirma.Systems.Primitives
{
    public class TFSimpleTrail : TFTrail
    {
        public float width = 0f;
        public Color color = Color.White;
        public Vector2 textureOffset = Vector2.Zero;
        public Vector2 trailOffset = Vector2.Zero;
        public override float Width(float segment) => width;
        public override Color TrailColor(float segment) => color;
        public override Vector2 TextureOffset(float segment) => textureOffset;
        public override Vector2 TrailOffset(float segment, float segmentRotation) => trailOffset;
    }

    public class TFAdvancedTrail : TFTrail
    {
        public delegate float widthDelegate(float segment);
        /// <summary>
        /// (float segment)
        /// </summary>
        public widthDelegate width;
        public delegate Color colorDelegate(float segment);
        /// <summary>
        /// (float segment)
        /// </summary>
        public colorDelegate color;
        public delegate Vector2 textureOffsetDelegate(float segment);
        /// <summary>
        /// (float segment)
        /// </summary>
        public textureOffsetDelegate textureOffset;
        public delegate Vector2 trailOffsetDelegate(float segment, float segmentRotation);
        /// <summary>
        /// (float segment, float segmentRotation)
        /// </summary>
        public trailOffsetDelegate trailOffset;
        public override float Width(float segment) => width(segment);
        public override Color TrailColor(float segment) => color(segment);
        public override Vector2 TextureOffset(float segment) => textureOffset(segment);
        public override Vector2 TrailOffset(float segment, float segmentRotation) => trailOffset(segment, segmentRotation);
    }
}
